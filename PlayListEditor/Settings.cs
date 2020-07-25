using System;
using System.Windows.Forms;

namespace PlayListEditor
{
    public static class Settings
    {
        public static string[] DefPLs = new[]
        {
            "Default.csv",
            "Sun.csv",
            "Mon.csv",
            "Tue.csv",
            "Wed.csv",
            "Thu.csv",
            "Fri.csv",
            "Sat.csv"
        };

        public static string[] AllowedExtensions = new[] { ".mp4", ".png" };

        public static string MediaFolder = String.Format(@"{0}\Media\", Application.StartupPath);
        public static string LocalPLFolder = String.Format(@"{0}\LocalPlaylists\", Application.StartupPath);

    }
}
