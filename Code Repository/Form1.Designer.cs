namespace Code_Repository
{
    partial class frmCdeRep
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCdeRep));
            this.cmdAdd = new System.Windows.Forms.Button();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.rdbNet = new System.Windows.Forms.RadioButton();
            this.rdbCS = new System.Windows.Forms.RadioButton();
            this.cmdCLR = new System.Windows.Forms.Button();
            this.cmdVwCode = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.TSSLocation = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.txtProjNme = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(15, 12);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(75, 23);
            this.cmdAdd.TabIndex = 0;
            this.cmdAdd.Text = "Add code";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(15, 67);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(516, 473);
            this.txtCode.TabIndex = 2;
            // 
            // rdbNet
            // 
            this.rdbNet.AutoSize = true;
            this.rdbNet.Location = new System.Drawing.Point(6, 19);
            this.rdbNet.Name = "rdbNet";
            this.rdbNet.Size = new System.Drawing.Size(43, 17);
            this.rdbNet.TabIndex = 3;
            this.rdbNet.TabStop = true;
            this.rdbNet.Text = ".net";
            this.rdbNet.UseVisualStyleBackColor = true;
            // 
            // rdbCS
            // 
            this.rdbCS.AutoSize = true;
            this.rdbCS.Location = new System.Drawing.Point(55, 19);
            this.rdbCS.Name = "rdbCS";
            this.rdbCS.Size = new System.Drawing.Size(39, 17);
            this.rdbCS.TabIndex = 3;
            this.rdbCS.TabStop = true;
            this.rdbCS.Text = "C#";
            this.rdbCS.UseVisualStyleBackColor = true;
            // 
            // cmdCLR
            // 
            this.cmdCLR.Location = new System.Drawing.Point(96, 12);
            this.cmdCLR.Name = "cmdCLR";
            this.cmdCLR.Size = new System.Drawing.Size(75, 23);
            this.cmdCLR.TabIndex = 0;
            this.cmdCLR.Text = "Clear Code";
            this.cmdCLR.UseVisualStyleBackColor = true;
            this.cmdCLR.Click += new System.EventHandler(this.cmdLoc_Click);
            // 
            // cmdVwCode
            // 
            this.cmdVwCode.Location = new System.Drawing.Point(177, 12);
            this.cmdVwCode.Name = "cmdVwCode";
            this.cmdVwCode.Size = new System.Drawing.Size(75, 23);
            this.cmdVwCode.TabIndex = 0;
            this.cmdVwCode.Text = "View Code";
            this.cmdVwCode.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1,
            this.TSSLocation});
            this.statusStrip1.Location = new System.Drawing.Point(0, 552);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(543, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // TSSLocation
            // 
            this.TSSLocation.Name = "TSSLocation";
            this.TSSLocation.Size = new System.Drawing.Size(53, 17);
            this.TSSLocation.Text = "Location";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownButtonWidth = 100;
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(121, 20);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            // 
            // txtProjNme
            // 
            this.txtProjNme.Location = new System.Drawing.Point(98, 41);
            this.txtProjNme.Name = "txtProjNme";
            this.txtProjNme.Size = new System.Drawing.Size(154, 20);
            this.txtProjNme.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbNet);
            this.groupBox1.Controls.Add(this.rdbCS);
            this.groupBox1.Location = new System.Drawing.Point(432, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(99, 49);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Codebase";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Project Name";
            // 
            // frmCdeRep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 574);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtProjNme);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.cmdVwCode);
            this.Controls.Add(this.cmdCLR);
            this.Controls.Add(this.cmdAdd);
            this.Name = "frmCdeRep";
            this.Text = "Code Repository";
            this.Load += new System.EventHandler(this.frmCdeRep_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.RadioButton rdbNet;
        private System.Windows.Forms.RadioButton rdbCS;
        private System.Windows.Forms.Button cmdCLR;
        private System.Windows.Forms.Button cmdVwCode;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripStatusLabel TSSLocation;
        private System.Windows.Forms.TextBox txtProjNme;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
    }
}

