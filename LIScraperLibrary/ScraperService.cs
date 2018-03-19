using LIScraperLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace LIScraperLibrary
{
    public class ScraperService
    {
        readonly string connectionString; 
        public ScraperService(string connectionString)
        {
            this.connectionString = connectionString;
        }
        SqlCommand ConnectionWrapper(string proc, SqlConnection con)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = proc;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return cmd;
        }

        public void Post(List<JobPosting> jobs)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                
                for (int i = 0; i < jobs.Count; i++)
                {
                    try
                    {
                        JobPosting job = jobs[i];
                        SqlCommand cmd = ConnectionWrapper("Jobs_insert", con);
                        cmd.Parameters.AddWithValue("@link", job.Url);
                        cmd.Parameters.AddWithValue("@company", job.Company);
                        cmd.Parameters.AddWithValue("@title", job.JobTitle);
                        cmd.Parameters.AddWithValue("@description", job.JobDescription);
                        cmd.Parameters.AddWithValue("@location", job.Location);
                        cmd.Parameters.AddWithValue("@post_date", job.PostDate);
                        cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                    }
                    catch (SqlException exp) when (exp.Number == 2601)
                    {
                        Trace.WriteLine(exp.Message);
                        continue;
                        // ignore and continue 
                    }
                }
                
            }
        }
    }
}