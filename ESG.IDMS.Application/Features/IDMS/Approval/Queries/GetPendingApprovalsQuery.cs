using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using ESG.Common.Core.Queries;
using ESG.Common.Utility.Extensions;
using ESG.Common.Utility.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESG.IDMS.Application.Features.IDMS.Approval.Queries;

public record GetPendingApprovalsQuery() : BaseQuery, IRequest<PagedListResponse<PendingApproval>>
{
    public string TableName { get; set; } = "";
}

public class GetPendingApprovalsQueryHandler(ApplicationContext context) : IRequestHandler<GetPendingApprovalsQuery, PagedListResponse<PendingApproval>>
{
    private readonly ApplicationContext _context = context;

    public async virtual Task<PagedListResponse<PendingApproval>> Handle(GetPendingApprovalsQuery request, CancellationToken cancellationToken = default)
	{
		var query = from a in _context.Approval
					join b in _context.ApprovalRecord on a.ApprovalRecordId equals b.Id
					where (a.Status == ApprovalStatus.ForApproval || a.Status == ApprovalStatus.PartiallyApproved)
					&& (a.EmailSendingStatus == SendingStatus.Failed || a.EmailSendingStatus == SendingStatus.Done)
					&& b.ApproverSetup!.TableName == request.TableName
					select new PendingApproval()
					{
						DataId = b.DataId,
						ApprovalId = a.Id,
						ApprovalStatus = a.Status,
						EmailSendingStatus = a.EmailSendingStatus,
						EmailSendingRemarks = a.EmailSendingRemarks,
						EmailSendingDateTime = a.EmailSendingDateTime,
						LastModifiedDate = a.LastModifiedDate,
					};
		var pagedList = query.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
																  request.SortColumn, request.SortOrder,
																  request.PageNumber, request.PageSize);
		foreach (var item in pagedList.Data)
		{
			item.RecordName = await GetRecordName(_context, request.TableName, item.DataId);

		}
		return pagedList;
	}    
    private static async Task<string?> GetRecordName(ApplicationContext context, string? tableName, string? dataId)
    {
        string? recordName = "";
		
        return recordName;
    }
}
public record PendingApproval
{
    public string DataId { get; set; } = "";
    public string? RecordName { get; set; } = "";
    public string ApprovalId { get; set; } = "";
    public string ApprovalStatus { get; set; } = "";
    public string EmailSendingStatus { get; set; } = "";
    public string EmailSendingRemarks { get; set; } = "";
    public DateTime? EmailSendingDateTime { get; set; }
    public DateTime LastModifiedDate { get; set; }
}