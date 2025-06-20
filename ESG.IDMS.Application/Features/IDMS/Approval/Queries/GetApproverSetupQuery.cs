using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using ESG.Common.Core.Queries;
using ESG.Common.Utility.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ESG.Common.Utility.Extensions;

namespace ESG.IDMS.Application.Features.IDMS.Approval.Queries;

public record GetApproverSetupQuery : BaseQuery, IRequest<PagedListResponse<ApproverSetupState>>;

public class GetApproverSetupQueryHandler(ApplicationContext context) : BaseQueryHandler<ApplicationContext, ApproverSetupState, GetApproverSetupQuery>(context), IRequestHandler<GetApproverSetupQuery, PagedListResponse<ApproverSetupState>>
{
    public override Task<PagedListResponse<ApproverSetupState>> Handle(GetApproverSetupQuery request, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Context.ApproverSetup.Where(l=>l.ApprovalSetupType == ApprovalSetupTypes.Modular).AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                                                                 request.SortColumn, request.SortOrder,
                                                                 request.PageNumber, request.PageSize));
    }

}
