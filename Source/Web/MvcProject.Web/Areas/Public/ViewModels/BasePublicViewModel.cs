namespace MvcProject.Web.Areas.Public.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class BasePublicViewModel<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}