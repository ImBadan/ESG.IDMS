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

namespace ESG.IDMS.Application.Features.IDMS.OtherEquipmentBrandModel.Commands;

public record DeleteOtherEquipmentBrandModelCommand : BaseCommand, IRequest<Validation<Error, OtherEquipmentBrandModelState>>;

public class DeleteOtherEquipmentBrandModelCommandHandler(ApplicationContext context,
                                   IMapper mapper,
                                   CompositeValidator<DeleteOtherEquipmentBrandModelCommand> validator) : BaseCommandHandler<ApplicationContext, OtherEquipmentBrandModelState, DeleteOtherEquipmentBrandModelCommand>(context, mapper, validator), IRequestHandler<DeleteOtherEquipmentBrandModelCommand, Validation<Error, OtherEquipmentBrandModelState>>
{ 
    public async Task<Validation<Error, OtherEquipmentBrandModelState>> Handle(DeleteOtherEquipmentBrandModelCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteOtherEquipmentBrandModelCommandValidator : AbstractValidator<DeleteOtherEquipmentBrandModelCommand>
{
    readonly ApplicationContext _context;

    public DeleteOtherEquipmentBrandModelCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<OtherEquipmentBrandModelState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("OtherEquipmentBrandModel with id {PropertyValue} does not exists");
    }
}
