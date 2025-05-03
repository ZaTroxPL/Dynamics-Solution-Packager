using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics_Solution_Mover.Model
{
    internal class AuthProfile(string index, string active, string kind, string name, string user, string cloud, string type, string environment, string environmentURL)
    {
        public string Index { get; set; } = index;
        public string Active { get; set; } = active;
        public string Kind { get; set; } = kind;
        public string Name { get; set; } = name;
        public string User { get; set; } = user;
        public string Cloud { get; set; } = cloud;
        public string Type { get; set; } = type;
        public string Environment { get; set; } = environment;
        public string EnvironmentURL { get; set; } = environmentURL;
    }
}
