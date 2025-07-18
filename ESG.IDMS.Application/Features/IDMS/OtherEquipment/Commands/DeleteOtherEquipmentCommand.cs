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

namespace ESG.IDMS.Application.Features.IDMS.OtherEquipment.Commands;

public record DeleteOtherEquipmentCommand : BaseCommand, IRequest<Validation<Error, OtherEquipmentState>>;

public class DeleteOtherEquipmentCommandHandler(ApplicationContext context,
                                   IMapper mapper,
                                   CompositeValidator<DeleteOtherEquipmentCommand> validator) : BaseCommandHandler<ApplicationContext, OtherEquipmentState, DeleteOtherEquipmentCommand>(context, mapper, validator), IRequestHandler<DeleteOtherEquipmentCommand, Validation<Error, OtherEquipmentState>>
{ 
    public async Task<Validation<Error, OtherEquipmentState>> Handle(DeleteOtherEquipmentCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteOtherEquipmentCommandValidator : AbstractValidator<DeleteOtherEquipmentCommand>
{
    readonly ApplicationContext _context;

    public DeleteOtherEquipmentCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<OtherEquipmentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("OtherEquipment with id {PropertyValue} does not exists");
    }
}
