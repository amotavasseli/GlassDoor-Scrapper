﻿using JobHunter.Web.Requests;
using LIScraperLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JobHunter.Web.Controllers
{
    [AllowAnonymous]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class JobsController : ApiController
    {
        readonly IJobsService jobs;
        readonly IScraper scraper;
        public JobsController(IJobsService jobs, IScraper scraper)
        {
            this.jobs = jobs;
            this.scraper = scraper;
        }

        [HttpGet, Route("api/jobs")]
        public HttpResponseMessage GetAll()
        {
            scraper.LIScraper();
            var listings = jobs.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, listings); 
        }

        [HttpPost, Route("api/jobs")]
        public HttpResponseMessage Create(JobRequest req)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            var id = jobs.Create(req);

            return Request.CreateResponse(HttpStatusCode.Created, id);

        }

        [HttpPut, Route("api/jobs/{id}")]
        public HttpResponseMessage Update(JobUpdateRequest req)
        {
            
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            jobs.Update(req);
            return Request.CreateResponse(HttpStatusCode.OK, ModelState);
        }
    }
}