using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dynamics_Solution_Mover
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();            
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo startInfo = new()
            {
                FileName = "pac",                
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            try
            {
                var proc = Process.Start(startInfo);

                ArgumentNullException.ThrowIfNull(proc);

                string output = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();

                Output.Text = output;
            }
            catch (Win32Exception ex)
            {
                PACInstaller.Visibility = Visibility.Visible;
                Output.Text = ex.Message;
            }
            catch (ArgumentNullException ex) 
            {
                Output.Text = ex.Message;   
            }            
        }

        private void PACInstaller_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo startInfo = new()
            {
                FileName = "dotnet",
                Arguments = "tool install --global Microsoft.PowerApps.CLI.Tool",
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            var proc = Process.Start(startInfo);

            ArgumentNullException.ThrowIfNull(proc);

            string output = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();

            Output.Text = output;
        }
    }
}