using Models;

namespace SamplesFactory
{
    public class GroupingSampleCreator : SampleCreator
    {
        public override Sample CreateSample(SamplesModel sampleModel)
        {
            var sample = new GroupingSample(sampleModel);
            return sample;
        }
    }
}