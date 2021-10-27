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
            this.btnMicrophone = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnWindowInspector = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnQuickstart
            // 
            this.btnQuickstart.Location = new System.Drawing.Point(14, 14);
            this.btnQuickstart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnQuickstart.Name = "btnQuickstart";
            this.btnQuickstart.Size = new System.Drawing.Size(88, 45);
            this.btnQuickstart.TabIndex = 0;
            this.btnQuickstart.Text = "Quickstart";
            this.btnQuickstart.UseVisualStyleBackColor = true;
            this.btnQuickstart.Click += new System.EventHandler(this.btnQuickstart_Click);
            // 
            // btnSimAudio
            // 
            this.btnSimAudio.Location = new System.Drawing.Point(14, 116);
            this.btnSimAudio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSimAudio.Name = "btnSimAudio";
            this.btnSimAudio.Size = new System.Drawing.Size(88, 45);
            this.btnSimAudio.TabIndex = 1;
            this.btnSimAudio.Text = "Simulated Audio";
            this.btnSimAudio.UseVisualStyleBackColor = true;
            this.btnSimAudio.Click += new System.EventHandler(this.btnSimAudio_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(108, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "minimal-case example with 128 points";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(108, 131);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "simulated 48kHz audio data";
            // 
            // btnMicrophone
            // 
            this.btnMicrophone.Location = new System.Drawing.Point(14, 168);
            this.btnMicrophone.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnMicrophone.Name = "btnMicrophone";
            this.btnMicrophone.Size = new System.Drawing.Size(88, 45);
            this.btnMicrophone.TabIndex = 4;
            this.btnMicrophone.Text = "Microphone Demo";
            this.btnMicrophone.UseVisualStyleBackColor = true;
            this.btnMicrophone.Click += new System.EventHandler(this.btnMicrophone_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(108, 183);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "display FFT of sound card audio";
            // 
            // btnWindowInspector
            // 
            this.btnWindowInspector.Location = new System.Drawing.Point(13, 65);
            this.btnWindowInspector.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnWindowInspector.Name = "btnWindowInspector";
            this.btnWindowInspector.Size = new System.Drawing.Size(88, 45);
            this.btnWindowInspector.TabIndex = 6;
            this.btnWindowInspector.Text = "Window Inspector";
            this.btnWindowInspector.UseVisualStyleBackColor = true;
            this.btnWindowInspector.Click += new System.EventHandler(this.btnWindowInspector_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(108, 80);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(209, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "compare time and frequency domains";
            // 
            // FormMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 234);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnMicrophone);
            this.Controls.Add(this.btnWindowInspector);
            this.Controls.Add(this.btnSimAudio);
            this.Controls.Add(this.btnQuickstart);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FftSharp Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnQuickstart;
        private System.Windows.Forms.Button btnSimAudio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnMicrophone;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnWindowInspector;
        private System.Windows.Forms.Label label4;
    }
}