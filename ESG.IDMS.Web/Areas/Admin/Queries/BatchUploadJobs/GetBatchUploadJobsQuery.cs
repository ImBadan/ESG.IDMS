using ESG.Common.Utility.Extensions;
using ESG.Common.Utility.Models;
using ESG.IDMS.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ESG.Common.Core.Queries;
using ESG.IDMS.Core.IDMS;

namespace ESG.IDMS.Web.Areas.Admin.Queries.BatchUploadJobs;

public record GetBatchUploadJobsQuery : BaseQuery, IRequest<PagedListResponse<UploadProcessorState>>
{
}

public class GetBatchUploadJobsQueryHandler(ApplicationContext context) : IRequestHandler<GetBatchUploadJobsQuery, PagedListResponse<UploadProcessorState>>
{

    public Task<PagedListResponse<UploadProcessorState>> Handle(GetBatchUploadJobsQuery request, CancellationToken cancellationToken) =>
        Task.FromResult(context.UploadProcessor.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                                                               request.SortColumn, request.SortOrder,
                                                               request.PageNumber, request.PageSize));
}
