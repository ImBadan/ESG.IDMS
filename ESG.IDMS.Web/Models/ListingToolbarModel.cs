namespace ESG.IDMS.Web.Models
{
    public class ListingToolbarModel(string pageTitle)
    {
        public string PageTitle { get; set; } = pageTitle;
        public List<Button> Buttons { get; set; } = [];
    }
    public class Button(string page, string area, string type, int sequence)
    {
        public Button(string type, int sequence) : this("", "", type, sequence)
        {
            this.Type = type;
            this.Sequence = sequence;
        }
        public string Page { get; set; } = page;
        public string Area { get; set; } = area;
        public int Sequence { get; set; } = sequence;
        public string Type { get; set; } = type;
        public string StyleClass
        {
            get
            {
                return this.Type switch
                {
                    ButtonType.Add => "btn btn-success text-light",
                    ButtonType.Download => "btn btn-primary text-light",
                    ButtonType.Upload or ButtonType.DownloadTemplate => "btn btn-custom-dark-blue text-light",
                    ButtonType.Back => "btn btn-secondary text-light",
                    _ => "btn btn-primary text-light",
                };
            }
        }
        public string DOMId
        {
            get
            {
                return $"{ListingToolbarConstants.ButtonPrefix}{this.Type}";
            }
        }
        public string Label
        {
            get
            {
                return this.Type switch
                {
                    ButtonType.Add or ButtonType.Download or ButtonType.Upload => this.Type,
                    ButtonType.DownloadTemplate => "Download Template",
                    _ => this.Type,
                };
            }
        }
    }
    public static class ButtonType
    {
        public const string Add = "Add";
        public const string Download = "Download";
        public const string Upload = "Upload";
        public const string Back = "Back";
        public const string DownloadTemplate = "DownloadTemplate";
    }
    public static class ListingToolbarConstants
    {
        public const string ButtonPrefix = "toolbarButton";
    }
}
