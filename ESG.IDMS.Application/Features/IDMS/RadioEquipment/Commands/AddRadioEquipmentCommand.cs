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

namespace ESG.IDMS.Application.Features.IDMS.RadioEquipment.Commands;

public record AddRadioEquipmentCommand : RadioEquipmentState, IRequest<Validation<Error, RadioEquipmentState>>;

public class AddRadioEquipmentCommandHandler(ApplicationContext context,
                                IMapper mapper,
                                CompositeValidator<AddRadioEquipmentCommand> validator) : BaseCommandHandler<ApplicationContext, RadioEquipmentState, AddRadioEquipmentCommand>(context, mapper, validator), IRequestHandler<AddRadioEquipmentCommand, Validation<Error, RadioEquipmentState>>
{
    
public async Task<Validation<Error, RadioEquipmentState>> Handle(AddRadioEquipmentCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
}

public class AddRadioEquipmentCommandValidator : AbstractValidator<AddRadioEquipmentCommand>
{
    readonly ApplicationContext _context;

    public AddRadioEquipmentCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<RadioEquipmentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("RadioEquipment with id {PropertyValue} already exists");
        
    }
}
