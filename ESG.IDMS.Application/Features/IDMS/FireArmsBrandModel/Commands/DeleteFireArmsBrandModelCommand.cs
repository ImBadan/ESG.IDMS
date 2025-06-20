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

namespace ESG.IDMS.Application.Features.IDMS.FireArmsBrandModel.Commands;

public record DeleteFireArmsBrandModelCommand : BaseCommand, IRequest<Validation<Error, FireArmsBrandModelState>>;

public class DeleteFireArmsBrandModelCommandHandler(ApplicationContext context,
                                   IMapper mapper,
                                   CompositeValidator<DeleteFireArmsBrandModelCommand> validator) : BaseCommandHandler<ApplicationContext, FireArmsBrandModelState, DeleteFireArmsBrandModelCommand>(context, mapper, validator), IRequestHandler<DeleteFireArmsBrandModelCommand, Validation<Error, FireArmsBrandModelState>>
{ 
    public async Task<Validation<Error, FireArmsBrandModelState>> Handle(DeleteFireArmsBrandModelCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteFireArmsBrandModelCommandValidator : AbstractValidator<DeleteFireArmsBrandModelCommand>
{
    readonly ApplicationContext _context;

    public DeleteFireArmsBrandModelCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<FireArmsBrandModelState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("FireArmsBrandModel with id {PropertyValue} does not exists");
    }
}
