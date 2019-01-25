using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditHWPidListGUI
{
    public partial class SearchGroupBox : Form
    {
        public SearchGroupBox()
        {
            InitializeComponent();
        }

        private void SearchGroupBox_Load(object sender, EventArgs e)
        {

        }

        private void SearchGroups_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SearchGroupButton_Click(object sender, EventArgs e)
        {
            var sgb = SearchGroupBox.ActiveForm;
            sgb.Close();
        }

        private void SearchGroupText_Click(object sender, EventArgs e)
        {

        }
    }
}
