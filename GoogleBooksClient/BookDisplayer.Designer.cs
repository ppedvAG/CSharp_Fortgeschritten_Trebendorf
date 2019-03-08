namespace GoogleBooksClient
{
    partial class BookDisplayer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelBookItems = new System.Windows.Forms.FlowLayoutPanel();
            this.labelDescription = new System.Windows.Forms.Label();
            this.checkBoxFavorit = new System.Windows.Forms.CheckBox();
            this.panelBookItems.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.panelBookItems.Controls.Add(this.labelDescription);
            this.panelBookItems.Controls.Add(this.checkBoxFavorit);
            this.panelBookItems.Location = new System.Drawing.Point(3, 0);
            this.panelBookItems.Name = "flowLayoutPanel1";
            this.panelBookItems.Size = new System.Drawing.Size(962, 106);
            this.panelBookItems.TabIndex = 0;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(3, 0);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(51, 20);
            this.labelDescription.TabIndex = 0;
            this.labelDescription.Text = "label1";
            // 
            // checkBoxFavorit
            // 
            this.checkBoxFavorit.AutoSize = true;
            this.checkBoxFavorit.Location = new System.Drawing.Point(60, 3);
            this.checkBoxFavorit.Name = "checkBoxFavorit";
            this.checkBoxFavorit.Size = new System.Drawing.Size(83, 24);
            this.checkBoxFavorit.TabIndex = 1;
            this.checkBoxFavorit.Text = "Favorit";
            this.checkBoxFavorit.UseVisualStyleBackColor = true;
            // 
            // BookDisplayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelBookItems);
            this.Name = "BookDisplayer";
            this.Size = new System.Drawing.Size(968, 109);
            this.panelBookItems.ResumeLayout(false);
            this.panelBookItems.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panelBookItems;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.CheckBox checkBoxFavorit;
    }
}
