namespace MindMap.Views
{
    partial class frmSave
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
            this.btnSave = new System.Windows.Forms.Button();
            this.tbxSave = new System.Windows.Forms.TextBox();
            this.ltwSave = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnSave.Location = new System.Drawing.Point(364, 247);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(96, 36);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbxSave
            // 
            this.tbxSave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxSave.Location = new System.Drawing.Point(11, 251);
            this.tbxSave.Name = "tbxSave";
            this.tbxSave.Size = new System.Drawing.Size(347, 29);
            this.tbxSave.TabIndex = 1;
            // 
            // ltwSave
            // 
            this.ltwSave.HideSelection = false;
            this.ltwSave.Location = new System.Drawing.Point(12, 12);
            this.ltwSave.Name = "ltwSave";
            this.ltwSave.Size = new System.Drawing.Size(460, 212);
            this.ltwSave.TabIndex = 3;
            this.ltwSave.UseCompatibleStateImageBehavior = false;
            // 
            // frmSave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(484, 298);
            this.Controls.Add(this.ltwSave);
            this.Controls.Add(this.tbxSave);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmSave";
            this.Opacity = 0.95D;
            this.Text = "Save";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tbxSave;
        private System.Windows.Forms.ListView ltwSave;
    }
}