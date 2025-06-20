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

namespace ESG.IDMS.Application.Features.IDMS.Vehicle.Commands;

public record EditVehicleCommand : VehicleState, IRequest<Validation<Error, VehicleState>>;

public class EditVehicleCommandHandler(ApplicationContext context,
                                 IMapper mapper,
                                 CompositeValidator<EditVehicleCommand> validator) : BaseCommandHandler<ApplicationContext, VehicleState, EditVehicleCommand>(context, mapper, validator), IRequestHandler<EditVehicleCommand, Validation<Error, VehicleState>>
{ 
    
public async Task<Validation<Error, VehicleState>> Handle(EditVehicleCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
}

public class EditVehicleCommandValidator : AbstractValidator<EditVehicleCommand>
{
    readonly ApplicationContext _context;

    public EditVehicleCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<VehicleState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Vehicle with id {PropertyValue} does not exists");
        
    }
}
