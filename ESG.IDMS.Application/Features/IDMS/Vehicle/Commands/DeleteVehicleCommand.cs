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

namespace ESG.IDMS.Application.Features.IDMS.Vehicle.Commands;

public record DeleteVehicleCommand : BaseCommand, IRequest<Validation<Error, VehicleState>>;

public class DeleteVehicleCommandHandler(ApplicationContext context,
                                   IMapper mapper,
                                   CompositeValidator<DeleteVehicleCommand> validator) : BaseCommandHandler<ApplicationContext, VehicleState, DeleteVehicleCommand>(context, mapper, validator), IRequestHandler<DeleteVehicleCommand, Validation<Error, VehicleState>>
{ 
    public async Task<Validation<Error, VehicleState>> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteVehicleCommandValidator : AbstractValidator<DeleteVehicleCommand>
{
    readonly ApplicationContext _context;

    public DeleteVehicleCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<VehicleState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Vehicle with id {PropertyValue} does not exists");
    }
}
