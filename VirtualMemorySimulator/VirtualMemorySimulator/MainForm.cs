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
            TimeSpan span= DateTime.Now.Subtract(new DateTime(1970,1,1,0,0,0));
            this.pathToInstructionsFileTextBox.Text = "C:\\VirtualMemorySimulatorInstructions_" + (int) span.TotalSeconds + ".txt";
            this.logFileTextBox.Text = "C:\\VirtualMemorySimulatorLog_" + (int)span.TotalSeconds + ".txt";
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
                                                            UInt32.Parse(operandsNumericUpDown.Text),
                                                            UInt32.Parse(percentBranchTakenNumbericUpDown.Text),
                                                            UInt64.Parse(operandLocalityNumericUpDown.Text));
            runner.Run();

            setComponents();
        }

        private void setComponents()
        {
            this.instructionsExecutedTextBox.Text = ConfigInfo.NumberOfInstructions.ToString();
            this.itlbAccessesTextBox.Text = StatisticsGatherer.itlbAccesses.ToString();
            this.dTLBAccess.Text = StatisticsGatherer.dtlbAccesses.ToString();
            this.tlbAccessesTextBox.Text = StatisticsGatherer.tlbAccesses.ToString();
            this.dL1AccessTextBox.Text = StatisticsGatherer.dL1CacheAccesses.ToString();
            this.iL1CacheAccessTextBox.Text = StatisticsGatherer.iL1CacheAccesses.ToString();
            this.L2CacheAccessTextBox.Text = StatisticsGatherer.L2CacheAccesses.ToString();
            this.L3CachesAccessTextBox.Text = StatisticsGatherer.L3CacheAccesses.ToString();
            this.mainMemoryAccessTextBox.Text = StatisticsGatherer.MemoryAccess.ToString();
            this.diskAccessesTextBox.Text = StatisticsGatherer.DiskAccess.ToString();
            this.iTLBHitsTextBox.Text = StatisticsGatherer.itlbHits.ToString();
            this.dTLBHitTextBox.Text = StatisticsGatherer.dtlbHits.ToString();
            this.TLBHitTextBox.Text = StatisticsGatherer.tlbHits.ToString();
            this.iL1CacheHitTextBox.Text = StatisticsGatherer.iL1CacheHits.ToString();
            this.dL1CacheHitTextBox.Text = StatisticsGatherer.dL1CacheHits.ToString();
            this.l2CacheHitTextBox.Text = StatisticsGatherer.L2CacheHits.ToString();
            this.L3CacheHitTextBox.Text = StatisticsGatherer.L3CacheHits.ToString();
            this.cycleTextBox.Text = StatisticsGatherer.Cycles.ToString();
            this.iTLBMissesTextBox.Text = StatisticsGatherer.itlbMisses.ToString();
            this.dTLBMissesTextBox.Text = StatisticsGatherer.dtlbMisses.ToString();
            this.tlbMissesTextBox.Text = StatisticsGatherer.tlbMisses.ToString();
            this.il1CacheMissesTextBox.Text = StatisticsGatherer.iL1CacheMisses.ToString();
            this.dl1CacheMissesTextBox.Text = StatisticsGatherer.dL1CacheMisses.ToString();
            this.l2CacheMisses.Text = StatisticsGatherer.L2CacheMisses.ToString();
            this.L3CacheMissesTextBox.Text = StatisticsGatherer.L3CacheMisses.ToString();
            this.pageFaultsTextBox.Text = StatisticsGatherer.PageFaults.ToString();

            cyclesPerInstructionTextBox.Text = ((float)StatisticsGatherer.Cycles / (float) ConfigInfo.NumberOfInstructions).ToString();

        }
    }
}
