using Dynamics_Solution_Mover.Model;
using System;
using System.Collections.Generic;
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

        List<AuthProfile> AuthProfiles;

        public ProfileAuthDialog(string authProfilesString)
        {
            InitializeComponent();

            List<string> rows = authProfilesString.Split("\r\n").ToList();
            List<string> headerSplit = Regex.Matches(rows[0], @"\w+\s+(?:Url)?").Cast<Match>().Select(m => m.Value).ToList();
            rows.RemoveAt(0);

            Dictionary<string, int> parseMap = new()
            {
                { "IndexLength", headerSplit[0].Length },
                { "ActiveLength", headerSplit[1].Length },
                { "KindLength", headerSplit[2].Length },
                { "NameLength", headerSplit[3].Length },
                { "UserLength", headerSplit[4].Length },
                { "CloudLength", headerSplit[5].Length },
                { "TypeLength", headerSplit[6].Length },
                { "EnvironmentLength", headerSplit[7].Length }
            };

            AuthProfiles = [];

            int i = 0;
            string row = rows[i];
            while (row != "")
            {
                string index = row.Substring(0, parseMap["IndexLength"]).Trim();
                string active = row.Substring(parseMap.Values.Take(1).Sum(), parseMap["ActiveLength"]).Trim();
                string kind = row.Substring(parseMap.Values.Take(2).Sum(), parseMap["KindLength"]).Trim();
                string name = row.Substring(parseMap.Values.Take(3).Sum(), parseMap["NameLength"]).Trim();
                string user = row.Substring(parseMap.Values.Take(4).Sum(), parseMap["UserLength"]).Trim();
                string cloud = row.Substring(parseMap.Values.Take(5).Sum(), parseMap["CloudLength"]).Trim();
                string type = row.Substring(parseMap.Values.Take(6).Sum(), parseMap["TypeLength"]).Trim();
                string environment = row.Substring(parseMap.Values.Take(7).Sum(), parseMap["EnvironmentLength"]).Trim();
                string environmentUrl = row.Substring(parseMap.Values.Take(8).Sum());
                AuthProfile authProfile = new AuthProfile(index, active, kind, name, user, cloud, type, environment, environmentUrl);
                AuthProfiles.Add(authProfile);
                row = rows[i+1];
            }

            dataGrid.ItemsSource = AuthProfiles;
        }
    }
}
