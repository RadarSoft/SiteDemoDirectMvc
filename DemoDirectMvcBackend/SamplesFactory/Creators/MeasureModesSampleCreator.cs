using Models;

namespace SamplesFactory
{
    public class MeasureModesSampleCreator : SampleCreator
    {
        public override Sample CreateSample(SamplesModel sampleModel)
        {
            var sample = new MeasureModesSample(sampleModel);
            return sample;
        }
    }
}