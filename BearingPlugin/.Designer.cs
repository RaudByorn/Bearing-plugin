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
            this.innerRimRadBox = new System.Windows.Forms.TextBox();
            this.innerRimWidthBox = new System.Windows.Forms.TextBox();
            this.gutterDepthBox = new System.Windows.Forms.TextBox();
            this.bearingWidthLabel = new System.Windows.Forms.Label();
            this.innerRimRadLabel = new System.Windows.Forms.Label();
            this.innerRimWidthLabel = new System.Windows.Forms.Label();
            this.gutterDepthLabel = new System.Windows.Forms.Label();
            this.ballRadBox = new System.Windows.Forms.TextBox();
            this.ballRadLabel = new System.Windows.Forms.Label();
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
            // innerRimRadBox
            // 
            this.innerRimRadBox.Location = new System.Drawing.Point(12, 38);
            this.innerRimRadBox.Name = "innerRimRadBox";
            this.innerRimRadBox.Size = new System.Drawing.Size(100, 20);
            this.innerRimRadBox.TabIndex = 3;
            this.innerRimRadBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.innerRimRadBox_KeyPress);
            // 
            // innerRimWidthBox
            // 
            this.innerRimWidthBox.Location = new System.Drawing.Point(12, 64);
            this.innerRimWidthBox.Name = "innerRimWidthBox";
            this.innerRimWidthBox.Size = new System.Drawing.Size(100, 20);
            this.innerRimWidthBox.TabIndex = 4;
            this.innerRimWidthBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.innerRimWidthBox_KeyPress);
            // 
            // gutterDepthBox
            // 
            this.gutterDepthBox.Location = new System.Drawing.Point(12, 90);
            this.gutterDepthBox.Name = "gutterDepthBox";
            this.gutterDepthBox.Size = new System.Drawing.Size(100, 20);
            this.gutterDepthBox.TabIndex = 5;
            this.gutterDepthBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gutterDepthBox_KeyPress);
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
            this.innerRimRadLabel.Size = new System.Drawing.Size(142, 13);
            this.innerRimRadLabel.TabIndex = 7;
            this.innerRimRadLabel.Text = "Радиус внутреннего обода";
            // 
            // innerRimWidthLabel
            // 
            this.innerRimWidthLabel.AutoSize = true;
            this.innerRimWidthLabel.Location = new System.Drawing.Point(119, 70);
            this.innerRimWidthLabel.Name = "innerRimWidthLabel";
            this.innerRimWidthLabel.Size = new System.Drawing.Size(145, 13);
            this.innerRimWidthLabel.TabIndex = 8;
            this.innerRimWidthLabel.Text = "Ширина внутреннего обода";
            // 
            // gutterDepthLabel
            // 
            this.gutterDepthLabel.AutoSize = true;
            this.gutterDepthLabel.Location = new System.Drawing.Point(119, 97);
            this.gutterDepthLabel.Name = "gutterDepthLabel";
            this.gutterDepthLabel.Size = new System.Drawing.Size(89, 13);
            this.gutterDepthLabel.TabIndex = 9;
            this.gutterDepthLabel.Text = "Глубина желоба";
            // 
            // ballRadBox
            // 
            this.ballRadBox.Location = new System.Drawing.Point(12, 116);
            this.ballRadBox.Name = "ballRadBox";
            this.ballRadBox.Size = new System.Drawing.Size(100, 20);
            this.ballRadBox.TabIndex = 10;
            this.ballRadBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ballRadBox_KeyPress);
            // 
            // ballRadLabel
            // 
            this.ballRadLabel.AutoSize = true;
            this.ballRadLabel.Location = new System.Drawing.Point(118, 119);
            this.ballRadLabel.Name = "ballRadLabel";
            this.ballRadLabel.Size = new System.Drawing.Size(84, 13);
            this.ballRadLabel.TabIndex = 11;
            this.ballRadLabel.Text = "Радиус шарика";
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
            this.Controls.Add(this.ballRadLabel);
            this.Controls.Add(this.ballRadBox);
            this.Controls.Add(this.gutterDepthLabel);
            this.Controls.Add(this.innerRimWidthLabel);
            this.Controls.Add(this.innerRimRadLabel);
            this.Controls.Add(this.bearingWidthLabel);
            this.Controls.Add(this.gutterDepthBox);
            this.Controls.Add(this.innerRimWidthBox);
            this.Controls.Add(this.innerRimRadBox);
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
        private System.Windows.Forms.TextBox innerRimRadBox;
        private System.Windows.Forms.TextBox innerRimWidthBox;
        private System.Windows.Forms.TextBox gutterDepthBox;
        private System.Windows.Forms.Label bearingWidthLabel;
        private System.Windows.Forms.Label innerRimRadLabel;
        private System.Windows.Forms.Label innerRimWidthLabel;
        private System.Windows.Forms.Label gutterDepthLabel;
        private System.Windows.Forms.TextBox ballRadBox;
        private System.Windows.Forms.Label ballRadLabel;
        private System.Windows.Forms.Button TestModParam;
    }
}

