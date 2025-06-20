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

namespace ESG.IDMS.Application.Features.IDMS.VehicleBrandModel.Commands;

public record DeleteVehicleBrandModelCommand : BaseCommand, IRequest<Validation<Error, VehicleBrandModelState>>;

public class DeleteVehicleBrandModelCommandHandler(ApplicationContext context,
                                   IMapper mapper,
                                   CompositeValidator<DeleteVehicleBrandModelCommand> validator) : BaseCommandHandler<ApplicationContext, VehicleBrandModelState, DeleteVehicleBrandModelCommand>(context, mapper, validator), IRequestHandler<DeleteVehicleBrandModelCommand, Validation<Error, VehicleBrandModelState>>
{ 
    public async Task<Validation<Error, VehicleBrandModelState>> Handle(DeleteVehicleBrandModelCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteVehicleBrandModelCommandValidator : AbstractValidator<DeleteVehicleBrandModelCommand>
{
    readonly ApplicationContext _context;

    public DeleteVehicleBrandModelCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<VehicleBrandModelState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("VehicleBrandModel with id {PropertyValue} does not exists");
    }
}
