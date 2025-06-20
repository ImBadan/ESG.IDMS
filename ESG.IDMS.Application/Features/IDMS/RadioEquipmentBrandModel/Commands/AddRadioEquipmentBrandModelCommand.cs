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

public record AddRadioEquipmentBrandModelCommand : RadioEquipmentBrandModelState, IRequest<Validation<Error, RadioEquipmentBrandModelState>>;

public class AddRadioEquipmentBrandModelCommandHandler(ApplicationContext context,
                                IMapper mapper,
                                CompositeValidator<AddRadioEquipmentBrandModelCommand> validator) : BaseCommandHandler<ApplicationContext, RadioEquipmentBrandModelState, AddRadioEquipmentBrandModelCommand>(context, mapper, validator), IRequestHandler<AddRadioEquipmentBrandModelCommand, Validation<Error, RadioEquipmentBrandModelState>>
{
    
public async Task<Validation<Error, RadioEquipmentBrandModelState>> Handle(AddRadioEquipmentBrandModelCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
}

public class AddRadioEquipmentBrandModelCommandValidator : AbstractValidator<AddRadioEquipmentBrandModelCommand>
{
    readonly ApplicationContext _context;

    public AddRadioEquipmentBrandModelCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<RadioEquipmentBrandModelState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("RadioEquipmentBrandModel with id {PropertyValue} already exists");
        
    }
}
