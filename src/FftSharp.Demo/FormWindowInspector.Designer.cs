namespace FftSharp.Demo
{
    partial class FormWindowInspector
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
            groupBox2 = new System.Windows.Forms.GroupBox();
            lbWindows = new System.Windows.Forms.ListBox();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            groupBox1 = new System.Windows.Forms.GroupBox();
            plotWindow = new ScottPlot.WinForms.FormsPlot();
            groupBox3 = new System.Windows.Forms.GroupBox();
            plotFreq = new ScottPlot.WinForms.FormsPlot();
            rtbDescription = new System.Windows.Forms.RichTextBox();
            groupBox2.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            groupBox2.Controls.Add(lbWindows);
            groupBox2.Location = new System.Drawing.Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(184, 236);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Windows";
            // 
            // lbWindows
            // 
            lbWindows.Dock = System.Windows.Forms.DockStyle.Fill;
            lbWindows.FormattingEnabled = true;
            lbWindows.ItemHeight = 15;
            lbWindows.Location = new System.Drawing.Point(3, 19);
            lbWindows.Name = "lbWindows";
            lbWindows.Size = new System.Drawing.Size(178, 214);
            lbWindows.TabIndex = 0;
            lbWindows.SelectedIndexChanged += lbWindows_SelectedIndexChanged;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new System.Drawing.Size(200, 100);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel2.Controls.Add(groupBox3, 1, 0);
            tableLayoutPanel2.Location = new System.Drawing.Point(202, 12);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new System.Drawing.Size(971, 451);
            tableLayoutPanel2.TabIndex = 3;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(plotWindow);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox1.Location = new System.Drawing.Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(479, 445);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Time Domain";
            // 
            // plotWindow
            // 
            plotWindow.BackColor = System.Drawing.Color.Transparent;
            plotWindow.DisplayScale = 1F;
            plotWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            plotWindow.Location = new System.Drawing.Point(3, 19);
            plotWindow.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            plotWindow.Name = "plotWindow";
            plotWindow.Size = new System.Drawing.Size(473, 423);
            plotWindow.TabIndex = 1;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(plotFreq);
            groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox3.Location = new System.Drawing.Point(488, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(480, 445);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "Frequency Domain";
            // 
            // plotFreq
            // 
            plotFreq.BackColor = System.Drawing.Color.Transparent;
            plotFreq.DisplayScale = 1F;
            plotFreq.Dock = System.Windows.Forms.DockStyle.Fill;
            plotFreq.Location = new System.Drawing.Point(3, 19);
            plotFreq.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            plotFreq.Name = "plotFreq";
            plotFreq.Size = new System.Drawing.Size(474, 423);
            plotFreq.TabIndex = 2;
            // 
            // rtbDescription
            // 
            rtbDescription.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            rtbDescription.BackColor = System.Drawing.SystemColors.Control;
            rtbDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            rtbDescription.Location = new System.Drawing.Point(12, 254);
            rtbDescription.Name = "rtbDescription";
            rtbDescription.Size = new System.Drawing.Size(181, 209);
            rtbDescription.TabIndex = 4;
            rtbDescription.Text = "";
            // 
            // FormWindowInspector
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1185, 475);
            Controls.Add(rtbDescription);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(groupBox2);
            Name = "FormWindowInspector";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "FftSharp Window Viewer";
            groupBox2.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ListBox lbWindows;
        private System.Windows.Forms.GroupBox groupBox1;
        private ScottPlot.WinForms.FormsPlot plotWindow;
        private System.Windows.Forms.GroupBox groupBox3;
        private ScottPlot.WinForms.FormsPlot plotFreq;
        private System.Windows.Forms.RichTextBox rtbDescription;
    }
}