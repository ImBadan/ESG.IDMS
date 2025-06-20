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

public record AddOtherEquipmentCommand : OtherEquipmentState, IRequest<Validation<Error, OtherEquipmentState>>;

public class AddOtherEquipmentCommandHandler(ApplicationContext context,
                                IMapper mapper,
                                CompositeValidator<AddOtherEquipmentCommand> validator) : BaseCommandHandler<ApplicationContext, OtherEquipmentState, AddOtherEquipmentCommand>(context, mapper, validator), IRequestHandler<AddOtherEquipmentCommand, Validation<Error, OtherEquipmentState>>
{
    
public async Task<Validation<Error, OtherEquipmentState>> Handle(AddOtherEquipmentCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
}

public class AddOtherEquipmentCommandValidator : AbstractValidator<AddOtherEquipmentCommand>
{
    readonly ApplicationContext _context;

    public AddOtherEquipmentCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<OtherEquipmentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("OtherEquipment with id {PropertyValue} already exists");
        
    }
}
