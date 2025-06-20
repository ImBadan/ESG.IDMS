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

namespace ESG.IDMS.Application.Features.IDMS.RadioEquipmentBrandModel.Commands;

public record EditRadioEquipmentBrandModelCommand : RadioEquipmentBrandModelState, IRequest<Validation<Error, RadioEquipmentBrandModelState>>;

public class EditRadioEquipmentBrandModelCommandHandler(ApplicationContext context,
                                 IMapper mapper,
                                 CompositeValidator<EditRadioEquipmentBrandModelCommand> validator) : BaseCommandHandler<ApplicationContext, RadioEquipmentBrandModelState, EditRadioEquipmentBrandModelCommand>(context, mapper, validator), IRequestHandler<EditRadioEquipmentBrandModelCommand, Validation<Error, RadioEquipmentBrandModelState>>
{ 
    
public async Task<Validation<Error, RadioEquipmentBrandModelState>> Handle(EditRadioEquipmentBrandModelCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
}

public class EditRadioEquipmentBrandModelCommandValidator : AbstractValidator<EditRadioEquipmentBrandModelCommand>
{
    readonly ApplicationContext _context;

    public EditRadioEquipmentBrandModelCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<RadioEquipmentBrandModelState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("RadioEquipmentBrandModel with id {PropertyValue} does not exists");
        
    }
}
