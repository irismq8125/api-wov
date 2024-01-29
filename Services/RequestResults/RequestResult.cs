using System.ComponentModel.DataAnnotations;

namespace api_wov.Services.RequestResults
{
    public class RequestResult
    {
        [Range(0, int.MaxValue)]
        public virtual int SkipCount { get; set; } = 0;

        [Range(0, int.MaxValue)]
        public virtual int MaxResultCount { get; set; } = 0;

        public virtual string Sorting { get; set; } = "";
        public string? Filters { get; set; } = "";
        public string? Status { get; set; } = "";
    }
}
