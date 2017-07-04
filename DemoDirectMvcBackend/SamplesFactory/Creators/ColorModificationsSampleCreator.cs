using Models;

namespace SamplesFactory
{
    public class ColorModificationsSampleCreator : SampleCreator
    {
        public override Sample CreateSample(SamplesModel sampleModel)
        {
            var sample = new ColorModificationsSample(sampleModel);
            return sample;
        }
    }
}