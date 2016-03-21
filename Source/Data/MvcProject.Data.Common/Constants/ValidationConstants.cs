namespace MvcProject.Data.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ValidationConstants
    {
        public const int MaxOriginalFileNameLength = 255;
        public const int MaxFileExtensionLength = 4;
        public const int MaxProductTitleLength = 50;
        public const int MaxShortDescriptionLength = 40;
        public const int MaxFullDescriptionLength = 500;
        public const int MinProductCommentLength = 20;
        public const int MaxProductCommentLength = 500;
    }
}
