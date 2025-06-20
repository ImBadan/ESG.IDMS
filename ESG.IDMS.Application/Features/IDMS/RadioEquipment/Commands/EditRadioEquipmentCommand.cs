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

public record EditRadioEquipmentCommand : RadioEquipmentState, IRequest<Validation<Error, RadioEquipmentState>>;

public class EditRadioEquipmentCommandHandler(ApplicationContext context,
                                 IMapper mapper,
                                 CompositeValidator<EditRadioEquipmentCommand> validator) : BaseCommandHandler<ApplicationContext, RadioEquipmentState, EditRadioEquipmentCommand>(context, mapper, validator), IRequestHandler<EditRadioEquipmentCommand, Validation<Error, RadioEquipmentState>>
{ 
    
public async Task<Validation<Error, RadioEquipmentState>> Handle(EditRadioEquipmentCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
}

public class EditRadioEquipmentCommandValidator : AbstractValidator<EditRadioEquipmentCommand>
{
    readonly ApplicationContext _context;

    public EditRadioEquipmentCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<RadioEquipmentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("RadioEquipment with id {PropertyValue} does not exists");
        
    }
}
