using System;
using System.ComponentModel.DataAnnotations;

namespace MSJennings.EFCoreDemo.WebApp.ApiModels.Events
{
    public class PutEventApiModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string LocationDescription { get; set; }
    }
}
