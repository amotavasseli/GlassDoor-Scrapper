using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobHunter.Web.Requests
{
    public class JobUpdateRequest : JobRequest
    {
        [Required]
        public int Id { get; set; }
    }
}