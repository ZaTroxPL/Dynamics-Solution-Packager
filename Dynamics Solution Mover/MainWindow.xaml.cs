using Dynamics_Solution_Mover.Dialogs;
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
            try
            {
                ProcessStartInfo checkIfPacExissts = new()
                {
                    FileName = "pac",
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                };

                var proc = Process.Start(checkIfPacExissts);
                ArgumentNullException.ThrowIfNull(proc);

                string output = proc.StandardOutput.ReadToEnd();
                Output.AppendText("Welcome to the Program :)\r\n");
                proc.WaitForExit();

            }
            catch (Win32Exception ex)
            {
                if (ex.Message.StartsWith("An error occurred trying to start process 'pac'"))
                {
                    PACInstaller.Visibility = Visibility.Visible;
                    Output.AppendText("Pac command not found, please install Power Platform CLI tools.\r\n");
                }
            }            
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
                      
        }

        private void PACInstaller_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo startInfo = new()
            {
                FileName = "dotnet",
                Arguments = "tool install --global Microsoft.PowerApps.CLI.Tool",
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };

            var proc = Process.Start(startInfo);

            ArgumentNullException.ThrowIfNull(proc);

            string output = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();

            Output.AppendText(output);
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo authList = new()
            {
                FileName = "pac",
                Arguments = "auth list",
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };

            try
            {
                Process? proc;
                string output;

                proc = Process.Start(authList);
                ArgumentNullException.ThrowIfNull(proc);

                output = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();

                if (output == "No profiles were found on this computer. Please run 'pac auth create' to create one.\r\n")
                {
                    Output.AppendText("No authentication profiles were found on this computer, creating new profile.\r\n");

                    ProcessStartInfo authCreate = new()
                    {
                        FileName = "pac",
                        Arguments = "auth create",
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false
                    };

                    proc = Process.Start(authCreate);
                    ArgumentNullException.ThrowIfNull(proc);

                    output = proc.StandardOutput.ReadToEnd();
                    proc.WaitForExit();
                }
                else if (output.StartsWith("Index"))
                {
                    ProfileAuthDialog profileAuthDialog = new(output)
                    {
                        Owner = this
                    };
                    profileAuthDialog.ShowDialog();
                }

                    Output.AppendText(output);
            }
            catch (ArgumentNullException ex)
            {
                Output.AppendText(ex.Message);
            }
        }
    }
}