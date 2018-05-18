using AngleSharp.Parser.Html;
using LIScraperLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LIScraperLibrary
{
    public class Scraper : IScraper
    {
        /*
         The scraper will scrape every job listing on linkedin that contains my desired job titles. 
         Job titles include: .NET Developer, C# Developer, Software Engineer, S Developer, Web Developer, 
                             Full Stack Developer, Back End Engineer, Front End Engineer, B and F Developer.
             
        */

        public void LIScraper()
        {
            List<JobPosting> jobs = new List<JobPosting>();
            // The URL uses LinkedIn's built-in filters, enabling us to input job titles, distance in miles from a specific location, location, and job type.
            // EX: "keywords=Job+Title distance=15 locationId=PLACES%2Eus%2E7-1-0-19-99&f_TP=1%2C2&f_E=3%2C2&f_  JT=FULL_TIME
            string initialUrl =
                "https://www.linkedin.com/jobs/search?keywords=Software+Developer&distance=15&locationId=PLACES%2Eus%2E7-1-0-19-99&f_TP=1%2C2&f_E=3%2C2&f_JT=FULL_TIME&orig=FCTD&trk=jobs_jserp_facet_exp";

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--incognito");
            options.AddArgument("--ignore-certificate-errors");
            //options.DebuggerAddress = "{http://localhost:49647|2605:e000:840e:f700:d5b4:8734:4391:4048}";
            Trace.WriteLine("Hello");
            Trace.WriteLine(options);

            IWebDriver chromeDriver = new ChromeDriver(options);
            int start = 1;
            string pageRange = "&start=" + start + "&count=50";
            string initialRange = initialUrl + pageRange;
            chromeDriver.Url = initialRange;
            var html = chromeDriver.PageSource;
            var parser = new HtmlParser();
            var doc = parser.Parse(html);
            var listings = doc.QuerySelectorAll("li.job-listing");
            string findListings = doc.QuerySelector("div.results-context > div > strong").TextContent;
            int totalListings = 0;
            if (findListings != null)
            {
                totalListings = Convert.ToInt32(findListings);
            }
            // LinkedIn allows up to 50 job listings on a single page.
            int pages = 1;
            addJobs(initialRange);
            if (totalListings > 50)
            {
                int extraPage = 0;
                if (totalListings % 50 > 0)
                {
                    extraPage = 1;
                }

                pages = (int)Math.Floor((decimal)totalListings / 50) + extraPage;

                for (int j = 1; j < pages; j++)
                {
                    start = j * 50 + 1;
                    pageRange = "&start=" + start.ToString() + "&count=50";
                    addJobs(initialUrl + pageRange);
                }

            }

            void addJobs(string url)
            {
                if (pages > 1)
                {
                    options = new ChromeOptions();
                    options.AddArgument("--headless");
                    options.AddArgument("--incognito");
                    options.AddArgument("--ignore-certificate-errors");
                    chromeDriver = new ChromeDriver(options);
                    chromeDriver.Url = url;
                    html = chromeDriver.PageSource;
                    parser = new HtmlParser();
                    doc = parser.Parse(html);
                    listings = doc.QuerySelectorAll("li.job-listing");
                }

                for (int i = 0; i < listings.Length; i++)
                {
                    JobPosting job = new JobPosting();
                    var listing = listings[i]
                        .QuerySelector("div.job-details");

                    var checkTitle = listing.QuerySelector("span.job-title-text").TextContent;
                    // Ignore job that contain these words in the job title. 
                    if (!checkTitle.Contains("Senior")
                        && !checkTitle.Contains("Sr")
                        && !checkTitle.Contains("Lead")
                        && !checkTitle.Contains("Principal")
                        && !checkTitle.Contains("Java")
                        && !checkTitle.Contains("Clearance")
                        && !checkTitle.Contains("Graphics")
                        && !checkTitle.Contains("Android")
                        && !checkTitle.Contains("iOS")
                        && !checkTitle.Contains("Wordpress")
                        && !checkTitle.Contains("WordPress")
                        && !checkTitle.Contains("PHP")
                        && !checkTitle.Contains("Architect")
                        && !checkTitle.Contains("Ruby")
                        && !checkTitle.Contains("Manager")
                        && !checkTitle.Contains("Design")
                        && !checkTitle.Contains("UI")
                        && !checkTitle.Contains("Python")
                        && !checkTitle.Contains("HTML")
                        && !checkTitle.Contains("CSS")
                        && !checkTitle.Contains("Salesforce")
                        && !checkTitle.Contains("SENIOR")
                        && !checkTitle.Contains("Analyst")
                        && checkTitle.Contains("Software Developer") //this needs to be changed with each search
                        )
                    {
                        job.JobTitle = checkTitle;
                        job.PostDate = listing.QuerySelector("span.date-posted-or-new").TextContent;
                        job.Company = listing.QuerySelector("span.company-name-text").TextContent;
                        string checkLocation = listing.QuerySelector("span.job-location > span").TextContent;
                        if (checkLocation.Contains(", US"))
                        {
                            job.Location = checkLocation.Replace(", US", "");
                        }
                        else
                        {
                            job.Location = checkLocation;
                        }

                        job.JobDescription = listing.QuerySelector("div.job-description").TextContent;
                        //Job Link
                        XmlDocument xml = new XmlDocument();
                        xml.LoadXml(listing.QuerySelector("a.job-title-link").OuterHtml);
                        XmlElement elem = xml.DocumentElement;
                        if (elem.HasAttribute("href"))
                        {
                            String attr = elem.GetAttribute("href");
                            var uri = attr.Split('?')[0];

                            job.Url = uri;
                        }
                        jobs.Add(job);
                    }
                }
            }
            ScraperService scraperService = new ScraperService(ConfigurationManager.ConnectionStrings["LIConnection"].ConnectionString);
            scraperService.Post(jobs);
        }
    }
}
