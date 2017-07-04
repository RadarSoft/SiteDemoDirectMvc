using Models;

namespace SamplesFactory
{
    public class ChartSampleCreator : SampleCreator
    {
        public override Sample CreateSample(SamplesModel sampleModel)
        {
            var sample = new ChartSample(sampleModel);
            return sample;
        }
    }
}