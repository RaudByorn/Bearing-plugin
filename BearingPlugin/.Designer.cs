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
            this.innerRimDiamLabel = new System.Windows.Forms.Label();
            this.outerRimDiamLabel = new System.Windows.Forms.Label();
            this.rollingElementDiam = new System.Windows.Forms.TextBox();
            this.rimsThicknessBox = new System.Windows.Forms.TextBox();
            this.ballDiamLabel = new System.Windows.Forms.Label();
            this.rimsThicknessLabel = new System.Windows.Forms.Label();
            this.RollingElementBall = new System.Windows.Forms.RadioButton();
            this.RollingElementCylinder = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(168, 240);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(119, 23);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Построить деталь";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.BuildBearingButton);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 240);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Выход";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.ClosePluginButton);
            // 
            // bearingWidthBox
            // 
            this.bearingWidthBox.Location = new System.Drawing.Point(6, 19);
            this.bearingWidthBox.Name = "bearingWidthBox";
            this.bearingWidthBox.Size = new System.Drawing.Size(100, 20);
            this.bearingWidthBox.TabIndex = 2;
            this.bearingWidthBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress);
            // 
            // innerRimDiamBox
            // 
            this.innerRimDiamBox.Location = new System.Drawing.Point(6, 45);
            this.innerRimDiamBox.Name = "innerRimDiamBox";
            this.innerRimDiamBox.Size = new System.Drawing.Size(100, 20);
            this.innerRimDiamBox.TabIndex = 3;
            this.innerRimDiamBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress);
            // 
            // outerRimDiamBox
            // 
            this.outerRimDiamBox.Location = new System.Drawing.Point(6, 71);
            this.outerRimDiamBox.Name = "outerRimDiamBox";
            this.outerRimDiamBox.Size = new System.Drawing.Size(100, 20);
            this.outerRimDiamBox.TabIndex = 4;
            this.outerRimDiamBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress);
            // 
            // bearingWidthLabel
            // 
            this.bearingWidthLabel.AutoSize = true;
            this.bearingWidthLabel.Location = new System.Drawing.Point(112, 22);
            this.bearingWidthLabel.Name = "bearingWidthLabel";
            this.bearingWidthLabel.Size = new System.Drawing.Size(111, 13);
            this.bearingWidthLabel.TabIndex = 6;
            this.bearingWidthLabel.Text = "Ширина подшипника";
            // 
            // innerRimDiamLabel
            // 
            this.innerRimDiamLabel.AutoSize = true;
            this.innerRimDiamLabel.Location = new System.Drawing.Point(112, 48);
            this.innerRimDiamLabel.Name = "innerRimDiamLabel";
            this.innerRimDiamLabel.Size = new System.Drawing.Size(152, 13);
            this.innerRimDiamLabel.TabIndex = 7;
            this.innerRimDiamLabel.Text = "Диаметр внутреннего обода";
            // 
            // outerRimDiamLabel
            // 
            this.outerRimDiamLabel.AutoSize = true;
            this.outerRimDiamLabel.Location = new System.Drawing.Point(112, 74);
            this.outerRimDiamLabel.Name = "outerRimDiamLabel";
            this.outerRimDiamLabel.Size = new System.Drawing.Size(138, 13);
            this.outerRimDiamLabel.TabIndex = 8;
            this.outerRimDiamLabel.Text = "Диаметр внешнего обода";
            // 
            // rollingElementDiam
            // 
            this.rollingElementDiam.Location = new System.Drawing.Point(6, 97);
            this.rollingElementDiam.Name = "rollingElementDiam";
            this.rollingElementDiam.Size = new System.Drawing.Size(100, 20);
            this.rollingElementDiam.TabIndex = 13;
            this.rollingElementDiam.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress);
            // 
            // rimsThicknessBox
            // 
            this.rimsThicknessBox.Location = new System.Drawing.Point(6, 123);
            this.rimsThicknessBox.Name = "rimsThicknessBox";
            this.rimsThicknessBox.Size = new System.Drawing.Size(100, 20);
            this.rimsThicknessBox.TabIndex = 14;
            this.rimsThicknessBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress);
            // 
            // ballDiamLabel
            // 
            this.ballDiamLabel.AutoSize = true;
            this.ballDiamLabel.Location = new System.Drawing.Point(112, 100);
            this.ballDiamLabel.Name = "ballDiamLabel";
            this.ballDiamLabel.Size = new System.Drawing.Size(149, 13);
            this.ballDiamLabel.TabIndex = 15;
            this.ballDiamLabel.Text = "Диаметр элемента качения";
            // 
            // rimsThicknessLabel
            // 
            this.rimsThicknessLabel.AutoSize = true;
            this.rimsThicknessLabel.Location = new System.Drawing.Point(112, 126);
            this.rimsThicknessLabel.Name = "rimsThicknessLabel";
            this.rimsThicknessLabel.Size = new System.Drawing.Size(92, 13);
            this.rimsThicknessLabel.TabIndex = 16;
            this.rimsThicknessLabel.Text = "Толщина ободов";
            // 
            // RollingElementBall
            // 
            this.RollingElementBall.AutoSize = true;
            this.RollingElementBall.Location = new System.Drawing.Point(6, 41);
            this.RollingElementBall.Name = "RollingElementBall";
            this.RollingElementBall.Size = new System.Drawing.Size(58, 17);
            this.RollingElementBall.TabIndex = 19;
            this.RollingElementBall.Text = "Шарик";
            this.RollingElementBall.UseVisualStyleBackColor = true;
            // 
            // RollingElementCylinder
            // 
            this.RollingElementCylinder.AutoSize = true;
            this.RollingElementCylinder.Location = new System.Drawing.Point(6, 19);
            this.RollingElementCylinder.Name = "RollingElementCylinder";
            this.RollingElementCylinder.Size = new System.Drawing.Size(69, 17);
            this.RollingElementCylinder.TabIndex = 20;
            this.RollingElementCylinder.Text = "Цилиндр";
            this.RollingElementCylinder.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bearingWidthBox);
            this.groupBox1.Controls.Add(this.bearingWidthLabel);
            this.groupBox1.Controls.Add(this.innerRimDiamBox);
            this.groupBox1.Controls.Add(this.rimsThicknessLabel);
            this.groupBox1.Controls.Add(this.innerRimDiamLabel);
            this.groupBox1.Controls.Add(this.rimsThicknessBox);
            this.groupBox1.Controls.Add(this.ballDiamLabel);
            this.groupBox1.Controls.Add(this.outerRimDiamBox);
            this.groupBox1.Controls.Add(this.outerRimDiamLabel);
            this.groupBox1.Controls.Add(this.rollingElementDiam);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(275, 153);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры подшипника";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RollingElementCylinder);
            this.groupBox2.Controls.Add(this.RollingElementBall);
            this.groupBox2.Location = new System.Drawing.Point(12, 170);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(275, 64);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Форма элемента качения";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 272);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.startButton);
            this.Name = "MainForm";
            this.Text = "Библиотека \"Подшипник\" для Компас-3D";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox bearingWidthBox;
        private System.Windows.Forms.TextBox innerRimDiamBox;
        private System.Windows.Forms.TextBox outerRimDiamBox;
        private System.Windows.Forms.Label bearingWidthLabel;
        private System.Windows.Forms.Label innerRimDiamLabel;
        private System.Windows.Forms.Label outerRimDiamLabel;
        private System.Windows.Forms.TextBox rollingElementDiam;
        private System.Windows.Forms.TextBox rimsThicknessBox;
        private System.Windows.Forms.Label ballDiamLabel;
        private System.Windows.Forms.Label rimsThicknessLabel;
        private System.Windows.Forms.RadioButton RollingElementBall;
        private System.Windows.Forms.RadioButton RollingElementCylinder;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

