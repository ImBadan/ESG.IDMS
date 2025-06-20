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

namespace ESG.IDMS.Application.Features.IDMS.Location.Commands;

public record EditLocationCommand : LocationState, IRequest<Validation<Error, LocationState>>;

public class EditLocationCommandHandler(ApplicationContext context,
                                 IMapper mapper,
                                 CompositeValidator<EditLocationCommand> validator) : BaseCommandHandler<ApplicationContext, LocationState, EditLocationCommand>(context, mapper, validator), IRequestHandler<EditLocationCommand, Validation<Error, LocationState>>
{ 
    
public async Task<Validation<Error, LocationState>> Handle(EditLocationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
}

public class EditLocationCommandValidator : AbstractValidator<EditLocationCommand>
{
    readonly ApplicationContext _context;

    public EditLocationCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<LocationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Location with id {PropertyValue} does not exists");
        
    }
}
