using Models;

namespace SamplesFactory
{
    public class SortingSampleCreator : SampleCreator
    {
        public override Sample CreateSample(SamplesModel sampleModel)
        {
            var sample = new SortingSample(sampleModel);
            return sample;
        }
    }
}