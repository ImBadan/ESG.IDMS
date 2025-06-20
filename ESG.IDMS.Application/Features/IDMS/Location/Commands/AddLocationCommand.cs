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

public record AddLocationCommand : LocationState, IRequest<Validation<Error, LocationState>>;

public class AddLocationCommandHandler(ApplicationContext context,
                                IMapper mapper,
                                CompositeValidator<AddLocationCommand> validator) : BaseCommandHandler<ApplicationContext, LocationState, AddLocationCommand>(context, mapper, validator), IRequestHandler<AddLocationCommand, Validation<Error, LocationState>>
{
    
public async Task<Validation<Error, LocationState>> Handle(AddLocationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
}

public class AddLocationCommandValidator : AbstractValidator<AddLocationCommand>
{
    readonly ApplicationContext _context;

    public AddLocationCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<LocationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Location with id {PropertyValue} already exists");
        
    }
}
