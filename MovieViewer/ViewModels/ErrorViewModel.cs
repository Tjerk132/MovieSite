using System;

namespace MovieSite.Models.ViewModels
{
    public class ErrorViewModel
    {
        public int? StatusCode { get; set; }
        public string StatusCodeTitle { get; set; }
        public string StatusCodeInfo { get; set; }
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}