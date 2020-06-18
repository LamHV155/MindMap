namespace MindMap.Views
{
    partial class frmDelete
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
            this.ltwDelete = new System.Windows.Forms.ListView();
            this.btnDetele = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ltwDelete
            // 
            this.ltwDelete.HideSelection = false;
            this.ltwDelete.Location = new System.Drawing.Point(13, 14);
            this.ltwDelete.Name = "ltwDelete";
            this.ltwDelete.Size = new System.Drawing.Size(460, 218);
            this.ltwDelete.TabIndex = 6;
            this.ltwDelete.UseCompatibleStateImageBehavior = false;
            this.ltwDelete.Click += new System.EventHandler(this.ltwDelete_Click);
            // 
            // btnDetele
            // 
            this.btnDetele.BackColor = System.Drawing.Color.SteelBlue;
            this.btnDetele.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetele.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnDetele.Location = new System.Drawing.Point(326, 250);
            this.btnDetele.Name = "btnDetele";
            this.btnDetele.Size = new System.Drawing.Size(96, 36);
            this.btnDetele.TabIndex = 4;
            this.btnDetele.Text = "Delete";
            this.btnDetele.UseVisualStyleBackColor = false;
            this.btnDetele.Click += new System.EventHandler(this.btnDetele_Click);
            // 
            // frmDelete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(484, 298);
            this.Controls.Add(this.ltwDelete);
            this.Controls.Add(this.btnDetele);
            this.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmDelete";
            this.Text = "Delete";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ltwDelete;
        private System.Windows.Forms.Button btnDetele;
    }
}