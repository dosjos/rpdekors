namespace CafeTerminal.UI
{
    partial class ControlPanel
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Innstillinger = new System.Windows.Forms.TabPage();
            this.EditPerson = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.vbilde = new System.Windows.Forms.TextBox();
            this.hbilde = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.Innstillinger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.Innstillinger);
            this.tabControl1.Controls.Add(this.EditPerson);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(407, 323);
            this.tabControl1.TabIndex = 0;
            // 
            // Innstillinger
            // 
            this.Innstillinger.Controls.Add(this.button2);
            this.Innstillinger.Controls.Add(this.button1);
            this.Innstillinger.Controls.Add(this.hbilde);
            this.Innstillinger.Controls.Add(this.vbilde);
            this.Innstillinger.Controls.Add(this.SaveButton);
            this.Innstillinger.Controls.Add(this.label3);
            this.Innstillinger.Controls.Add(this.numericUpDown2);
            this.Innstillinger.Controls.Add(this.label2);
            this.Innstillinger.Controls.Add(this.numericUpDown1);
            this.Innstillinger.Controls.Add(this.label1);
            this.Innstillinger.Location = new System.Drawing.Point(4, 22);
            this.Innstillinger.Name = "Innstillinger";
            this.Innstillinger.Padding = new System.Windows.Forms.Padding(3);
            this.Innstillinger.Size = new System.Drawing.Size(399, 297);
            this.Innstillinger.TabIndex = 0;
            this.Innstillinger.Text = "Innstillinger";
            this.Innstillinger.UseVisualStyleBackColor = true;
            // 
            // EditPerson
            // 
            this.EditPerson.Location = new System.Drawing.Point(4, 22);
            this.EditPerson.Name = "EditPerson";
            this.EditPerson.Padding = new System.Windows.Forms.Padding(3);
            this.EditPerson.Size = new System.Drawing.Size(545, 278);
            this.EditPerson.TabIndex = 1;
            this.EditPerson.Text = "Rediger person";
            this.EditPerson.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fra fødselsår";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(80, 67);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(68, 20);
            this.numericUpDown1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Til fødselsår";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(80, 102);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(68, 20);
            this.numericUpDown2.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Programinnstillinger";
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.Location = new System.Drawing.Point(345, 341);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "Lukk";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.Save_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SaveButton.Location = new System.Drawing.Point(7, 268);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 5;
            this.SaveButton.Text = "Lagre";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // vbilde
            // 
            this.vbilde.Location = new System.Drawing.Point(10, 154);
            this.vbilde.Name = "vbilde";
            this.vbilde.Size = new System.Drawing.Size(273, 20);
            this.vbilde.TabIndex = 6;
            // 
            // hbilde
            // 
            this.hbilde.Location = new System.Drawing.Point(10, 197);
            this.hbilde.Name = "hbilde";
            this.hbilde.Size = new System.Drawing.Size(273, 20);
            this.hbilde.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(289, 152);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Venstre bilde";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(289, 195);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Høyre bilde";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // ControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 376);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(448, 414);
            this.Name = "ControlPanel";
            this.Text = "ControlPanel";
            this.tabControl1.ResumeLayout(false);
            this.Innstillinger.ResumeLayout(false);
            this.Innstillinger.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Innstillinger;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage EditPerson;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox hbilde;
        private System.Windows.Forms.TextBox vbilde;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}