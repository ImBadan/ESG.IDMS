using ESG.Common.Core.Queries;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESG.IDMS.Application.Features.IDMS.FurnitureAndFixtureBrandModel.Queries;

public record GetFurnitureAndFixtureBrandModelByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<FurnitureAndFixtureBrandModelState>>;

public class GetFurnitureAndFixtureBrandModelByIdQueryHandler(ApplicationContext context) : BaseQueryByIdHandler<ApplicationContext, FurnitureAndFixtureBrandModelState, GetFurnitureAndFixtureBrandModelByIdQuery>(context), IRequestHandler<GetFurnitureAndFixtureBrandModelByIdQuery, Option<FurnitureAndFixtureBrandModelState>>
{
		
}
