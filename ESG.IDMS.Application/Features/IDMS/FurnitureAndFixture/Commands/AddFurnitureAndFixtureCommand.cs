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

public record AddFurnitureAndFixtureCommand : FurnitureAndFixtureState, IRequest<Validation<Error, FurnitureAndFixtureState>>;

public class AddFurnitureAndFixtureCommandHandler(ApplicationContext context,
                                IMapper mapper,
                                CompositeValidator<AddFurnitureAndFixtureCommand> validator) : BaseCommandHandler<ApplicationContext, FurnitureAndFixtureState, AddFurnitureAndFixtureCommand>(context, mapper, validator), IRequestHandler<AddFurnitureAndFixtureCommand, Validation<Error, FurnitureAndFixtureState>>
{
    
public async Task<Validation<Error, FurnitureAndFixtureState>> Handle(AddFurnitureAndFixtureCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
}

public class AddFurnitureAndFixtureCommandValidator : AbstractValidator<AddFurnitureAndFixtureCommand>
{
    readonly ApplicationContext _context;

    public AddFurnitureAndFixtureCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<FurnitureAndFixtureState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("FurnitureAndFixture with id {PropertyValue} already exists");
        
    }
}
