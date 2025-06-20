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

public record AddVehicleCommand : VehicleState, IRequest<Validation<Error, VehicleState>>;

public class AddVehicleCommandHandler(ApplicationContext context,
                                IMapper mapper,
                                CompositeValidator<AddVehicleCommand> validator) : BaseCommandHandler<ApplicationContext, VehicleState, AddVehicleCommand>(context, mapper, validator), IRequestHandler<AddVehicleCommand, Validation<Error, VehicleState>>
{
    
public async Task<Validation<Error, VehicleState>> Handle(AddVehicleCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
}

public class AddVehicleCommandValidator : AbstractValidator<AddVehicleCommand>
{
    readonly ApplicationContext _context;

    public AddVehicleCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<VehicleState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Vehicle with id {PropertyValue} already exists");
        
    }
}
