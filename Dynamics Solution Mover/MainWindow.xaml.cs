using Dynamics_Solution_Mover.Dialogs;
using Dynamics_Solution_Mover.Model;
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

            ShowSpinner();
            CheckIfPacExists();
            CheckCurrentUser();            
        }

        private async void CheckIfPacExists()
        {            
            try
            {
                string output = await PacCommands.CheckIfPacAsync();

                Output.AppendText("Welcome to the Program :)\r\n");
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

        private async void CheckCurrentUser()
        {
            var users = AuthProfile.ParseAuthProfiles(await PacCommands.PacAuthListAsync());
            var user = users.Where(user => user.Active).FirstOrDefault();

            if (user == null)
            {
                HideSpinner();
                return;
            }

            if (user.TokenStatus(await PacCommands.PacAuthWhoAsync()))
            {
                MessageBox.Show("Current Profile has expired, please login in again", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                HideSpinner();
                return;
            }

            SetCurrentUserDetails(user);
            HideSpinner();
        }

        internal void SetCurrentUserDetails(AuthProfile authProfile)
        {
            string userName = authProfile.User ?? string.Empty;
            string envUrl = authProfile.EnvironmentURL ?? string.Empty;
            CurrentUser.Content = $"Profile: {userName}";
            EnvUrl.Content = envUrl;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
                      
        }

        private async void PACInstaller_Click(object sender, RoutedEventArgs e)
        {                   
            ShowSpinner();
            string output = await PacCommands.InstallPacAsync();            

            Output.AppendText(output);
            HideSpinner();
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {            
            ShowSpinner();
            try
            {
                string output = await PacCommands.PacAuthListAsync();                
                

                if (output == "No profiles were found on this computer. Please run 'pac auth create' to create one.\r\n")
                {
                    Output.AppendText("No authentication profiles were found on this computer, creating new profile.\r\n");

                    output = await PacCommands.PacAuthCreateAsync();

                    Output.AppendText(output);
                }
                else if (output.StartsWith("Index"))
                {
                    HideSpinner();
                    ProfileAuthDialog profileAuthDialog = new(output)
                    {
                        Owner = this,                        
                    };
                    profileAuthDialog.ShowDialog();                    
                }

            }
            catch (ArgumentNullException ex)
            {
                Output.AppendText(ex.Message);
            }
            catch (Exception ex) 
            {
                Output.AppendText(ex.Message);
            }

            HideSpinner();
        }

        private void ShowSpinner()
        {
            SpinnerBackground.Visibility = Visibility.Visible;
            Spinner.Spin = true;
            Spinner.Visibility = Visibility.Visible;
        }

        private void HideSpinner()
        {
            SpinnerBackground.Visibility = Visibility.Collapsed;
            Spinner.Spin = false;
            Spinner.Visibility = Visibility.Collapsed;
        }
    }
}