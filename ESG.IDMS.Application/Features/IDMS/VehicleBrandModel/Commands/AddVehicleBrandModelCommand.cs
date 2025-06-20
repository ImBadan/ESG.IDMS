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

public record AddVehicleBrandModelCommand : VehicleBrandModelState, IRequest<Validation<Error, VehicleBrandModelState>>;

public class AddVehicleBrandModelCommandHandler(ApplicationContext context,
                                IMapper mapper,
                                CompositeValidator<AddVehicleBrandModelCommand> validator) : BaseCommandHandler<ApplicationContext, VehicleBrandModelState, AddVehicleBrandModelCommand>(context, mapper, validator), IRequestHandler<AddVehicleBrandModelCommand, Validation<Error, VehicleBrandModelState>>
{
    
public async Task<Validation<Error, VehicleBrandModelState>> Handle(AddVehicleBrandModelCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
}

public class AddVehicleBrandModelCommandValidator : AbstractValidator<AddVehicleBrandModelCommand>
{
    readonly ApplicationContext _context;

    public AddVehicleBrandModelCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<VehicleBrandModelState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("VehicleBrandModel with id {PropertyValue} already exists");
        
    }
}
