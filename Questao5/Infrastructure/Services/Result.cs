using Questao5.Domain.Enumerators;

namespace Questao5.Infrastructure.Services
{
    public class Result<T>
    {
        private Result(bool success, T data, ResultEnum result)
        {
            Success = success;
            Data = data;
            ResultEnum = result;
        }

        public bool Success { get; private set; }
        public T Data { get; private set; }
        public ResultEnum ResultEnum { get; private set; }

        public static Result<T> WithSuccess(T data)
        {
            return new Result<T>(true, data, ResultEnum.SUCCESS);
        }

        public static Result<T> WithError(T data, ResultEnum resultEnum)
        {
            return new Result<T>(false, data, resultEnum);
        }

    }
}
