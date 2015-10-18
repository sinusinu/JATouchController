namespace JATouchController
{
    partial class frmMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnConnectToAnalyser = new System.Windows.Forms.Button();
            this.tbxCurrentTitle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbxTouchRange = new System.Windows.Forms.GroupBox();
            this.lblTouchRange = new System.Windows.Forms.Label();
            this.tbrTouchRange = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.tmrExitFlagRemover = new System.Windows.Forms.Timer(this.components);
            this.gbxTouchRange.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrTouchRange)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConnectToAnalyser
            // 
            this.btnConnectToAnalyser.Location = new System.Drawing.Point(12, 121);
            this.btnConnectToAnalyser.Name = "btnConnectToAnalyser";
            this.btnConnectToAnalyser.Size = new System.Drawing.Size(401, 220);
            this.btnConnectToAnalyser.TabIndex = 0;
            this.btnConnectToAnalyser.Text = "Search for jubeat analyser";
            this.btnConnectToAnalyser.UseVisualStyleBackColor = true;
            this.btnConnectToAnalyser.Click += new System.EventHandler(this.btnConnectToAnalyser_Click);
            // 
            // tbxCurrentTitle
            // 
            this.tbxCurrentTitle.Font = new System.Drawing.Font("굴림", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbxCurrentTitle.Location = new System.Drawing.Point(12, 83);
            this.tbxCurrentTitle.Name = "tbxCurrentTitle";
            this.tbxCurrentTitle.Size = new System.Drawing.Size(401, 32);
            this.tbxCurrentTitle.TabIndex = 1;
            this.tbxCurrentTitle.Text = "music select";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.label1.Size = new System.Drawing.Size(403, 71);
            this.label1.TabIndex = 2;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // gbxTouchRange
            // 
            this.gbxTouchRange.BackColor = System.Drawing.SystemColors.Control;
            this.gbxTouchRange.Controls.Add(this.lblTouchRange);
            this.gbxTouchRange.Controls.Add(this.tbrTouchRange);
            this.gbxTouchRange.Controls.Add(this.label2);
            this.gbxTouchRange.Location = new System.Drawing.Point(0, 340);
            this.gbxTouchRange.Name = "gbxTouchRange";
            this.gbxTouchRange.Size = new System.Drawing.Size(423, 62);
            this.gbxTouchRange.TabIndex = 6;
            this.gbxTouchRange.TabStop = false;
            // 
            // lblTouchRange
            // 
            this.lblTouchRange.Location = new System.Drawing.Point(372, 27);
            this.lblTouchRange.Name = "lblTouchRange";
            this.lblTouchRange.Size = new System.Drawing.Size(38, 12);
            this.lblTouchRange.TabIndex = 8;
            this.lblTouchRange.Text = "0%";
            this.lblTouchRange.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbrTouchRange
            // 
            this.tbrTouchRange.AutoSize = false;
            this.tbrTouchRange.LargeChange = 1;
            this.tbrTouchRange.Location = new System.Drawing.Point(143, 20);
            this.tbrTouchRange.Maximum = 15;
            this.tbrTouchRange.Name = "tbrTouchRange";
            this.tbrTouchRange.Size = new System.Drawing.Size(231, 31);
            this.tbrTouchRange.TabIndex = 7;
            this.tbrTouchRange.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbrTouchRange.ValueChanged += new System.EventHandler(this.tbrTouchRange_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Expand Touch Range";
            // 
            // tmrExitFlagRemover
            // 
            this.tmrExitFlagRemover.Interval = 1000;
            this.tmrExitFlagRemover.Tick += new System.EventHandler(this.tmrExitFlagRemover_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 403);
            this.Controls.Add(this.gbxTouchRange);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxCurrentTitle);
            this.Controls.Add(this.btnConnectToAnalyser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.Opacity = 0.8D;
            this.Text = "jubeat analyser touch controller";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.ResizeEnd += new System.EventHandler(this.frmMain_ResizeEnd);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMain_Paint);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.gbxTouchRange.ResumeLayout(false);
            this.gbxTouchRange.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrTouchRange)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnectToAnalyser;
        private System.Windows.Forms.TextBox tbxCurrentTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbxTouchRange;
        private System.Windows.Forms.Label lblTouchRange;
        private System.Windows.Forms.TrackBar tbrTouchRange;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer tmrExitFlagRemover;

    }
}

