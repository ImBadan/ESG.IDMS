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

namespace ESG.IDMS.Application.Features.IDMS.OtherEquipmentBrandModel.Commands;

public record EditOtherEquipmentBrandModelCommand : OtherEquipmentBrandModelState, IRequest<Validation<Error, OtherEquipmentBrandModelState>>;

public class EditOtherEquipmentBrandModelCommandHandler(ApplicationContext context,
                                 IMapper mapper,
                                 CompositeValidator<EditOtherEquipmentBrandModelCommand> validator) : BaseCommandHandler<ApplicationContext, OtherEquipmentBrandModelState, EditOtherEquipmentBrandModelCommand>(context, mapper, validator), IRequestHandler<EditOtherEquipmentBrandModelCommand, Validation<Error, OtherEquipmentBrandModelState>>
{ 
    
public async Task<Validation<Error, OtherEquipmentBrandModelState>> Handle(EditOtherEquipmentBrandModelCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
}

public class EditOtherEquipmentBrandModelCommandValidator : AbstractValidator<EditOtherEquipmentBrandModelCommand>
{
    readonly ApplicationContext _context;

    public EditOtherEquipmentBrandModelCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<OtherEquipmentBrandModelState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("OtherEquipmentBrandModel with id {PropertyValue} does not exists");
        
    }
}
