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

namespace ESG.IDMS.Application.Features.IDMS.FireArmsBrandModel.Commands;

public record EditFireArmsBrandModelCommand : FireArmsBrandModelState, IRequest<Validation<Error, FireArmsBrandModelState>>;

public class EditFireArmsBrandModelCommandHandler(ApplicationContext context,
                                 IMapper mapper,
                                 CompositeValidator<EditFireArmsBrandModelCommand> validator) : BaseCommandHandler<ApplicationContext, FireArmsBrandModelState, EditFireArmsBrandModelCommand>(context, mapper, validator), IRequestHandler<EditFireArmsBrandModelCommand, Validation<Error, FireArmsBrandModelState>>
{ 
    
public async Task<Validation<Error, FireArmsBrandModelState>> Handle(EditFireArmsBrandModelCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
}

public class EditFireArmsBrandModelCommandValidator : AbstractValidator<EditFireArmsBrandModelCommand>
{
    readonly ApplicationContext _context;

    public EditFireArmsBrandModelCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<FireArmsBrandModelState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("FireArmsBrandModel with id {PropertyValue} does not exists");
        
    }
}
