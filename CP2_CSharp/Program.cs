using CP2_CSharp.Views;

namespace CP2_CSharp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // 1) Mostra o Splash e aguarda ele terminar
            using (var splash = new SplashForm())
            {
                splash.ShowDialog();
            }

            // 2) Abre a tela de Menu (Views/MenuForm.cs, feita por outro integrante)
            // Enquanto o Menu não existir, pode usar o Form1 para testar.
            Application.Run(new Form1());
        }
    }
}