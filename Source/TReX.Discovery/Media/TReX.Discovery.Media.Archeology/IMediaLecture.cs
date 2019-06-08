using CSharpFunctionalExtensions;
using TReX.Discovery.Media.Domain;

namespace TReX.Discovery.Media.Archeology
{
    public interface IMediaLecture
    {
        Result<MediaResource> ToResource();
    }
}