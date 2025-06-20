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

public record AddStatusCommand : StatusState, IRequest<Validation<Error, StatusState>>;

public class AddStatusCommandHandler(ApplicationContext context,
                                IMapper mapper,
                                CompositeValidator<AddStatusCommand> validator) : BaseCommandHandler<ApplicationContext, StatusState, AddStatusCommand>(context, mapper, validator), IRequestHandler<AddStatusCommand, Validation<Error, StatusState>>
{
    
public async Task<Validation<Error, StatusState>> Handle(AddStatusCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
}

public class AddStatusCommandValidator : AbstractValidator<AddStatusCommand>
{
    readonly ApplicationContext _context;

    public AddStatusCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<StatusState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Status with id {PropertyValue} already exists");
        
    }
}
