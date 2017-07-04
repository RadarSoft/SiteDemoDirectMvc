using Models;

namespace SamplesFactory
{
    public class ContextMenuSampleCreator : SampleCreator
    {
        public override Sample CreateSample(SamplesModel sampleModel)
        {
            var sample = new ContextMenuSample(sampleModel);
            return sample;
        }
    }
}