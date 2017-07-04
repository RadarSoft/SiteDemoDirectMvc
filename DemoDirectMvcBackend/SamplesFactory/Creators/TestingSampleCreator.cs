using Models;

namespace SamplesFactory
{
    public class TestingSampleCreator : SampleCreator
    {
        public override Sample CreateSample(SamplesModel sampleModel)
        {
            var sample = new TestingSample(sampleModel);
            return sample;
        }
    }
}