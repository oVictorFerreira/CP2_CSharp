using CP2_CSharp.Models;

namespace CP2_CSharp.Views
{
    /// <summary>
    /// Tela de listagem e visualização das vendas realizadas.
    /// </summary>
    public class RelatorioForm : Form
    {
        // Linha exibida na grade de pedidos
        private sealed record LinhaPedido(int Id, string Data, string Cliente, int QtdItens, decimal Total);

        private readonly DataGridView _gridPedidos = new();
        private readonly DataGridView _gridItens = new();
        private readonly Label _lblResumo;

        public RelatorioForm()
        {
            Text = "Cafeteria Dev - Relatório de Vendas";
            ClientSize = new Size(800, 560);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Tema.Fundo;

            Controls.Add(Tema.CriarLabel("Relatório de Vendas", 30, 15, 740, 40, 18F, FontStyle.Bold));

            // Pedidos
            Tema.EstilizarGrid(_gridPedidos);
            _gridPedidos.SetBounds(30, 65, 740, 220);
            _gridPedidos.Columns.Add(Tema.Coluna("Nº", nameof(LinhaPedido.Id)));
            _gridPedidos.Columns.Add(Tema.Coluna("Data/Hora", nameof(LinhaPedido.Data)));
            _gridPedidos.Columns.Add(Tema.Coluna("Cliente", nameof(LinhaPedido.Cliente)));
            _gridPedidos.Columns.Add(Tema.Coluna("Itens", nameof(LinhaPedido.QtdItens)));
            _gridPedidos.Columns.Add(Tema.Coluna("Total", nameof(LinhaPedido.Total), "C2"));
            Controls.Add(_gridPedidos);

            // Itens do pedido selecionado
            Controls.Add(Tema.CriarLabel("Itens do pedido selecionado:", 30, 295, 400, 25, 11F, FontStyle.Bold));
            Tema.EstilizarGrid(_gridItens);
            _gridItens.SetBounds(30, 325, 740, 135);
            _gridItens.Columns.Add(Tema.Coluna("Produto", nameof(ItemPedido.Nome)));
            _gridItens.Columns.Add(Tema.Coluna("Preço", nameof(ItemPedido.Preco), "C2"));
            _gridItens.Columns.Add(Tema.Coluna("Qtd", nameof(ItemPedido.Quantidade)));
            _gridItens.Columns.Add(Tema.Coluna("Subtotal", nameof(ItemPedido.Subtotal), "C2"));
            Controls.Add(_gridItens);

            // Rodapé
            _lblResumo = Tema.CriarLabel("", 30, 480, 540, 40, 12F, FontStyle.Bold);
            Controls.Add(_lblResumo);
            Controls.Add(Tema.CriarBotao("Fechar", 590, 480, 180, 40, (_, _) => Close()));

            CarregarPedidos();
            _gridPedidos.SelectionChanged += (_, _) => MostrarItens();
            MostrarItens();
        }

        private void CarregarPedidos()
        {
            var pedidos = VendaRepository.Pedidos;

            _gridPedidos.DataSource = pedidos
                .Select(p => new LinhaPedido(
                    p.Id,
                    p.DataHora.ToString("dd/MM/yyyy HH:mm"),
                    p.Cliente,
                    p.Itens.Sum(i => i.Quantidade),
                    p.Total))
                .ToList();

            decimal faturamento = pedidos.Sum(p => p.Total);
            _lblResumo.Text = pedidos.Count == 0
                ? "Nenhuma venda registrada ainda."
                : $"Pedidos: {pedidos.Count}   |   Faturamento: {faturamento:C2}";
        }

        private void MostrarItens()
        {
            if (_gridPedidos.CurrentRow?.DataBoundItem is LinhaPedido linha)
            {
                var pedido = VendaRepository.Pedidos.First(p => p.Id == linha.Id);
                _gridItens.DataSource = pedido.Itens.ToList();
            }
            else
            {
                _gridItens.DataSource = null;
            }
        }
    }
}
