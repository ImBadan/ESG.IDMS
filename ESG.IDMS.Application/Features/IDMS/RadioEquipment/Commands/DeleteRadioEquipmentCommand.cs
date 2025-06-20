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

namespace ESG.IDMS.Application.Features.IDMS.RadioEquipment.Commands;

public record DeleteRadioEquipmentCommand : BaseCommand, IRequest<Validation<Error, RadioEquipmentState>>;

public class DeleteRadioEquipmentCommandHandler(ApplicationContext context,
                                   IMapper mapper,
                                   CompositeValidator<DeleteRadioEquipmentCommand> validator) : BaseCommandHandler<ApplicationContext, RadioEquipmentState, DeleteRadioEquipmentCommand>(context, mapper, validator), IRequestHandler<DeleteRadioEquipmentCommand, Validation<Error, RadioEquipmentState>>
{ 
    public async Task<Validation<Error, RadioEquipmentState>> Handle(DeleteRadioEquipmentCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteRadioEquipmentCommandValidator : AbstractValidator<DeleteRadioEquipmentCommand>
{
    readonly ApplicationContext _context;

    public DeleteRadioEquipmentCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<RadioEquipmentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("RadioEquipment with id {PropertyValue} does not exists");
    }
}
