namespace PISeguros.API.Models.Responses
{
    public class ErroResponse
    {
        public ErroResponse(string mensagem)
        {
            Erros = new List<string> { mensagem };
        }

        public ErroResponse(IEnumerable<string> mensagens)
        {
            Erros = mensagens.ToList();
        }

        public List<string> Erros { get; set; }
    }
}
