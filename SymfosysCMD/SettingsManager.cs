using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymfosysCMD
{
    public partial class SettingsManager
    {
        private MainWindow mainWindow;

        public SettingsManager(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        class MySettings : AppSettings<MySettings>
        {
            public string documentRoot = null;
            public string SymfonyVersion = null;
            public int profileCount = 0;
            public string activeProfile = null;
            public string theme = null;
            public Dictionary<string, Profile> profileArray;
        }

        public string getSettingsDocumentRoot()
        {
            MySettings settings = MySettings.Load();
            return settings.documentRoot;
        }

        public void setSettingsDocumentRoot(string documentRoot)
        {
            MySettings settings = MySettings.Load();
            settings.documentRoot = documentRoot;
            settings.Save();
        }

        public string getSettingsSymfonyVersion()
        {
            MySettings settings = MySettings.Load();
            return settings.SymfonyVersion;
        }

        public void setSettingsSymfonyVersion(string symfonyVersion)
        {
            MySettings settings = MySettings.Load();
            settings.SymfonyVersion = symfonyVersion;
            settings.Save();
        }

        public string getSettingsTheme()
        {
            MySettings settings = MySettings.Load();
            if (string.IsNullOrEmpty(settings.theme))
                return "light";
            return settings.theme;
        }
        public void setSettingsTheme(string theme)
        {
            MySettings settings = MySettings.Load();
            settings.theme = theme;
            settings.Save();
        }

        public int getSettingsProfileCount()
        {
            MySettings settings = MySettings.Load();
            return settings.profileCount;
        }

        public void setSettingsProfileCount(int profileCount)
        {
            MySettings settings = MySettings.Load();
            settings.profileCount = profileCount;
            settings.Save();
        }

        public string getSettingsActiveProfile()
        {
            MySettings settings = MySettings.Load();
            return settings.activeProfile;
        }

        public void setSettingsActiveProfile(string activeProfile)
        {
            MySettings settings = MySettings.Load();
            settings.activeProfile = activeProfile;
            settings.Save();
        }

        public Profile getSettingsProfile(string profile)
        {
            MySettings settings = MySettings.Load();
            return settings.profileArray[profile];
            
        }

        public Dictionary<string, Profile> getSettingsProfiles()
        {
            MySettings settings = MySettings.Load();
            return settings.profileArray;
        }

        public void setSettingsProfiles(Dictionary<string, Profile> profiles)
        {
            MySettings settings = MySettings.Load();
            settings.profileArray = profiles;
            settings.Save();
        }
    }
}
