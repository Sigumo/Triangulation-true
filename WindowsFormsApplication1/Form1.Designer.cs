namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.Open = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cleanButton = new System.Windows.Forms.Button();
            this.DivImage = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.Triangulation = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.Draw = new System.Windows.Forms.Button();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.triangulationPoints = new System.Windows.Forms.PictureBox();
            this.fragmentPic = new System.Windows.Forms.PictureBox();
            this.cutButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.triangulationPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fragmentPic)).BeginInit();
            this.SuspendLayout();
            // 
            // Open
            // 
            this.Open.Location = new System.Drawing.Point(12, 12);
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(82, 36);
            this.Open.TabIndex = 0;
            this.Open.Text = "Open";
            this.Open.UseVisualStyleBackColor = true;
            this.Open.Click += new System.EventHandler(this.Open_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(276, 10);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(82, 36);
            this.ExitButton.TabIndex = 2;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBox3.Location = new System.Drawing.Point(706, 386);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(256, 256);
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(100, 11);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(82, 36);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // cleanButton
            // 
            this.cleanButton.Enabled = false;
            this.cleanButton.Location = new System.Drawing.Point(188, 11);
            this.cleanButton.Name = "cleanButton";
            this.cleanButton.Size = new System.Drawing.Size(82, 35);
            this.cleanButton.TabIndex = 9;
            this.cleanButton.Text = "Clean";
            this.cleanButton.UseVisualStyleBackColor = true;
            this.cleanButton.Click += new System.EventHandler(this.cleanButton_Click);
            // 
            // DivImage
            // 
            this.DivImage.Location = new System.Drawing.Point(540, 329);
            this.DivImage.Name = "DivImage";
            this.DivImage.Size = new System.Drawing.Size(96, 51);
            this.DivImage.TabIndex = 11;
            this.DivImage.Text = "Разбить изображение";
            this.DivImage.UseVisualStyleBackColor = true;
            this.DivImage.Click += new System.EventHandler(this.DivImage_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(452, 345);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(82, 20);
            this.numericUpDown1.TabIndex = 12;
            this.numericUpDown1.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // Triangulation
            // 
            this.Triangulation.AllowDrop = true;
            this.Triangulation.Location = new System.Drawing.Point(781, 329);
            this.Triangulation.Name = "Triangulation";
            this.Triangulation.Size = new System.Drawing.Size(101, 51);
            this.Triangulation.TabIndex = 13;
            this.Triangulation.Text = "Триангуляция Делоне";
            this.Triangulation.UseVisualStyleBackColor = true;
            this.Triangulation.Click += new System.EventHandler(this.Treangulation_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBox1.Location = new System.Drawing.Point(57, 67);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 256);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBox2.Location = new System.Drawing.Point(380, 67);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(256, 256);
            this.pictureBox2.TabIndex = 15;
            this.pictureBox2.TabStop = false;
            // 
            // Draw
            // 
            this.Draw.Location = new System.Drawing.Point(781, 648);
            this.Draw.Name = "Draw";
            this.Draw.Size = new System.Drawing.Size(101, 51);
            this.Draw.TabIndex = 17;
            this.Draw.Text = "Раскрасить";
            this.Draw.UseVisualStyleBackColor = true;
            this.Draw.Click += new System.EventHandler(this.Draw_Click);
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBox5.Location = new System.Drawing.Point(706, 67);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(256, 256);
            this.pictureBox5.TabIndex = 19;
            this.pictureBox5.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(364, 348);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Порог яркости";
            // 
            // triangulationPoints
            // 
            this.triangulationPoints.BackColor = System.Drawing.Color.Gainsboro;
            this.triangulationPoints.Location = new System.Drawing.Point(380, 386);
            this.triangulationPoints.Name = "triangulationPoints";
            this.triangulationPoints.Size = new System.Drawing.Size(256, 256);
            this.triangulationPoints.TabIndex = 22;
            this.triangulationPoints.TabStop = false;
            // 
            // fragmentPic
            // 
            this.fragmentPic.BackColor = System.Drawing.Color.Gainsboro;
            this.fragmentPic.Location = new System.Drawing.Point(57, 386);
            this.fragmentPic.Name = "fragmentPic";
            this.fragmentPic.Size = new System.Drawing.Size(256, 256);
            this.fragmentPic.TabIndex = 23;
            this.fragmentPic.TabStop = false;
            // 
            // cutButton
            // 
            this.cutButton.Location = new System.Drawing.Point(100, 329);
            this.cutButton.Name = "cutButton";
            this.cutButton.Size = new System.Drawing.Size(182, 51);
            this.cutButton.TabIndex = 24;
            this.cutButton.Text = "Выделить фрагмент";
            this.cutButton.UseVisualStyleBackColor = true;
            this.cutButton.Click += new System.EventHandler(this.cutButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 741);
            this.Controls.Add(this.cutButton);
            this.Controls.Add(this.fragmentPic);
            this.Controls.Add(this.triangulationPoints);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.Draw);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Triangulation);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.DivImage);
            this.Controls.Add(this.cleanButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.Open);
            this.Name = "Form1";
            this.RightToLeftLayout = true;
            this.Text = "ImageChanger";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.triangulationPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fragmentPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Open;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button cleanButton;
        private System.Windows.Forms.Button DivImage;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button Triangulation;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button Draw;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox triangulationPoints;
        private System.Windows.Forms.PictureBox fragmentPic;
        private System.Windows.Forms.Button cutButton;




    }
}

