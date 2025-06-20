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

public record EditRemarksCommand : RemarksState, IRequest<Validation<Error, RemarksState>>;

public class EditRemarksCommandHandler(ApplicationContext context,
                                 IMapper mapper,
                                 CompositeValidator<EditRemarksCommand> validator) : BaseCommandHandler<ApplicationContext, RemarksState, EditRemarksCommand>(context, mapper, validator), IRequestHandler<EditRemarksCommand, Validation<Error, RemarksState>>
{ 
    
public async Task<Validation<Error, RemarksState>> Handle(EditRemarksCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
}

public class EditRemarksCommandValidator : AbstractValidator<EditRemarksCommand>
{
    readonly ApplicationContext _context;

    public EditRemarksCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<RemarksState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Remarks with id {PropertyValue} does not exists");
        
    }
}
