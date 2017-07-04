using Models;

namespace SamplesFactory
{
    public class InfoAttributesSampleCreator : SampleCreator
    {
        public override Sample CreateSample(SamplesModel sampleModel)
        {
            var sample = new InfoAttributesSample(sampleModel);
            return sample;
        }
    }
}