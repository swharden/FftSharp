namespace FftSharp.Demo
{
    partial class FormMicrophone
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
            components = new System.ComponentModel.Container();
            cbDevices = new System.Windows.Forms.ComboBox();
            label1 = new System.Windows.Forms.Label();
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            timer1 = new System.Windows.Forms.Timer(components);
            cbAutoAxis = new System.Windows.Forms.CheckBox();
            cbDecibel = new System.Windows.Forms.CheckBox();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            lblPeak = new System.Windows.Forms.Label();
            cbPeak = new System.Windows.Forms.CheckBox();
            SuspendLayout();
            // 
            // cbDevices
            // 
            cbDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbDevices.FormattingEnabled = true;
            cbDevices.Location = new System.Drawing.Point(14, 29);
            cbDevices.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbDevices.Name = "cbDevices";
            cbDevices.Size = new System.Drawing.Size(140, 23);
            cbDevices.TabIndex = 0;
            cbDevices.SelectedIndexChanged += cbDevices_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(14, 10);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(77, 15);
            label1.TabIndex = 1;
            label1.Text = "Audio Device";
            // 
            // formsPlot1
            // 
            formsPlot1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            formsPlot1.Location = new System.Drawing.Point(14, 60);
            formsPlot1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new System.Drawing.Size(905, 445);
            formsPlot1.TabIndex = 2;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 20;
            timer1.Tick += timer1_Tick;
            // 
            // cbAutoAxis
            // 
            cbAutoAxis.AutoSize = true;
            cbAutoAxis.Checked = true;
            cbAutoAxis.CheckState = System.Windows.Forms.CheckState.Checked;
            cbAutoAxis.Location = new System.Drawing.Point(162, 31);
            cbAutoAxis.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbAutoAxis.Name = "cbAutoAxis";
            cbAutoAxis.Size = new System.Drawing.Size(79, 19);
            cbAutoAxis.TabIndex = 3;
            cbAutoAxis.Text = "Auto-Axis";
            cbAutoAxis.UseVisualStyleBackColor = true;
            cbAutoAxis.CheckedChanged += cbAutoAxis_CheckedChanged;
            // 
            // cbDecibel
            // 
            cbDecibel.AutoSize = true;
            cbDecibel.Location = new System.Drawing.Point(251, 31);
            cbDecibel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbDecibel.Name = "cbDecibel";
            cbDecibel.Size = new System.Drawing.Size(40, 19);
            cbDecibel.TabIndex = 4;
            cbDecibel.Text = "dB";
            cbDecibel.UseVisualStyleBackColor = true;
            cbDecibel.CheckedChanged += cbDecibel_CheckedChanged;
            // 
            // label3
            // 
            label3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            label3.AutoSize = true;
            label3.ForeColor = System.Drawing.SystemColors.ControlDark;
            label3.Location = new System.Drawing.Point(789, 25);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(108, 15);
            label3.TabIndex = 6;
            label3.Text = "Left-click-drag pan";
            // 
            // label4
            // 
            label4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            label4.AutoSize = true;
            label4.ForeColor = System.Drawing.SystemColors.ControlDark;
            label4.Location = new System.Drawing.Point(789, 40);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(126, 15);
            label4.TabIndex = 7;
            label4.Text = "Right-click-drag zoom";
            // 
            // label5
            // 
            label5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            label5.AutoSize = true;
            label5.ForeColor = System.Drawing.SystemColors.ControlDark;
            label5.Location = new System.Drawing.Point(789, 10);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(125, 15);
            label5.TabIndex = 8;
            label5.Text = "Middle-click auto-axis";
            // 
            // lblPeak
            // 
            lblPeak.AutoSize = true;
            lblPeak.Location = new System.Drawing.Point(303, 10);
            lblPeak.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblPeak.Name = "lblPeak";
            lblPeak.Size = new System.Drawing.Size(137, 15);
            lblPeak.TabIndex = 9;
            lblPeak.Text = "Peak Frequency: 1234 Hz";
            // 
            // cbPeak
            // 
            cbPeak.AutoSize = true;
            cbPeak.Location = new System.Drawing.Point(307, 31);
            cbPeak.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbPeak.Name = "cbPeak";
            cbPeak.Size = new System.Drawing.Size(82, 19);
            cbPeak.TabIndex = 10;
            cbPeak.Text = "show peak";
            cbPeak.UseVisualStyleBackColor = true;
            // 
            // FormMicrophone
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(933, 519);
            Controls.Add(lblPeak);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(cbPeak);
            Controls.Add(cbDecibel);
            Controls.Add(cbAutoAxis);
            Controls.Add(formsPlot1);
            Controls.Add(label1);
            Controls.Add(cbDevices);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "FormMicrophone";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "FftSharp Microphone Demo";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox cbDevices;
        private System.Windows.Forms.Label label1;
        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox cbAutoAxis;
        private System.Windows.Forms.CheckBox cbDecibel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblPeak;
        private System.Windows.Forms.CheckBox cbPeak;
    }
}