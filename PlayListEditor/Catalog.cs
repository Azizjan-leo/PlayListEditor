using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PlayListEditor
{
    public class MediaItem
    {
        public string Name { get; set; }
        public string Length { get; set; }

        public MediaItem(string name, TimeSpan length)
        {
            Name = name;
            Length = length.ToString(@"hh\:mm\:ss");
        }

        public MediaItem(string name, string length)
        {
            Name = name;
            Length = length;
        }

        public string ToCSVLine()
        {
            return Name + "," + Length;
        }

    }

    public class PlayList
    {
        public string Name { get; set; }
        public List<MediaItem> Items { get; set; }
        public TimeSpan Duration 
        {
            get
            {
                TimeSpan tmp = default;
                foreach (var item in Items)
                {
                    tmp += TimeSpan.Parse(item.Length);
                }
                return tmp;
            } 
        }
        public PlayList(string name)
        {
            Name = name;
            Items = new List<MediaItem>();
        }
    }
    public static class Catalog
    {
        public static TimeSpan Duration { 
            get
            {
                TimeSpan tmp = default;
                foreach (var list in Lists)
                {
                    tmp += list.Duration;
                }
                return tmp;
            }
        }
        public static List<PlayList> Lists = new List<PlayList>();
        private static PlayList source = new PlayList("Source");

        public static PlayList Source 
        {
            get { return source; }
            set
            {
                source.Items.Clear();
                var files = Directory
                 .GetFiles(Settings.MediaFolder)
                 .Where(file => Settings.AllowedExtensions.Any(file.ToLower().EndsWith))
                 .ToList();
                TimeSpan totalDuration = default;
                foreach (var file in files)
                {
                    var duration = TimeSpan.FromSeconds(15);
                    if (Path.GetExtension(file) == ".mp4")
                    {
                        using (var shell = ShellObject.FromParsingName(file))
                        {
                            IShellProperty prop = shell.Properties.System.Media.Duration;
                            var t = (ulong)prop.ValueAsObject;
                            duration = TimeSpan.FromTicks((long)t);
                        }
                    }
                   source.Items.Add(new MediaItem(
                   Path.GetFileNameWithoutExtension(file),
                   duration
                   ));
                    totalDuration += duration;
                }
                // Write to Source.csv
                var sourceFile = Application.StartupPath + "\\Source.csv";
                var sb = new StringBuilder();
                foreach (var item in source.Items)
                {
                    sb.AppendLine(item.ToCSVLine());
                }
                File.WriteAllText(sourceFile, sb.ToString());
            }
        }

    }
}
