using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PlayListEditor
{
    public partial class Form1 : Form
    {
        readonly List<MediaItem> Media;
        readonly List<string> Files;
        private ContextMenuStrip AllMediaLVContext;
        private ContextMenuStrip ListboxContextMenu;

        public Form1()
        {
            Media = new List<MediaItem>();
            Files = new List<string>();
           
            AllMediaLVContext = new ContextMenuStrip();
            var addTo = new ToolStripMenuItem();
            addTo.Text = "Add to";
            addTo.DropDownItems.Add("Mon", null, AddToPL);
            AllMediaLVContext.Items.Add(addTo);
            
            ContextMenu contextMenu = new ContextMenu();
            contextMenu.MenuItems.Add(new MenuItem("Create playlist", CreatePL));
            InitializeComponent();
            LocalPLsLB.ContextMenu = contextMenu;

            ListboxContextMenu = new ContextMenuStrip();
        }

        private void AddToPL(object sender, EventArgs e)
        {
            var text = (sender as ToolStripMenuItem).Text;

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
                if(!Catalog.Lists.Select(x=>x.Name).ToList().Contains(textBox.Text))
                {
                    var fileName = Settings.LocalPLFolder + "\\" + textBox.Text + ".csv";
                    File.Create(fileName);
                    Catalog.Lists.Add(new PlayList(textBox.Text));
                    LocalPLsLB.Items.Add(textBox.Text);
                }
                
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
            foreach (var pl in Catalog.Lists)
            {
                LocalPLsLB.Items.Add(pl.Name);
            }
        }
        private void LoadPLFromFile(string path)
        {
            if (!File.Exists(path))
                return;
            var pl = new PlayList(Path.GetFileNameWithoutExtension(path));

            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    pl.Items.Add(new MediaItem(values[0], values[1]));
                }
            }

            Catalog.Lists.Add(pl);
        }
        private void LoadCatalogFromCSVs()
        {
            foreach (var file in Directory.GetFiles(Settings.LocalPLFolder, "*.csv").OrderBy(x => x).ToList())
            {
                LoadPLFromFile(file);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadMedia();
            WriteToFiles();
            LoadCatalogFromCSVs();
            LoadListViews();
        }

   

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void AllMediaListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                if (AllMediaListView.FocusedItem != null && AllMediaListView.FocusedItem.Bounds.Contains(e.Location))
                {
                    AllMediaLVContext.Show(Cursor.Position);
                }
            }
        }

        private void LocalPLsLB_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //select the item under the mouse pointer
                LocalPLsLB.SelectedIndex = LocalPLsLB.IndexFromPoint(e.Location);
                if (LocalPLsLB.SelectedIndex != -1)
                {
                    LocalPLsLB.ContextMenu.MenuItems.Add(new MenuItem("Delete", DeletePlayList));
                }
            }
        }

        private void DeletePlayList(object sender, EventArgs e)
        {
            var plName = LocalPLsLB.SelectedItem.ToString();
            var file = Settings.LocalPLFolder + "\\" + plName + ".csv";
            if (File.Exists(file))
            {
                File.Delete(file);
                var pl = Catalog.Lists.Where(x => x.Name == plName).FirstOrDefault();
                if (pl != null)
                {
                    Catalog.Lists.Remove(pl);
                }
                LocalPLsLB.Items.Remove(LocalPLsLB.SelectedItem);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
