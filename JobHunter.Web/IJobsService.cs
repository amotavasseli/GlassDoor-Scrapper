using System.Collections.Generic;
using JobHunter.Web.Domains;

namespace JobHunter.Web
{
    public interface IJobsService
    {
        List<Job> GetAll();
    }
}