using Models;

namespace SamplesFactory
{
    public class ToolboxSampleCreator : SampleCreator
    {
        public override Sample CreateSample(SamplesModel sampleModel)
        {
            var sample = new ToolboxSample(sampleModel);
            return sample;
        }
    }
}