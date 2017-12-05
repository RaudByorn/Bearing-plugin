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
            double innerRimRad;
            double innerRimWidth;
            double gutterDepth;
            double ballRad;

            bearingWidth = Convert.ToDouble(bearingWidthBox.Text);
            innerRimRad = Convert.ToDouble(innerRimRadBox.Text);
            innerRimWidth = Convert.ToDouble(innerRimWidthBox.Text);
            gutterDepth = Convert.ToDouble(gutterDepthBox.Text);
            ballRad = Convert.ToDouble(ballRadBox.Text);

            BearingParametrs bearing = null;
            bearing = new BearingParametrs(bearingWidth, innerRimRad, innerRimWidth, gutterDepth, ballRad);

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
            /*bearingWidthBox.Text = "4";
            innerRimRadBox.Text = "5";
            innerRimWidthBox.Text = "3";
            gutterDepthBox.Text = "0,5";
            ballRadBox.Text = "1";*/
        }

        private void bearingWidthBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }

        private void innerRimRadBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }

        private void innerRimWidthBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }

        private void gutterDepthBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }

        private void ballRadBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }
    }
}
