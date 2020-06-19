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

namespace ArcgisEngineApplication1
{
    public partial class AddForm : DevExpress.XtraEditors.XtraForm
    {
        public AddForm()
        {
            InitializeComponent();
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            ArrayList arr = new ArrayList();
            arr.Add("code:"+this.textBox_code.Text);
            arr.Add("name:'" + this.textBox_name.Text+"'");
            arr.Add("YMD:'" + this.date_edit.Text+"'");
            arr.Add("AllConfiemed:" + this.spinEdit_AllConfiemed.Text);
            arr.Add("CurConfirmeed:" + this.spinEdit_CurConfirmeed.Text);
            arr.Add("Cured:" + this.spinEdit_Cured.Text);
            arr.Add("Death:" + this.spinEdit_Death.Text);
            int result = OperateDatabase.Insert("data",arr);
            if (result == 1)
            {
                MessageBox.Show("添加成功");
                return;
            }else {
                MessageBox.Show("添加失败");
                return;
            }
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}