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

namespace ESG.IDMS.Application.Features.IDMS.FurnitureAndFixture.Commands;

public record DeleteFurnitureAndFixtureCommand : BaseCommand, IRequest<Validation<Error, FurnitureAndFixtureState>>;

public class DeleteFurnitureAndFixtureCommandHandler(ApplicationContext context,
                                   IMapper mapper,
                                   CompositeValidator<DeleteFurnitureAndFixtureCommand> validator) : BaseCommandHandler<ApplicationContext, FurnitureAndFixtureState, DeleteFurnitureAndFixtureCommand>(context, mapper, validator), IRequestHandler<DeleteFurnitureAndFixtureCommand, Validation<Error, FurnitureAndFixtureState>>
{ 
    public async Task<Validation<Error, FurnitureAndFixtureState>> Handle(DeleteFurnitureAndFixtureCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteFurnitureAndFixtureCommandValidator : AbstractValidator<DeleteFurnitureAndFixtureCommand>
{
    readonly ApplicationContext _context;

    public DeleteFurnitureAndFixtureCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<FurnitureAndFixtureState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("FurnitureAndFixture with id {PropertyValue} does not exists");
    }
}
