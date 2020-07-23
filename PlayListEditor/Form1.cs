using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using System;
using System.Collections.Generic;
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
        ContextMenuStrip AllMediaLVContext;
        ListViewItem PrevSelectedLocalPL;
        bool isSelected = false;

        public bool IsToSave 
        {
            get
            {
                return savePB.Visible;
            }
            set
            {
                savePB.Visible = value;
            }
        }
        public Form1()
        {
            Media = new List<MediaItem>();
            Files = new List<string>();
           
            AllMediaLVContext = new ContextMenuStrip();
            var addTo = new ToolStripMenuItem();
            addTo.Text = "Add to";
            addTo.DropDownItems.Add("Mon", null, AddToPL);
            AllMediaLVContext.Items.Add(addTo);
            
            InitializeComponent();

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
                Media.Add(new MediaItem(
                    Path.GetFileNameWithoutExtension(file), 
                    duration
                    ));
                totalDuration += duration;
            }
            AllMediaDurationLbl.Text = $"{(int)totalDuration.TotalHours}:{(int)totalDuration.Minutes}:{totalDuration.Seconds:00}";

        }
        private void CreatePL(object sender, EventArgs e) 
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Create new playlist",
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
                if(!Catalog.Lists.Select(x=>x.Name).Contains(textBox.Text))
                {
                    var fileName = Settings.LocalPLFolder + "\\" + textBox.Text + ".csv";
                    File.Create(fileName);
                    Catalog.Lists.Add(new PlayList(textBox.Text));
                    LocalPLsLV.Items.Add(textBox.Text);
                }
                else
                {
                    MessageBox.Show(textBox.Text + " playlist already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void WriteToFiles()
        {
            // Write to Source.csv
            {
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
            //Write all playlists
            foreach (var pl in Catalog.Lists)
            {
                var path = Settings.LocalPLFolder + pl.Name + ".csv";
                if (File.Exists(path))
                {
                    var sb = new StringBuilder();
                    foreach (var item in pl.Items)
                    {
                        sb.AppendLine(item.ToCSVLine());
                    }
                    File.WriteAllText(path, sb.ToString());
                }
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
            LocalPLsLV.Items.AddRange(Catalog.Lists.Select(l => new ListViewItem { Text = l.Name }).ToArray());
          
            LocalPLDurationLbl.Text = $"{(int)Catalog.Duration.TotalHours}:{(int)Catalog.Duration.Minutes}:{Catalog.Duration.Seconds:00}";

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


        private void DeletePlayList(object sender, EventArgs e)
        {
            var plName = LocalPLsLV.SelectedItems.ToString();
            var file = Settings.LocalPLFolder + "\\" + plName + ".csv";
            if (File.Exists(file))
            {
                File.Delete(file);
                var pl = Catalog.Lists.Where(x => x.Name == plName).FirstOrDefault();
                if (pl != null)
                {
                    Catalog.Lists.Remove(pl);
                }
                LocalPLsLV.Items.Remove(LocalPLsLV.FocusedItem);
            }
        }

        private void DeletePB_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LocalPLsLV.SelectedItems)
            {
                // it is not one of the default playlist so we can delete the file permanently
                var pl = Catalog.Lists.FirstOrDefault(l => l.Name == item.Text);

                if (!Settings.DefPLs.Contains(item.Text + ".csv"))
                {                
                    LocalPLsLV.Items.Remove(item);
                    Catalog.Lists.Remove(pl);
                }
                else // one of the default lists
                {
                    pl.Items.Clear();
                }
            }
            PlaylistMediaLbl.Text = "Playlist media";
            PlaylistMediaLV.Items.Clear();
            PlaylistMediaDurationLbl.Text = string.Empty;
            LocalPLDurationLbl.Text = $"{(int)Catalog.Duration.TotalHours}:{(int)Catalog.Duration.Minutes}:{Catalog.Duration.Seconds:00}";
            deletePB.Visible = false;
            IsToSave = true;
        }

 
        private void PlaylistMediaLV_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void AllMediaListView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            foreach (ListViewItem item in AllMediaListView.SelectedItems)
            {
                PlaylistMediaLV.DoDragDrop(item, DragDropEffects.Copy); // just for copying effect

                string[] vb = { item.SubItems[0].Text, item.SubItems[1].Text };

                PlaylistMediaLV.Items.Add(new ListViewItem(vb));
                              
                IsToSave = true;
            }
            TimeSpan totalDuration = default;
            foreach (ListViewItem item in PlaylistMediaLV.Items)
            {
                totalDuration += TimeSpan.Parse(item.SubItems[1].Text);
            }
            PlaylistMediaDurationLbl.Text = $"{(int)totalDuration.TotalHours}:{(int)totalDuration.Minutes}:{totalDuration.Seconds:00}";
        }

        private void SavePB_Click(object sender, EventArgs e)
        {
            if(PlaylistMediaLbl.Text == "Playlist media" && PlaylistMediaLV.Items.Count != 0)
            {
                Form prompt = new Form()
                {
                    Width = 500,
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = "Create new playlist",
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

                if (prompt.ShowDialog() == DialogResult.OK)
                {
                    if (!Catalog.Lists.Select(x => x.Name).Contains(textBox.Text))
                    {
                        var fileName = Settings.LocalPLFolder + "\\" + textBox.Text + ".csv";
                        var fs = File.Create(fileName);
                        fs.Close();
                        var newPl = new PlayList(textBox.Text);
                        //newPl.Items = (PlaylistMediaLV.Items as IEnumerable<ListViewItem>)
                        //.Select(lvi => new MediaItem(lvi.SubItems[0].Text, lvi.SubItems[1].Text)).ToList();
                        foreach (ListViewItem item in PlaylistMediaLV.Items)
                        {
                            newPl.Items.Add(new MediaItem(item.SubItems[0].Text, item.SubItems[1].Text));
                        }
                        Catalog.Lists.Add(newPl);
                        PlaylistMediaLbl.Text = newPl.Name;
                        LocalPLsLV.Items.Add(newPl.Name);
                        WriteToFiles();
                    }
                    else
                    {
                        MessageBox.Show(textBox.Text + " playlist already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            // there are some pls to delete
            else if(PlaylistMediaLbl.Text == "Playlist media" && PlaylistMediaLV.Items.Count == 0)
            {
                foreach (var file in Directory.GetFiles(Settings.LocalPLFolder, "*.csv"))
                {
                    if(!Catalog.Lists.Select(l => l.Name).Contains(Path.GetFileNameWithoutExtension(file)))
                    {
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        File.Delete(file);
                    }
                    else
                    {
                        var pl = Catalog.Lists.Where(l => l.Name == Path.GetFileNameWithoutExtension(file)).FirstOrDefault();
                        File.WriteAllText(file, string.Empty);

                        var sb = new StringBuilder();
                        foreach (var item in pl.Items)
                        {
                            sb.AppendLine(item.ToCSVLine());
                        }
                        File.WriteAllText(file, sb.ToString());
                    }
                }
            }
            else // it is an old playlist
            {
                var pl = Catalog.Lists.Where(l => l.Name == PlaylistMediaLbl.Text).First();
                pl.Items.Clear();

                foreach (ListViewItem item in PlaylistMediaLV.Items)
                {
                    pl.Items.Add(new MediaItem(item.SubItems[0].Text, item.SubItems[1].Text));
                }
                WriteToFiles();
            }
            IsToSave = false;
        }

      

        private void LocalPLsLV_ItemDrag(object sender, ItemDragEventArgs e)
        {
            ListViewItem item = (ListViewItem)e.Item;
            var pl = Catalog.Lists.Where(l => l.Name == item.Text).First();

            PlaylistMediaLV.DoDragDrop(item, DragDropEffects.Copy); // just for copying effect

            PlaylistMediaLV.Items.AddRange(pl.Items.Select(l => new ListViewItem(new string[] { l.Name, l.Length })).ToArray()); ;
            
            IsToSave = true;
            TimeSpan totalDuration = default;
            foreach (ListViewItem i in PlaylistMediaLV.Items)
            {
                totalDuration += TimeSpan.Parse(i.SubItems[1].Text);
            }
            PlaylistMediaDurationLbl.Text = $"{(int)totalDuration.TotalHours}:{(int)totalDuration.Minutes}:{totalDuration.Seconds:00}";

            LocalPLsLV.Items[PrevSelectedLocalPL.Index].Selected = true;
           // LocalPLsLV.MouseUp -= LocalPLsLV_MouseUp;
           //// LocalPLsLV.SelectedItems[0] = PrevSelectedLocalPL;
           // LocalPLsLV.MouseUp += LocalPLsLV_MouseUp;
        }

        private void UpdatePB_Click(object sender, EventArgs e)
        {
            AllMediaListView.SelectedItems.Clear();
            LocalPLsLV.SelectedItems.Clear();
            PlaylistMediaLbl.Text = "Playlist media";
            PlaylistMediaLV.Items.Clear();
            deletePB.Visible = false;
            IsToSave = false;
        }

        private void LocalPLsLV_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

            //if (isSelected)
            //{
            //    LocalPLsLV.Items[PrevSelectedLocalPL.Index].Selected = true;
            //    LocalPLsLV.Items[PrevSelectedLocalPL.Index].Focused = true;
            //    e.Item.Selected = false;
            //}
            //{
            //    FirstChange = false;
            //}
            //if(PrevSelectedLocalPL != null && PrevSelectedLocalPL.Index != -1 && PrevSelectedLocalPL.Index != e.ItemIndex)
            //{
            //    LocalPLsLV.Items[PrevSelectedLocalPL.Index].Selected = true;
            //    LocalPLsLV.Items[PrevSelectedLocalPL.Index].Focused = true;
            //}

        }

        private void LocalPLsLV_DoubleClick(object sender, EventArgs e)
        {
            var item = LocalPLsLV.FocusedItem;
            if (item != null)
            {
                isSelected = true;
                PrevSelectedLocalPL = item;
               // LocalPLsLV.Items[item.Index].Selected = true;

                PlaylistMediaLV.Items.Clear();
                deletePB.Visible = true;
                PlaylistMediaLbl.Text = item.Text;
                var pl = Catalog.Lists.Where(l => l.Name == item.Text).First();

                foreach (var media in pl.Items)
                {
                    var li = new ListViewItem(new string[] { media.Name, media.Length });
                    PlaylistMediaLV.Items.Add(li);
                }

                PlaylistMediaDurationLbl.Text = $"{(int)pl.Duration.TotalHours}:{(int)pl.Duration.Minutes}:{pl.Duration.Seconds:00}";

            }
            //else
            //{
            //    PlaylistMediaLbl.Text = "Playlist media";
            //    PlaylistMediaLV.Items.Clear();
            //    PlaylistMediaDurationLbl.Text = string.Empty;
            //}
        }

        private void LocalPLsLV_MouseDown(object sender, MouseEventArgs e)
        {
            if (isSelected)
            {
                LocalPLsLV.Items[PrevSelectedLocalPL.Index].Selected = true;
                LocalPLsLV.Items[PrevSelectedLocalPL.Index].Focused = true;
            }
            else
            {
                var item = LocalPLsLV.GetItemAt(e.X, e.Y);
                PrevSelectedLocalPL = item;
            }
          
        }
    }
}
