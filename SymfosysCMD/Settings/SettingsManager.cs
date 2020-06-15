using SymfosysCMD.Framework;
using System.Collections.Generic;

namespace SymfosysCMD.Settings
{
    public partial class SettingsManager
    {

        public SettingsManager()
        {
        }

        class MySettings : AppSettings<MySettings>
        {
            public string activeProfile = null;
            public string theme = null;
            public Dictionary<string, Profile> profileArray;
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
            if (settings.profileArray != null)
            {
                if(settings.profileArray.ContainsKey(profile))
                return settings.profileArray[profile];
            }
            Profile newProfile = new Profile();
            newProfile.setName(profile);
            return newProfile;
        }

        public Dictionary<string, Profile> getSettingsProfiles()
        {
            MySettings settings = MySettings.Load();
            if (settings.profileArray != null)
                return settings.profileArray;
            Dictionary<string, Profile> profileArray = new Dictionary<string, Profile>();
            settings.profileArray = profileArray;
            settings.Save();
            return settings.profileArray;
        }

        public void setSettingsProfiles(Dictionary<string, Profile> profiles)
        {
            MySettings settings = MySettings.Load();
            settings.profileArray = profiles;
            settings.Save();
        }

        public void addSettingsProfile(Profile profile)
        {
            MySettings settings = MySettings.Load();
            if (settings.profileArray != null)
            {
                if (!settings.profileArray.ContainsKey(profile.getName()))
                {
                    settings.profileArray.Add(profile.getName(), profile);
                    settings.Save();
                }
            }
        }

        public void updateSettingsProfile(Profile profile, string originalProfileName)
        {
            MySettings settings = MySettings.Load();
            if(settings.profileArray != null)
            {
                if(settings.profileArray.ContainsKey(originalProfileName))
                {
                    settings.profileArray.Remove(originalProfileName);
                    settings.profileArray.Add(profile.getName(),profile);
                    settings.Save();
                }
            }
        }
    }
}
