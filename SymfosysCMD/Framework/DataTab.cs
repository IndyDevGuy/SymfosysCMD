using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymfosysCMD.Framework
{
    public class DataTab
    {
        private string _tabName;
        private string _tabIndex;
        private string _command;

        public string tabName { get { return _tabName; } set { _tabName = value; } }

        public string tabIndex { get { return _tabIndex; } set { _tabIndex = value; } }

        public string command { get { return _command; } set { _command = value; } }
    }
}
