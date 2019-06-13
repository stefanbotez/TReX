using System;
using System.Collections.Generic;
using System.Linq;
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

        public static async Task<Maybe<T>> ToMaybe<T>(this Task<T> subjectTask)
        {
            return await subjectTask;
        }

        public static async Task<K> Unwrap<T, K>(this Task<Maybe<T>> subjectTask, Func<T, K> selector, K defaultValue = default)
        {
            var maybe = await subjectTask;
            return maybe.Unwrap(selector, defaultValue);
        }

        public static Maybe<T> FirstOrNothing<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.FirstOrDefault();
        }

        public static Maybe<T> FirstOrNothing<T>(this IEnumerable<T> enumerable, Func<T, bool> func)
        {
            return enumerable.FirstOrDefault(func);
        }

        public static Result OnFailureCompensate<T>(this Result<T> result, Func<Result> compensation)
        {
            if (result.IsFailure)
            {
                return compensation();
            }

            return result;
        }

        public static Maybe<T> ToMaybe<T>(this Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Maybe<T>.From(result.Value);
            }

            return Maybe<T>.None;
        }

        public static async Task<Maybe<T>> ToMaybe<T>(this Task<Result<T>> resultTask)
        {
            return (await resultTask).ToMaybe();
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

        public static string PascalToCamelCase(this string text)
        {
            return text[0].ToString().ToLower() + text.Substring(1);
        }
    }
}