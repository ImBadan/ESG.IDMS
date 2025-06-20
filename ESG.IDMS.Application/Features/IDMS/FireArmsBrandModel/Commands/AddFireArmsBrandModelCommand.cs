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

public record AddFireArmsBrandModelCommand : FireArmsBrandModelState, IRequest<Validation<Error, FireArmsBrandModelState>>;

public class AddFireArmsBrandModelCommandHandler(ApplicationContext context,
                                IMapper mapper,
                                CompositeValidator<AddFireArmsBrandModelCommand> validator) : BaseCommandHandler<ApplicationContext, FireArmsBrandModelState, AddFireArmsBrandModelCommand>(context, mapper, validator), IRequestHandler<AddFireArmsBrandModelCommand, Validation<Error, FireArmsBrandModelState>>
{
    
public async Task<Validation<Error, FireArmsBrandModelState>> Handle(AddFireArmsBrandModelCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
}

public class AddFireArmsBrandModelCommandValidator : AbstractValidator<AddFireArmsBrandModelCommand>
{
    readonly ApplicationContext _context;

    public AddFireArmsBrandModelCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<FireArmsBrandModelState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("FireArmsBrandModel with id {PropertyValue} already exists");
        
    }
}
