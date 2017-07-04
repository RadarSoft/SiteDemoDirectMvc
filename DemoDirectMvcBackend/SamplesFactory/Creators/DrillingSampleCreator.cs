using Models;

namespace SamplesFactory
{
    public class DrillingSampleCreator : SampleCreator
    {
        public override Sample CreateSample(SamplesModel sampleModel)
        {
            var sample = new DrillingSample(sampleModel);
            return sample;
        }
    }
}