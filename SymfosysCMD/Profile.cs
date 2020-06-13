
namespace SymfosysCMD
{
    public class Profile
    {
        private string _name;
        private string _documentRoot;
        private string _symfonyVersion;

        public Profile()
        {
        }
        public string name { get { return this._name; } set { this._name = value; } }
        public string documentRoot { get { return this._documentRoot; } set { this._documentRoot = value;  } }
        public string symfonyVersion { get { return this._symfonyVersion; } set { this._symfonyVersion = value; } }

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

    }
}
