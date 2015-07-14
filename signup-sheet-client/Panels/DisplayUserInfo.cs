using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace signup_sheet_client.Panels
{
    public partial class DisplayUserInfo : UserControl
    {
        private const double splitterPercentage = 0.25;

        public DisplayUserInfo()
        {
            InitializeComponent();

            // Set splitter location.
            this.displayRegion.SplitterDistance = (int)(this.displayRegion.ClientSize.Width * splitterPercentage);
            this.nameRegion.SplitterDistance = (int)(this.displayRegion.ClientSize.Width * 0.5);
        }

        public string RegId
        {
            set
            {
                this.regId.Text = value;
            }
        }

        public string FirstName
        {
            set
            {
                this.firstName.Text = value;
            }
        }

        public string LastName
        {
            set
            {
                this.lastName.Text = value;
            }
        }

        public bool Visibility
        {
            set
            {
                this.Visible = value;
            }
        }
    }
}
