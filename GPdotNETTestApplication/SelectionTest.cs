using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GPdotNETLib;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace GPdotNETTestApplication
{
    public partial class SelectionTest : Form
    {
        GPPopulation pop;
        public SelectionTest()
        {
            InitializeComponent();
        }

        private void SelectionTest_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            TestUtility.TerminaliIFunkcije();

            bool parallel= radioButton2.Checked;
            GPPopulation.GPFunctionSet = TestUtility.functionSet;
            GPPopulation.GPFunctionSet.functions = TestUtility.functionSet.functions.Where(x => x.Aritry == 2).ToList();
            GPPopulation.GPTerminalSet = TestUtility.terminalSet;
            GPPopulation.GPParameters = new GPParameters();
            GPPopulation.GPParameters.einitializationMethod = EInitializationMethod.FullInitialization;

            pop = new GPPopulation(1000, TestUtility.terminalSet,
                TestUtility.functionSet, GPPopulation.GPParameters,false);

            button1_Click(null, null);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable("Tab");
            DataColumn col = new DataColumn("Index", typeof(int));
            tbl.Columns.Add(col);
            GPPopulation.GPParameters.eselectionMethod = (ESelectionMethod)comboBox1.SelectedIndex;
            //dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            int number=int.Parse(textBox1.Text);
           
            foreach (var p in GPPopulation.GPParameters.GPSelectionMethod.Select(pop.Population,number).Take(number))
            {
                DataRow row= tbl.NewRow();
                row[0]=pop.Population.IndexOf(p)+1;
                tbl.Rows.Add(row);
            }
           
           dataGridView1.AutoGenerateColumns = true;
           dataGridView1.DataSource = tbl;           
           dataGridView1.Update();
           dataGridView1.Refresh();
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
                pop.StartEvolution();
        }

        //Selection for mating
        private void button3_Click(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable("Tab");
            DataColumn col = new DataColumn("Index", typeof(int));
            tbl.Columns.Add(col);
            GPPopulation.GPParameters.eselectionMethod = (ESelectionMethod)comboBox1.SelectedIndex;
            //dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            int number = int.Parse(textBox1.Text);

            for (int i = 0; i < number; i++ )
            {
                DataRow row = tbl.NewRow();
                var p = pop.SelectChromosomeFromPopulation();
                row[0] = pop.Population.IndexOf(p) + 1;
                tbl.Rows.Add(row);
            }

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = tbl;
            dataGridView1.Update();
            dataGridView1.Refresh();
            
        }
    }
}
