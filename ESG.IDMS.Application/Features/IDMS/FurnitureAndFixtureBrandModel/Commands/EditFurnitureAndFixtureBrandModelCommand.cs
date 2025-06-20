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

public record EditFurnitureAndFixtureBrandModelCommand : FurnitureAndFixtureBrandModelState, IRequest<Validation<Error, FurnitureAndFixtureBrandModelState>>;

public class EditFurnitureAndFixtureBrandModelCommandHandler(ApplicationContext context,
                                 IMapper mapper,
                                 CompositeValidator<EditFurnitureAndFixtureBrandModelCommand> validator) : BaseCommandHandler<ApplicationContext, FurnitureAndFixtureBrandModelState, EditFurnitureAndFixtureBrandModelCommand>(context, mapper, validator), IRequestHandler<EditFurnitureAndFixtureBrandModelCommand, Validation<Error, FurnitureAndFixtureBrandModelState>>
{ 
    
public async Task<Validation<Error, FurnitureAndFixtureBrandModelState>> Handle(EditFurnitureAndFixtureBrandModelCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
}

public class EditFurnitureAndFixtureBrandModelCommandValidator : AbstractValidator<EditFurnitureAndFixtureBrandModelCommand>
{
    readonly ApplicationContext _context;

    public EditFurnitureAndFixtureBrandModelCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<FurnitureAndFixtureBrandModelState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("FurnitureAndFixtureBrandModel with id {PropertyValue} does not exists");
        
    }
}
