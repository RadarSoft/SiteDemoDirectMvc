using Models;

namespace SamplesFactory
{
    public class MultiSeriesChartSampleCreator : SampleCreator
    {
        public override Sample CreateSample(SamplesModel sampleModel)
        {
            var sample = new MultiSeriesChartSample(sampleModel);
            return sample;
        }
    }
}