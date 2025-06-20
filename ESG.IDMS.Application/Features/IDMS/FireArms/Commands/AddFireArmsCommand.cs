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

public record AddFireArmsCommand : FireArmsState, IRequest<Validation<Error, FireArmsState>>;

public class AddFireArmsCommandHandler(ApplicationContext context,
                                IMapper mapper,
                                CompositeValidator<AddFireArmsCommand> validator) : BaseCommandHandler<ApplicationContext, FireArmsState, AddFireArmsCommand>(context, mapper, validator), IRequestHandler<AddFireArmsCommand, Validation<Error, FireArmsState>>
{
    
public async Task<Validation<Error, FireArmsState>> Handle(AddFireArmsCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
}

public class AddFireArmsCommandValidator : AbstractValidator<AddFireArmsCommand>
{
    readonly ApplicationContext _context;

    public AddFireArmsCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<FireArmsState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("FireArms with id {PropertyValue} already exists");
        
    }
}
