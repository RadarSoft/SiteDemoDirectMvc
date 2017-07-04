using Models;

namespace SamplesFactory
{
    public class CellContentCustomizationSampleCreator : SampleCreator
    {
        public override Sample CreateSample(SamplesModel sampleModel)
        {
            var sample = new CellContentCustomizationSample(sampleModel);
            return sample;
        }
    }
}