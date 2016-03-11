using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MvcProject.Data.Models;
using MvcProject.Web.Infrastructure.Mapping;

namespace MvcProject.Web.Areas.Common.ViewModels.Kendo
{
    public class KendoTestViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Registration Date")]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string Country { get; set; }

        public IEnumerable<string> Countries { get; set; }
    }
}