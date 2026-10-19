namespace CP2_CSharp.Models
{
    public record Produto(string Nome, decimal Preco)
    {
        // Usado no ComboBox da tela de venda
        public string Descricao => $"{Nome} - {Preco:C2}";
    }
}
