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

namespace ESG.IDMS.Application.Features.IDMS.FurnitureAndFixture.Commands;

public record EditFurnitureAndFixtureCommand : FurnitureAndFixtureState, IRequest<Validation<Error, FurnitureAndFixtureState>>;

public class EditFurnitureAndFixtureCommandHandler(ApplicationContext context,
                                 IMapper mapper,
                                 CompositeValidator<EditFurnitureAndFixtureCommand> validator) : BaseCommandHandler<ApplicationContext, FurnitureAndFixtureState, EditFurnitureAndFixtureCommand>(context, mapper, validator), IRequestHandler<EditFurnitureAndFixtureCommand, Validation<Error, FurnitureAndFixtureState>>
{ 
    
public async Task<Validation<Error, FurnitureAndFixtureState>> Handle(EditFurnitureAndFixtureCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
}

public class EditFurnitureAndFixtureCommandValidator : AbstractValidator<EditFurnitureAndFixtureCommand>
{
    readonly ApplicationContext _context;

    public EditFurnitureAndFixtureCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<FurnitureAndFixtureState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("FurnitureAndFixture with id {PropertyValue} does not exists");
        
    }
}
