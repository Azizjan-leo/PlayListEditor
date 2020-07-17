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
using Microsoft.VisualBasic;

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

        public string ToCSVLine()
        {
            return Name + "," + Length.ToString(@"hh\:mm\:ss");
        }

    }
    public partial class Form1 : Form
    {
        readonly List<MediaItem> Media;
        readonly List<string> Files;
        private List<string> LocalPLs;
        
        public Form1()
        {
            Media = new List<MediaItem>();
            Files = new List<string>();
            LocalPLs = new List<string>();
            ContextMenu contextMenu = new ContextMenu();
            contextMenu.MenuItems.Add(new MenuItem("Create playlist", CreatePL));
          
            InitializeComponent();
            LocalPLsLB.ContextMenu = contextMenu;
        }

        /// <summary>
        /// Loads all local files to Media list
        /// </summary>
        private void LoadMedia()
        {
            var files = Directory
             .GetFiles(Settings.MediaFolder)
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
        private void CreatePL(object sender, EventArgs e) 
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Enter playlist's name",
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = "Playlist" };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (s, a) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            if(prompt.ShowDialog() == DialogResult.OK)
            {
                LocalPLsLB.Items.Add(textBox.Text);
            }
                

        }
        private void WriteToFiles()
        {
            // Write to Source.csv
            var file = Application.StartupPath + "\\Source.csv";
            var sb = new StringBuilder();
            foreach (var item in Media)
            {                
                sb.AppendLine(item.ToCSVLine());
            }
            File.WriteAllText(file, sb.ToString());

            // Checking if all default files are there
            foreach (var item in Settings.DefPLs)
            {
                string fileName = Settings.LocalPLFolder + item;

                if (!File.Exists(fileName))
                {
                    File.Create(fileName);
                }

                Files.Add(fileName);
            }

            
        }

        private void LoadListViews()
        {
            // for media listview
            using (var reader = new StreamReader(Application.StartupPath + "\\Source.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    AllMediaListView.Items.Add(new ListViewItem(values));
                }
            }

            // for local playlists
            foreach (var item in LocalPLs)
            {
                LocalPLsLB.Items.Add(Path.GetFileName(item));
            }
        }

        private void LoadLocalPLs()
        {
            foreach (var file in Directory.GetFiles(Settings.LocalPLFolder, "*.csv").OrderBy(x => x).ToList())
            {
                LocalPLs.Add(Path.GetFileNameWithoutExtension(file));
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadMedia();
            WriteToFiles();
            LoadLocalPLs();
            LoadListViews();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
