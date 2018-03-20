using JobHunter.Web.Domains;
using LIScraperLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JobHunter.Web
{
    public class JobsService : IJobsService
    {
        string connectionString = ConfigurationManager.ConnectionStrings["JobHunterConnection"].ConnectionString;
        SqlCommand ConnectionWrapper(string proc, SqlConnection con)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = proc;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            return cmd;
        }

        public List<Job> GetAll()
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = ConnectionWrapper("Jobs_getall", con);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    List<Job> jobs = new List<Job>();
                    while (reader.Read())
                    {
                        Job job = new Job();
                        job.Id = reader.GetInt32(0);
                        job.Title = reader.GetString(1);
                        job.Link = reader.GetString(2);
                        job.Company = reader.GetString(3);
                        job.Description = reader.GetString(4);
                        job.Location = reader.GetString(5);
                        job.PostDate = reader.GetString(6);
                        job.DateCreated = reader.GetDateTime(7);
                        job.DateModified = reader.GetDateTime(8);
                        job.DateApplied = reader["date_applied"] is DBNull ? (DateTime?)null : (DateTime?)reader["date_applied"];
                        jobs.Add(job);
                    }
                    return jobs;
                }
            }
        }
    }
}