using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DDD_Sample.Domain;

namespace DDD_Sample.Controllers
{
    [RoutePrefix("api/samples")]
    public class SamplesController : ApiController
    {
        private readonly ISamplesApplicationService _samplesApplicationService;

        private static readonly List<SampleInfo> samples = new List<SampleInfo>
        {
            new SampleInfo {SampleId = Guid.NewGuid(), Name = "sample value 1"},
            new SampleInfo {SampleId = Guid.NewGuid(), Name = "sample value 2"}
        };

        public SamplesController(ISamplesApplicationService samplesApplicationService)
        {
            _samplesApplicationService = samplesApplicationService;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<SampleInfo> Get()
        {
            return samples;
        }

        [HttpGet]
        [Route("{sampleId}")]
        public SampleInfo Get(int sampleId)
        {
            return new SampleInfo { SampleId = Guid.NewGuid(), Name = "value" };
        }

        [HttpPost]
        [Route("start")]
        public HttpResponseMessage Post([FromBody]StartRequest req)
        {
            var id = _samplesApplicationService.When(req);
            return Request.CreateResponse(HttpStatusCode.Created, new {sampleId = id});
        }

        [HttpPost]
        [Route("next")]
        public void Post([FromBody]NextRequest req)
        {
            _samplesApplicationService.When(req);
        }
    }

    public class StartRequest
    {
        public string Name { get; set; }
    }

    public class NextRequest
    {
        public Guid SampleId { get; set; }
        public DateTime SomeDate { get; set; }
    }

    public class SampleInfo
    {
        public Guid SampleId { get; set; }
        public string Name { get; set; }
        public DateTime? SomeDate { get; set; }
    }
}