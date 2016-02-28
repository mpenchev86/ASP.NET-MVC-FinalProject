﻿namespace MvcProject.Web.Infrastructure.Sanitizer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISanitizer
    {
        string Sanitize(string html);
    }
}
