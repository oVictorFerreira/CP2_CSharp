namespace CP2_CSharp.Views
{
    /// <summary>
    /// Tela principal de navegação.
    /// </summary>
    public class MenuForm : Form
    {
        public MenuForm()
        {
            Text = "Cafeteria Dev - Menu";
            ClientSize = new Size(520, 420);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = Tema.Fundo;

            Controls.Add(Tema.CriarLabel("☕ Cafeteria Dev", 0, 40, ClientSize.Width, 60,
                26F, FontStyle.Bold, ContentAlignment.MiddleCenter));
            Controls.Add(Tema.CriarLabel("O que vamos fazer hoje?", 0, 100, ClientSize.Width, 30,
                11F, FontStyle.Italic, ContentAlignment.MiddleCenter));

            Controls.Add(Tema.CriarBotao("Nova Venda", 110, 160, 300, 50, BtnVenda_Click));
            Controls.Add(Tema.CriarBotao("Relatório de Vendas", 110, 230, 300, 50, BtnRelatorio_Click));
            Controls.Add(Tema.CriarBotao("Sair", 110, 300, 300, 50, (_, _) => Close()));
        }

        private void BtnVenda_Click(object? sender, EventArgs e)
        {
            using var tela = new VendaForm();
            tela.ShowDialog(this);
        }

        private void BtnRelatorio_Click(object? sender, EventArgs e)
        {
            using var tela = new RelatorioForm();
            tela.ShowDialog(this);
        }
    }
}
