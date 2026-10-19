using System.ComponentModel;
using CP2_CSharp.Models;

namespace CP2_CSharp.Views
{
    /// <summary>
    /// Tela de lançamento de pedidos.
    /// </summary>
    public class VendaForm : Form
    {
        private readonly TextBox _txtCliente = new();
        private readonly ComboBox _cboProduto = new();
        private readonly NumericUpDown _nudQuantidade = new();
        private readonly DataGridView _gridItens = new();
        private readonly Label _lblTotal;
        private readonly BindingList<ItemPedido> _itens = new();

        public VendaForm()
        {
            Text = "Cafeteria Dev - Nova Venda";
            ClientSize = new Size(710, 530);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Tema.Fundo;

            // Cliente
            Controls.Add(Tema.CriarLabel("Cliente:", 30, 15, 200, 25, 11F));
            _txtCliente.SetBounds(30, 42, 380, 30);
            _txtCliente.Font = new Font("Segoe UI", 11F);
            Controls.Add(_txtCliente);

            // Produto e quantidade
            Controls.Add(Tema.CriarLabel("Produto:", 30, 85, 200, 25, 11F));
            _cboProduto.SetBounds(30, 112, 380, 30);
            _cboProduto.Font = new Font("Segoe UI", 11F);
            _cboProduto.DropDownStyle = ComboBoxStyle.DropDownList;
            _cboProduto.DataSource = Cardapio.Produtos;
            _cboProduto.DisplayMember = nameof(Produto.Descricao);
            Controls.Add(_cboProduto);

            Controls.Add(Tema.CriarLabel("Qtd:", 430, 85, 80, 25, 11F));
            _nudQuantidade.SetBounds(430, 112, 80, 30);
            _nudQuantidade.Font = new Font("Segoe UI", 11F);
            _nudQuantidade.Minimum = 1;
            _nudQuantidade.Maximum = 99;
            _nudQuantidade.Value = 1;
            Controls.Add(_nudQuantidade);

            Controls.Add(Tema.CriarBotao("Adicionar", 530, 105, 150, 40, BtnAdicionar_Click));

            // Itens do pedido
            Tema.EstilizarGrid(_gridItens);
            _gridItens.SetBounds(30, 165, 650, 235);
            _gridItens.Columns.Add(Tema.Coluna("Produto", nameof(ItemPedido.Nome)));
            _gridItens.Columns.Add(Tema.Coluna("Preço", nameof(ItemPedido.Preco), "C2"));
            _gridItens.Columns.Add(Tema.Coluna("Qtd", nameof(ItemPedido.Quantidade)));
            _gridItens.Columns.Add(Tema.Coluna("Subtotal", nameof(ItemPedido.Subtotal), "C2"));
            _gridItens.DataSource = _itens;
            Controls.Add(_gridItens);

            // Rodapé
            Controls.Add(Tema.CriarBotao("Remover item", 30, 412, 200, 40, BtnRemover_Click));
            _lblTotal = Tema.CriarLabel("Total: R$ 0,00", 300, 412, 380, 40,
                16F, FontStyle.Bold, ContentAlignment.MiddleRight);
            Controls.Add(_lblTotal);

            Controls.Add(Tema.CriarBotao("Finalizar venda", 30, 470, 320, 45, BtnFinalizar_Click));
            Controls.Add(Tema.CriarBotao("Cancelar", 360, 470, 320, 45, (_, _) => Close()));

            AtualizarTotal();
        }

        private void BtnAdicionar_Click(object? sender, EventArgs e)
        {
            if (_cboProduto.SelectedItem is not Produto produto)
            {
                return;
            }

            int quantidade = (int)_nudQuantidade.Value;

            // Se o produto já está no pedido, soma a quantidade
            var existente = _itens.FirstOrDefault(i => i.Nome == produto.Nome);
            if (existente != null)
            {
                quantidade += existente.Quantidade;
                _itens.Remove(existente);
            }

            _itens.Add(new ItemPedido(produto, quantidade));
            _nudQuantidade.Value = 1;
            AtualizarTotal();
        }

        private void BtnRemover_Click(object? sender, EventArgs e)
        {
            if (_gridItens.CurrentRow?.DataBoundItem is ItemPedido item)
            {
                _itens.Remove(item);
                AtualizarTotal();
            }
        }

        private void BtnFinalizar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtCliente.Text))
            {
                MessageBox.Show("Informe o nome do cliente.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtCliente.Focus();
                return;
            }

            if (_itens.Count == 0)
            {
                MessageBox.Show("Adicione pelo menos um item ao pedido.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var pedido = new Pedido
            {
                Cliente = _txtCliente.Text.Trim(),
                DataHora = DateTime.Now,
                Itens = _itens.ToList()
            };
            VendaRepository.Adicionar(pedido);

            MessageBox.Show($"Pedido #{pedido.Id} registrado!\nTotal: {pedido.Total:C2}", "Venda finalizada",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void AtualizarTotal()
        {
            _lblTotal.Text = $"Total: {_itens.Sum(i => i.Subtotal):C2}";
        }
    }
}
