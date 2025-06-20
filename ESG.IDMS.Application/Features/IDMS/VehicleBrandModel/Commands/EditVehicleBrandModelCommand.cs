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

namespace ESG.IDMS.Application.Features.IDMS.VehicleBrandModel.Commands;

public record EditVehicleBrandModelCommand : VehicleBrandModelState, IRequest<Validation<Error, VehicleBrandModelState>>;

public class EditVehicleBrandModelCommandHandler(ApplicationContext context,
                                 IMapper mapper,
                                 CompositeValidator<EditVehicleBrandModelCommand> validator) : BaseCommandHandler<ApplicationContext, VehicleBrandModelState, EditVehicleBrandModelCommand>(context, mapper, validator), IRequestHandler<EditVehicleBrandModelCommand, Validation<Error, VehicleBrandModelState>>
{ 
    
public async Task<Validation<Error, VehicleBrandModelState>> Handle(EditVehicleBrandModelCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
}

public class EditVehicleBrandModelCommandValidator : AbstractValidator<EditVehicleBrandModelCommand>
{
    readonly ApplicationContext _context;

    public EditVehicleBrandModelCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<VehicleBrandModelState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("VehicleBrandModel with id {PropertyValue} does not exists");
        
    }
}
