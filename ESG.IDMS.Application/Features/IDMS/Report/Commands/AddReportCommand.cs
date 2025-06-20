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

namespace ESG.IDMS.Application.Features.IDMS.Report.Commands;

public record AddReportCommand : ReportState, IRequest<Validation<Error, ReportState>>;

public class AddReportCommandHandler(ApplicationContext context,
                                IMapper mapper,
                                CompositeValidator<AddReportCommand> validator) : BaseCommandHandler<ApplicationContext, ReportState, AddReportCommand>(context, mapper, validator), IRequestHandler<AddReportCommand, Validation<Error, ReportState>>
{
    public async Task<Validation<Error, ReportState>> Handle(AddReportCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddReport(request, cancellationToken));


	public async Task<Validation<Error, ReportState>> AddReport(AddReportCommand request, CancellationToken cancellationToken)
	{
		var error = Helpers.SQLValidatorHelper.Validate(request.QueryString) ;
        if (!string.IsNullOrEmpty(error))
        {
            return Error.New(error);
        }
        ReportState entity = Mapper.Map<ReportState>(request);		
		UpdateReportQueryFilterList(entity);
		UpdateReportRoleAssignmentList(entity);
        _ = await Context.AddAsync(entity, cancellationToken);	
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ReportState>(entity);
	}	

	private void UpdateReportQueryFilterList(ReportState entity)
	{
		if (entity.ReportQueryFilterList?.Count > 0)
		{
			foreach (var reportQueryFilter in entity.ReportQueryFilterList!)
			{
				Context.Entry(reportQueryFilter).State = EntityState.Added;
			}
		}
	}
    private void UpdateReportRoleAssignmentList(ReportState entity)
    {
        if (entity.ReportQueryFilterList?.Count > 0)
        {
            foreach (var reportRoleAssignment in entity.ReportRoleAssignmentList!)
            {
                Context.Entry(reportRoleAssignment).State = EntityState.Added;
            }
        }
    }
}

public class AddReportCommandValidator : AbstractValidator<AddReportCommand>
{
    readonly ApplicationContext _context;

    public AddReportCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ReportState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Report with id {PropertyValue} already exists");
        RuleFor(x => x.ReportName).MustAsync(async (reportName, cancellation) => await _context.NotExists<ReportState>(x => x.ReportName == reportName, cancellationToken: cancellation)).WithMessage("Report with reportName {PropertyValue} already exists");
	
    }
}
