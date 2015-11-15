using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualMemorySimulator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        private void startSimulationButton_Click_1(object sender, EventArgs e)
        {
            ulong addressSpaceSize = 1;
            for (ulong i = 0; i < UInt32.Parse(VirtualAddressSpaceSizeUpDown.Text); i++)
            {
                addressSpaceSize = addressSpaceSize * 2;
            }

            SimulationRunner runner = new SimulationRunner(this.pathToInstructionsFileTextBox.Text,
                                                            addressSpaceSize,
                                                            UInt64.Parse(numberOfInstructionsNumericUpDown.Text),
                                                            UInt32.Parse(loadFrequencyNumericUpDown.Text),
                                                            UInt32.Parse(storeInstructionFrequencyNumericUpDown.Text),
                                                            UInt32.Parse(testBranchFrequencyNumericUpdDown.Text),
                                                            UInt32.Parse(otherInstructionFrequencyNumericUpDown.Text),
                                                            logFileTextBox.Text,
                                                            UInt32.Parse(operandsNumericUpDown.Text));
            runner.Run();
        }
    }
}
