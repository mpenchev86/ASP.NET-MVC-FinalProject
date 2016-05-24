namespace MvcProject.Web.Areas.Admin.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public abstract class BaseAdminViewModel
    {
        //[HiddenInput(DisplayValue = false)]
        //[DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }

        //[HiddenInput(DisplayValue = false)]
        //[DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
    }
}