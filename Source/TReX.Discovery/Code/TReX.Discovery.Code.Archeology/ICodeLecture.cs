using CSharpFunctionalExtensions;
using TReX.Discovery.Code.Domain;

namespace TReX.Discovery.Code.Archeology
{
    internal interface ICodeLecture
    {
        Result<CodeResource> ToResource();
    }
}