using System;
using DDD_Sample.Controllers;
using DDD_Sample.Infrastructure;

namespace DDD_Sample.Domain
{
    public interface ISamplesApplicationService
    {
        Guid When(StartRequest req);
        void When(NextRequest req);
    }

    public class SamplesApplicationService : ISamplesApplicationService
    {
        private readonly IRepository _repository;

        public SamplesApplicationService(IRepository repository)
        {
            _repository = repository;
        }

        public Guid When(StartRequest req)
        {
            var sampleId = CombGuid.Generate();
            var agg = new SampleAggregate(null);
            agg.Start(sampleId, req.Name);
            _repository.Save(agg);
            return sampleId;
        }

        public void When(NextRequest req)
        {
            var agg = _repository.GetById<SampleAggregate, Sample>(req.SampleId);
            agg.Next(req.SomeDate);
            _repository.Save(agg);
        }
    }
}