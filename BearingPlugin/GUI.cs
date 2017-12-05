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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double bearingWidth;
            double innerRimDiam;
            double outerRimDiam;


            bearingWidth = Convert.ToDouble(bearingWidthBox.Text);
            innerRimDiam = Convert.ToDouble(innerRimDiamBox.Text);
            outerRimDiam = Convert.ToDouble(outerRimDiamBox.Text);

            BearingParametrs bearing = null;
            bearing = new BearingParametrs(bearingWidth, innerRimDiam, outerRimDiam);

            var Kompas3D = new Kompas3D();
            Kompas3D.RunKompas3D();
            
            Kompas3D.BuildBearing(bearing);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TestModParam_Click(object sender, EventArgs e)
        {
            bearingWidthBox.Text = "3";
            innerRimDiamBox.Text = "3";
            outerRimDiamBox.Text = "8";
        }

        private void bearingWidthBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }

        private void innerRimDiamBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }

        private void outerRimDiamBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }
    }
}
