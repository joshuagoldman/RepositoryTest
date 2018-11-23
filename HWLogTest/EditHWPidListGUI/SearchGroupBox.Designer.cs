namespace EditHWPidListGUI
{
    partial class SearchGroupBox
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
            this.SearchGroups = new System.Windows.Forms.ComboBox();
            this.SearchGroupsText = new System.Windows.Forms.Label();
            this.SearchGroupButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SearchGroups
            // 
            this.SearchGroups.FormattingEnabled = true;
            this.SearchGroups.Items.AddRange(new object[] {
            "DUW_DUL_DUS",
            "Radio",
            "DUG",
            "Baseband",
            "TCU_SIU"});
            this.SearchGroups.Location = new System.Drawing.Point(63, 79);
            this.SearchGroups.Name = "SearchGroups";
            this.SearchGroups.Size = new System.Drawing.Size(121, 24);
            this.SearchGroups.TabIndex = 0;
            this.SearchGroups.SelectedIndexChanged += new System.EventHandler(this.SearchGroups_SelectedIndexChanged);
            // 
            // SearchGroupsText
            // 
            this.SearchGroupsText.AutoSize = true;
            this.SearchGroupsText.Location = new System.Drawing.Point(65, 33);
            this.SearchGroupsText.Name = "SearchGroupsText";
            this.SearchGroupsText.Size = new System.Drawing.Size(119, 17);
            this.SearchGroupsText.TabIndex = 1;
            this.SearchGroupsText.Text = "Enter SearchKey:";
            this.SearchGroupsText.Click += new System.EventHandler(this.SearchGroupText_Click);
            // 
            // SearchGroupButton
            // 
            this.SearchGroupButton.Location = new System.Drawing.Point(86, 126);
            this.SearchGroupButton.Name = "SearchGroupButton";
            this.SearchGroupButton.Size = new System.Drawing.Size(75, 23);
            this.SearchGroupButton.TabIndex = 2;
            this.SearchGroupButton.Text = "Ok";
            this.SearchGroupButton.UseVisualStyleBackColor = true;
            this.SearchGroupButton.Click += new System.EventHandler(this.SearchGroupButton_Click);
            // 
            // SearchGroupBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 179);
            this.Controls.Add(this.SearchGroupButton);
            this.Controls.Add(this.SearchGroupsText);
            this.Controls.Add(this.SearchGroups);
            this.Name = "SearchGroupBox";
            this.Text = "Search Group";
            this.Load += new System.EventHandler(this.SearchGroupBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox SearchGroups;
        private System.Windows.Forms.Label SearchGroupsText;
        private System.Windows.Forms.Button SearchGroupButton;
    }
}