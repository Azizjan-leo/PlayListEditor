using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using PlayListEditor.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace PlayListEditor
{
    public struct MediaItem
    {
        public string Name { get; set; }
        public TimeSpan Length { get; set; }

        public MediaItem(string name, TimeSpan length) : this()
        {
            Name = name;
            Length = length;
        }

    }
    public partial class Form1 : Form
    {
        
      
        readonly List<MediaItem> Media;

        public Form1()
        {
            Media = new List<MediaItem>();
            InitializeComponent();
        }

        private void LoadMedia()
        {
            var files = Directory
             .GetFiles(string.Format(@"{0}\Media\", Application.StartupPath))
             .Where(file => Settings.AllowedExtensions.Any(file.ToLower().EndsWith))
             .ToList();

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
                Media.Add(new MediaItem(
                    Path.GetFileNameWithoutExtension(file), 
                    duration
                    ));
            }
        }
        private async Task LoadPlaylists()
        {
            foreach (var item in Settings.DefPLs)
            {
                string fileName = String.Format(@"{0}\LocalPlaylists\{1}", Application.StartupPath, item);

                if (File.Exists(fileName))
                {

                }
            }

            sb.AppendLine(String.Format(@"{0},{1}", Path.GetFileNameWithoutExtension(file), duration.ToString(@"hh\:mm\:ss")));

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadMedia();
            Task.Run(() => LoadPlaylists());

           
            var sb = new StringBuilder();
            
           
            File.AppendAllText(fileName, sb.ToString());

            using (var reader = new StreamReader(String.Format(@"{0}\LocalPlaylists\Source.csv", Application.StartupPath)))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    AllMediaListView.Items.Add(new ListViewItem(values));
                }
            }
        }
    }
}
