namespace ArcgisEngineApplication1
{
    partial class AddForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.请输入 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.textBox_code = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.spinEdit_AllConfiemed = new DevExpress.XtraEditors.SpinEdit();
            this.spinEdit_CurConfirmeed = new DevExpress.XtraEditors.SpinEdit();
            this.spinEdit_Cured = new DevExpress.XtraEditors.SpinEdit();
            this.spinEdit_Death = new DevExpress.XtraEditors.SpinEdit();
            this.add_btn = new DevExpress.XtraEditors.SimpleButton();
            this.cancel_btn = new DevExpress.XtraEditors.SimpleButton();
            this.date_edit = new DevExpress.XtraEditors.DateEdit();
            this.请输入.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit_AllConfiemed.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit_CurConfirmeed.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit_Cured.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit_Death.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_edit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_edit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // 请输入
            // 
            this.请输入.Controls.Add(this.date_edit);
            this.请输入.Controls.Add(this.cancel_btn);
            this.请输入.Controls.Add(this.add_btn);
            this.请输入.Controls.Add(this.spinEdit_Death);
            this.请输入.Controls.Add(this.spinEdit_Cured);
            this.请输入.Controls.Add(this.spinEdit_CurConfirmeed);
            this.请输入.Controls.Add(this.spinEdit_AllConfiemed);
            this.请输入.Controls.Add(this.label7);
            this.请输入.Controls.Add(this.label6);
            this.请输入.Controls.Add(this.label5);
            this.请输入.Controls.Add(this.label4);
            this.请输入.Controls.Add(this.label3);
            this.请输入.Controls.Add(this.textBox_code);
            this.请输入.Controls.Add(this.label2);
            this.请输入.Controls.Add(this.textBox_name);
            this.请输入.Controls.Add(this.label1);
            this.请输入.Dock = System.Windows.Forms.DockStyle.Fill;
            this.请输入.Location = new System.Drawing.Point(0, 0);
            this.请输入.Name = "请输入";
            this.请输入.Size = new System.Drawing.Size(471, 495);
            this.请输入.TabIndex = 0;
            this.请输入.TabStop = false;
            this.请输入.Text = "请输入";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "区划名称：";
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(145, 54);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(253, 26);
            this.textBox_name.TabIndex = 1;
            // 
            // textBox_code
            // 
            this.textBox_code.Location = new System.Drawing.Point(145, 104);
            this.textBox_code.Name = "textBox_code";
            this.textBox_code.Size = new System.Drawing.Size(253, 26);
            this.textBox_code.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "区划编码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "累计确诊：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 254);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "当前确诊：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(54, 303);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 18);
            this.label5.TabIndex = 8;
            this.label5.Text = "已出院：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(54, 358);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 18);
            this.label6.TabIndex = 10;
            this.label6.Text = "死亡人数：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(54, 155);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 18);
            this.label7.TabIndex = 12;
            this.label7.Text = "日期：";
            // 
            // spinEdit_AllConfiemed
            // 
            this.spinEdit_AllConfiemed.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEdit_AllConfiemed.Location = new System.Drawing.Point(145, 198);
            this.spinEdit_AllConfiemed.Name = "spinEdit_AllConfiemed";
            this.spinEdit_AllConfiemed.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEdit_AllConfiemed.Size = new System.Drawing.Size(253, 24);
            this.spinEdit_AllConfiemed.TabIndex = 15;
            // 
            // spinEdit_CurConfirmeed
            // 
            this.spinEdit_CurConfirmeed.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEdit_CurConfirmeed.Location = new System.Drawing.Point(145, 251);
            this.spinEdit_CurConfirmeed.Name = "spinEdit_CurConfirmeed";
            this.spinEdit_CurConfirmeed.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEdit_CurConfirmeed.Size = new System.Drawing.Size(253, 24);
            this.spinEdit_CurConfirmeed.TabIndex = 16;
            // 
            // spinEdit_Cured
            // 
            this.spinEdit_Cured.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEdit_Cured.Location = new System.Drawing.Point(145, 309);
            this.spinEdit_Cured.Name = "spinEdit_Cured";
            this.spinEdit_Cured.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEdit_Cured.Size = new System.Drawing.Size(253, 24);
            this.spinEdit_Cured.TabIndex = 17;
            // 
            // spinEdit_Death
            // 
            this.spinEdit_Death.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEdit_Death.Location = new System.Drawing.Point(145, 355);
            this.spinEdit_Death.Name = "spinEdit_Death";
            this.spinEdit_Death.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEdit_Death.Size = new System.Drawing.Size(253, 24);
            this.spinEdit_Death.TabIndex = 18;
            // 
            // add_btn
            // 
            this.add_btn.Location = new System.Drawing.Point(110, 409);
            this.add_btn.Name = "add_btn";
            this.add_btn.Size = new System.Drawing.Size(94, 30);
            this.add_btn.TabIndex = 19;
            this.add_btn.Text = "添加";
            this.add_btn.Click += new System.EventHandler(this.add_btn_Click);
            // 
            // cancel_btn
            // 
            this.cancel_btn.Location = new System.Drawing.Point(270, 409);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(93, 30);
            this.cancel_btn.TabIndex = 20;
            this.cancel_btn.Text = "取消";
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // date_edit
            // 
            this.date_edit.EditValue = null;
            this.date_edit.Location = new System.Drawing.Point(145, 149);
            this.date_edit.Name = "date_edit";
            this.date_edit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.date_edit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.date_edit.Size = new System.Drawing.Size(253, 24);
            this.date_edit.TabIndex = 21;
            // 
            // AddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 495);
            this.Controls.Add(this.请输入);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加数据";
            this.请输入.ResumeLayout(false);
            this.请输入.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit_AllConfiemed.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit_CurConfirmeed.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit_Cured.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit_Death.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_edit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_edit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox 请输入;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton cancel_btn;
        private DevExpress.XtraEditors.SimpleButton add_btn;
        private DevExpress.XtraEditors.SpinEdit spinEdit_Death;
        private DevExpress.XtraEditors.SpinEdit spinEdit_Cured;
        private DevExpress.XtraEditors.SpinEdit spinEdit_CurConfirmeed;
        private DevExpress.XtraEditors.SpinEdit spinEdit_AllConfiemed;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_code;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit date_edit;
    }
}