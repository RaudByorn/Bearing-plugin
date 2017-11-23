using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BearingPlugin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var bear = new BearingParametrs();
            var Kompas3D = new Kompas3D();
            Kompas3D.RunKompas3D();
            Kompas3D.BuildBearing(bear);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
