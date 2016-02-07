using Quantic.Etl.Abstractions;

namespace Quantic.Etl
{
    public static class Current
    {
        public static IPipeline TransformationPipeline { get; private set; }

        public static IPipeline LoadPipeline { get; private set; }
    }
}