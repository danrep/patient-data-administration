using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatientDataAdministration.Client
{
    public partial class DataCentral : MetroFramework.Forms.MetroForm
    {
        public DataCentral()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataCentral_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
