using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace BearingPlugin
{
    public partial class MainForm : Form
    {
        /// <summary>
        ///     Объект обертки Компас
        /// </summary>
        private Kompas3D _kompas3D = new Kompas3D();

        /// <summary>
        /// Список с ошибками
        /// </summary>
        private readonly List<string> _errorList = new List<string>();

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Построение детали
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuildBearingButton(object sender, EventArgs e)
        {
            ValidateTextFields();
            if (_errorList.Count != 0)
            { 
                return;
            }
            _errorList.Clear();

            RollingElementForm rollingElementForm = RollingElementForm.Ball;
            if (RollingElementBall.Checked)
            {
                rollingElementForm = RollingElementForm.Ball;
            }
            if (RollingElementCylinder.Checked)
            {
                rollingElementForm = RollingElementForm.Cylinder;
            }

            double bearingWidth = Convert.ToDouble(bearingWidthBox.Text);
            double innerRimDiam = Convert.ToDouble(innerRimDiamBox.Text);
            double outerRimDiam = Convert.ToDouble(outerRimDiamBox.Text);
            double rimsThickness = Convert.ToDouble(rimsThicknessBox.Text);
            double ballDiam = Convert.ToDouble(rollingElementDiam.Text);

            BearingParametrs bearing = null;
            try
            {
                bearing = new BearingParametrs(rollingElementForm ,bearingWidth, innerRimDiam, outerRimDiam, rimsThickness, ballDiam);
            }
            catch (ArgumentException exception)
            {
                _errorList.Add(exception.Message);
                ShowErrors();
                return;
            }
            _kompas3D.BuildBearing(bearing);
        }

        /// <summary>
        /// Закрыть плагин
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClosePluginButton(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Проверка полей значений подшипника
        /// </summary>
        private void ValidateTextFields()
        {
            _errorList.Clear();
            
                foreach (TextBox tb in  Controls.OfType<TextBox>())
                {
                    if (tb.TextLength == 0 ||
                    double.Parse(tb.Text) <= 0)
                    {
                        _errorList.Add("Размер не может быть отрицательным или равен нулю!");
                        break;
                    }
                }
            ShowErrors();
        }

        /// <summary>
        /// Вывод ошибок
        /// </summary>
        private void ShowErrors()
        {
            if (_errorList.Count != 0)
            {
                var errors = @"";

                for (var i = 0; i < _errorList.Count; i++)
                {
                    errors += _errorList[i] + "\n";
                }

                MessageBox.Show(errors, @"Данные заполенены не верно!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Проверка введеных данных в поля на правильность
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                return;
            }
            if (e.KeyChar == ',' || e.KeyChar == (char)Keys.Back)
            {
                if (e.KeyChar == (char)Keys.Back)
                {
                }
                else if ((sender as TextBox).Text.Contains(","))
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}