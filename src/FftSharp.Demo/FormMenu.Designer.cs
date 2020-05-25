namespace FftSharp.Demo
{
    partial class FormMenu
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
            this.btnQuickstart = new System.Windows.Forms.Button();
            this.btnSimAudio = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnQuickstart
            // 
            this.btnQuickstart.Location = new System.Drawing.Point(12, 12);
            this.btnQuickstart.Name = "btnQuickstart";
            this.btnQuickstart.Size = new System.Drawing.Size(75, 39);
            this.btnQuickstart.TabIndex = 0;
            this.btnQuickstart.Text = "Quickstart";
            this.btnQuickstart.UseVisualStyleBackColor = true;
            this.btnQuickstart.Click += new System.EventHandler(this.btnQuickstart_Click);
            // 
            // btnSimAudio
            // 
            this.btnSimAudio.Location = new System.Drawing.Point(12, 57);
            this.btnSimAudio.Name = "btnSimAudio";
            this.btnSimAudio.Size = new System.Drawing.Size(75, 39);
            this.btnSimAudio.TabIndex = 1;
            this.btnSimAudio.Text = "Simulated Audio";
            this.btnSimAudio.UseVisualStyleBackColor = true;
            this.btnSimAudio.Click += new System.EventHandler(this.btnSimAudio_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(93, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "minimal-case example with 128 points";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(93, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "simulated 48kHz audio data";
            // 
            // FormMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 123);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSimAudio);
            this.Controls.Add(this.btnQuickstart);
            this.Name = "FormMenu";
            this.Text = "FftSharp Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnQuickstart;
        private System.Windows.Forms.Button btnSimAudio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}