using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dynamics_Solution_Mover.Model
{
    internal class AuthProfile(int index, bool active, string kind, string name, string user, string cloud, string type, string environment, string environmentURL)
    {
        public int Index { get; set; } = index;
        public bool Active { get; set; } = active;
        public string Kind { get; set; } = kind;
        public string Name { get; set; } = name;
        public string User { get; set; } = user;
        public string Cloud { get; set; } = cloud;
        public string Type { get; set; } = type;
        public string Environment { get; set; } = environment;
        public string EnvironmentURL { get; set; } = environmentURL;

        public static List<AuthProfile> ParseAuthProfiles(string output)
        {
            List<string> rows = output.Split("\r\n").ToList();
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

            List<AuthProfile> authProfiles = [];

            int i = 0;
            string row = rows[i];
            while (row != "")
            {
                string indexString = row.Substring(0, parseMap["IndexLength"]).Trim();
                int index = int.Parse(indexString.Replace("[", "").Replace("]", ""));
                string activeString = row.Substring(parseMap.Values.Take(1).Sum(), parseMap["ActiveLength"]).Trim();                               
                bool active = !string.IsNullOrWhiteSpace(activeString);
                string kind = row.Substring(parseMap.Values.Take(2).Sum(), parseMap["KindLength"]).Trim();
                string name = row.Substring(parseMap.Values.Take(3).Sum(), parseMap["NameLength"]).Trim();
                string user = row.Substring(parseMap.Values.Take(4).Sum(), parseMap["UserLength"]).Trim();
                string cloud = row.Substring(parseMap.Values.Take(5).Sum(), parseMap["CloudLength"]).Trim();
                string type = row.Substring(parseMap.Values.Take(6).Sum(), parseMap["TypeLength"]).Trim();
                string environment = row.Substring(parseMap.Values.Take(7).Sum(), parseMap["EnvironmentLength"]).Trim();
                string environmentUrl = row.Substring(parseMap.Values.Take(8).Sum());
                AuthProfile authProfile = new AuthProfile(index, active, kind, name, user, cloud, type, environment, environmentUrl);
                authProfiles.Add(authProfile);
                row = rows[i + 1];
            }

            return authProfiles;
        }
    }
}
