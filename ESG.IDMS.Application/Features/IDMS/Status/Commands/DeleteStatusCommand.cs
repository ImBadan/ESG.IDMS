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

namespace ESG.IDMS.Application.Features.IDMS.Status.Commands;

public record DeleteStatusCommand : BaseCommand, IRequest<Validation<Error, StatusState>>;

public class DeleteStatusCommandHandler(ApplicationContext context,
                                   IMapper mapper,
                                   CompositeValidator<DeleteStatusCommand> validator) : BaseCommandHandler<ApplicationContext, StatusState, DeleteStatusCommand>(context, mapper, validator), IRequestHandler<DeleteStatusCommand, Validation<Error, StatusState>>
{ 
    public async Task<Validation<Error, StatusState>> Handle(DeleteStatusCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteStatusCommandValidator : AbstractValidator<DeleteStatusCommand>
{
    readonly ApplicationContext _context;

    public DeleteStatusCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<StatusState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Status with id {PropertyValue} does not exists");
    }
}
