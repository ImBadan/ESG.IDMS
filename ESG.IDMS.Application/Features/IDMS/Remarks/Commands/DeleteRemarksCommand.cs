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

namespace ESG.IDMS.Application.Features.IDMS.Remarks.Commands;

public record DeleteRemarksCommand : BaseCommand, IRequest<Validation<Error, RemarksState>>;

public class DeleteRemarksCommandHandler(ApplicationContext context,
                                   IMapper mapper,
                                   CompositeValidator<DeleteRemarksCommand> validator) : BaseCommandHandler<ApplicationContext, RemarksState, DeleteRemarksCommand>(context, mapper, validator), IRequestHandler<DeleteRemarksCommand, Validation<Error, RemarksState>>
{ 
    public async Task<Validation<Error, RemarksState>> Handle(DeleteRemarksCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteRemarksCommandValidator : AbstractValidator<DeleteRemarksCommand>
{
    readonly ApplicationContext _context;

    public DeleteRemarksCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<RemarksState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Remarks with id {PropertyValue} does not exists");
    }
}
