using Models;

namespace SamplesFactory
{
    public class CalculatedFieldsSampleCreator : SampleCreator
    {
        public override Sample CreateSample(SamplesModel sampleModel)
        {
            var sample = new CalculatedFieldsSample(sampleModel);
            return sample;
        }
    }
}