using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SymfosysCMD.Settings
{
    public static class StaticSettings
    {
        public static string SettingsFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\settings.json";
    }
}
