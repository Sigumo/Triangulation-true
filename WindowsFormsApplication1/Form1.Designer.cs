namespace Triangulation
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
            this.paintedPicture = new System.Windows.Forms.PictureBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cleanButton = new System.Windows.Forms.Button();
            this.DivideImage = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.Triangulation = new System.Windows.Forms.Button();
            this.originalPicture = new System.Windows.Forms.PictureBox();
            this.poligonalNet = new System.Windows.Forms.PictureBox();
            this.Draw = new System.Windows.Forms.Button();
            this.triangulatedPicture = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.triangulationPoints = new System.Windows.Forms.PictureBox();
            this.fragmentPicture = new System.Windows.Forms.PictureBox();
            this.cutButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.paintedPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.originalPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.poligonalNet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.triangulatedPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.triangulationPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fragmentPicture)).BeginInit();
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
            // paintedPicture
            // 
            this.paintedPicture.BackColor = System.Drawing.Color.Gainsboro;
            this.paintedPicture.Location = new System.Drawing.Point(706, 386);
            this.paintedPicture.Name = "paintedPicture";
            this.paintedPicture.Size = new System.Drawing.Size(256, 256);
            this.paintedPicture.TabIndex = 5;
            this.paintedPicture.TabStop = false;
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
            this.cleanButton.Location = new System.Drawing.Point(188, 11);
            this.cleanButton.Name = "cleanButton";
            this.cleanButton.Size = new System.Drawing.Size(82, 35);
            this.cleanButton.TabIndex = 9;
            this.cleanButton.Text = "Clean";
            this.cleanButton.UseVisualStyleBackColor = true;
            this.cleanButton.Click += new System.EventHandler(this.cleanButton_Click);
            // 
            // DivideImage
            // 
            this.DivideImage.Location = new System.Drawing.Point(540, 329);
            this.DivideImage.Name = "DivideImage";
            this.DivideImage.Size = new System.Drawing.Size(96, 51);
            this.DivideImage.TabIndex = 11;
            this.DivideImage.Text = "Разбить изображение";
            this.DivideImage.UseVisualStyleBackColor = true;
            this.DivideImage.Click += new System.EventHandler(this.DivideImage_Click);
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
            // originalPicture
            // 
            this.originalPicture.BackColor = System.Drawing.Color.Gainsboro;
            this.originalPicture.Location = new System.Drawing.Point(57, 67);
            this.originalPicture.Name = "originalPicture";
            this.originalPicture.Size = new System.Drawing.Size(256, 256);
            this.originalPicture.TabIndex = 14;
            this.originalPicture.TabStop = false;
            this.originalPicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.originalPicture_MouseDown);
            this.originalPicture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.originalPicture_MouseUp);
            // 
            // poligonalNet
            // 
            this.poligonalNet.BackColor = System.Drawing.Color.Gainsboro;
            this.poligonalNet.Location = new System.Drawing.Point(380, 67);
            this.poligonalNet.Name = "poligonalNet";
            this.poligonalNet.Size = new System.Drawing.Size(256, 256);
            this.poligonalNet.TabIndex = 15;
            this.poligonalNet.TabStop = false;
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
            // triangulatedPicture
            // 
            this.triangulatedPicture.BackColor = System.Drawing.Color.Gainsboro;
            this.triangulatedPicture.Location = new System.Drawing.Point(706, 67);
            this.triangulatedPicture.Name = "triangulatedPicture";
            this.triangulatedPicture.Size = new System.Drawing.Size(256, 256);
            this.triangulatedPicture.TabIndex = 19;
            this.triangulatedPicture.TabStop = false;
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
            // fragmentPicture
            // 
            this.fragmentPicture.BackColor = System.Drawing.Color.Gainsboro;
            this.fragmentPicture.Location = new System.Drawing.Point(57, 386);
            this.fragmentPicture.Name = "fragmentPicture";
            this.fragmentPicture.Size = new System.Drawing.Size(256, 256);
            this.fragmentPicture.TabIndex = 23;
            this.fragmentPicture.TabStop = false;
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
            this.Controls.Add(this.fragmentPicture);
            this.Controls.Add(this.triangulationPoints);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.triangulatedPicture);
            this.Controls.Add(this.Draw);
            this.Controls.Add(this.poligonalNet);
            this.Controls.Add(this.originalPicture);
            this.Controls.Add(this.Triangulation);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.DivideImage);
            this.Controls.Add(this.cleanButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.paintedPicture);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.Open);
            this.Name = "Form1";
            this.RightToLeftLayout = true;
            this.Text = "Триангуляция Делоне";
            ((System.ComponentModel.ISupportInitialize)(this.paintedPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.originalPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.poligonalNet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.triangulatedPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.triangulationPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fragmentPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Open;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.PictureBox paintedPicture;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button cleanButton;
        private System.Windows.Forms.Button DivideImage;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button Triangulation;
        private System.Windows.Forms.PictureBox originalPicture;
        private System.Windows.Forms.PictureBox poligonalNet;
        private System.Windows.Forms.Button Draw;
        private System.Windows.Forms.PictureBox triangulatedPicture;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox triangulationPoints;
        private System.Windows.Forms.PictureBox fragmentPicture;
        private System.Windows.Forms.Button cutButton;




    }
}

