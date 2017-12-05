namespace BearingPlugin
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.startButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.bearingWidthBox = new System.Windows.Forms.TextBox();
            this.innerRimDiamBox = new System.Windows.Forms.TextBox();
            this.outerRimDiamBox = new System.Windows.Forms.TextBox();
            this.bearingWidthLabel = new System.Windows.Forms.Label();
            this.innerRimRadLabel = new System.Windows.Forms.Label();
            this.innerRimWidthLabel = new System.Windows.Forms.Label();
            this.TestModParam = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(153, 226);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(119, 23);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Построить деталь";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 226);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Выход";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // bearingWidthBox
            // 
            this.bearingWidthBox.Location = new System.Drawing.Point(12, 12);
            this.bearingWidthBox.Name = "bearingWidthBox";
            this.bearingWidthBox.Size = new System.Drawing.Size(100, 20);
            this.bearingWidthBox.TabIndex = 2;
            this.bearingWidthBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.bearingWidthBox_KeyPress);
            // 
            // innerRimDiamBox
            // 
            this.innerRimDiamBox.Location = new System.Drawing.Point(12, 38);
            this.innerRimDiamBox.Name = "innerRimDiamBox";
            this.innerRimDiamBox.Size = new System.Drawing.Size(100, 20);
            this.innerRimDiamBox.TabIndex = 3;
            this.innerRimDiamBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.innerRimDiamBox_KeyPress);
            // 
            // outerRimDiamBox
            // 
            this.outerRimDiamBox.Location = new System.Drawing.Point(12, 64);
            this.outerRimDiamBox.Name = "outerRimDiamBox";
            this.outerRimDiamBox.Size = new System.Drawing.Size(100, 20);
            this.outerRimDiamBox.TabIndex = 4;
            this.outerRimDiamBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.outerRimDiamBox_KeyPress);
            // 
            // bearingWidthLabel
            // 
            this.bearingWidthLabel.AutoSize = true;
            this.bearingWidthLabel.Location = new System.Drawing.Point(118, 15);
            this.bearingWidthLabel.Name = "bearingWidthLabel";
            this.bearingWidthLabel.Size = new System.Drawing.Size(111, 13);
            this.bearingWidthLabel.TabIndex = 6;
            this.bearingWidthLabel.Text = "Ширина подшипника";
            // 
            // innerRimRadLabel
            // 
            this.innerRimRadLabel.AutoSize = true;
            this.innerRimRadLabel.Location = new System.Drawing.Point(118, 41);
            this.innerRimRadLabel.Name = "innerRimRadLabel";
            this.innerRimRadLabel.Size = new System.Drawing.Size(152, 13);
            this.innerRimRadLabel.TabIndex = 7;
            this.innerRimRadLabel.Text = "Диаметр внутреннего обода";
            // 
            // innerRimWidthLabel
            // 
            this.innerRimWidthLabel.AutoSize = true;
            this.innerRimWidthLabel.Location = new System.Drawing.Point(119, 70);
            this.innerRimWidthLabel.Name = "innerRimWidthLabel";
            this.innerRimWidthLabel.Size = new System.Drawing.Size(138, 13);
            this.innerRimWidthLabel.TabIndex = 8;
            this.innerRimWidthLabel.Text = "Диаметр внешнего обода";
            // 
            // TestModParam
            // 
            this.TestModParam.Location = new System.Drawing.Point(12, 197);
            this.TestModParam.Name = "TestModParam";
            this.TestModParam.Size = new System.Drawing.Size(118, 23);
            this.TestModParam.TabIndex = 12;
            this.TestModParam.Text = "Тестовые данные";
            this.TestModParam.UseVisualStyleBackColor = true;
            this.TestModParam.Click += new System.EventHandler(this.TestModParam_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.TestModParam);
            this.Controls.Add(this.innerRimWidthLabel);
            this.Controls.Add(this.innerRimRadLabel);
            this.Controls.Add(this.bearingWidthLabel);
            this.Controls.Add(this.outerRimDiamBox);
            this.Controls.Add(this.innerRimDiamBox);
            this.Controls.Add(this.bearingWidthBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.startButton);
            this.Name = "MainForm";
            this.Text = "Библиотека \"Подшипник\" для Компас-3D";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox bearingWidthBox;
        private System.Windows.Forms.TextBox innerRimDiamBox;
        private System.Windows.Forms.TextBox outerRimDiamBox;
        private System.Windows.Forms.Label bearingWidthLabel;
        private System.Windows.Forms.Label innerRimRadLabel;
        private System.Windows.Forms.Label innerRimWidthLabel;
        private System.Windows.Forms.Button TestModParam;
    }
}

