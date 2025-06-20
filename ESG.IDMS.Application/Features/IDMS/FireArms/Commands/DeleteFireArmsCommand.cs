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

namespace ESG.IDMS.Application.Features.IDMS.FireArms.Commands;

public record DeleteFireArmsCommand : BaseCommand, IRequest<Validation<Error, FireArmsState>>;

public class DeleteFireArmsCommandHandler(ApplicationContext context,
                                   IMapper mapper,
                                   CompositeValidator<DeleteFireArmsCommand> validator) : BaseCommandHandler<ApplicationContext, FireArmsState, DeleteFireArmsCommand>(context, mapper, validator), IRequestHandler<DeleteFireArmsCommand, Validation<Error, FireArmsState>>
{ 
    public async Task<Validation<Error, FireArmsState>> Handle(DeleteFireArmsCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteFireArmsCommandValidator : AbstractValidator<DeleteFireArmsCommand>
{
    readonly ApplicationContext _context;

    public DeleteFireArmsCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<FireArmsState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("FireArms with id {PropertyValue} does not exists");
    }
}
