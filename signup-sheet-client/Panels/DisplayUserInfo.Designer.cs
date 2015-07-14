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
            this.displayRegion = new System.Windows.Forms.Panel();
            this.idRegion = new System.Windows.Forms.Panel();
            this.firstNameRegion = new System.Windows.Forms.Panel();
            this.lastNameRegion = new System.Windows.Forms.Panel();
            this.firstName = new System.Windows.Forms.Label();
            this.lastName = new System.Windows.Forms.Label();
            this.regId = new System.Windows.Forms.Label();
            this.displayRegion.SuspendLayout();
            this.idRegion.SuspendLayout();
            this.firstNameRegion.SuspendLayout();
            this.lastNameRegion.SuspendLayout();
            this.SuspendLayout();
            // 
            // displayRegion
            // 
            this.displayRegion.Controls.Add(this.lastNameRegion);
            this.displayRegion.Controls.Add(this.firstNameRegion);
            this.displayRegion.Controls.Add(this.idRegion);
            this.displayRegion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayRegion.Location = new System.Drawing.Point(0, 0);
            this.displayRegion.Name = "displayRegion";
            this.displayRegion.Size = new System.Drawing.Size(821, 217);
            this.displayRegion.TabIndex = 0;
            // 
            // idRegion
            // 
            this.idRegion.Controls.Add(this.regId);
            this.idRegion.Dock = System.Windows.Forms.DockStyle.Left;
            this.idRegion.Location = new System.Drawing.Point(0, 0);
            this.idRegion.Name = "idRegion";
            this.idRegion.Size = new System.Drawing.Size(200, 217);
            this.idRegion.TabIndex = 0;
            // 
            // firstNameRegion
            // 
            this.firstNameRegion.Controls.Add(this.firstName);
            this.firstNameRegion.Dock = System.Windows.Forms.DockStyle.Top;
            this.firstNameRegion.Location = new System.Drawing.Point(200, 0);
            this.firstNameRegion.Name = "firstNameRegion";
            this.firstNameRegion.Size = new System.Drawing.Size(621, 100);
            this.firstNameRegion.TabIndex = 1;
            // 
            // lastNameRegion
            // 
            this.lastNameRegion.Controls.Add(this.lastName);
            this.lastNameRegion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lastNameRegion.Location = new System.Drawing.Point(200, 106);
            this.lastNameRegion.Name = "lastNameRegion";
            this.lastNameRegion.Size = new System.Drawing.Size(621, 111);
            this.lastNameRegion.TabIndex = 2;
            // 
            // firstName
            // 
            this.firstName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.firstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firstName.Location = new System.Drawing.Point(0, 0);
            this.firstName.Name = "firstName";
            this.firstName.Size = new System.Drawing.Size(621, 100);
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
            this.lastName.Size = new System.Drawing.Size(621, 111);
            this.lastName.TabIndex = 0;
            this.lastName.Text = "LASTNAME";
            this.lastName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // regId
            // 
            this.regId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.regId.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.regId.Location = new System.Drawing.Point(0, 0);
            this.regId.Name = "regId";
            this.regId.Size = new System.Drawing.Size(200, 217);
            this.regId.TabIndex = 1;
            this.regId.Text = "DLxxx";
            this.regId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DisplayUserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.displayRegion);
            this.Name = "DisplayUserInfo";
            this.Size = new System.Drawing.Size(821, 217);
            this.displayRegion.ResumeLayout(false);
            this.idRegion.ResumeLayout(false);
            this.firstNameRegion.ResumeLayout(false);
            this.lastNameRegion.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel displayRegion;
        private System.Windows.Forms.Panel idRegion;
        private System.Windows.Forms.Panel lastNameRegion;
        private System.Windows.Forms.Panel firstNameRegion;
        private System.Windows.Forms.Label lastName;
        private System.Windows.Forms.Label firstName;
        private System.Windows.Forms.Label regId;

    }
}
