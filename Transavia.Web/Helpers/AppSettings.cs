using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transavia.Web.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string SendGridKey { get; set; }
        public string SendGridUser { get; set; }
    }
}
