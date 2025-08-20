using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace EvernoteClone
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IConfiguration Configuration { get; private set; }

        public static string UserID = string.Empty;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())   // шлях до exe
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

            // Тест: виведе ключ у Debug
            string key = Configuration["AzureSpeech:Key"];
            string region = Configuration["AzureSpeech:Region"];
            System.Diagnostics.Debug.WriteLine($"Azure Key: {key}, Region: {region}");
        }
    }

}
