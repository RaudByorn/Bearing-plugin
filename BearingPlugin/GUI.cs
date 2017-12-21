using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

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

        public MainForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Построение детали
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ValidateTextFields();
            _errorList.Clear();

            double bearingWidth = Convert.ToDouble(bearingWidthBox.Text);
            double innerRimDiam = Convert.ToDouble(innerRimDiamBox.Text);
            double outerRimDiam = Convert.ToDouble(outerRimDiamBox.Text);
            double rimsThickness = Convert.ToDouble(rimsThicknessBox.Text);
            double ballDiam = Convert.ToDouble(ballDiamBox.Text);


            BearingParametrs bearing = null;
            try
            {
                bearing = new BearingParametrs(bearingWidth, innerRimDiam, outerRimDiam, rimsThickness, ballDiam);
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
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Тестовые параметры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestModParam_Click(object sender, EventArgs e)
        {
            bearingWidthBox.Text = "3";
            innerRimDiamBox.Text = "3";
            outerRimDiamBox.Text = "8";
            rimsThicknessBox.Text = "0,7";
            ballDiamBox.Text = "1,6";
            
        }
        /// <summary>
        /// Проверка полей значений подшипника
        /// </summary>
        private void ValidateTextFields()
        {
            _errorList.Clear();

            if (bearingWidthBox.TextLength == 0 ||
                double.Parse(bearingWidthBox.Text) <= 0)
            {
                bearingWidthLabel.ForeColor = Color.Brown;

                _errorList.Add("Не указана ширина подшипника!");
            }
            else
            {
                bearingWidthLabel.ForeColor = Color.Black;
            }

            if (innerRimDiamBox.TextLength == 0 ||
                double.Parse(innerRimDiamBox.Text) <= 0)
            {
                innerRimDiamLabel.ForeColor = Color.Brown;
                _errorList.Add("Не указан диаметр внутреннего кольца!");
            }
            else
            {
                innerRimDiamLabel.ForeColor = Color.Black;
            }

            if (outerRimDiamBox.TextLength == 0 ||
                double.Parse(outerRimDiamBox.Text) <= 0)
            {
                outerRimDiamLabel.ForeColor = Color.Brown;
                _errorList.Add("Не указан диаметр внешнего кольца!");
            }
            else
            {
                outerRimDiamLabel.ForeColor = Color.Black;
            }

            if (rimsThicknessBox.TextLength == 0 ||
                double.Parse(rimsThicknessBox.Text) <= 0)
            {
                rimsThicknessLabel.ForeColor = Color.Brown;
                _errorList.Add("Не указана толщина колец!");
            }
            else
            {
                rimsThicknessLabel.ForeColor = Color.Black;
            }

            if (ballDiamBox.TextLength == 0 ||
                double.Parse(ballDiamBox.Text) <= 0)
            {
                ballDiamLabel.ForeColor = Color.Brown;
                _errorList.Add("Не указан диаметр шариков!");
            }
            else
            {
                ballDiamLabel.ForeColor = Color.Black;
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
        /// <param name="e"></param>
        /// <param name="text"></param>
        private static void ValidateTextBoxForNumeric(KeyPressEventArgs e, TextBoxBase text)
        {
            if (char.IsDigit(e.KeyChar)) return;
            if (e.KeyChar == ',' || e.KeyChar == (char)Keys.Back)
            {
                if (e.KeyChar == (char)Keys.Back)
                {
                }
                else if (text.Text.Contains(","))
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// Обработчик события нажатия на кнопки в текстовом поле
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bearingWidthBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidateTextBoxForNumeric(e, (TextBoxBase)sender);
        }
        /// <summary>
        /// Обработчик события нажатия на кнопки в текстовом поле
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void innerRimDiamBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidateTextBoxForNumeric(e, (TextBoxBase)sender);
        }
        /// <summary>
        /// Обработчик события нажатия на кнопки в текстовом поле
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void outerRimDiamBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidateTextBoxForNumeric(e, (TextBoxBase)sender);
        }
        /// <summary>
        /// Обработчик события нажатия на кнопки в текстовом поле
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ballDiamBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidateTextBoxForNumeric(e, (TextBoxBase)sender);
        }
        /// <summary>
        /// Обработчик события нажатия на кнопки в текстовом поле
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rimsThicknessBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidateTextBoxForNumeric(e, (TextBoxBase)sender);
        }
    }
}
