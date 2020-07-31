using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static ExtensionMethods.Extensions;

namespace PlayListEditor
{
    public partial class Form1 : Form
    {
        ContextMenuStrip AllMediaLVContext;
        ListViewItem PrevSelectedLocalPL;
        ListView activeListView;
        ToolStripMenuItem ForAllMediaContext;
        private bool LPLLastActionDrag;
        private bool mouseDown;
        ContextMenuStrip PlayListMediaContextMenu;
  
        public bool IsToSave 
        {
            get
            {
                return savePB.Enabled;
            }
            set
            {
                savePB.Enabled = value;
            }
        }
        public Form1()
        {
            AllMediaLVContext = new ContextMenuStrip();
            ForAllMediaContext = new ToolStripMenuItem() { Text = "Add to" };
            PlayListMediaContextMenu = new ContextMenuStrip();
            var ForPlayListMediaContextMenu = new ToolStripMenuItem("Delete selected", null, DeletePLItem) { Text = "Delete selected" };
            PlayListMediaContextMenu.Items.Add(ForPlayListMediaContextMenu);
            activeListView = new ListView();
            Settings.DefPLs();
            InitializeComponent();
        }

        private void DeletePLItem(object sender, EventArgs e)
        {
            var pl = new PlayList(null);
            if (PlaylistMediaLbl.Text != "Playlist media")
            {
                pl = Catalog.Lists.Where(l => l.Name == PlaylistMediaLbl.Text).FirstOrDefault();
            }
            foreach (ListViewItem lvi in PlaylistMediaLV.SelectedItems)
            {
                PlaylistMediaLV.Items.Remove(lvi);
                pl?.Items.Remove(pl.Items.Where(i => i.Name == lvi.SubItems[0].Text).FirstOrDefault());
            }
            TimeSpan totalDuration = default;
            foreach (ListViewItem item in PlaylistMediaLV.Items)
            {
                totalDuration += TimeSpan.Parse(item.SubItems[1].Text);
            }
            PlaylistMediaDurationLbl.Text = $"{(int)totalDuration.TotalHours}:{(int)totalDuration.Minutes}:{totalDuration.Seconds:00}";
            LoadListViews();
            IsToSave = true;
        }

        private void AddToPL(object sender, EventArgs e)
        {
            var destinationPL = (sender as ToolStripMenuItem).Text;
            foreach (ListViewItem lvi in AllMediaListView.SelectedItems)
            {
                var pl = Catalog.Lists.Where(l => l.Name == destinationPL).FirstOrDefault();
                pl.Items.Add(Catalog.Source.Items.Where(i => i.Name == lvi.Text).First());
            }
            LoadListViews();
            IsToSave = true;
        }
               
        private void LoadMedia()
        {
            // Let's check if some of the playlists contains elements that doesn't in the Source
           
            for (int j = 0; j < Catalog.Lists.Count; j++)
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
            UpdateAllMediaLVContext();
        }

        private void UpdateAllMediaLVContext()
        {
            ForAllMediaContext.DropDownItems.Clear();
            foreach (var def in Settings.DefPLs())
            {
                ForAllMediaContext.DropDownItems.Add(def.Replace(".csv",string.Empty), null, AddToPL);
            }
            foreach (var pl in Catalog.Lists.Where(l => !Settings.DefPLs().Contains(l.Name + ".csv")))
            {
                ForAllMediaContext.DropDownItems.Add(pl.Name, null, AddToPL);
            }
            AllMediaLVContext.Items.Add(ForAllMediaContext);
        }

        private void CreatePL(object sender, EventArgs e) 
        {
            Form prompt = new Form()
            {
                Width = 380,
                Height = 100,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Create a new playlist",
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false
            };
            Label textLabel = new Label() { Left = 20, Top = 23, Text = "Name:" };
            TextBox textBox = new TextBox() { Left = 55, Top = 20, Width = 190, MaxLength = 15 };

            Button confirmation = new Button() { Text = "Ok", Left = 255, Width = 80, Top = 19, DialogResult = DialogResult.OK };

            confirmation.Click += (s, a) => 
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    MessageBox.Show("Name is required!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    prompt.DialogResult = DialogResult.Retry;
                }
                else
                {
                    if (!Catalog.Lists.Select(x => x.Name).Contains(textBox.Text))
                    {
                        var fileName = Settings.LocalPLFolder + "\\" + textBox.Text + ".csv";
                        var fs = File.Create(fileName);
                        fs.Close();
                        Catalog.Lists.Add(new PlayList(textBox.Text));
                        ListViewItem newItem = new ListViewItem(textBox.Text);
                        LocalPLsLV.Items.Add(newItem);
                        newItem.Selected = true;
                        newItem.Focused = true;
                        PlaylistMediaLbl.Text = textBox.Text;
                        PlaylistMediaLV.Items.Clear();
                        PlaylistMediaDurationLbl.Text = "0:0:00";
                        prompt.Close();
                    }
                    else
                    {
                        MessageBox.Show(textBox.Text + " playlist already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        prompt.DialogResult = DialogResult.Retry;
                    }
                    
                }
            };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;
            var res = DialogResult.Retry;
            do
            {
                res = prompt.ShowDialog();
            } while (res == DialogResult.Retry);
                
            if (res == DialogResult.OK)
            {
                UpdateAllMediaLVContext();
            }
        }

        private void WriteToFiles()
        {
            Settings.DefPLs();
            //Write all playlists
            foreach (var pl in Catalog.Lists)
            {
                var path = Settings.LocalPLFolder + pl.Name + ".csv";
                if (!File.Exists(path))
                {
                    File.Create(path);
                }
                var sb = new StringBuilder();
                foreach (var item in pl.Items)
                {
                    sb.AppendLine(item.ToCSVLine());
                }
                File.WriteAllText(path, sb.ToString());
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
            LocalPLsLV.Items.AddRange(Settings.DefPLs().Select(l => new ListViewItem { Text = l.Replace(".csv", string.Empty)}).ToArray());
            LocalPLsLV.Items.AddRange(Catalog.Lists.Where(l => !Settings.DefPLs().Contains(l.Name + ".csv")).Select(l => new ListViewItem { Text = l.Name }).ToArray());
           
            // for the playlist media
            if (PlaylistMediaLbl.Text != "Playlist media")
            {
                var pl = Catalog.Lists.Where(l => l.Name == PlaylistMediaLbl.Text).FirstOrDefault();
                if (pl != null)
                {
                    PlaylistMediaLV.Items.Clear();
                    PlaylistMediaLV.Items.AddRange(pl.Items.Select(l => new ListViewItem(new string[] { l.Name, l.Length })).ToArray());
                    PlaylistMediaDurationLbl.Text = $"{(int)pl.Duration.TotalHours}:{(int)pl.Duration.Minutes}:{pl.Duration.Seconds:00}";
                }
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
            Settings.DefPLs();
            Catalog.Lists.Clear();
            foreach (var file in Directory.GetFiles(Settings.LocalPLFolder, "*.csv").OrderBy(x => x).ToList())
            {
                LoadPLFromFile(file);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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

                if (!Settings.DefPLs().Contains(item.Text + ".csv"))
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
                
            }
            else if (activeListView.Equals(PlaylistMediaLV))
            {
                var pl = Catalog.Lists.Where(l => l.Name == PlaylistMediaLbl.Text).First();
                foreach (ListViewItem item in PlaylistMediaLV.SelectedItems)
                {
                    pl.Items.RemoveAll(i => i.Name == item.SubItems[0].Text);
                    PlaylistMediaLV.Items.Remove(item);
                }
                TimeSpan totalDuration = default;
                foreach (ListViewItem item in PlaylistMediaLV.Items)
                {
                    totalDuration += TimeSpan.Parse(item.SubItems[1].Text);
                }
                PlaylistMediaDurationLbl.Text = $"{(int)totalDuration.TotalHours}:{(int)totalDuration.Minutes}:{totalDuration.Seconds:00}";
            }

            activeListView = null;
            deletePB.Enabled = false;
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

                var pl = Catalog.Lists.Where(l => l.Name == PlaylistMediaLbl.Text).FirstOrDefault();
                if (pl != null)
                {
                    pl.Items.Add(new MediaItem(vb[0], vb[1]));
                }          

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
            Settings.DefPLs();
            if (PlaylistMediaLbl.Text == "Playlist media" && PlaylistMediaLV.Items.Count != 0)
            {
                Form prompt = new Form()
                {
                    Width = 380,
                    Height = 100,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = "Create a new playlist",
                    StartPosition = FormStartPosition.CenterScreen,
                    MaximizeBox = false,
                    MinimizeBox = false
                };
                Label textLabel = new Label() { Left = 20, Top = 23, Text = "Name:" };
                TextBox textBox = new TextBox() { Left = 55, Top = 20, Width = 190, MaxLength = 15 };

                Button confirmation = new Button() { Text = "Ok", Left = 255, Width = 80, Top = 19, DialogResult = DialogResult.OK };
                confirmation.Click += (s, a) =>
                {
                    if (string.IsNullOrEmpty(textBox.Text))
                    {
                        MessageBox.Show("Name is required!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        prompt.DialogResult = DialogResult.Retry;
                    }
                    else
                    {
                        if (!Catalog.Lists.Select(x => x.Name).Contains(textBox.Text))
                        {
                            var fileName = Settings.LocalPLFolder + "\\" + textBox.Text + ".csv";
                            //var fs = File.Create(fileName);
                            //fs.Close();
                            var pl = new PlayList(textBox.Text);
                            var sb = new StringBuilder();
                                                      
                            foreach (ListViewItem item in PlaylistMediaLV.Items)
                            {
                                var mediaItem = new MediaItem(item.SubItems[0].Text, item.SubItems[1].Text);
                                pl.Items.Add(mediaItem);
                                sb.AppendLine(mediaItem.ToCSVLine());
                            }
                            File.WriteAllText(fileName, sb.ToString());
                            Catalog.Lists.Add(pl);
                            ListViewItem listViewItem = new ListViewItem(textBox.Text);
                            listViewItem.Selected = true;
                            listViewItem.Focused = true;
                            LocalPLsLV.Items.Add(listViewItem);
                            PlaylistMediaLbl.Text = textBox.Text;
                            prompt.Close();
                        }
                        else
                        {
                            MessageBox.Show(textBox.Text + " playlist already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            prompt.DialogResult = DialogResult.Retry;
                        }

                    }
                };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;
                var res = DialogResult.Retry;
                do
                {
                    res = prompt.ShowDialog();
                } while (res == DialogResult.Retry);

                if (res == DialogResult.OK)
                {
                    
                    UpdateAllMediaLVContext();
                }
                else if(res == DialogResult.Cancel)
                {
                    PlaylistMediaLV.Items.Clear();
                }
            }
            // there are some pls to delete
            else if(PlaylistMediaLbl.Text == "Playlist media" && PlaylistMediaLV.Items.Count == 0)
            {
                foreach (var file in Directory.GetFiles(Settings.LocalPLFolder, "*.csv"))
                {
                    if(!Catalog.Lists.Select(l => l.Name).Contains(file))
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
                WriteToFiles();
            }
            UpdateAllMediaLVContext();
            IsToSave = false;
            deletePB.Enabled = false;
        }
        
        private void LocalPLsLV_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Settings.DefPLs();
            if (PrevSelectedLocalPL != null && PrevSelectedLocalPL.Index != -1)
            {
                LocalPLsLV.Items[PrevSelectedLocalPL.Index].Selected = true;
            }
            ListViewItem item = (ListViewItem)e.Item;
            var pl = Catalog.Lists.Where(l => l.Name == item.Text).First();

            PlaylistMediaLV.DoDragDrop(item, DragDropEffects.Copy); // just for copying effect

            PlaylistMediaLV.Items.AddRange(pl.Items.Select(l => new ListViewItem(new string[] { l.Name, l.Length })).ToArray()); ;
                        
            TimeSpan totalDuration = default;
            PlayList plToAdd = new PlayList("");
            if (PlaylistMediaLbl.Text != "Playlist media")
                plToAdd = Catalog.Lists.Where(l => l.Name == PlaylistMediaLbl.Text).First();
            plToAdd.Items.Clear();
            foreach (ListViewItem i in PlaylistMediaLV.Items)
            {
                plToAdd.Items.Add(new MediaItem(i.SubItems[0].Text, i.SubItems[1].Text));
                totalDuration += TimeSpan.Parse(i.SubItems[1].Text);
                IsToSave = true;
            }
            PlaylistMediaDurationLbl.Text = $"{(int)totalDuration.TotalHours}:{(int)totalDuration.Minutes}:{totalDuration.Seconds:00}";
            LPLLastActionDrag = true;
           
        }

        private void UpdatePB_Click(object sender, EventArgs e)
        {
            Settings.DefPLs();
            LoadCatalogFromCSVs();
            LoadMedia();
            LoadListViews();
            UpdateAllMediaLVContext();
            AllMediaListView.SelectedItems.Clear();
            LocalPLsLV.SelectedItems.Clear();
            PlaylistMediaLbl.Text = "Playlist media";
            PlaylistMediaDurationLbl.Text = string.Empty;
            PlaylistMediaLV.Items.Clear();
            deletePB.Enabled = false;
            activeListView = null;
            IsToSave = false;
        }

        private void LocalPLsLV_MouseClick(object sender, MouseEventArgs e)
        {
            var item = LocalPLsLV.GetItemAt(e.X, e.Y);
            item.Selected = false;
            LPLLastActionDrag = false;
        }

        private void PlaylistMediaLV_SelectedIndexChanged(object sender, EventArgs e)
        {
            activeListView = PlaylistMediaLV;
            PLMediaItemMovingUpBtn.Visible = PLMediaItemMovingDownBtn.Visible = true;
        }

        private void LocalPLsLV_MouseUp(object sender, MouseEventArgs e)
        {
            var item = LocalPLsLV.GetItemAt(e.X, e.Y);
            if (item != null && LPLLastActionDrag == false)
            {
                activeListView = LocalPLsLV;
                PrevSelectedLocalPL = item;
                item.Selected = true;
                PlaylistMediaLV.Items.Clear();
                deletePB.Enabled = true;
                PlaylistMediaLbl.Text = item.Text;
                var pl = Catalog.Lists.Where(l => l.Name == item.Text).First();

                foreach (var media in pl.Items)
                {
                    var li = new ListViewItem(new string[] { media.Name, media.Length });
                    PlaylistMediaLV.Items.Add(li);
                }

                PlaylistMediaDurationLbl.Text = $"{(int)pl.Duration.TotalHours}:{(int)pl.Duration.Minutes}:{pl.Duration.Seconds:00}";
            }
            else if(!LPLLastActionDrag)
            {
                PlaylistMediaLV.Items.Clear();
                PlaylistMediaLbl.Text = "Playlist media";
                deletePB.Enabled = false;
                PlaylistMediaDurationLbl.Text = string.Empty;
            }
        }

        private void LocalPLsLV_MouseDown(object sender, MouseEventArgs e)
        {
            LPLLastActionDrag = false;
            mouseDown = true;
        }

        private void LocalPLsLV_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (mouseDown && e.IsSelected)
            {
                e.Item.Selected = false;
                e.Item.Focused = false;
                mouseDown = false;
                if (PrevSelectedLocalPL != null && PrevSelectedLocalPL.Index != -1)
                {
                    LocalPLsLV.Items[PrevSelectedLocalPL.Index].Selected = true;
                }
            }
           // e.IsSelected
        }

        private void PlaylistMediaLV_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (PlaylistMediaLV.FocusedItem != null && PlaylistMediaLV.FocusedItem.Bounds.Contains(e.Location))
                {
                    PlayListMediaContextMenu.Show(Cursor.Position);
                }
            }
        }

        private void UploadPB_Click(object sender, EventArgs e)
        {
            Catalog.Upload();
            MessageBox.Show("All the local playlists were successfully uploaded to the remote machine!", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void PLMediaItemMovingUpBtn_Click(object sender, EventArgs e)
        {
            if(MoveListviewItems(PlaylistMediaLV, MoveDirection.Up))
            {
                IsToSave = true;
                if (PlaylistMediaLbl.Text != "Playlist media")
                {
                    var pl = Catalog.Lists.Where(l => l.Name == PlaylistMediaLbl.Text).FirstOrDefault();
                    pl.Items.Clear();

                    foreach (ListViewItem item in PlaylistMediaLV.Items)
                    {
                        pl.Items.Add(new MediaItem(item.SubItems[0].Text, item.SubItems[1].Text));
                    }
                }
            }
        }

        private void PLMediaItemMovingDownBtn_Click(object sender, EventArgs e)
        {

            if (MoveListviewItems(PlaylistMediaLV, MoveDirection.Down))
            {
                IsToSave = true;
                if (PlaylistMediaLbl.Text != "Playlist media")
                {
                    var pl = Catalog.Lists.Where(l => l.Name == PlaylistMediaLbl.Text).FirstOrDefault();
                    pl.Items.Clear();

                    foreach (ListViewItem item in PlaylistMediaLV.Items)
                    {
                        pl.Items.Add(new MediaItem(item.SubItems[0].Text, item.SubItems[1].Text));
                    }
                }
            }
        }
    }
}
