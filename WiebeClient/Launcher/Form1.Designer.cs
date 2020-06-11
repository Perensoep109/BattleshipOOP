namespace Launcher
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
            this.btn_join = new System.Windows.Forms.Button();
            this.txt_gameID = new System.Windows.Forms.TextBox();
            this.lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_join
            // 
            this.btn_join.Location = new System.Drawing.Point(12, 12);
            this.btn_join.Name = "btn_join";
            this.btn_join.Size = new System.Drawing.Size(75, 23);
            this.btn_join.TabIndex = 0;
            this.btn_join.Text = "Join Game";
            this.btn_join.UseVisualStyleBackColor = true;
            this.btn_join.Click += new System.EventHandler(this.btn_join_Click);
            // 
            // txt_gameID
            // 
            this.txt_gameID.Location = new System.Drawing.Point(149, 14);
            this.txt_gameID.Name = "txt_gameID";
            this.txt_gameID.Size = new System.Drawing.Size(100, 20);
            this.txt_gameID.TabIndex = 1;
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(94, 17);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(49, 13);
            this.lbl.TabIndex = 2;
            this.lbl.Text = "Game ID";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 75);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.txt_gameID);
            this.Controls.Add(this.btn_join);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_join;
        private System.Windows.Forms.TextBox txt_gameID;
        private System.Windows.Forms.Label lbl;
    }
}

