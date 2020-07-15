using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SymfosysCMD.Settings
{
    public class AppSettings<T> where T : new()
    {

        public void Save()
        {
            File.WriteAllText(StaticSettings.SettingsFilePath, (JsonConvert.SerializeObject(this)));
        }

        public static void Save(T pSettings)
        {
            File.WriteAllText(StaticSettings.SettingsFilePath, (JsonConvert.SerializeObject(pSettings)));
        }

        public static T Load()
        {
            T t = new T();
            if (File.Exists(StaticSettings.SettingsFilePath))
                t = (JsonConvert.DeserializeObject<T>(File.ReadAllText(StaticSettings.SettingsFilePath)));
            return t;
        }
    }
}
