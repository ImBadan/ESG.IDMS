using ESG.IDMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESG.IDMS.Web.Areas.IDMS.Pages.ViewFile
{
    [Authorize]
    public class IndexModel(IConfiguration configuration) : BasePageModel<IndexModel>
    {
        private readonly IConfiguration _configuration = configuration;

        public IActionResult OnGet(string subFolder, string fileName)
        {
            try
            {
                var uploadFilesPath = _configuration.GetValue<string>("UsersUpload:UploadFilesPath");
                // Construct the path to the requested file in your static folder.
                string filePath = Path.Combine(uploadFilesPath!, subFolder, fileName!);

                // Serve the file.
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return File(fileStream, Helper.ContentTypeHelper.GetContentType(fileName!), fileName);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error fetching the file. File Name {FileName}", fileName);
                return NotFound();
            }
        }        
    }
}
