/**
 * signup-sheet-client
 * Copyright (C) 2015 Yen-Ting Liu
 * windows.linux.mac@gmail.com
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License along
 * with this program; if not, write to the Free Software Foundation, Inc.,
 * 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace signup_sheet_client
{
    public partial class AskForServer : Form
    {
        private string newAddress;

        public AskForServer()
        {
            InitializeComponent();

            // Default address as empty;
        }

        public AskForServer(string original)
        {
            InitializeComponent();

            this.newAddress = original;
            this.addressTextBox.Text = this.newAddress;
        }

        public string Address
        {
            get
            {
                return this.newAddress;
            }
        }

        #region Button functions.

        private void confirmButton_Click(object sender, EventArgs e)
        {
            this.newAddress = this.addressTextBox.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        #endregion
    }
}
