using ESG.IDMS.ExcelProcessor.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ESG.IDMS.ExcelProcessor
{
    public static class ServiceExtensions
    {
        public static void AddExcelProcessor(this IServiceCollection services)
        {
            services.AddTransient<ExcelService>();
        }
    }
}
