namespace FftSharp.Demo
{
    partial class FormAudio
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
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            plotAudio = new ScottPlot.WinForms.FormsPlot();
            plotKernel = new ScottPlot.WinForms.FormsPlot();
            plotWindowed = new ScottPlot.WinForms.FormsPlot();
            plotFFT = new ScottPlot.WinForms.FormsPlot();
            label1 = new System.Windows.Forms.Label();
            comboBox1 = new System.Windows.Forms.ComboBox();
            label2 = new System.Windows.Forms.Label();
            tbNoise = new System.Windows.Forms.TrackBar();
            cbLog = new System.Windows.Forms.CheckBox();
            richTextBox1 = new System.Windows.Forms.RichTextBox();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbNoise).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(plotFFT, 0, 1);
            tableLayoutPanel1.Location = new System.Drawing.Point(14, 144);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new System.Drawing.Size(1089, 476);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel2.Controls.Add(plotAudio, 0, 0);
            tableLayoutPanel2.Controls.Add(plotKernel, 1, 0);
            tableLayoutPanel2.Controls.Add(plotWindowed, 2, 0);
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new System.Drawing.Size(1083, 222);
            tableLayoutPanel2.TabIndex = 3;
            // 
            // plotAudio
            // 
            plotAudio.BackColor = System.Drawing.Color.Transparent;
            plotAudio.DisplayScale = 1F;
            plotAudio.Dock = System.Windows.Forms.DockStyle.Fill;
            plotAudio.Location = new System.Drawing.Point(5, 3);
            plotAudio.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            plotAudio.Name = "plotAudio";
            plotAudio.Size = new System.Drawing.Size(351, 216);
            plotAudio.TabIndex = 1;
            // 
            // plotKernel
            // 
            plotKernel.BackColor = System.Drawing.Color.Transparent;
            plotKernel.DisplayScale = 1F;
            plotKernel.Location = new System.Drawing.Point(365, 3);
            plotKernel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            plotKernel.Name = "plotKernel";
            plotKernel.Size = new System.Drawing.Size(353, 216);
            plotKernel.TabIndex = 2;
            // 
            // plotWindowed
            // 
            plotWindowed.BackColor = System.Drawing.Color.Transparent;
            plotWindowed.DisplayScale = 1F;
            plotWindowed.Location = new System.Drawing.Point(726, 3);
            plotWindowed.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            plotWindowed.Name = "plotWindowed";
            plotWindowed.Size = new System.Drawing.Size(353, 216);
            plotWindowed.TabIndex = 3;
            // 
            // plotFFT
            // 
            plotFFT.BackColor = System.Drawing.Color.Transparent;
            plotFFT.DisplayScale = 1F;
            plotFFT.Dock = System.Windows.Forms.DockStyle.Fill;
            plotFFT.Location = new System.Drawing.Point(5, 231);
            plotFFT.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            plotFFT.Name = "plotFFT";
            plotFFT.Size = new System.Drawing.Size(1079, 222);
            plotFFT.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            label1.Location = new System.Drawing.Point(14, 17);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(71, 21);
            label1.TabIndex = 1;
            label1.Text = "Window:";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox1.Font = new System.Drawing.Font("Segoe UI", 12F);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new System.Drawing.Point(104, 14);
            comboBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(140, 29);
            comboBox1.TabIndex = 2;
            comboBox1.SelectedIndexChanged += OnSelectedWindow;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 12F);
            label2.Location = new System.Drawing.Point(331, 17);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(53, 21);
            label2.TabIndex = 3;
            label2.Text = "Noise:";
            // 
            // tbNoise
            // 
            tbNoise.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tbNoise.Location = new System.Drawing.Point(382, 18);
            tbNoise.Maximum = 100;
            tbNoise.Name = "tbNoise";
            tbNoise.Size = new System.Drawing.Size(723, 45);
            tbNoise.TabIndex = 4;
            tbNoise.Value = 10;
            tbNoise.KeyUp += tbNoise_KeyUp;
            tbNoise.MouseUp += tbNoise_MouseUp;
            // 
            // cbLog
            // 
            cbLog.AutoSize = true;
            cbLog.Checked = true;
            cbLog.CheckState = System.Windows.Forms.CheckState.Checked;
            cbLog.Font = new System.Drawing.Font("Segoe UI", 12F);
            cbLog.Location = new System.Drawing.Point(251, 16);
            cbLog.Name = "cbLog";
            cbLog.Size = new System.Drawing.Size(73, 25);
            cbLog.TabIndex = 5;
            cbLog.Text = "Log10";
            cbLog.UseVisualStyleBackColor = true;
            cbLog.CheckedChanged += cbLog_CheckedChanged;
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            richTextBox1.BackColor = System.Drawing.SystemColors.Control;
            richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            richTextBox1.Font = new System.Drawing.Font("Segoe UI", 12F);
            richTextBox1.Location = new System.Drawing.Point(14, 49);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new System.Drawing.Size(1091, 92);
            richTextBox1.TabIndex = 6;
            richTextBox1.Text = "";
            // 
            // FormAudio
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1117, 634);
            Controls.Add(richTextBox1);
            Controls.Add(cbLog);
            Controls.Add(label2);
            Controls.Add(comboBox1);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(label1);
            Controls.Add(tbNoise);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "FormAudio";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "FftSharp Demo - Simulated Audio";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tbNoise).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ScottPlot.WinForms.FormsPlot plotFFT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private ScottPlot.WinForms.FormsPlot plotAudio;
        private ScottPlot.WinForms.FormsPlot plotKernel;
        private ScottPlot.WinForms.FormsPlot plotWindowed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar tbNoise;
        private System.Windows.Forms.CheckBox cbLog;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}