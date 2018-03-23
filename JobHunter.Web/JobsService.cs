using JobHunter.Web.Domains;
using JobHunter.Web.Requests;
using LIScraperLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
                        job.Archived = reader.GetBoolean(10);
                        jobs.Add(job);
                    }
                    return jobs;
                }
            }
        }
        public int Create(JobRequest req)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = ConnectionWrapper("Jobs_insert", con);
                cmd.Parameters.AddWithValue("@title", req.Title);
                cmd.Parameters.AddWithValue("@link", req.Link);
                cmd.Parameters.AddWithValue("@company", req.Company);
                cmd.Parameters.AddWithValue("@description", req.Description);
                cmd.Parameters.AddWithValue("@location", req.Location);
                cmd.Parameters.AddWithValue("@post_date", req.PostDate);
                cmd.Parameters.AddWithValue("@date_applied", req.DateApplied ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@archived", req.Archived);
                cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                

                return (int)cmd.Parameters["@id"].Value;

            }

        }

        public void Update(JobUpdateRequest req)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = ConnectionWrapper("Jobs_update", con);
                cmd.Parameters.AddWithValue("@id", req.Id);
                cmd.Parameters.AddWithValue("@title", req.Title);
                cmd.Parameters.AddWithValue("@link", req.Link);
                cmd.Parameters.AddWithValue("@company", req.Company);
                cmd.Parameters.AddWithValue("@description", req.Description);
                cmd.Parameters.AddWithValue("@location", req.Location);
                cmd.Parameters.AddWithValue("@post_date", req.PostDate);
                cmd.Parameters.AddWithValue("@date_applied", req.DateApplied ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@archived", req.Archived);

                cmd.ExecuteNonQuery();
                
            }
        }
    }
}