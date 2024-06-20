namespace Triangulation;
partial class AppForm
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
        Open = new System.Windows.Forms.Button();
        ExitButton = new System.Windows.Forms.Button();
        paintedPicture = new System.Windows.Forms.PictureBox();
        saveButton = new System.Windows.Forms.Button();
        openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
        cleanButton = new System.Windows.Forms.Button();
        DivideImage = new System.Windows.Forms.Button();
        numericUpDown1 = new System.Windows.Forms.NumericUpDown();
        Triangulation = new System.Windows.Forms.Button();
        originalPicture = new System.Windows.Forms.PictureBox();
        poligonalNet = new System.Windows.Forms.PictureBox();
        Draw = new System.Windows.Forms.Button();
        triangulatedPicture = new System.Windows.Forms.PictureBox();
        label1 = new System.Windows.Forms.Label();
        triangulationPoints = new System.Windows.Forms.PictureBox();
        fragmentPicture = new System.Windows.Forms.PictureBox();
        cutButton = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)paintedPicture).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)originalPicture).BeginInit();
        ((System.ComponentModel.ISupportInitialize)poligonalNet).BeginInit();
        ((System.ComponentModel.ISupportInitialize)triangulatedPicture).BeginInit();
        ((System.ComponentModel.ISupportInitialize)triangulationPoints).BeginInit();
        ((System.ComponentModel.ISupportInitialize)fragmentPicture).BeginInit();
        SuspendLayout();
        // 
        // Open
        // 
        Open.Location = new System.Drawing.Point(14, 14);
        Open.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        Open.Name = "Open";
        Open.Size = new System.Drawing.Size(96, 42);
        Open.TabIndex = 0;
        Open.Text = "Open";
        Open.UseVisualStyleBackColor = true;
        Open.Click += Open_Click;
        // 
        // ExitButton
        // 
        ExitButton.Location = new System.Drawing.Point(322, 12);
        ExitButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        ExitButton.Name = "ExitButton";
        ExitButton.Size = new System.Drawing.Size(96, 42);
        ExitButton.TabIndex = 2;
        ExitButton.Text = "Exit";
        ExitButton.UseVisualStyleBackColor = true;
        ExitButton.Click += ExitButton_Click;
        // 
        // paintedPicture
        // 
        paintedPicture.BackColor = System.Drawing.Color.Gainsboro;
        paintedPicture.Location = new System.Drawing.Point(824, 445);
        paintedPicture.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        paintedPicture.Name = "paintedPicture";
        paintedPicture.Size = new System.Drawing.Size(256, 256);
        paintedPicture.TabIndex = 5;
        paintedPicture.TabStop = false;
        // 
        // saveButton
        // 
        saveButton.Location = new System.Drawing.Point(117, 13);
        saveButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        saveButton.Name = "saveButton";
        saveButton.Size = new System.Drawing.Size(96, 42);
        saveButton.TabIndex = 6;
        saveButton.Text = "Save";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += SaveButton_Click;
        // 
        // openFileDialog1
        // 
        openFileDialog1.FileName = "openFileDialog1";
        // 
        // cleanButton
        // 
        cleanButton.Location = new System.Drawing.Point(219, 13);
        cleanButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        cleanButton.Name = "cleanButton";
        cleanButton.Size = new System.Drawing.Size(96, 40);
        cleanButton.TabIndex = 9;
        cleanButton.Text = "Clean";
        cleanButton.UseVisualStyleBackColor = true;
        cleanButton.Click += CleanButton_Click;
        // 
        // DivideImage
        // 
        DivideImage.Location = new System.Drawing.Point(630, 339);
        DivideImage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        DivideImage.Name = "DivideImage";
        DivideImage.Size = new System.Drawing.Size(112, 59);
        DivideImage.TabIndex = 11;
        DivideImage.Text = "Разбить изображение";
        DivideImage.UseVisualStyleBackColor = true;
        DivideImage.Click += DivideImage_Click;
        // 
        // numericUpDown1
        // 
        numericUpDown1.Location = new System.Drawing.Point(527, 357);
        numericUpDown1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        numericUpDown1.Maximum = new decimal(new int[] { 256, 0, 0, 0 });
        numericUpDown1.Name = "numericUpDown1";
        numericUpDown1.Size = new System.Drawing.Size(96, 23);
        numericUpDown1.TabIndex = 12;
        numericUpDown1.Value = new decimal(new int[] { 100, 0, 0, 0 });
        // 
        // Triangulation
        // 
        Triangulation.AllowDrop = true;
        Triangulation.Location = new System.Drawing.Point(895, 339);
        Triangulation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        Triangulation.Name = "Triangulation";
        Triangulation.Size = new System.Drawing.Size(118, 59);
        Triangulation.TabIndex = 13;
        Triangulation.Text = "Триангуляция Делоне";
        Triangulation.UseVisualStyleBackColor = true;
        Triangulation.Click += Triangulation_Click;
        // 
        // originalPicture
        // 
        originalPicture.BackColor = System.Drawing.Color.Gainsboro;
        originalPicture.Location = new System.Drawing.Point(66, 77);
        originalPicture.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        originalPicture.Name = "originalPicture";
        originalPicture.Size = new System.Drawing.Size(256, 256);
        originalPicture.TabIndex = 14;
        originalPicture.TabStop = false;
        originalPicture.MouseDown += OriginalPicture_MouseDown;
        originalPicture.MouseUp += OriginalPicture_MouseUp;
        // 
        // poligonalNet
        // 
        poligonalNet.BackColor = System.Drawing.Color.Gainsboro;
        poligonalNet.Location = new System.Drawing.Point(443, 77);
        poligonalNet.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        poligonalNet.Name = "poligonalNet";
        poligonalNet.Size = new System.Drawing.Size(256, 256);
        poligonalNet.TabIndex = 15;
        poligonalNet.TabStop = false;
        // 
        // Draw
        // 
        Draw.Location = new System.Drawing.Point(895, 707);
        Draw.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        Draw.Name = "Draw";
        Draw.Size = new System.Drawing.Size(118, 59);
        Draw.TabIndex = 17;
        Draw.Text = "Раскрасить";
        Draw.UseVisualStyleBackColor = true;
        Draw.Click += Draw_Click;
        // 
        // triangulatedPicture
        // 
        triangulatedPicture.BackColor = System.Drawing.Color.Gainsboro;
        triangulatedPicture.Location = new System.Drawing.Point(824, 77);
        triangulatedPicture.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        triangulatedPicture.Name = "triangulatedPicture";
        triangulatedPicture.Size = new System.Drawing.Size(256, 256);
        triangulatedPicture.TabIndex = 19;
        triangulatedPicture.TabStop = false;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(425, 361);
        label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(89, 15);
        label1.TabIndex = 21;
        label1.Text = "Порог яркости";
        // 
        // triangulationPoints
        // 
        triangulationPoints.BackColor = System.Drawing.Color.Gainsboro;
        triangulationPoints.Location = new System.Drawing.Point(443, 445);
        triangulationPoints.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        triangulationPoints.Name = "triangulationPoints";
        triangulationPoints.Size = new System.Drawing.Size(256, 256);
        triangulationPoints.TabIndex = 22;
        triangulationPoints.TabStop = false;
        // 
        // fragmentPicture
        // 
        fragmentPicture.BackColor = System.Drawing.Color.Gainsboro;
        fragmentPicture.Location = new System.Drawing.Point(66, 445);
        fragmentPicture.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        fragmentPicture.Name = "fragmentPicture";
        fragmentPicture.Size = new System.Drawing.Size(256, 256);
        fragmentPicture.TabIndex = 23;
        fragmentPicture.TabStop = false;
        // 
        // cutButton
        // 
        cutButton.Location = new System.Drawing.Point(89, 339);
        cutButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        cutButton.Name = "cutButton";
        cutButton.Size = new System.Drawing.Size(212, 59);
        cutButton.TabIndex = 24;
        cutButton.Text = "Выделить фрагмент";
        cutButton.UseVisualStyleBackColor = true;
        cutButton.Click += CutButton_Click;
        // 
        // AppForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(1116, 783);
        Controls.Add(cutButton);
        Controls.Add(fragmentPicture);
        Controls.Add(triangulationPoints);
        Controls.Add(label1);
        Controls.Add(triangulatedPicture);
        Controls.Add(Draw);
        Controls.Add(poligonalNet);
        Controls.Add(originalPicture);
        Controls.Add(Triangulation);
        Controls.Add(numericUpDown1);
        Controls.Add(DivideImage);
        Controls.Add(cleanButton);
        Controls.Add(saveButton);
        Controls.Add(paintedPicture);
        Controls.Add(ExitButton);
        Controls.Add(Open);
        Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        Name = "AppForm";
        RightToLeftLayout = true;
        Text = "Триангуляция Делоне";
        ((System.ComponentModel.ISupportInitialize)paintedPicture).EndInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
        ((System.ComponentModel.ISupportInitialize)originalPicture).EndInit();
        ((System.ComponentModel.ISupportInitialize)poligonalNet).EndInit();
        ((System.ComponentModel.ISupportInitialize)triangulatedPicture).EndInit();
        ((System.ComponentModel.ISupportInitialize)triangulationPoints).EndInit();
        ((System.ComponentModel.ISupportInitialize)fragmentPicture).EndInit();
        ResumeLayout(false);
        PerformLayout();
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
