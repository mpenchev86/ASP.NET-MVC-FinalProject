﻿namespace MvcProject.Services.Logic.ServiceModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RefinementFilter
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<string> SelectedOptions { get; set; }
    }
}
