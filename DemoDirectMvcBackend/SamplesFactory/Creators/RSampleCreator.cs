using Models;

namespace SamplesFactory
{
    public class RSampleCreator : SampleCreator
    {
        public override Sample CreateSample(SamplesModel sampleModel)
        {
            var sample = new RSample(sampleModel);
            return sample;
        }
    }
}