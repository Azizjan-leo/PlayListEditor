using System;
using System.IO;
using System.Windows.Forms;

namespace PlayListEditor
{
    public static class Settings
    {
        public static string[] DefPLs() 
        { 
            if (!Directory.Exists(mediaFolder))
            {
                Directory.CreateDirectory(mediaFolder);
            }
            // Checking if all default files are there
            foreach (var item in defPLs)
            {
                string fileName = LocalPLFolder + item;

                if (!File.Exists(fileName))
                {
                    var fs = File.Create(fileName);
                    fs.Close();
                }
            }
            return defPLs;
        }

        private static readonly string[] defPLs = new[]
        {
            "Sun.csv",
            "Mon.csv",
            "Tue.csv",
            "Wed.csv",
            "Thu.csv",
            "Fri.csv",
            "Sat.csv",
            "Default.csv"
        };

        public static readonly string[] AllowedExtensions = new[] { ".mp4", ".png" };

        public static string MediaFolder
        {
            get
            {
                if (!Directory.Exists(mediaFolder))
                {
                    Directory.CreateDirectory(mediaFolder);
                }
                return mediaFolder;
            } 
        }
        public static string LocalPLFolder 
        {
            get
            {
                if (!Directory.Exists(localPLFolder))
                {
                    Directory.CreateDirectory(localPLFolder);
                }
                return localPLFolder;
            }    
        }

        public static string RemotePLFolder
        {
            get
            {
                if (!Directory.Exists(remotePLFolder))
                {
                    Directory.CreateDirectory(remotePLFolder);
                }
                return remotePLFolder;
            }
        }

        private static readonly string remotePLFolder = String.Format(@"{0}\RemotePlaylists\", Application.StartupPath);
        private static readonly string mediaFolder = String.Format(@"{0}\Media\", Application.StartupPath);
        private static readonly string localPLFolder = String.Format(@"{0}\LocalPlaylists\", Application.StartupPath);

    }
}
