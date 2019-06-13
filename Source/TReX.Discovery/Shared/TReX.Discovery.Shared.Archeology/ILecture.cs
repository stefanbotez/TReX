using CSharpFunctionalExtensions;

namespace TReX.Discovery.Shared.Archeology
{
    public interface ILecture<TResource>
    {
        Result<TResource> ToResource();
    }
}