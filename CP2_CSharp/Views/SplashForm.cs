namespace CP2_CSharp.Views;

/// <summary>
/// Tela de abertura da Cafeteria Dev.
/// Mostra o nome do sistema, uma barra de progresso e mensagens de carregamento.
/// Ao terminar, fecha com DialogResult.OK.
/// </summary>
public class SplashForm : Form
{
    private readonly System.Windows.Forms.Timer _timer = new() { Interval = 30 };
    private readonly Label _lblTitulo = new();
    private readonly Label _lblSubtitulo = new();
    private readonly Label _lblStatus = new();
    private readonly ProgressBar _progress = new();

    private readonly string[] _mensagens =
    {
        "Moendo os grãos...",
        "Aquecendo a água...",
        "Preparando o cardápio...",
        "Pronto!"
    };

    private static readonly Color CorFundo = Color.FromArgb(59, 36, 22);   // marrom café
    private static readonly Color CorDestaque = Color.FromArgb(212, 163, 115); // caramelo
    private static readonly Color CorTexto = Color.FromArgb(250, 237, 205);    // creme

    public SplashForm()
    {
        ConfigurarForm();
        ConfigurarControles();

        _timer.Tick += Timer_Tick;
        Shown += (_, _) => _timer.Start();
    }

    private void ConfigurarForm()
    {
        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(520, 300);
        BackColor = CorFundo;
        ShowInTaskbar = false;
        DoubleBuffered = true;
    }

    private void ConfigurarControles()
    {
        _lblTitulo.Text = "☕ Cafeteria Dev";
        _lblTitulo.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
        _lblTitulo.ForeColor = CorTexto;
        _lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
        _lblTitulo.SetBounds(0, 70, ClientSize.Width, 70);

        _lblSubtitulo.Text = "Código forte, café melhor ainda";
        _lblSubtitulo.Font = new Font("Segoe UI", 11F, FontStyle.Italic);
        _lblSubtitulo.ForeColor = CorDestaque;
        _lblSubtitulo.TextAlign = ContentAlignment.MiddleCenter;
        _lblSubtitulo.SetBounds(0, 140, ClientSize.Width, 30);

        _progress.Minimum = 0;
        _progress.Maximum = 100;
        _progress.Style = ProgressBarStyle.Continuous;
        _progress.SetBounds(60, 215, ClientSize.Width - 120, 12);

        _lblStatus.Text = _mensagens[0];
        _lblStatus.Font = new Font("Segoe UI", 9F);
        _lblStatus.ForeColor = CorTexto;
        _lblStatus.TextAlign = ContentAlignment.MiddleCenter;
        _lblStatus.SetBounds(0, 240, ClientSize.Width, 25);

        Controls.AddRange(new Control[] { _lblTitulo, _lblSubtitulo, _progress, _lblStatus });
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        _progress.Value = Math.Min(100, _progress.Value + 1);

        int indice = Math.Min(_mensagens.Length - 1, _progress.Value / 25);
        _lblStatus.Text = _mensagens[indice];

        if (_progress.Value >= 100)
        {
            _timer.Stop();
            DialogResult = DialogResult.OK; // fecha o ShowDialog()
        }
    }

    // Borda fina em volta da janela sem barra de título
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        using var pen = new Pen(CorDestaque, 2);
        e.Graphics.DrawRectangle(pen, 1, 1, Width - 3, Height - 3);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _timer.Dispose();
        }
        base.Dispose(disposing);
    }
}
