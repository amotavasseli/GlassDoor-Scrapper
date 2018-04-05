using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIScraperLibrary
{
    public class JobPosting
    {
        public string Url { get; set; }
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string PostDate { get; set; }
        public string JobDescription { get; set; }
        public DateTime? DateApplied { get; set; }
        public bool Archived { get; set; }
    }
}
