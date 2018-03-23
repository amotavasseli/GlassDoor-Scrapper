using System.Collections.Generic;
using JobHunter.Web.Domains;
using JobHunter.Web.Requests;

namespace JobHunter.Web
{
    public interface IJobsService
    {
        List<Job> GetAll();
        void Update(JobUpdateRequest req);
        int Create(JobRequest req);
    }
}