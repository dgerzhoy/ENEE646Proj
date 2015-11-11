namespace VirtualMemorySimulator
{
    partial class MainForm
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
            this.startSimulationButton = new System.Windows.Forms.Button();
            this.pathToInstructionsFileTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // startSimulationButton
            // 
            this.startSimulationButton.Location = new System.Drawing.Point(475, 12);
            this.startSimulationButton.Name = "startSimulationButton";
            this.startSimulationButton.Size = new System.Drawing.Size(128, 23);
            this.startSimulationButton.TabIndex = 0;
            this.startSimulationButton.Text = "Start Simulation!";
            this.startSimulationButton.UseVisualStyleBackColor = true;
            this.startSimulationButton.Click += new System.EventHandler(this.startSimulationButton_Click);
            // 
            // pathToInstructionsFileTextBox
            // 
            this.pathToInstructionsFileTextBox.Location = new System.Drawing.Point(98, 14);
            this.pathToInstructionsFileTextBox.Name = "pathToInstructionsFileTextBox";
            this.pathToInstructionsFileTextBox.Size = new System.Drawing.Size(367, 20);
            this.pathToInstructionsFileTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Instructions File";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 187);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pathToInstructionsFileTextBox);
            this.Controls.Add(this.startSimulationButton);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startSimulationButton;
        private System.Windows.Forms.TextBox pathToInstructionsFileTextBox;
        private System.Windows.Forms.Label label1;
    }
}

