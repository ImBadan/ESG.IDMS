using AutoMapper;
using ESG.Common.Core.Commands;
using ESG.Common.Utility.Validators;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using static LanguageExt.Prelude;

namespace ESG.IDMS.Application.Features.IDMS.Report.Commands;

public record AddReportAnalyticsCommand : ReportAIIntegrationState, IRequest<Validation<Error, ReportAIIntegrationState>>;

public class AddReportAnalyticsCommandHandler(ApplicationContext context,
                                IMapper mapper,
                                CompositeValidator<AddReportAnalyticsCommand> validator) : BaseCommandHandler<ApplicationContext, ReportState, AddReportAnalyticsCommand>(context, mapper, validator), IRequestHandler<AddReportAnalyticsCommand, Validation<Error, ReportAIIntegrationState>>
{
    public async Task<Validation<Error, ReportAIIntegrationState>> Handle(AddReportAnalyticsCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddReport(request, cancellationToken));
	public async Task<Validation<Error, ReportAIIntegrationState>> AddReport(AddReportAnalyticsCommand request, CancellationToken cancellationToken)
	{
        ReportAIIntegrationState entity = Mapper.Map<ReportAIIntegrationState>(request);			
        _ = await Context.AddAsync(entity, cancellationToken);	
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ReportAIIntegrationState>(entity);
	}	
}

public class AddReportAnalyticsCommandValidator : AbstractValidator<AddReportAnalyticsCommand>
{
    public AddReportAnalyticsCommandValidator()
    {
      
    }
}