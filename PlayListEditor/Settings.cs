using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayListEditor
{
    public static class Settings
    {
        public static string[] DefPLs = new[]
        {
            "Source.csv",
            "Default.csv",
            "Mon.csv",
            "Tue.csv",
            "Wed.csv",
            "Thu.csv",
            "Fri.csv",
            "Sat.csv",
            "Sun.csv",
        };

        public static string[] AllowedExtensions = new[] { ".mp4", ".png" };
    }
}
