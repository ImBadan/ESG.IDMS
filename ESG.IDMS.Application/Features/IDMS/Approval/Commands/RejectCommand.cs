using ESG.IDMS.Infrastructure.Data;
using ESG.Common.Core.Base.Models;
using ESG.Common.Identity.Abstractions;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;
using ESG.IDMS.Core.IDMS;

namespace ESG.IDMS.Application.Features.IDMS.Approval.Commands;

public record RejectCommand(string DataId, string? RejectRemarks, string Module) : IRequest<Validation<Error, RejectResult>>;

public class RejectCommandHandler(ApplicationContext context, IAuthenticatedUser authenticatedUser) : IRequestHandler<RejectCommand, Validation<Error, RejectResult>>
{
    private readonly ApplicationContext _context = context;
    private readonly IAuthenticatedUser _authenticatedUser = authenticatedUser;

    public async Task<Validation<Error, RejectResult>> Handle(RejectCommand request, CancellationToken cancellationToken) =>
        await Reject(request, cancellationToken);

    public async Task<Validation<Error, RejectResult>> Reject(RejectCommand request, CancellationToken cancellationToken)
    {
        var entity = await (from a in _context.Approval
                    join b in _context.ApprovalRecord on a.ApprovalRecordId equals b.Id
                    where b.DataId == request.DataId && a.ApproverUserId == _authenticatedUser.UserId
                       && b.ApproverSetup!.TableName == request.Module && !ApprovalStatus.ExcludeFromForApproval.Contains(a.Status)
                    select a).SingleAsync(cancellationToken);
		using (var transaction = _context.Database.BeginTransaction())
		{
			entity.Reject(request.RejectRemarks);
			_context.Update(entity);
			_ = await _context.SaveChangesAsync(cancellationToken);
			var approvalRecord = await _context.ApprovalRecord.Where(l => l.Id == entity.ApprovalRecordId)
			.Include(l => l.ApproverSetup)
			.Include(l => l.ApprovalList)
			.FirstOrDefaultAsync(cancellationToken: cancellationToken);
			approvalRecord!.UpdateApprovalStatus();
			_ = await _context.SaveChangesAsync(cancellationToken);
			transaction.Commit();
		}
		return Success<Error, RejectResult>(new RejectResult(request.DataId));
    }
}
public record RejectResult : BaseEntity
{
    public RejectResult(string dataId)
    {
        this.Id = dataId;
    }
}
