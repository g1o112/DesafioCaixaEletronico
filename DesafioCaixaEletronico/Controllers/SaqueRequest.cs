namespace DesafioCaixaEletronico.Models
{
    public class SaqueRequest
    {
        public int Valor { get; set; }
        public string? Senha { get; set; }
        public string? NumeroConta { get; set; }  // Adicione esta linha
    }
}
