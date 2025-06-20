using AutoMapper;
using ESG.Common.Core.Commands;
using ESG.Common.Data;
using ESG.Common.Utility.Validators;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace ESG.IDMS.Application.Features.IDMS.FurnitureAndFixtureBrandModel.Commands;

public record AddFurnitureAndFixtureBrandModelCommand : FurnitureAndFixtureBrandModelState, IRequest<Validation<Error, FurnitureAndFixtureBrandModelState>>;

public class AddFurnitureAndFixtureBrandModelCommandHandler(ApplicationContext context,
                                IMapper mapper,
                                CompositeValidator<AddFurnitureAndFixtureBrandModelCommand> validator) : BaseCommandHandler<ApplicationContext, FurnitureAndFixtureBrandModelState, AddFurnitureAndFixtureBrandModelCommand>(context, mapper, validator), IRequestHandler<AddFurnitureAndFixtureBrandModelCommand, Validation<Error, FurnitureAndFixtureBrandModelState>>
{
    
public async Task<Validation<Error, FurnitureAndFixtureBrandModelState>> Handle(AddFurnitureAndFixtureBrandModelCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
}

public class AddFurnitureAndFixtureBrandModelCommandValidator : AbstractValidator<AddFurnitureAndFixtureBrandModelCommand>
{
    readonly ApplicationContext _context;

    public AddFurnitureAndFixtureBrandModelCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<FurnitureAndFixtureBrandModelState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("FurnitureAndFixtureBrandModel with id {PropertyValue} already exists");
        
    }
}
