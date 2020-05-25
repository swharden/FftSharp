using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FftSharp.Demo
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }

        private void btnQuickstart_Click(object sender, EventArgs e)
        {
            new FormQuickstart().ShowDialog();
        }

        private void btnSimAudio_Click(object sender, EventArgs e)
        {
            new FormAudio().ShowDialog();
        }
    }
}
