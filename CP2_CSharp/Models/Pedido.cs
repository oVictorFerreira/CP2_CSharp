namespace CP2_CSharp.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; } = DateTime.Now;
        public string Cliente { get; set; } = string.Empty;
        public List<ItemPedido> Itens { get; set; } = new();
        public decimal Total => Itens.Sum(i => i.Subtotal);
    }
}
