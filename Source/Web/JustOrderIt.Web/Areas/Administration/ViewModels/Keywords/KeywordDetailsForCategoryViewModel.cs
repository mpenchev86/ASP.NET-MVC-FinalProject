﻿namespace JustOrderIt.Web.Areas.Administration.ViewModels.Keywords
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Data.Models.Search;
    using Infrastructure.Mapping;

    public class KeywordDetailsForCategoryViewModel : BaseAdminViewModel<int>, IMapFrom<Keyword>
    {
        [Required]
        public string SearchTerm { get; set; }
    }
}