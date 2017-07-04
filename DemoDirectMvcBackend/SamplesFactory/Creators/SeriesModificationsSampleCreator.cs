using Models;

namespace SamplesFactory
{
    public class SeriesModificationsSampleCreator : SampleCreator
    {
        public override Sample CreateSample(SamplesModel sampleModel)
        {
            var sample = new SeriesModificationsSample(sampleModel);
            return sample;
        }
    }
}