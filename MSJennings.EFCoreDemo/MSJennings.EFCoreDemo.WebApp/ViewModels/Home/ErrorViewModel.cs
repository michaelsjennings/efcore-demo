using System;

namespace MSJennings.EFCoreDemo.WebApp.ViewModels.Home
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
