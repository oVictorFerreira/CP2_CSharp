namespace CP2_CSharp.Models
{
    public class ItemPedido
    {
        public string Nome { get; }
        public decimal Preco { get; }
        public int Quantidade { get; }
        public decimal Subtotal => Preco * Quantidade;

        public ItemPedido(Produto produto, int quantidade)
        {
            Nome = produto.Nome;
            Preco = produto.Preco;
            Quantidade = quantidade;
        }
    }
}
