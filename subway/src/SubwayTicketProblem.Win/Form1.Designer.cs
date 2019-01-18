namespace SubwayTicketProblem.Win
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_NewCard = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Balance = new System.Windows.Forms.TextBox();
            this.txtOpenDisplay = new System.Windows.Forms.RichTextBox();
            this.btn_Charge = new System.Windows.Forms.Button();
            this.btn_EntryStation = new System.Windows.Forms.Button();
            this.btn_GoOut = new System.Windows.Forms.Button();
            this.cmb_EntryStation = new System.Windows.Forms.ComboBox();
            this.txt_ChargeAmount = new System.Windows.Forms.TextBox();
            this.cmb_GoOutStation = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btn_NewCard
            // 
            this.btn_NewCard.Location = new System.Drawing.Point(12, 37);
            this.btn_NewCard.Name = "btn_NewCard";
            this.btn_NewCard.Size = new System.Drawing.Size(75, 23);
            this.btn_NewCard.TabIndex = 0;
            this.btn_NewCard.Text = "发卡";
            this.btn_NewCard.UseVisualStyleBackColor = true;
            this.btn_NewCard.Click += new System.EventHandler(this.btn_NewCard_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "余额：";
            // 
            // txt_Balance
            // 
            this.txt_Balance.Location = new System.Drawing.Point(93, 78);
            this.txt_Balance.Name = "txt_Balance";
            this.txt_Balance.ReadOnly = true;
            this.txt_Balance.Size = new System.Drawing.Size(121, 21);
            this.txt_Balance.TabIndex = 2;
            // 
            // txtOpenDisplay
            // 
            this.txtOpenDisplay.Location = new System.Drawing.Point(271, 37);
            this.txtOpenDisplay.Name = "txtOpenDisplay";
            this.txtOpenDisplay.Size = new System.Drawing.Size(443, 360);
            this.txtOpenDisplay.TabIndex = 3;
            this.txtOpenDisplay.Text = "";
            // 
            // btn_Charge
            // 
            this.btn_Charge.Location = new System.Drawing.Point(12, 113);
            this.btn_Charge.Name = "btn_Charge";
            this.btn_Charge.Size = new System.Drawing.Size(75, 23);
            this.btn_Charge.TabIndex = 4;
            this.btn_Charge.Text = "充值";
            this.btn_Charge.UseVisualStyleBackColor = true;
            this.btn_Charge.Click += new System.EventHandler(this.btn_Charge_Click);
            // 
            // btn_EntryStation
            // 
            this.btn_EntryStation.Location = new System.Drawing.Point(12, 158);
            this.btn_EntryStation.Name = "btn_EntryStation";
            this.btn_EntryStation.Size = new System.Drawing.Size(75, 23);
            this.btn_EntryStation.TabIndex = 5;
            this.btn_EntryStation.Text = "进站";
            this.btn_EntryStation.UseVisualStyleBackColor = true;
            this.btn_EntryStation.Click += new System.EventHandler(this.btn_EntryStation_Click);
            // 
            // btn_GoOut
            // 
            this.btn_GoOut.Location = new System.Drawing.Point(12, 197);
            this.btn_GoOut.Name = "btn_GoOut";
            this.btn_GoOut.Size = new System.Drawing.Size(75, 23);
            this.btn_GoOut.TabIndex = 6;
            this.btn_GoOut.Text = "出站";
            this.btn_GoOut.UseVisualStyleBackColor = true;
            this.btn_GoOut.Click += new System.EventHandler(this.btn_GoOut_Click);
            // 
            // cmb_EntryStation
            // 
            this.cmb_EntryStation.FormattingEnabled = true;
            this.cmb_EntryStation.Location = new System.Drawing.Point(93, 161);
            this.cmb_EntryStation.Name = "cmb_EntryStation";
            this.cmb_EntryStation.Size = new System.Drawing.Size(121, 20);
            this.cmb_EntryStation.TabIndex = 7;
            // 
            // txt_ChargeAmount
            // 
            this.txt_ChargeAmount.Location = new System.Drawing.Point(93, 113);
            this.txt_ChargeAmount.Name = "txt_ChargeAmount";
            this.txt_ChargeAmount.Size = new System.Drawing.Size(121, 21);
            this.txt_ChargeAmount.TabIndex = 8;
            // 
            // cmb_GoOutStation
            // 
            this.cmb_GoOutStation.FormattingEnabled = true;
            this.cmb_GoOutStation.Location = new System.Drawing.Point(93, 199);
            this.cmb_GoOutStation.Name = "cmb_GoOutStation";
            this.cmb_GoOutStation.Size = new System.Drawing.Size(121, 20);
            this.cmb_GoOutStation.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 409);
            this.Controls.Add(this.txt_ChargeAmount);
            this.Controls.Add(this.cmb_GoOutStation);
            this.Controls.Add(this.cmb_EntryStation);
            this.Controls.Add(this.btn_GoOut);
            this.Controls.Add(this.btn_EntryStation);
            this.Controls.Add(this.btn_Charge);
            this.Controls.Add(this.txtOpenDisplay);
            this.Controls.Add(this.txt_Balance);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_NewCard);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_NewCard;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Balance;
        private System.Windows.Forms.RichTextBox txtOpenDisplay;
        private System.Windows.Forms.Button btn_Charge;
        private System.Windows.Forms.Button btn_EntryStation;
        private System.Windows.Forms.Button btn_GoOut;
        private System.Windows.Forms.ComboBox cmb_EntryStation;
        private System.Windows.Forms.TextBox txt_ChargeAmount;
        private System.Windows.Forms.ComboBox cmb_GoOutStation;
    }
}

