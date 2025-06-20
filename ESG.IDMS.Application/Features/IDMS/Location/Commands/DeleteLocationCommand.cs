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

namespace ESG.IDMS.Application.Features.IDMS.Location.Commands;

public record DeleteLocationCommand : BaseCommand, IRequest<Validation<Error, LocationState>>;

public class DeleteLocationCommandHandler(ApplicationContext context,
                                   IMapper mapper,
                                   CompositeValidator<DeleteLocationCommand> validator) : BaseCommandHandler<ApplicationContext, LocationState, DeleteLocationCommand>(context, mapper, validator), IRequestHandler<DeleteLocationCommand, Validation<Error, LocationState>>
{ 
    public async Task<Validation<Error, LocationState>> Handle(DeleteLocationCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteLocationCommandValidator : AbstractValidator<DeleteLocationCommand>
{
    readonly ApplicationContext _context;

    public DeleteLocationCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<LocationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Location with id {PropertyValue} does not exists");
    }
}
