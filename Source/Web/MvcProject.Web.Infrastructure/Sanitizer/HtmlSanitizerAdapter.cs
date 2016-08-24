namespace MvcProject.Web.Infrastructure.Sanitizer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Ganss.XSS;

    public class HtmlSanitizerAdapter : ISanitizer
    {
        public static string StaticSanitize(string html)
        {
            var sanitizer = new HtmlSanitizer();
            var result = sanitizer.Sanitize(html);
            return result;
        }

        public string Sanitize(string html)
        {
            return StaticSanitize(html);
        }
    }
}
