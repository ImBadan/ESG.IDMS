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

namespace ESG.IDMS.Application.Features.IDMS.FireArms.Commands;

public record EditFireArmsCommand : FireArmsState, IRequest<Validation<Error, FireArmsState>>;

public class EditFireArmsCommandHandler(ApplicationContext context,
                                 IMapper mapper,
                                 CompositeValidator<EditFireArmsCommand> validator) : BaseCommandHandler<ApplicationContext, FireArmsState, EditFireArmsCommand>(context, mapper, validator), IRequestHandler<EditFireArmsCommand, Validation<Error, FireArmsState>>
{ 
    
public async Task<Validation<Error, FireArmsState>> Handle(EditFireArmsCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
}

public class EditFireArmsCommandValidator : AbstractValidator<EditFireArmsCommand>
{
    readonly ApplicationContext _context;

    public EditFireArmsCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<FireArmsState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("FireArms with id {PropertyValue} does not exists");
        
    }
}
