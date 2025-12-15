#region 변경 이력
/*
 * Author : Linktech DJJUNG (2020. 10. 07)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2020-10-07   DJJUNG          최초 작성.
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Link.EFAM.Agent.UI
{
    public partial class AlertDialog : Form
    {
        public AlertDialog()
        {
            InitializeComponent();
        }

        private void buttonAlertOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
