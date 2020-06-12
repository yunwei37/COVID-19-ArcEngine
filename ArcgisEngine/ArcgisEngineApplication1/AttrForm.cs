using ESRI.ArcGIS.Carto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcgisEngineApplication1
{
    public partial class AttrForm : Form
    {
        private ILayer pLayer;
        public AttrForm(ILayer pLayer)
        {
            this.pLayer = pLayer;
            InitializeComponent();
        }

        private void AttrForm_Load(object sender, EventArgs e)
        {
            DataTable dt=GisClass.fillTable(pLayer,pLayer.Name);
            this.gridControl1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
