using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using ESG.Common.Core.Queries;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESG.IDMS.Application.Features.IDMS.Approval.Queries;

public record GetApproverSetupByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ApproverSetupState>>;

public class GetApproverSetupByIdQueryHandler(ApplicationContext context) : BaseQueryByIdHandler<ApplicationContext, ApproverSetupState, GetApproverSetupByIdQuery>(context), IRequestHandler<GetApproverSetupByIdQuery, Option<ApproverSetupState>>
{
    public override async Task<Option<ApproverSetupState>> Handle(GetApproverSetupByIdQuery request, CancellationToken cancellationToken = default)
    {
        return await Context.ApproverSetup
            .Include(l => l.ApproverAssignmentList)
            .Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
    }

}