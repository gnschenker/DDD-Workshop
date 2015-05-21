using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DDD_Sample.Contracts.Commands;
using DDD_Sample.Domain;

namespace DDD_Sample.Controllers
{
    [RoutePrefix("api/samples")]
    public class SamplesController : ApiController
    {
        private readonly ISamplesApplicationService _samplesApplicationService;

        private static readonly List<SampleInfo> samples = new List<SampleInfo>
        {
            new SampleInfo {SampleId = Guid.NewGuid(), Name = "sample value 1", SomeDate = DateTime.Today},
            new SampleInfo {SampleId = Guid.NewGuid(), Name = "sample value 2", SomeDate = DateTime.Today.AddDays(3)}
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
        public SampleInfo Get(Guid sampleId)
        {
            return new SampleInfo { SampleId = sampleId, Name = "value", SomeDate = DateTime.Today};
        }

        [HttpPost]
        [Route("start")]
        public HttpResponseMessage Post([FromBody]StartRequest req)
        {
            var id = _samplesApplicationService.When(new StartCommand{Name = req.Name});
            return Request.CreateResponse(HttpStatusCode.Created, new {sampleId = id});
        }

        [HttpPost]
        [Route("{sampleId}/next")]
        public void Post(Guid sampleId, [FromBody]NextRequest req)
        {
            _samplesApplicationService.When(new NextCommand{Id = sampleId, SomeDate = req.SomeDate});
        }
    }

    public class StartRequest
    {
        public string Name { get; set; }
    }

    public class NextRequest
    {
        public DateTime SomeDate { get; set; }
    }

    public class SampleInfo
    {
        public Guid SampleId { get; set; }
        public string Name { get; set; }
        public DateTime? SomeDate { get; set; }
    }
}