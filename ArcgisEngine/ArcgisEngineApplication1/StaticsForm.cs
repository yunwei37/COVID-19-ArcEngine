using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using ToolClass.Operation;
using DevExpress.XtraCharts;

namespace ArcgisEngineApplication1
{
    public partial class StaticsForm : DevExpress.XtraEditors.XtraForm
    {
        public StaticsForm()
        {
            InitializeComponent();
        }

        private void statics_btn_Click(object sender, EventArgs e)
        {
            //查询起始日期的数字
            if (this.dateEdit_start.Text == "" || this.dateEdit_target.Text == "") {
                MessageBox.Show("请填写起止日期");
                return;
            }
            ArrayList arr1 = new ArrayList();
            arr1.Add("YMD:'" + this.dateEdit_start.Text + "'");
            DataTable dt1 = OperateDatabase.select("data",arr1);
            ArrayList arr2 = new ArrayList();
            arr1.Add("YMD:'" + this.dateEdit_target.Text + "'");
            DataTable dt2 = OperateDatabase.select("data", arr1);
            Series s1 = this.chartControl1.Series[0];
            s1.DataSource = dt1;
            s1.ArgumentDataMember = "name";
            s1.ValueDataMembers[0] = "CurConfirmeed";
        }
    }
}