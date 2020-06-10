using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymfosysCMD
{
    public class Profile
    {
        private string _name;
        private string _documentRoot;
        private string _symfonyVersion;
        private bool _active;
        private int _profileNumber;
        private MainWindow mainWindow;
        private IDictionary<int, SymfosysConsole> symfosysConsoles = new Dictionary<int, SymfosysConsole>();

        public string profileIndex;

        public Profile(MainWindow mainWindow, string profileIndex)
        {
            this.mainWindow = mainWindow;
            this.profileIndex = profileIndex;
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

        public bool setActive(bool active)
        {
            this._active = active;
            return this._active;
        }

        public bool getActive()
        {
            return this._active;
        }

        public int setProfileNumber(int profileNumber)
        {
            this._profileNumber = profileNumber;
            return this._profileNumber;
        }

        public int getProfileNumber()
        {
            return this._profileNumber;
        }

        public IDictionary<int, SymfosysConsole> getSymfosysConsoles()
        {
            return this.symfosysConsoles;
        }

        public SymfosysConsole GetSymfosysConsoleByID(int id)
        {
            SymfosysConsole console = new SymfosysConsole(this.mainWindow,this,this.symfosysConsoles.Count + 1,"");
            foreach(KeyValuePair<int,SymfosysConsole> item in this.symfosysConsoles)
            {
                if (item.Key == id)
                    console = item.Value;
            }
            return console;
        }

        public bool addSymfosysConsole(int index, SymfosysConsole console)
        {
            bool found = false;
            foreach (KeyValuePair<int, SymfosysConsole> item in this.symfosysConsoles)
            {
                if (item.Key == index)
                    found = true;
            }
            if(!found)
            {
                this.symfosysConsoles.Add(index,console);
                return true;
            }
            return false;
        }

        public bool removeSymfosysConsole(int index)
        {
            bool found = false;
            foreach (KeyValuePair<int, SymfosysConsole> item in this.symfosysConsoles)
            {
                if (item.Key == index)
                    found = true;
            }
            if(found)
            {
                this.symfosysConsoles.Remove(index);
                return true;
            }
            return false;
        }
    }
}
