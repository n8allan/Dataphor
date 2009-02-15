namespace Alphora.Dataphor.Dataphoria
{
    partial class FreeDataphoria
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
            this.FStatusStrip = new System.Windows.Forms.StatusStrip();
            this.FMainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.FFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FConnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FDisconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FNewScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FOpenFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FOpenFileWithToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FSaveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FLaunchFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FDataphorExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FWarningsErrorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FClearWarningsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LConfigureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FDesignerLibrariesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FDocumentTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FDataphorDocumentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FAlphoraWebSiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FAlphoraDiscussionGroupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FWebDocumentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FAboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.FDataphoriaMainControl = new Alphora.Dataphor.Dataphoria.DataphoriaMainControl();
            this.FileToolStrip = new System.Windows.Forms.ToolStrip();
            this.FNewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.FNewScriptToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.FOpenFileToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.FOpenFileWithToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.FLaunchFormToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.FMainMenuStrip.SuspendLayout();
            this.FToolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.FToolStripContainer.ContentPanel.SuspendLayout();
            this.FToolStripContainer.TopToolStripPanel.SuspendLayout();
            this.FToolStripContainer.SuspendLayout();
            this.FileToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // FStatusStrip
            // 
            this.FStatusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.FStatusStrip.Location = new System.Drawing.Point(0, 0);
            this.FStatusStrip.Name = "FStatusStrip";
            this.FStatusStrip.Size = new System.Drawing.Size(664, 22);
            this.FStatusStrip.TabIndex = 2;
            this.FStatusStrip.Text = "statusStrip1";
            // 
            // FMainMenuStrip
            // 
            this.FMainMenuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.FMainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FFileToolStripMenuItem,
            this.FViewToolStripMenuItem,
            this.LConfigureToolStripMenuItem,
            this.LHelpToolStripMenuItem});
            this.FMainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.FMainMenuStrip.Name = "FMainMenuStrip";
            this.FMainMenuStrip.Size = new System.Drawing.Size(664, 24);
            this.FMainMenuStrip.TabIndex = 3;
            this.FMainMenuStrip.Text = "menuStrip1";
            // 
            // FFileToolStripMenuItem
            // 
            this.FFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FConnectToolStripMenuItem,
            this.FDisconnectToolStripMenuItem,
            this.FNewToolStripMenuItem,
            this.FNewScriptToolStripMenuItem,
            this.FOpenFileToolStripMenuItem,
            this.FOpenFileWithToolStripMenuItem,
            this.FSaveAllToolStripMenuItem,
            this.FLaunchFormToolStripMenuItem,
            this.FExitToolStripMenuItem});
            this.FFileToolStripMenuItem.Name = "FFileToolStripMenuItem";
            this.FFileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FFileToolStripMenuItem.Text = "File";
            // 
            // FConnectToolStripMenuItem
            // 
            this.FConnectToolStripMenuItem.Image = global::Alphora.Dataphor.Dataphoria.MenuImages.Connect;
            this.FConnectToolStripMenuItem.Name = "FConnectToolStripMenuItem";
            this.FConnectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.FConnectToolStripMenuItem.Text = "Connect";
            this.FConnectToolStripMenuItem.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // FDisconnectToolStripMenuItem
            // 
            this.FDisconnectToolStripMenuItem.Name = "FDisconnectToolStripMenuItem";
            this.FDisconnectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.FDisconnectToolStripMenuItem.Text = "Disconnect";
            this.FDisconnectToolStripMenuItem.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // FNewToolStripMenuItem
            // 
            this.FNewToolStripMenuItem.Name = "FNewToolStripMenuItem";
            this.FNewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.FNewToolStripMenuItem.Text = "New";
            this.FNewToolStripMenuItem.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // FNewScriptToolStripMenuItem
            // 
            this.FNewScriptToolStripMenuItem.Name = "FNewScriptToolStripMenuItem";
            this.FNewScriptToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.FNewScriptToolStripMenuItem.Text = "New Script";
            this.FNewScriptToolStripMenuItem.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // FOpenFileToolStripMenuItem
            // 
            this.FOpenFileToolStripMenuItem.Name = "FOpenFileToolStripMenuItem";
            this.FOpenFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.FOpenFileToolStripMenuItem.Text = "Open File";
            this.FOpenFileToolStripMenuItem.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // FOpenFileWithToolStripMenuItem
            // 
            this.FOpenFileWithToolStripMenuItem.Name = "FOpenFileWithToolStripMenuItem";
            this.FOpenFileWithToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.FOpenFileWithToolStripMenuItem.Text = "Open File With";
            // 
            // FSaveAllToolStripMenuItem
            // 
            this.FSaveAllToolStripMenuItem.Name = "FSaveAllToolStripMenuItem";
            this.FSaveAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.FSaveAllToolStripMenuItem.Text = "Save All";
            this.FSaveAllToolStripMenuItem.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // FLaunchFormToolStripMenuItem
            // 
            this.FLaunchFormToolStripMenuItem.Name = "FLaunchFormToolStripMenuItem";
            this.FLaunchFormToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.FLaunchFormToolStripMenuItem.Text = "Launch Form";
            this.FLaunchFormToolStripMenuItem.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // FExitToolStripMenuItem
            // 
            this.FExitToolStripMenuItem.Name = "FExitToolStripMenuItem";
            this.FExitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.FExitToolStripMenuItem.Text = "Exit";
            this.FExitToolStripMenuItem.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // FViewToolStripMenuItem
            // 
            this.FViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FDataphorExplorerToolStripMenuItem,
            this.FWarningsErrorsToolStripMenuItem,
            this.FClearWarningsToolStripMenuItem});
            this.FViewToolStripMenuItem.Name = "FViewToolStripMenuItem";
            this.FViewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.FViewToolStripMenuItem.Text = "View";
            this.FViewToolStripMenuItem.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // FDataphorExplorerToolStripMenuItem
            // 
            this.FDataphorExplorerToolStripMenuItem.Name = "FDataphorExplorerToolStripMenuItem";
            this.FDataphorExplorerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.FDataphorExplorerToolStripMenuItem.Text = "Dataphor Explorer";
            this.FDataphorExplorerToolStripMenuItem.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // FWarningsErrorsToolStripMenuItem
            // 
            this.FWarningsErrorsToolStripMenuItem.Name = "FWarningsErrorsToolStripMenuItem";
            this.FWarningsErrorsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.FWarningsErrorsToolStripMenuItem.Text = "Warnings and Errors";
            this.FWarningsErrorsToolStripMenuItem.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // FClearWarningsToolStripMenuItem
            // 
            this.FClearWarningsToolStripMenuItem.Name = "FClearWarningsToolStripMenuItem";
            this.FClearWarningsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.FClearWarningsToolStripMenuItem.Text = "Clear Warnings";
            this.FClearWarningsToolStripMenuItem.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // LConfigureToolStripMenuItem
            // 
            this.LConfigureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FDesignerLibrariesToolStripMenuItem,
            this.FDocumentTypesToolStripMenuItem});
            this.LConfigureToolStripMenuItem.Name = "LConfigureToolStripMenuItem";
            this.LConfigureToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.LConfigureToolStripMenuItem.Text = "Configure";
            this.LConfigureToolStripMenuItem.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // FDesignerLibrariesToolStripMenuItem
            // 
            this.FDesignerLibrariesToolStripMenuItem.Name = "FDesignerLibrariesToolStripMenuItem";
            this.FDesignerLibrariesToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.FDesignerLibrariesToolStripMenuItem.Text = "Designer Libraries";
            this.FDesignerLibrariesToolStripMenuItem.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // FDocumentTypesToolStripMenuItem
            // 
            this.FDocumentTypesToolStripMenuItem.Name = "FDocumentTypesToolStripMenuItem";
            this.FDocumentTypesToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.FDocumentTypesToolStripMenuItem.Text = "Document Types";
            this.FDocumentTypesToolStripMenuItem.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // LHelpToolStripMenuItem
            // 
            this.LHelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FDataphorDocumentationToolStripMenuItem,
            this.FAlphoraWebSiteToolStripMenuItem,
            this.FAlphoraDiscussionGroupsToolStripMenuItem,
            this.FWebDocumentationToolStripMenuItem,
            this.FAboutToolStripMenuItem});
            this.LHelpToolStripMenuItem.Name = "LHelpToolStripMenuItem";
            this.LHelpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.LHelpToolStripMenuItem.Text = "Help";
            this.LHelpToolStripMenuItem.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // FDataphorDocumentationToolStripMenuItem
            // 
            this.FDataphorDocumentationToolStripMenuItem.Name = "FDataphorDocumentationToolStripMenuItem";
            this.FDataphorDocumentationToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.FDataphorDocumentationToolStripMenuItem.Text = "Dataphor Documentation";
            this.FDataphorDocumentationToolStripMenuItem.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // FAlphoraWebSiteToolStripMenuItem
            // 
            this.FAlphoraWebSiteToolStripMenuItem.Name = "FAlphoraWebSiteToolStripMenuItem";
            this.FAlphoraWebSiteToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.FAlphoraWebSiteToolStripMenuItem.Text = "Alphora WebSite";
            this.FAlphoraWebSiteToolStripMenuItem.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // FAlphoraDiscussionGroupsToolStripMenuItem
            // 
            this.FAlphoraDiscussionGroupsToolStripMenuItem.Name = "FAlphoraDiscussionGroupsToolStripMenuItem";
            this.FAlphoraDiscussionGroupsToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.FAlphoraDiscussionGroupsToolStripMenuItem.Text = "Alphora Discussion Groups";
            this.FAlphoraDiscussionGroupsToolStripMenuItem.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // FWebDocumentationToolStripMenuItem
            // 
            this.FWebDocumentationToolStripMenuItem.Name = "FWebDocumentationToolStripMenuItem";
            this.FWebDocumentationToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.FWebDocumentationToolStripMenuItem.Text = "Web Documentation";
            this.FWebDocumentationToolStripMenuItem.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // FAboutToolStripMenuItem
            // 
            this.FAboutToolStripMenuItem.Name = "FAboutToolStripMenuItem";
            this.FAboutToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.FAboutToolStripMenuItem.Text = "About";
            this.FAboutToolStripMenuItem.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // FToolStripContainer
            // 
            // 
            // FToolStripContainer.BottomToolStripPanel
            // 
            this.FToolStripContainer.BottomToolStripPanel.Controls.Add(this.FStatusStrip);
            // 
            // FToolStripContainer.ContentPanel
            // 
            this.FToolStripContainer.ContentPanel.Controls.Add(this.FDataphoriaMainControl);
            this.FToolStripContainer.ContentPanel.Size = new System.Drawing.Size(664, 436);
            this.FToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.FToolStripContainer.Name = "FToolStripContainer";
            this.FToolStripContainer.Size = new System.Drawing.Size(664, 507);
            this.FToolStripContainer.TabIndex = 6;
            this.FToolStripContainer.Text = "toolStripContainer1";
            // 
            // FToolStripContainer.TopToolStripPanel
            // 
            this.FToolStripContainer.TopToolStripPanel.Controls.Add(this.FMainMenuStrip);
            this.FToolStripContainer.TopToolStripPanel.Controls.Add(this.FileToolStrip);
            // 
            // FDataphoriaMainControl
            // 
            this.FDataphoriaMainControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FDataphoriaMainControl.Location = new System.Drawing.Point(0, 0);
            this.FDataphoriaMainControl.Name = "FDataphoriaMainControl";
            this.FDataphoriaMainControl.Size = new System.Drawing.Size(664, 436);
            this.FDataphoriaMainControl.TabIndex = 0;
            // 
            // FileToolStrip
            // 
            this.FileToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.FileToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FNewToolStripButton,
            this.FNewScriptToolStripButton,
            this.FOpenFileToolStripButton,
            this.FOpenFileWithToolStripButton,
            this.FLaunchFormToolStripButton});
            this.FileToolStrip.Location = new System.Drawing.Point(3, 24);
            this.FileToolStrip.Name = "FileToolStrip";
            this.FileToolStrip.Size = new System.Drawing.Size(127, 25);
            this.FileToolStrip.TabIndex = 4;
            // 
            // FNewToolStripButton
            // 
            this.FNewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FNewToolStripButton.Image = global::Alphora.Dataphor.Dataphoria.MenuImages.New;
            this.FNewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FNewToolStripButton.Name = "FNewToolStripButton";
            this.FNewToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.FNewToolStripButton.Text = "toolStripButton1";
            this.FNewToolStripButton.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // FNewScriptToolStripButton
            // 
            this.FNewScriptToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FNewScriptToolStripButton.Image = global::Alphora.Dataphor.Dataphoria.MenuImages.NewScript;
            this.FNewScriptToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FNewScriptToolStripButton.Name = "FNewScriptToolStripButton";
            this.FNewScriptToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.FNewScriptToolStripButton.Text = "toolStripButton2";
            this.FNewScriptToolStripButton.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // FOpenFileToolStripButton
            // 
            this.FOpenFileToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FOpenFileToolStripButton.Image = global::Alphora.Dataphor.Dataphoria.MenuImages.Open;
            this.FOpenFileToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FOpenFileToolStripButton.Name = "FOpenFileToolStripButton";
            this.FOpenFileToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.FOpenFileToolStripButton.Text = "toolStripButton3";
            this.FOpenFileToolStripButton.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // FOpenFileWithToolStripButton
            // 
            this.FOpenFileWithToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FOpenFileWithToolStripButton.Image = global::Alphora.Dataphor.Dataphoria.MenuImages.OpenWith;
            this.FOpenFileWithToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FOpenFileWithToolStripButton.Name = "FOpenFileWithToolStripButton";
            this.FOpenFileWithToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.FOpenFileWithToolStripButton.Text = "toolStripButton4";
            this.FOpenFileWithToolStripButton.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // FLaunchFormToolStripButton
            // 
            this.FLaunchFormToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FLaunchFormToolStripButton.Image = global::Alphora.Dataphor.Dataphoria.MenuImages.LaunchForm;
            this.FLaunchFormToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FLaunchFormToolStripButton.Name = "FLaunchFormToolStripButton";
            this.FLaunchFormToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.FLaunchFormToolStripButton.Text = "toolStripButton5";
            this.FLaunchFormToolStripButton.Click += new System.EventHandler(this.FMainMenuStrip_ItemClicked);
            // 
            // FreeDataphoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 507);
            this.Controls.Add(this.FToolStripContainer);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.FMainMenuStrip;
            this.Name = "FreeDataphoria";
            this.Text = "FreeDataphoria";
            this.Shown += new System.EventHandler(this.Dataphoria_Shown);
            this.FMainMenuStrip.ResumeLayout(false);
            this.FMainMenuStrip.PerformLayout();
            this.FToolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.FToolStripContainer.BottomToolStripPanel.PerformLayout();
            this.FToolStripContainer.ContentPanel.ResumeLayout(false);
            this.FToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.FToolStripContainer.TopToolStripPanel.PerformLayout();
            this.FToolStripContainer.ResumeLayout(false);
            this.FToolStripContainer.PerformLayout();
            this.FileToolStrip.ResumeLayout(false);
            this.FileToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusStrip FStatusStrip;
        private System.Windows.Forms.MenuStrip FMainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FConnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FDisconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FNewScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FOpenFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FOpenFileWithToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FSaveAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FLaunchFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FDataphorExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FWarningsErrorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FClearWarningsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LConfigureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FDesignerLibrariesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FDocumentTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FDataphorDocumentationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FAlphoraWebSiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FAlphoraDiscussionGroupsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FWebDocumentationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FAboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer FToolStripContainer;
        private System.Windows.Forms.ToolStrip FileToolStrip;
        private System.Windows.Forms.ToolStripButton FNewToolStripButton;
        private System.Windows.Forms.ToolStripButton FNewScriptToolStripButton;
        private System.Windows.Forms.ToolStripButton FOpenFileToolStripButton;
        private System.Windows.Forms.ToolStripButton FOpenFileWithToolStripButton;
        private System.Windows.Forms.ToolStripButton FLaunchFormToolStripButton;
        private DataphoriaMainControl FDataphoriaMainControl;
    }
}
