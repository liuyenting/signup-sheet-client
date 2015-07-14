namespace signup_sheet_client.Panels
{
    partial class DisplayUserInfo
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
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.displayRegion = new System.Windows.Forms.SplitContainer();
            this.regId = new System.Windows.Forms.Label();
            this.nameRegion = new System.Windows.Forms.SplitContainer();
            this.firstName = new System.Windows.Forms.Label();
            this.lastName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.displayRegion)).BeginInit();
            this.displayRegion.Panel1.SuspendLayout();
            this.displayRegion.Panel2.SuspendLayout();
            this.displayRegion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nameRegion)).BeginInit();
            this.nameRegion.Panel1.SuspendLayout();
            this.nameRegion.Panel2.SuspendLayout();
            this.nameRegion.SuspendLayout();
            this.SuspendLayout();
            // 
            // displayRegion
            // 
            this.displayRegion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayRegion.IsSplitterFixed = true;
            this.displayRegion.Location = new System.Drawing.Point(0, 0);
            this.displayRegion.Name = "displayRegion";
            // 
            // displayRegion.Panel1
            // 
            this.displayRegion.Panel1.Controls.Add(this.regId);
            // 
            // displayRegion.Panel2
            // 
            this.displayRegion.Panel2.Controls.Add(this.nameRegion);
            this.displayRegion.Size = new System.Drawing.Size(821, 217);
            this.displayRegion.SplitterDistance = 273;
            this.displayRegion.TabIndex = 0;
            // 
            // regId
            // 
            this.regId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.regId.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.regId.Location = new System.Drawing.Point(0, 0);
            this.regId.Name = "regId";
            this.regId.Size = new System.Drawing.Size(273, 217);
            this.regId.TabIndex = 0;
            this.regId.Text = "DL000";
            this.regId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nameRegion
            // 
            this.nameRegion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameRegion.Location = new System.Drawing.Point(0, 0);
            this.nameRegion.Name = "nameRegion";
            this.nameRegion.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // nameRegion.Panel1
            // 
            this.nameRegion.Panel1.Controls.Add(this.firstName);
            // 
            // nameRegion.Panel2
            // 
            this.nameRegion.Panel2.Controls.Add(this.lastName);
            this.nameRegion.Size = new System.Drawing.Size(544, 217);
            this.nameRegion.SplitterDistance = 113;
            this.nameRegion.TabIndex = 0;
            // 
            // firstName
            // 
            this.firstName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.firstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firstName.Location = new System.Drawing.Point(0, 0);
            this.firstName.Name = "firstName";
            this.firstName.Size = new System.Drawing.Size(544, 113);
            this.firstName.TabIndex = 0;
            this.firstName.Text = "FIRSTNAME";
            this.firstName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lastName
            // 
            this.lastName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastName.Location = new System.Drawing.Point(0, 0);
            this.lastName.Name = "lastName";
            this.lastName.Size = new System.Drawing.Size(544, 100);
            this.lastName.TabIndex = 0;
            this.lastName.Text = "LASTNAME";
            this.lastName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DisplayUserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.displayRegion);
            this.Name = "DisplayUserInfo";
            this.Size = new System.Drawing.Size(821, 217);
            this.displayRegion.Panel1.ResumeLayout(false);
            this.displayRegion.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.displayRegion)).EndInit();
            this.displayRegion.ResumeLayout(false);
            this.nameRegion.Panel1.ResumeLayout(false);
            this.nameRegion.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nameRegion)).EndInit();
            this.nameRegion.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer displayRegion;
        private System.Windows.Forms.Label regId;
        private System.Windows.Forms.SplitContainer nameRegion;
        private System.Windows.Forms.Label firstName;
        private System.Windows.Forms.Label lastName;
    }
}
