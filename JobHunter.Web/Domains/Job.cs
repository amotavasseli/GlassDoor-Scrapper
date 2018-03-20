using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobHunter.Web.Domains
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string PostDate { get; set; }
        public DateTime? DateApplied { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}