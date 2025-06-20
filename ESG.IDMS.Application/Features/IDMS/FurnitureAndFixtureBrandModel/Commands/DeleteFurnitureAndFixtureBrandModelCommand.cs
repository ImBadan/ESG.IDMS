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

namespace ESG.IDMS.Application.Features.IDMS.FurnitureAndFixtureBrandModel.Commands;

public record DeleteFurnitureAndFixtureBrandModelCommand : BaseCommand, IRequest<Validation<Error, FurnitureAndFixtureBrandModelState>>;

public class DeleteFurnitureAndFixtureBrandModelCommandHandler(ApplicationContext context,
                                   IMapper mapper,
                                   CompositeValidator<DeleteFurnitureAndFixtureBrandModelCommand> validator) : BaseCommandHandler<ApplicationContext, FurnitureAndFixtureBrandModelState, DeleteFurnitureAndFixtureBrandModelCommand>(context, mapper, validator), IRequestHandler<DeleteFurnitureAndFixtureBrandModelCommand, Validation<Error, FurnitureAndFixtureBrandModelState>>
{ 
    public async Task<Validation<Error, FurnitureAndFixtureBrandModelState>> Handle(DeleteFurnitureAndFixtureBrandModelCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteFurnitureAndFixtureBrandModelCommandValidator : AbstractValidator<DeleteFurnitureAndFixtureBrandModelCommand>
{
    readonly ApplicationContext _context;

    public DeleteFurnitureAndFixtureBrandModelCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<FurnitureAndFixtureBrandModelState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("FurnitureAndFixtureBrandModel with id {PropertyValue} does not exists");
    }
}
