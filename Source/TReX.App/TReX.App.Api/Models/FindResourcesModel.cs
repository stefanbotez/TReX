namespace TReX.App.Api.Models
{
    public sealed class FindResourcesModel
    {
        public string Topic { get; set; }

        public string OrderBy { get; set; }

        public int Page { get; set; }
    }
}