using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace TReX.Kernel.Shared
{
    public static class Extensions
    {
        public static Maybe<T> ToMaybe<T>(this T subject)
        {
            return subject;
        }

        public static async Task<Result> TryAsync(Func<Task> func)
        {
            try
            {
                await func();
                return Result.Ok();
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }
    }
}