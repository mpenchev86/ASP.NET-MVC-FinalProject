using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MvcProject.Data.Models;
using MvcProject.Web.Infrastructure.Mapping;

namespace MvcProject.Web.Areas.Common.ViewModels.Kendo
{
    public class KendoTestViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime SomeDate { get; set; }

        public string Country { get; set; }

        public IEnumerable<string> Countries { get; set; }
    }
}