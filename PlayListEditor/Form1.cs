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
       // readonly List<MediaItem> Media;
        readonly List<string> Files;
        ContextMenuStrip AllMediaLVContext;
        ListViewItem PrevSelectedLocalPL;
        ListView activeListView;

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
           // Media = new List<MediaItem>();
            Files = new List<string>();
           
            AllMediaLVContext = new ContextMenuStrip();
            var addTo = new ToolStripMenuItem();
            addTo.Text = "Add to";
            addTo.DropDownItems.Add("Mon", null, AddToPL);
            AllMediaLVContext.Items.Add(addTo);

            activeListView = new ListView();

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
            Catalog.Source = new PlayList(null); // Just to cause the 'set' accessor to be called
            // Let's check if some of the playlists contains elements that doesn't in the Source
            for(int j = 0; j < Catalog.Lists.Count; j++)
            {
                var pl = Catalog.Lists[j];
                var path = Settings.LocalPLFolder + pl.Name + ".csv";
                if (File.Exists(path) == false) // if the file does not exist at all, remove it
                {
                    Catalog.Lists.RemoveAt(j);
                    continue;
                }
                var sb = new StringBuilder();
                for (int i = 0; i < pl.Items.Count; i++)
                {
                    if (Catalog.Source.Items.Where(x => x.Name == pl.Items[i].Name).FirstOrDefault() == null)
                    {
                        pl.Items.RemoveAt(i);
                        continue;
                    }
                    sb.AppendLine(pl.Items[i].ToCSVLine());
                }
                File.WriteAllText(path, sb.ToString());
            }
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

        private void CheckDefFiles()
        {
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

        private void WriteToFiles()
        {
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
            AllMediaListView.Items.Clear();
            AllMediaListView.Items.AddRange(Catalog.Source.Items.Select(l => new ListViewItem(new string[] { l.Name,l.Length })).ToArray());
            var totalDuration = Catalog.Duration;
            AllMediaDurationLbl.Text = $"{(int)totalDuration.TotalHours}:{(int)totalDuration.Minutes}:{totalDuration.Seconds:00}";

            // for local playlists
            LocalPLsLV.Items.Clear();
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
            Catalog.Lists.Clear();
            foreach (var file in Directory.GetFiles(Settings.LocalPLFolder, "*.csv").OrderBy(x => x).ToList())
            {
                LoadPLFromFile(file);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckDefFiles();
            LoadCatalogFromCSVs();
            LoadMedia();
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
            if (activeListView.Equals(LocalPLsLV))
            {
                var item = LocalPLsLV.SelectedItems[0];
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
                
                PlaylistMediaLbl.Text = "Playlist media";
                PlaylistMediaLV.Items.Clear();
                PlaylistMediaDurationLbl.Text = string.Empty;
                LocalPLDurationLbl.Text = $"{(int)Catalog.Duration.TotalHours}:{(int)Catalog.Duration.Minutes}:{Catalog.Duration.Seconds:00}";
                deletePB.Visible = false;
                IsToSave = true;
                activeListView = null;
            }
            else if (activeListView.Equals(PlaylistMediaLV))
            {
                var pl = Catalog.Lists.Where(l => l.Name == PlaylistMediaLbl.Text).First();
                foreach (ListViewItem item in PlaylistMediaLV.SelectedItems)
                {
                    pl.Items.RemoveAll(i => i.Name == item.SubItems[0].Text);
                    PlaylistMediaLV.Items.Remove(item);
                }
            }
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
        }

        private void UpdatePB_Click(object sender, EventArgs e)
        {
            CheckDefFiles();
            LoadCatalogFromCSVs();
            LoadMedia();
            LoadListViews();
            AllMediaListView.SelectedItems.Clear();
            LocalPLsLV.SelectedItems.Clear();
            PlaylistMediaLbl.Text = "Playlist media";
            PlaylistMediaLV.Items.Clear();
            deletePB.Visible = false;
            activeListView = null;
            IsToSave = false;
        }


        private void LocalPLsLV_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var item = LocalPLsLV.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                activeListView = LocalPLsLV;
                PrevSelectedLocalPL = item;
                item.Selected = true;
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
        }

        private void LocalPLsLV_MouseClick(object sender, MouseEventArgs e)
        {
            var item = LocalPLsLV.GetItemAt(e.X, e.Y);
            item.Selected = false;
        }

        private void PlaylistMediaLV_SelectedIndexChanged(object sender, EventArgs e)
        {
            activeListView = PlaylistMediaLV;
        }
    }
}
