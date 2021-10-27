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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.plotAudio = new ScottPlot.FormsPlot();
            this.plotKernel = new ScottPlot.FormsPlot();
            this.plotWindowed = new ScottPlot.FormsPlot();
            this.plotFFT = new ScottPlot.FormsPlot();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbNoise = new System.Windows.Forms.TrackBar();
            this.cbLog = new System.Windows.Forms.CheckBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbNoise)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.plotFFT, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(14, 144);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1089, 476);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.plotAudio, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.plotKernel, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.plotWindowed, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1083, 222);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // plotAudio
            // 
            this.plotAudio.BackColor = System.Drawing.Color.Transparent;
            this.plotAudio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotAudio.Location = new System.Drawing.Point(5, 3);
            this.plotAudio.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.plotAudio.Name = "plotAudio";
            this.plotAudio.Size = new System.Drawing.Size(351, 216);
            this.plotAudio.TabIndex = 1;
            // 
            // plotKernel
            // 
            this.plotKernel.BackColor = System.Drawing.Color.Transparent;
            this.plotKernel.Location = new System.Drawing.Point(365, 3);
            this.plotKernel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.plotKernel.Name = "plotKernel";
            this.plotKernel.Size = new System.Drawing.Size(353, 216);
            this.plotKernel.TabIndex = 2;
            // 
            // plotWindowed
            // 
            this.plotWindowed.BackColor = System.Drawing.Color.Transparent;
            this.plotWindowed.Location = new System.Drawing.Point(726, 3);
            this.plotWindowed.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.plotWindowed.Name = "plotWindowed";
            this.plotWindowed.Size = new System.Drawing.Size(353, 216);
            this.plotWindowed.TabIndex = 3;
            // 
            // plotFFT
            // 
            this.plotFFT.BackColor = System.Drawing.Color.Transparent;
            this.plotFFT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotFFT.Location = new System.Drawing.Point(5, 231);
            this.plotFFT.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.plotFFT.Name = "plotFFT";
            this.plotFFT.Size = new System.Drawing.Size(1079, 222);
            this.plotFFT.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(14, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Window:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(104, 14);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(140, 29);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.OnSelectedWindow);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(331, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Noise:";
            // 
            // tbNoise
            // 
            this.tbNoise.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNoise.Location = new System.Drawing.Point(382, 18);
            this.tbNoise.Maximum = 100;
            this.tbNoise.Name = "tbNoise";
            this.tbNoise.Size = new System.Drawing.Size(723, 45);
            this.tbNoise.TabIndex = 4;
            this.tbNoise.Value = 10;
            this.tbNoise.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbNoise_KeyUp);
            this.tbNoise.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbNoise_MouseUp);
            // 
            // cbLog
            // 
            this.cbLog.AutoSize = true;
            this.cbLog.Checked = true;
            this.cbLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLog.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbLog.Location = new System.Drawing.Point(251, 16);
            this.cbLog.Name = "cbLog";
            this.cbLog.Size = new System.Drawing.Size(73, 25);
            this.cbLog.TabIndex = 5;
            this.cbLog.Text = "Log10";
            this.cbLog.UseVisualStyleBackColor = true;
            this.cbLog.CheckedChanged += new System.EventHandler(this.cbLog_CheckedChanged);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.richTextBox1.Location = new System.Drawing.Point(14, 49);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1091, 92);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            // 
            // FormAudio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1117, 634);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.cbLog);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbNoise);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormAudio";
            this.Text = "FftSharp Demo - Simulated Audio";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbNoise)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ScottPlot.FormsPlot plotFFT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private ScottPlot.FormsPlot plotAudio;
        private ScottPlot.FormsPlot plotKernel;
        private ScottPlot.FormsPlot plotWindowed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar tbNoise;
        private System.Windows.Forms.CheckBox cbLog;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}