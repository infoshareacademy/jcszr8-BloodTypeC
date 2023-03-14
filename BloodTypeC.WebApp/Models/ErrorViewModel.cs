namespace BloodTypeC.WebApp.Models
{
    public class AllBeersViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}