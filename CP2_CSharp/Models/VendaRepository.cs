namespace CP2_CSharp.Models
{
    /// <summary>
    /// Guarda os pedidos em memória, compartilhado entre a tela de Venda e a de Relatório.
    /// </summary>
    public static class VendaRepository
    {
        private static readonly List<Pedido> _pedidos = new();

        public static IReadOnlyList<Pedido> Pedidos => _pedidos;

        public static void Adicionar(Pedido pedido)
        {
            pedido.Id = _pedidos.Count + 1;
            _pedidos.Add(pedido);
        }
    }
}
