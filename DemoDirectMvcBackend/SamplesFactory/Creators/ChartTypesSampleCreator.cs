using Models;

namespace SamplesFactory
{
    public class ChartTypesSampleCreator : SampleCreator
    {
        public override Sample CreateSample(SamplesModel sampleModel)
        {
            var sample = new ChartTypesSample(sampleModel);
            return sample;
        }
    }
}