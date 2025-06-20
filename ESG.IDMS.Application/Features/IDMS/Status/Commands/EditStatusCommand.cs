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

namespace ESG.IDMS.Application.Features.IDMS.Status.Commands;

public record EditStatusCommand : StatusState, IRequest<Validation<Error, StatusState>>;

public class EditStatusCommandHandler(ApplicationContext context,
                                 IMapper mapper,
                                 CompositeValidator<EditStatusCommand> validator) : BaseCommandHandler<ApplicationContext, StatusState, EditStatusCommand>(context, mapper, validator), IRequestHandler<EditStatusCommand, Validation<Error, StatusState>>
{ 
    
public async Task<Validation<Error, StatusState>> Handle(EditStatusCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
}

public class EditStatusCommandValidator : AbstractValidator<EditStatusCommand>
{
    readonly ApplicationContext _context;

    public EditStatusCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<StatusState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Status with id {PropertyValue} does not exists");
        
    }
}
