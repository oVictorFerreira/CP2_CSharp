namespace CP2_CSharp.Views
{
    /// <summary>
    /// Cores e helpers visuais compartilhados pelas telas.
    /// </summary>
    internal static class Tema
    {
        public static readonly Color Fundo = Color.FromArgb(59, 36, 22);
        public static readonly Color Painel = Color.FromArgb(84, 54, 36);
        public static readonly Color Destaque = Color.FromArgb(212, 163, 115);
        public static readonly Color Texto = Color.FromArgb(250, 237, 205);

        public static Button CriarBotao(string texto, int x, int y, int largura, int altura, EventHandler aoClicar)
        {
            var botao = new Button
            {
                Text = texto,
                FlatStyle = FlatStyle.Flat,
                BackColor = Destaque,
                ForeColor = Fundo,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            botao.FlatAppearance.BorderSize = 0;
            botao.SetBounds(x, y, largura, altura);
            botao.Click += aoClicar;
            return botao;
        }

        public static Label CriarLabel(string texto, int x, int y, int largura, int altura,
            float tamanho = 10F, FontStyle estilo = FontStyle.Regular,
            ContentAlignment alinhamento = ContentAlignment.MiddleLeft)
        {
            var label = new Label
            {
                Text = texto,
                ForeColor = Texto,
                Font = new Font("Segoe UI", tamanho, estilo),
                TextAlign = alinhamento
            };
            label.SetBounds(x, y, largura, altura);
            return label;
        }

        public static void EstilizarGrid(DataGridView grid)
        {
            grid.BackgroundColor = Painel;
            grid.BorderStyle = BorderStyle.None;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Destaque;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Fundo;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grid.DefaultCellStyle.BackColor = Texto;
            grid.DefaultCellStyle.ForeColor = Fundo;
            grid.DefaultCellStyle.SelectionBackColor = Destaque;
            grid.DefaultCellStyle.SelectionForeColor = Fundo;
            grid.RowHeadersVisible = false;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.MultiSelect = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoGenerateColumns = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public static DataGridViewTextBoxColumn Coluna(string titulo, string propriedade, string? formato = null)
        {
            var coluna = new DataGridViewTextBoxColumn
            {
                HeaderText = titulo,
                DataPropertyName = propriedade
            };
            if (formato != null)
            {
                coluna.DefaultCellStyle.Format = formato;
            }
            return coluna;
        }
    }
}
