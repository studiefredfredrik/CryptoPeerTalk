namespace CryptoPeerTalk
{
    partial class CPT
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPT));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contactsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tryUPnPForwardingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showExternalIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newConversationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeConversationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem,
            this.tabsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(683, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.contactsToolStripMenuItem,
            this.tryUPnPForwardingToolStripMenuItem,
            this.showExternalIPToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.helpToolStripMenuItem.Text = "Menu";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.FileToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
            this.helpToolStripMenuItem1.Text = "Help";
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
            // 
            // contactsToolStripMenuItem
            // 
            this.contactsToolStripMenuItem.Name = "contactsToolStripMenuItem";
            this.contactsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.contactsToolStripMenuItem.Text = "Contact list";
            this.contactsToolStripMenuItem.Click += new System.EventHandler(this.contactsToolStripMenuItem_Click);
            // 
            // tryUPnPForwardingToolStripMenuItem
            // 
            this.tryUPnPForwardingToolStripMenuItem.Name = "tryUPnPForwardingToolStripMenuItem";
            this.tryUPnPForwardingToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.tryUPnPForwardingToolStripMenuItem.Text = "Try UPnP forwarding";
            this.tryUPnPForwardingToolStripMenuItem.Click += new System.EventHandler(this.tryUPnPForwardingToolStripMenuItem_Click);
            // 
            // showExternalIPToolStripMenuItem
            // 
            this.showExternalIPToolStripMenuItem.Name = "showExternalIPToolStripMenuItem";
            this.showExternalIPToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.showExternalIPToolStripMenuItem.Text = "Show external IP";
            this.showExternalIPToolStripMenuItem.Click += new System.EventHandler(this.showExternalIPToolStripMenuItem_Click);
            // 
            // tabsToolStripMenuItem
            // 
            this.tabsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newConversationToolStripMenuItem,
            this.closeConversationToolStripMenuItem});
            this.tabsToolStripMenuItem.Name = "tabsToolStripMenuItem";
            this.tabsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.tabsToolStripMenuItem.Text = "Tabs";
            // 
            // newConversationToolStripMenuItem
            // 
            this.newConversationToolStripMenuItem.Name = "newConversationToolStripMenuItem";
            this.newConversationToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.newConversationToolStripMenuItem.Text = "New conversation";
            this.newConversationToolStripMenuItem.Click += new System.EventHandler(this.newConversationToolStripMenuItem_Click);
            // 
            // closeConversationToolStripMenuItem
            // 
            this.closeConversationToolStripMenuItem.Name = "closeConversationToolStripMenuItem";
            this.closeConversationToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.closeConversationToolStripMenuItem.Text = "Close tab";
            this.closeConversationToolStripMenuItem.Click += new System.EventHandler(this.closeConversationToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Location = new System.Drawing.Point(12, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(659, 335);
            this.tabControl1.TabIndex = 16;
            // 
            // CPT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(683, 374);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "CPT";
            this.Text = "CPT - Crypto Peer Talk";
            this.Activated += new System.EventHandler(this.CPT_Activated);
            this.Deactivate += new System.EventHandler(this.CPT_Deactivate);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem contactsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tabsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newConversationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeConversationToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStripMenuItem tryUPnPForwardingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showExternalIPToolStripMenuItem;
    }
}

