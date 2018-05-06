namespace HaroldAdviser.ViewModels
{
    public interface IWebhook
    {
        string CloneUrl { get; }

        string HtmlUrl { get; }

        string CommitId { get; }
    }
}