namespace ESG.IDMS.Web.Models;

public record BaseViewModel
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
}
