namespace PlayListEditor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.LocalPLDurationLbl = new System.Windows.Forms.Label();
            this.AllMediaDurationLbl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PlaylistMediaDurationLbl = new System.Windows.Forms.Label();
            this.PlaylistMediaLbl = new System.Windows.Forms.Label();
            this.deletePB = new System.Windows.Forms.PictureBox();
            this.savePB = new System.Windows.Forms.PictureBox();
            this.AddPlPB = new System.Windows.Forms.PictureBox();
            this.updatePB = new System.Windows.Forms.PictureBox();
            this.AllMediaListView = new System.Windows.Forms.ListView();
            this.NameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LengthHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LocalPLsLV = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PlaylistMediaLV = new System.Windows.Forms.ListView();
            this.NameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LenghtColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.deletePB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.savePB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddPlPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.updatePB)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(501, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Local playlists";
            // 
            // LocalPLDurationLbl
            // 
            this.LocalPLDurationLbl.AutoSize = true;
            this.LocalPLDurationLbl.Location = new System.Drawing.Point(357, 247);
            this.LocalPLDurationLbl.Name = "LocalPLDurationLbl";
            this.LocalPLDurationLbl.Size = new System.Drawing.Size(0, 15);
            this.LocalPLDurationLbl.TabIndex = 2;
            // 
            // AllMediaDurationLbl
            // 
            this.AllMediaDurationLbl.AutoSize = true;
            this.AllMediaDurationLbl.Location = new System.Drawing.Point(165, 247);
            this.AllMediaDurationLbl.Name = "AllMediaDurationLbl";
            this.AllMediaDurationLbl.Size = new System.Drawing.Size(0, 15);
            this.AllMediaDurationLbl.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "All media";
            // 
            // PlaylistMediaDurationLbl
            // 
            this.PlaylistMediaDurationLbl.AutoSize = true;
            this.PlaylistMediaDurationLbl.Location = new System.Drawing.Point(570, 247);
            this.PlaylistMediaDurationLbl.Name = "PlaylistMediaDurationLbl";
            this.PlaylistMediaDurationLbl.Size = new System.Drawing.Size(0, 15);
            this.PlaylistMediaDurationLbl.TabIndex = 8;
            // 
            // PlaylistMediaLbl
            // 
            this.PlaylistMediaLbl.AutoSize = true;
            this.PlaylistMediaLbl.Location = new System.Drawing.Point(255, 10);
            this.PlaylistMediaLbl.Name = "PlaylistMediaLbl";
            this.PlaylistMediaLbl.Size = new System.Drawing.Size(91, 15);
            this.PlaylistMediaLbl.TabIndex = 7;
            this.PlaylistMediaLbl.Text = "Playlist media";
            // 
            // deletePB
            // 
            this.deletePB.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.deletePB.Image = global::PlayListEditor.Properties.Resources.button_cancel;
            this.deletePB.Location = new System.Drawing.Point(751, 159);
            this.deletePB.Name = "deletePB";
            this.deletePB.Size = new System.Drawing.Size(36, 38);
            this.deletePB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.deletePB.TabIndex = 16;
            this.deletePB.TabStop = false;
            this.deletePB.Visible = false;
            this.deletePB.Click += new System.EventHandler(this.DeletePB_Click);
            // 
            // savePB
            // 
            this.savePB.Image = global::PlayListEditor.Properties.Resources.save;
            this.savePB.Location = new System.Drawing.Point(751, 115);
            this.savePB.Name = "savePB";
            this.savePB.Size = new System.Drawing.Size(36, 38);
            this.savePB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.savePB.TabIndex = 15;
            this.savePB.TabStop = false;
            this.savePB.Visible = false;
            this.savePB.Click += new System.EventHandler(this.SavePB_Click);
            // 
            // AddPlPB
            // 
            this.AddPlPB.Image = global::PlayListEditor.Properties.Resources.plus;
            this.AddPlPB.Location = new System.Drawing.Point(751, 27);
            this.AddPlPB.Name = "AddPlPB";
            this.AddPlPB.Size = new System.Drawing.Size(36, 38);
            this.AddPlPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.AddPlPB.TabIndex = 14;
            this.AddPlPB.TabStop = false;
            this.AddPlPB.Click += new System.EventHandler(this.CreatePL);
            // 
            // updatePB
            // 
            this.updatePB.Image = global::PlayListEditor.Properties.Resources._0pvBq;
            this.updatePB.Location = new System.Drawing.Point(751, 71);
            this.updatePB.Name = "updatePB";
            this.updatePB.Size = new System.Drawing.Size(36, 38);
            this.updatePB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.updatePB.TabIndex = 13;
            this.updatePB.TabStop = false;
            this.updatePB.Click += new System.EventHandler(this.UpdatePB_Click);
            // 
            // AllMediaListView
            // 
            this.AllMediaListView.AllowDrop = true;
            this.AllMediaListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AllMediaListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameHeader,
            this.LengthHeader});
            this.AllMediaListView.GridLines = true;
            this.AllMediaListView.HideSelection = false;
            this.AllMediaListView.Location = new System.Drawing.Point(12, 28);
            this.AllMediaListView.Name = "AllMediaListView";
            this.AllMediaListView.Size = new System.Drawing.Size(240, 371);
            this.AllMediaListView.TabIndex = 17;
            this.AllMediaListView.UseCompatibleStateImageBehavior = false;
            this.AllMediaListView.View = System.Windows.Forms.View.Details;
            this.AllMediaListView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.AllMediaListView_ItemDrag);
            this.AllMediaListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AllMediaListView_MouseDown);
            // 
            // NameHeader
            // 
            this.NameHeader.Text = "Name";
            this.NameHeader.Width = 173;
            // 
            // LengthHeader
            // 
            this.LengthHeader.Text = "Length";
            this.LengthHeader.Width = 63;
            // 
            // LocalPLsLV
            // 
            this.LocalPLsLV.AllowDrop = true;
            this.LocalPLsLV.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.LocalPLsLV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.LocalPLsLV.HideSelection = false;
            this.LocalPLsLV.Location = new System.Drawing.Point(505, 29);
            this.LocalPLsLV.MultiSelect = false;
            this.LocalPLsLV.Name = "LocalPLsLV";
            this.LocalPLsLV.Size = new System.Drawing.Size(228, 370);
            this.LocalPLsLV.TabIndex = 18;
            this.LocalPLsLV.UseCompatibleStateImageBehavior = false;
            this.LocalPLsLV.View = System.Windows.Forms.View.Details;
            this.LocalPLsLV.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.LocalPLsLV_ItemDrag);
            this.LocalPLsLV.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.LocalPLsLV_ItemSelectionChanged);
            this.LocalPLsLV.DragEnter += new System.Windows.Forms.DragEventHandler(this.PlaylistMediaLV_DragEnter);
            this.LocalPLsLV.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LocalPLsLV_MouseClick);
            this.LocalPLsLV.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LocalPLsLV_MouseDown);
            this.LocalPLsLV.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LocalPLsLV_MouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 223;
            // 
            // PlaylistMediaLV
            // 
            this.PlaylistMediaLV.AllowDrop = true;
            this.PlaylistMediaLV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlaylistMediaLV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameColumn,
            this.LenghtColumn});
            this.PlaylistMediaLV.GridLines = true;
            this.PlaylistMediaLV.HideSelection = false;
            this.PlaylistMediaLV.Location = new System.Drawing.Point(258, 28);
            this.PlaylistMediaLV.Name = "PlaylistMediaLV";
            this.PlaylistMediaLV.Size = new System.Drawing.Size(241, 370);
            this.PlaylistMediaLV.TabIndex = 19;
            this.PlaylistMediaLV.UseCompatibleStateImageBehavior = false;
            this.PlaylistMediaLV.View = System.Windows.Forms.View.Details;
            this.PlaylistMediaLV.SelectedIndexChanged += new System.EventHandler(this.PlaylistMediaLV_SelectedIndexChanged);
            this.PlaylistMediaLV.DragEnter += new System.Windows.Forms.DragEventHandler(this.PlaylistMediaLV_DragEnter);
            // 
            // NameColumn
            // 
            this.NameColumn.Text = "Name";
            this.NameColumn.Width = 173;
            // 
            // LenghtColumn
            // 
            this.LenghtColumn.Text = "Length";
            this.LenghtColumn.Width = 63;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(799, 410);
            this.Controls.Add(this.PlaylistMediaLV);
            this.Controls.Add(this.LocalPLsLV);
            this.Controls.Add(this.AllMediaListView);
            this.Controls.Add(this.deletePB);
            this.Controls.Add(this.savePB);
            this.Controls.Add(this.AddPlPB);
            this.Controls.Add(this.updatePB);
            this.Controls.Add(this.PlaylistMediaDurationLbl);
            this.Controls.Add(this.PlaylistMediaLbl);
            this.Controls.Add(this.AllMediaDurationLbl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LocalPLDurationLbl);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("News706 BT", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Playlists Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.deletePB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.savePB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddPlPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.updatePB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LocalPLDurationLbl;
        private System.Windows.Forms.Label AllMediaDurationLbl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label PlaylistMediaDurationLbl;
        private System.Windows.Forms.Label PlaylistMediaLbl;
        private System.Windows.Forms.PictureBox updatePB;
        private System.Windows.Forms.PictureBox AddPlPB;
        private System.Windows.Forms.PictureBox savePB;
        private System.Windows.Forms.PictureBox deletePB;
        private System.Windows.Forms.ListView AllMediaListView;
        private System.Windows.Forms.ColumnHeader NameHeader;
        private System.Windows.Forms.ColumnHeader LengthHeader;
        private System.Windows.Forms.ListView LocalPLsLV;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView PlaylistMediaLV;
        private System.Windows.Forms.ColumnHeader NameColumn;
        private System.Windows.Forms.ColumnHeader LenghtColumn;
    }
}

