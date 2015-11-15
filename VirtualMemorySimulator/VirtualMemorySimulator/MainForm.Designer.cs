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
            this.operandsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.logFileTextBox = new System.Windows.Forms.TextBox();
            this.otherInstructionFrequencyNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.testBranchFrequencyNumericUpdDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.storeInstructionFrequencyNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.loadFrequencyNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.VirtualAddressSpaceSizeUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numberOfInstructionsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pathToInstructionsFileTextBox = new System.Windows.Forms.TextBox();
            this.startSimulationButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.operandsNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.otherInstructionFrequencyNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.testBranchFrequencyNumericUpdDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.storeInstructionFrequencyNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadFrequencyNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VirtualAddressSpaceSizeUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfInstructionsNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // operandsNumericUpDown
            // 
            this.operandsNumericUpDown.Location = new System.Drawing.Point(204, 175);
            this.operandsNumericUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.operandsNumericUpDown.Name = "operandsNumericUpDown";
            this.operandsNumericUpDown.Size = new System.Drawing.Size(164, 20);
            this.operandsNumericUpDown.TabIndex = 37;
         
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 180);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(131, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "Max Number of Operands:";
            
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 280);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 35;
            this.label8.Text = "Log File Path";
            
            // 
            // logFileTextBox
            // 
            this.logFileTextBox.Location = new System.Drawing.Point(98, 277);
            this.logFileTextBox.Name = "logFileTextBox";
            this.logFileTextBox.Size = new System.Drawing.Size(367, 20);
            this.logFileTextBox.TabIndex = 34;
            
            // 
            // otherInstructionFrequencyNumericUpDown
            // 
            this.otherInstructionFrequencyNumericUpDown.Location = new System.Drawing.Point(205, 148);
            this.otherInstructionFrequencyNumericUpDown.Name = "otherInstructionFrequencyNumericUpDown";
            this.otherInstructionFrequencyNumericUpDown.Size = new System.Drawing.Size(164, 20);
            this.otherInstructionFrequencyNumericUpDown.TabIndex = 33;
            
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 153);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(141, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "Other Instruction Frequency:";
            
            // 
            // testBranchFrequencyNumericUpdDown
            // 
            this.testBranchFrequencyNumericUpdDown.Location = new System.Drawing.Point(205, 119);
            this.testBranchFrequencyNumericUpdDown.Name = "testBranchFrequencyNumericUpdDown";
            this.testBranchFrequencyNumericUpdDown.Size = new System.Drawing.Size(164, 20);
            this.testBranchFrequencyNumericUpdDown.TabIndex = 31;
            
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(175, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "Test/Branch Instruction Frequency:";
            
            // 
            // storeInstructionFrequencyNumericUpDown
            // 
            this.storeInstructionFrequencyNumericUpDown.Location = new System.Drawing.Point(205, 93);
            this.storeInstructionFrequencyNumericUpDown.Name = "storeInstructionFrequencyNumericUpDown";
            this.storeInstructionFrequencyNumericUpDown.Size = new System.Drawing.Size(164, 20);
            this.storeInstructionFrequencyNumericUpDown.TabIndex = 29;
            
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Store Instruction Frequency:";
            
            // 
            // loadFrequencyNumericUpDown
            // 
            this.loadFrequencyNumericUpDown.Location = new System.Drawing.Point(205, 65);
            this.loadFrequencyNumericUpDown.Name = "loadFrequencyNumericUpDown";
            this.loadFrequencyNumericUpDown.Size = new System.Drawing.Size(164, 20);
            this.loadFrequencyNumericUpDown.TabIndex = 27;
            
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Load Instruction Frequency:";
            
            // 
            // VirtualAddressSpaceSizeUpDown
            // 
            this.VirtualAddressSpaceSizeUpDown.Location = new System.Drawing.Point(204, 34);
            this.VirtualAddressSpaceSizeUpDown.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.VirtualAddressSpaceSizeUpDown.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.VirtualAddressSpaceSizeUpDown.Name = "VirtualAddressSpaceSizeUpDown";
            this.VirtualAddressSpaceSizeUpDown.Size = new System.Drawing.Size(164, 20);
            this.VirtualAddressSpaceSizeUpDown.TabIndex = 25;
            this.VirtualAddressSpaceSizeUpDown.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Virtual Address Space Size (Bits)";
            // 
            // numberOfInstructionsNumericUpDown
            // 
            this.numberOfInstructionsNumericUpDown.Location = new System.Drawing.Point(204, 9);
            this.numberOfInstructionsNumericUpDown.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numberOfInstructionsNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberOfInstructionsNumericUpDown.Name = "numberOfInstructionsNumericUpDown";
            this.numberOfInstructionsNumericUpDown.Size = new System.Drawing.Size(164, 20);
            this.numberOfInstructionsNumericUpDown.TabIndex = 23;
            this.numberOfInstructionsNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Number of Instructions:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 254);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Instructions File";
            // 
            // pathToInstructionsFileTextBox
            // 
            this.pathToInstructionsFileTextBox.Location = new System.Drawing.Point(98, 251);
            this.pathToInstructionsFileTextBox.Name = "pathToInstructionsFileTextBox";
            this.pathToInstructionsFileTextBox.Size = new System.Drawing.Size(367, 20);
            this.pathToInstructionsFileTextBox.TabIndex = 20;
            // 
            // startSimulationButton
            // 
            this.startSimulationButton.Location = new System.Drawing.Point(475, 249);
            this.startSimulationButton.Name = "startSimulationButton";
            this.startSimulationButton.Size = new System.Drawing.Size(128, 23);
            this.startSimulationButton.TabIndex = 19;
            this.startSimulationButton.Text = "Start Simulation!";
            this.startSimulationButton.UseVisualStyleBackColor = true;
            this.startSimulationButton.Click += new System.EventHandler(this.startSimulationButton_Click_1);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 527);
            this.Controls.Add(this.operandsNumericUpDown);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.logFileTextBox);
            this.Controls.Add(this.otherInstructionFrequencyNumericUpDown);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.testBranchFrequencyNumericUpdDown);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.storeInstructionFrequencyNumericUpDown);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.loadFrequencyNumericUpDown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.VirtualAddressSpaceSizeUpDown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numberOfInstructionsNumericUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pathToInstructionsFileTextBox);
            this.Controls.Add(this.startSimulationButton);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.operandsNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.otherInstructionFrequencyNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.testBranchFrequencyNumericUpdDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.storeInstructionFrequencyNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadFrequencyNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VirtualAddressSpaceSizeUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfInstructionsNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown operandsNumericUpDown;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox logFileTextBox;
        private System.Windows.Forms.NumericUpDown otherInstructionFrequencyNumericUpDown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown testBranchFrequencyNumericUpdDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown storeInstructionFrequencyNumericUpDown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown loadFrequencyNumericUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown VirtualAddressSpaceSizeUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numberOfInstructionsNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox pathToInstructionsFileTextBox;
        private System.Windows.Forms.Button startSimulationButton;

    }
}

