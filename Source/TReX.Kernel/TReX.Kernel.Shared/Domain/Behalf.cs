using System;
using CSharpFunctionalExtensions;

namespace TReX.Kernel.Shared.Domain
{
    public sealed class Behalf
    {
        public Guid Value { get; private set; }

        private Behalf()
        {
        }

        public static Result<Behalf> Create(Guid id)
        {
            return Result.Ok(id)
                .Ensure(x => x != Guid.Empty, "Behalf cannot be empty")
                .OnSuccess(x => new Behalf { Value = x });
        }
    }
}