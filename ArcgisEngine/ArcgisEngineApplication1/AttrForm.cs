using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
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
        private IActiveView activeView;
        public AttrForm(ILayer pLayer, IActiveView activeView)
        {
            this.pLayer = pLayer;
            InitializeComponent();
            this.activeView = activeView;
        }

        private void AttrForm_Load(object sender, EventArgs e)
        {
            DataTable dt=GisClass.fillTable(pLayer,pLayer.Name);
            this.gridControl1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null); 
            IQueryFilter pQuery = new QueryFilterClass();
            string val;
            string col;
            DataRow dr = this.gridView1.GetDataRow(e.RowHandle);
            string Fid = dr["Fid"].ToString();
            //设置高亮要素的查询条件
            pQuery.WhereClause = "Fid=" + Fid;
            IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
            IFeatureSelection pFeatSelection;
            pFeatSelection = pFeatureLayer as IFeatureSelection;
            pFeatSelection.SelectFeatures(pQuery, esriSelectionResultEnum.esriSelectionResultNew, false);
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);      
        }
    }
}
