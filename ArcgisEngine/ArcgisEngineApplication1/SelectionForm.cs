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
    public partial class SelectionForm : Form
    {
        private IMap map;
        private IFeatureLayer currentLayer;
        public SelectionForm(IMap map)
        {
            this.map = map;
            InitializeComponent();
        }

        private void SelectionForm_Load(object sender, EventArgs e)
        {
            IFeatureLayer featureLayer;
            String layerName;
            TreeNode treeNode;
            for (int i = 0; i < map.LayerCount; i++) {
                layerName = map.get_Layer(i).Name;
                featureLayer = map.get_Layer(i) as IFeatureLayer;
                if (((IFeatureSelection)featureLayer).SelectionSet.Count > 0) { 
                    treeNode=new TreeNode(layerName);
                    treeNode.Tag = featureLayer;
                    this.treeView1.TopNode.Nodes.Add(treeNode);
                }
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.gridView1.Columns.Clear();
            currentLayer = e.Node.Tag as IFeatureLayer;
            if (currentLayer == null) {
                return;
            }
            IFeatureSelection featureSelection = currentLayer as IFeatureSelection;
            //获取选中得要素几何
            ISelectionSet selectionSet = featureSelection.SelectionSet;
            //获取字段
            IFields fields = currentLayer.FeatureClass.Fields;
            DataTable dt = new DataTable();
            for (int i = 0; i < fields.FieldCount; i++) {
                dt.Columns.Add(fields.get_Field(i).Name);
            }
            //获取整个数据集
            ICursor cursor;
            selectionSet.Search(null,false,out cursor);
            //获取每个要素
            IFeatureCursor featureCursor = cursor as IFeatureCursor;
            //遍历
            IFeature feature = featureCursor.NextFeature();
            String[] strs;
            while (feature != null) {
                strs = new String[fields.FieldCount];
                for (int i = 0; i < fields.FieldCount; i++) {
                    strs[i] = feature.get_Value(i).ToString();
                }
                dt.Rows.Add(strs);
                feature = featureCursor.NextFeature();
            }
            this.gridControl1.DataSource = dt;
           
        }
    }
}
