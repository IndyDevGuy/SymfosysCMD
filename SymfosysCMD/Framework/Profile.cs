
using SymfosysCMD.Console;
using System.Collections.Generic;

namespace SymfosysCMD.Framework
{
    public class Profile
    {
        private string _name;
        private string _documentRoot;
        private string _symfonyVersion;
        private Dictionary<string, DataTab> _tabs;

        public Profile()
        {
            this._tabs = new Dictionary<string, DataTab>();
        }
        public string name { get { return this._name; } set { this._name = value; } }
        public string documentRoot { get { return this._documentRoot; } set { this._documentRoot = value;  } }
        public string symfonyVersion { get { return this._symfonyVersion; } set { this._symfonyVersion = value; } }
        public Dictionary<string, DataTab> tabs { get { return _tabs; } set { _tabs = value; } }
        public override string ToString()
        {
            return name;
        }

        public string setName(string name)
        {
            this._name = name;
            return this._name;
        }

        public string getName()
        {
            return this._name;
        }

        public string setDocumentRoot(string documentRoot)
        {
            this._documentRoot = documentRoot;
            return this._documentRoot;
        }

        public string getDocumentRoot()
        {
            return this._documentRoot;
        }

        public string setSymfonyVersion(string symfonyVersion)
        {
            this._symfonyVersion = symfonyVersion;
            return this._symfonyVersion;
        }

        public string getSymfonyVersion()
        {
            return this._symfonyVersion;
        }

        public Dictionary<string, DataTab> setTabs(Dictionary<string, DataTab> tabs)
        {
            this._tabs = tabs;
            return this._tabs;
        }

        public Dictionary<string, DataTab> getTabs()
        {
            return this._tabs;
        }

        public void addTab(DataTab tab)
        {
            if(!this._tabs.ContainsKey(tab.tabIndex))
            {
                this._tabs.Add(tab.tabIndex, tab);
            }
        }

        public void removeTab(string tabIndex)
        {
            if(this._tabs.ContainsKey(tabIndex))
            {
                this._tabs.Remove(tabIndex);
            }
        }

    }
}
