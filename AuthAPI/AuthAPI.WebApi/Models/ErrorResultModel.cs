namespace AuthAPI.WebApi.Models
{
    public class ErrorResultModel
    {
        public ErrorResultModel()
        {
            
        }

        public ErrorResultModel(int statusCode, string mensagem)
        {
            StatusCode = statusCode;
            Mensagem = mensagem;
        }

        public int StatusCode { get; set; }
        public string Mensagem { get; set; }
    }
}