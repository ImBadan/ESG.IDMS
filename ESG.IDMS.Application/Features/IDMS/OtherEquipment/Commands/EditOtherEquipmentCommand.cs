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

namespace ESG.IDMS.Application.Features.IDMS.OtherEquipment.Commands;

public record EditOtherEquipmentCommand : OtherEquipmentState, IRequest<Validation<Error, OtherEquipmentState>>;

public class EditOtherEquipmentCommandHandler(ApplicationContext context,
                                 IMapper mapper,
                                 CompositeValidator<EditOtherEquipmentCommand> validator) : BaseCommandHandler<ApplicationContext, OtherEquipmentState, EditOtherEquipmentCommand>(context, mapper, validator), IRequestHandler<EditOtherEquipmentCommand, Validation<Error, OtherEquipmentState>>
{ 
    
public async Task<Validation<Error, OtherEquipmentState>> Handle(EditOtherEquipmentCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
}

public class EditOtherEquipmentCommandValidator : AbstractValidator<EditOtherEquipmentCommand>
{
    readonly ApplicationContext _context;

    public EditOtherEquipmentCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<OtherEquipmentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("OtherEquipment with id {PropertyValue} does not exists");
        
    }
}
