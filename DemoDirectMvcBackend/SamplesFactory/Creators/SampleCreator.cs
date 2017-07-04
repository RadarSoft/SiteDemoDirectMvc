using Models;

namespace SamplesFactory
{
    public abstract class SampleCreator
    {
        public abstract Sample CreateSample(SamplesModel sampleModel);
    }
}