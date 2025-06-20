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

public record AddOtherEquipmentBrandModelCommand : OtherEquipmentBrandModelState, IRequest<Validation<Error, OtherEquipmentBrandModelState>>;

public class AddOtherEquipmentBrandModelCommandHandler(ApplicationContext context,
                                IMapper mapper,
                                CompositeValidator<AddOtherEquipmentBrandModelCommand> validator) : BaseCommandHandler<ApplicationContext, OtherEquipmentBrandModelState, AddOtherEquipmentBrandModelCommand>(context, mapper, validator), IRequestHandler<AddOtherEquipmentBrandModelCommand, Validation<Error, OtherEquipmentBrandModelState>>
{
    
public async Task<Validation<Error, OtherEquipmentBrandModelState>> Handle(AddOtherEquipmentBrandModelCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
}

public class AddOtherEquipmentBrandModelCommandValidator : AbstractValidator<AddOtherEquipmentBrandModelCommand>
{
    readonly ApplicationContext _context;

    public AddOtherEquipmentBrandModelCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<OtherEquipmentBrandModelState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("OtherEquipmentBrandModel with id {PropertyValue} already exists");
        
    }
}
