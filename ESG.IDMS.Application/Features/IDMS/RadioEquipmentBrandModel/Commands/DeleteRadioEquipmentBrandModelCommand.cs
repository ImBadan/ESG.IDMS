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

namespace ESG.IDMS.Application.Features.IDMS.RadioEquipmentBrandModel.Commands;

public record DeleteRadioEquipmentBrandModelCommand : BaseCommand, IRequest<Validation<Error, RadioEquipmentBrandModelState>>;

public class DeleteRadioEquipmentBrandModelCommandHandler(ApplicationContext context,
                                   IMapper mapper,
                                   CompositeValidator<DeleteRadioEquipmentBrandModelCommand> validator) : BaseCommandHandler<ApplicationContext, RadioEquipmentBrandModelState, DeleteRadioEquipmentBrandModelCommand>(context, mapper, validator), IRequestHandler<DeleteRadioEquipmentBrandModelCommand, Validation<Error, RadioEquipmentBrandModelState>>
{ 
    public async Task<Validation<Error, RadioEquipmentBrandModelState>> Handle(DeleteRadioEquipmentBrandModelCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteRadioEquipmentBrandModelCommandValidator : AbstractValidator<DeleteRadioEquipmentBrandModelCommand>
{
    readonly ApplicationContext _context;

    public DeleteRadioEquipmentBrandModelCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<RadioEquipmentBrandModelState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("RadioEquipmentBrandModel with id {PropertyValue} does not exists");
    }
}
