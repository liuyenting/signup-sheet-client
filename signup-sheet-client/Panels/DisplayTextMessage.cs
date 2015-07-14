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
    public partial class DisplayTextMessage : UserControl
    {
        public DisplayTextMessage()
        {
            InitializeComponent();
        }

        
        public override string Text
        {
            set
            {
                this.message.Text = value;
            }
        }

        public Color Color
        {
            set
            {
                this.message.ForeColor = value;
            }
        }
    }
}
