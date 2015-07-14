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

            // Set panel size.
            this.idRegion.Width = (int)(this.ClientSize.Width * 0.4);
            this.firstNameRegion.Height = this.lastNameRegion.Height = (int)(this.idRegion.ClientSize.Height * 0.5);
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
    }
}
