using Dynamics_Solution_Mover.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Dynamics_Solution_Mover.Dialogs
{
    /// <summary>
    /// Interaction logic for ProfileAuthDialog.xaml
    /// </summary>
    public partial class ProfileAuthDialog : Window
    {
        public ProfileAuthDialog(string authProfilesString)
        {
            InitializeComponent();            

            dataGrid.ItemsSource = AuthProfile.ParseAuthProfiles(authProfilesString);
        }

        private async void ConfirmSelected_Click(object sender, RoutedEventArgs e)
        {
            ShowSpinner();

            if (dataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show("No profile selected", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                HideSpinner();
                return;
            }

            AuthProfile selectedProfile = dataGrid.SelectedItems[0] as AuthProfile;

            if (selectedProfile.Active)
            {
                MessageBox.Show("Selected Profile is already active", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Information);
                HideSpinner();
                return;
            }

            await PacCommands.PacAuthSelectAsync(selectedProfile.Index);

            dataGrid.ItemsSource = AuthProfile.ParseAuthProfiles(await PacCommands.PacAuthListAsync());

            HideSpinner();
        }

        private async void AddNew_Click(object sender, RoutedEventArgs e)
        {            
            string output = await PacCommands.PacAuthCreateAsync();

            dataGrid.ItemsSource = AuthProfile.ParseAuthProfiles(await PacCommands.PacAuthListAsync());            
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
