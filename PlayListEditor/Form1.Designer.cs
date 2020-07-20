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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.PlaylistMediaLbl = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.listBox4 = new System.Windows.Forms.ListBox();
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
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.deletePB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.savePB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddPlPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.updatePB)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(230, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Local playlists";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(238, 250);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "0 min";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 247);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "0 min";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "All media";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(410, 252);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "0 min";
            // 
            // PlaylistMediaLbl
            // 
            this.PlaylistMediaLbl.AutoSize = true;
            this.PlaylistMediaLbl.Location = new System.Drawing.Point(413, 12);
            this.PlaylistMediaLbl.Name = "PlaylistMediaLbl";
            this.PlaylistMediaLbl.Size = new System.Drawing.Size(91, 15);
            this.PlaylistMediaLbl.TabIndex = 7;
            this.PlaylistMediaLbl.Text = "Playlist media";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(632, 250);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "0 min";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(609, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 15);
            this.label8.TabIndex = 10;
            this.label8.Text = "Result playlist";
            // 
            // listBox4
            // 
            this.listBox4.FormattingEnabled = true;
            this.listBox4.ItemHeight = 15;
            this.listBox4.Location = new System.Drawing.Point(635, 30);
            this.listBox4.Name = "listBox4";
            this.listBox4.Size = new System.Drawing.Size(107, 214);
            this.listBox4.TabIndex = 9;
            // 
            // deletePB
            // 
            this.deletePB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deletePB.Image = global::PlayListEditor.Properties.Resources.button_cancel;
            this.deletePB.Location = new System.Drawing.Point(748, 162);
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
            this.savePB.Location = new System.Drawing.Point(748, 118);
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
            this.AddPlPB.Location = new System.Drawing.Point(748, 30);
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
            this.updatePB.Location = new System.Drawing.Point(748, 74);
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
            this.AllMediaListView.Size = new System.Drawing.Size(215, 216);
            this.AllMediaListView.TabIndex = 17;
            this.AllMediaListView.UseCompatibleStateImageBehavior = false;
            this.AllMediaListView.View = System.Windows.Forms.View.Details;
            this.AllMediaListView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.AllMediaListView_ItemDrag);
            this.AllMediaListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AllMediaListView_MouseDown);
            // 
            // NameHeader
            // 
            this.NameHeader.Text = "Name";
            this.NameHeader.Width = 150;
            // 
            // LengthHeader
            // 
            this.LengthHeader.Text = "Length";
            // 
            // LocalPLsLV
            // 
            this.LocalPLsLV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LocalPLsLV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.LocalPLsLV.HideSelection = false;
            this.LocalPLsLV.Location = new System.Drawing.Point(233, 28);
            this.LocalPLsLV.MultiSelect = false;
            this.LocalPLsLV.Name = "LocalPLsLV";
            this.LocalPLsLV.Size = new System.Drawing.Size(173, 216);
            this.LocalPLsLV.TabIndex = 18;
            this.LocalPLsLV.UseCompatibleStateImageBehavior = false;
            this.LocalPLsLV.View = System.Windows.Forms.View.Details;
            this.LocalPLsLV.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LocalPLsLV_MouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 168;
            // 
            // PlaylistMediaLV
            // 
            this.PlaylistMediaLV.AllowDrop = true;
            this.PlaylistMediaLV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlaylistMediaLV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3});
            this.PlaylistMediaLV.GridLines = true;
            this.PlaylistMediaLV.HideSelection = false;
            this.PlaylistMediaLV.Location = new System.Drawing.Point(416, 30);
            this.PlaylistMediaLV.Name = "PlaylistMediaLV";
            this.PlaylistMediaLV.Size = new System.Drawing.Size(215, 216);
            this.PlaylistMediaLV.TabIndex = 19;
            this.PlaylistMediaLV.UseCompatibleStateImageBehavior = false;
            this.PlaylistMediaLV.View = System.Windows.Forms.View.Details;
            this.PlaylistMediaLV.DragEnter += new System.Windows.Forms.DragEventHandler(this.PlaylistMediaLV_DragEnter);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Length";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(813, 306);
            this.Controls.Add(this.PlaylistMediaLV);
            this.Controls.Add(this.LocalPLsLV);
            this.Controls.Add(this.AllMediaListView);
            this.Controls.Add(this.deletePB);
            this.Controls.Add(this.savePB);
            this.Controls.Add(this.AddPlPB);
            this.Controls.Add(this.updatePB);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.listBox4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.PlaylistMediaLbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("News706 BT", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label PlaylistMediaLbl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox listBox4;
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
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}

