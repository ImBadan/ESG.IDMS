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

namespace ESG.IDMS.Application.Features.IDMS.Remarks.Commands;

public record AddRemarksCommand : RemarksState, IRequest<Validation<Error, RemarksState>>;

public class AddRemarksCommandHandler(ApplicationContext context,
                                IMapper mapper,
                                CompositeValidator<AddRemarksCommand> validator) : BaseCommandHandler<ApplicationContext, RemarksState, AddRemarksCommand>(context, mapper, validator), IRequestHandler<AddRemarksCommand, Validation<Error, RemarksState>>
{
    
public async Task<Validation<Error, RemarksState>> Handle(AddRemarksCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
}

public class AddRemarksCommandValidator : AbstractValidator<AddRemarksCommand>
{
    readonly ApplicationContext _context;

    public AddRemarksCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<RemarksState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Remarks with id {PropertyValue} already exists");
        
    }
}
