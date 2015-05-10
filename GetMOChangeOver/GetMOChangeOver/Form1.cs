using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetMOChangeOver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string address = "net.tcp://1202sgn157:9500/Siemens.Siplace.Oib.SiplacePro";
            //this.Cursor = Cursors.WaitCursor;
#region connection to SiplacePro
            try
            {
                
            }
            catch (Exception)
            {
                
                throw;
            }

#endregion 
        }
    }
}
