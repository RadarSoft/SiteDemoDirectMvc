using Models;

namespace SamplesFactory
{
    public class FilteringSampleCreator : SampleCreator
    {
        public override Sample CreateSample(SamplesModel sampleModel)
        {
            var sample = new FilteringSample(sampleModel);
            return sample;
        }
    }
}