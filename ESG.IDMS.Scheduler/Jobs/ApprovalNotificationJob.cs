using ESG.IDMS.Core.Identity;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using ESG.Common.Services.Shared.Interfaces;
using ESG.Common.Services.Shared.Models.Mail;

namespace ESG.IDMS.Scheduler.Jobs
{
	[DisallowConcurrentExecution]
    public class ApprovalNotificationJob(ApplicationContext context, ILogger<ApprovalNotificationJob> logger, IConfiguration configuration, IMailService emailSender,
            UserManager<ApplicationUser> userManager) : IJob
    {
        private readonly string? _baseUrl = configuration.GetValue<string>("BaseUrl");

        public async Task Execute(IJobExecutionContext context)
        {
            await ProcessEmailNotificationAsync();
        }
        private async Task ProcessEmailNotificationAsync()
        {
            var approvalRecords = await context.ApprovalRecord.Where(l => l.Status == ApprovalStatus.New || l.Status == ApprovalStatus.PartiallyApproved)
                .Include(l => l.ApprovalList).Include(l => l.ApproverSetup!).IgnoreQueryFilters().ToListAsync();
            foreach (var item in approvalRecords)
            {
                if (item.ApprovalList != null)
                {
                    foreach (var approvalItem in item.ApprovalList)
                    {
                        try
                        {
                            var user = await userManager.FindByIdAsync(approvalItem.ApproverUserId);
                            if (approvalItem.EmailSendingStatus == SendingStatus.Pending
                                || (item.ApproverSetup!.ApprovalType == ApprovalTypes.InSequence && approvalItem.Sequence == 1 && approvalItem.Status == ApprovalStatus.New))
                            {
                                approvalItem.SendingDone();
                                await SendApprovalNotification(item, user!);
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, @"ProcessEmailNotificationAsync Error Message : {Message} / StackTrace : {StackTrace}", ex.Message, ex.StackTrace);
                            approvalItem.SendingFailed(ex.Message);
                        }
                    }
                    if (item.ApproverSetup!.ApprovalType == ApprovalTypes.Any && item.ApprovalList.Where(l => l.Status == ApprovalStatus.Approved).Any())
                    {
                        item.Approve();
                    }
                    else if (item.ApprovalList.Where(l => l.Status == ApprovalStatus.Approved || l.Status == ApprovalStatus.Skipped).Count() == item.ApprovalList.Count)
                    {
                        item.Approve();
                    }
                    else if (item.ApprovalList.Where(l => l.Status == ApprovalStatus.Rejected).Any())
                    {
                        item.Reject();
                    }
                    else if (item.ApprovalList.Where(l => l.Status == ApprovalStatus.Approved).Any())
                    {
                        item.PartiallyApprove();
                    }
                }
                context.Update(item);
                await context.UpdateRecordFromJobsAsync();
            }
        }
        private async Task SendApprovalNotification(ApprovalRecordState approvalRecord, ApplicationUser user)
        {
            string subject = approvalRecord!.ApproverSetup!.EmailSubject;
            string message = SetApproverName(approvalRecord!.ApproverSetup!.EmailBody, user);
            message = SetApprovalUrl(message, approvalRecord);
            await emailSender.SendAsync(new MailRequest() { To = user.Email!, Subject = subject, Body = message });
        }
        private static string SetApproverName(string message, ApplicationUser user)
        {
            if (message.Contains(EmailContentPlaceHolder.ApproverName))
            {
                message = message.Replace(EmailContentPlaceHolder.ApproverName, user.Name);
            }
            return message;
        }
        private string SetApprovalUrl(string message, ApprovalRecordState approvalRecord)
        {
            if (message.Contains(EmailContentPlaceHolder.ApprovalUrl))
            {
				message = message.Replace(EmailContentPlaceHolder.ApprovalUrl, $"{_baseUrl}/ProjectPL/{Common.Utility.Helpers.ConstantHelper.GetPropertyNameByValue(typeof(ApprovalModule), approvalRecord!.ApproverSetup!.TableName)}/Approve?Id={approvalRecord.DataId}");
            }
            return message;
        }
        public static class EmailContentPlaceHolder
        {
            public const string ApproverName = "{ApproverName}";
            public const string ApprovalUrl = "{ApprovalUrl}";
        }
    }
}
