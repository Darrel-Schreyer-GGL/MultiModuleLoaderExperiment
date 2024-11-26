using Plugin.Abstractions;
using System.Text;
using System.Windows;

namespace WpfAppClient
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void SetPlugins(List<IPlugin> plugins)
        {
            StringBuilder output = new StringBuilder();
            foreach (var plugin in plugins)
            {
                try
                {
                    var version = plugin.Initialize();  // This will load and possibly log the Newtonsoft.Json version
                    output.AppendLine($"Initializing {plugin.Name}: Version: {version}");
                }
                catch (Exception ex)
                {
                    output.AppendLine($"Error initializing {plugin.Name}: {ex.Message}");
                }
            }
            txt.Text = output.ToString();
        }
    }
}