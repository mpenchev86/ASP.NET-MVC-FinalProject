namespace JustOrderIt.Web.ViewModels.Manage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Microsoft.AspNet.Identity;

    public class IndexViewModel
    {
        public bool HasPassword { get; set; }

        public bool HasUserName { get; set; }

        public IList<UserLoginInfo> Logins { get; set; }

        public string PhoneNumber { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }
    }
}