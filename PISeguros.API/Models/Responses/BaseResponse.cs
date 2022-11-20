namespace PISeguros.API.Models.Responses
{
    public class BaseResponse<T>
    {

        public BaseResponse(T result)
        {
            Resultado = result;
        }

        public T Resultado { get; }
    }
}
