using Models;

namespace SamplesFactory
{
    public class GettingStartedSampleCreator : SampleCreator
    {
        public override Sample CreateSample(SamplesModel sampleModel)
        {
            var sample = new GettingStartedSample(sampleModel);
            return sample;
        }
    }
}