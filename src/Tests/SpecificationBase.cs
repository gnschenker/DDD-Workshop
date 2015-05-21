using NUnit.Framework;

namespace Tests
{
    public class SpecificationBase
    {
        [SetUp]
        public void SetUp()
        {
            Given();
            When();
        }

        protected virtual void Given() { }
        protected virtual void When() { }
    }

    public class ThenAttribute : TestAttribute { }
}
