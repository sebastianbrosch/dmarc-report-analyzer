namespace DMARCReportAnalyzer
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewFeedback = new DataGridView();
            menuStrip1 = new MenuStrip();
            datenbankToolStripMenuItem = new ToolStripMenuItem();
            NewDatabaseToolStripMenuItem = new ToolStripMenuItem();
            OpenDatabaseToolStripMenuItem = new ToolStripMenuItem();
            importToolStripMenuItem = new ToolStripMenuItem();
            ButtonExportXML = new Button();
            ButtonImportXML = new Button();
            ((System.ComponentModel.ISupportInitialize)DataGridViewFeedback).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // DataGridViewFeedback
            // 
            DataGridViewFeedback.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewFeedback.Location = new Point(12, 56);
            DataGridViewFeedback.Name = "DataGridViewFeedback";
            DataGridViewFeedback.Size = new Size(776, 382);
            DataGridViewFeedback.TabIndex = 5;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { datenbankToolStripMenuItem, importToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 6;
            menuStrip1.Text = "menuStrip1";
            // 
            // datenbankToolStripMenuItem
            // 
            datenbankToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { NewDatabaseToolStripMenuItem, OpenDatabaseToolStripMenuItem });
            datenbankToolStripMenuItem.Name = "datenbankToolStripMenuItem";
            datenbankToolStripMenuItem.Size = new Size(76, 20);
            datenbankToolStripMenuItem.Text = "Datenbank";
            // 
            // NewDatabaseToolStripMenuItem
            // 
            NewDatabaseToolStripMenuItem.Name = "NewDatabaseToolStripMenuItem";
            NewDatabaseToolStripMenuItem.Size = new Size(120, 22);
            NewDatabaseToolStripMenuItem.Text = "Neu...";
            NewDatabaseToolStripMenuItem.Click += NewDatabaseToolStripMenuItem_Click;
            // 
            // OpenDatabaseToolStripMenuItem
            // 
            OpenDatabaseToolStripMenuItem.Name = "OpenDatabaseToolStripMenuItem";
            OpenDatabaseToolStripMenuItem.Size = new Size(120, 22);
            OpenDatabaseToolStripMenuItem.Text = "Öffnen...";
            OpenDatabaseToolStripMenuItem.Click += OpenDatabaseToolStripMenuItem_Click;
            // 
            // importToolStripMenuItem
            // 
            importToolStripMenuItem.Name = "importToolStripMenuItem";
            importToolStripMenuItem.Size = new Size(55, 20);
            importToolStripMenuItem.Text = "Import";
            importToolStripMenuItem.Click += importToolStripMenuItem_Click;
            // 
            // ButtonExportXML
            // 
            ButtonExportXML.Location = new Point(713, 27);
            ButtonExportXML.Name = "ButtonExportXML";
            ButtonExportXML.Size = new Size(75, 23);
            ButtonExportXML.TabIndex = 7;
            ButtonExportXML.Text = "Export XML";
            ButtonExportXML.UseVisualStyleBackColor = true;
            ButtonExportXML.Click += ButtonExportXML_Click;
            // 
            // ButtonImportXML
            // 
            ButtonImportXML.Location = new Point(627, 27);
            ButtonImportXML.Name = "ButtonImportXML";
            ButtonImportXML.Size = new Size(80, 23);
            ButtonImportXML.TabIndex = 8;
            ButtonImportXML.Text = "Import XML";
            ButtonImportXML.UseVisualStyleBackColor = true;
            ButtonImportXML.Click += ButtonImportXML_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ButtonImportXML);
            Controls.Add(ButtonExportXML);
            Controls.Add(DataGridViewFeedback);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "FormMain";
            Text = "DMARC Report Analyzer";
            ((System.ComponentModel.ISupportInitialize)DataGridViewFeedback).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView DataGridViewFeedback;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem datenbankToolStripMenuItem;
        private ToolStripMenuItem OpenDatabaseToolStripMenuItem;
        private ToolStripMenuItem NewDatabaseToolStripMenuItem;
        private ToolStripMenuItem importToolStripMenuItem;
        private Button ButtonExportXML;
        private Button ButtonImportXML;
    }
}
