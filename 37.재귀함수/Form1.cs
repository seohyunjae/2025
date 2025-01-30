using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _37.재귀함수
{
    public partial class Form1 : Form
    {
        enum enControlType
        {
            Unknown,
            Label,
            Textbox,
            Button,
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void btnExE_Click(object sender, EventArgs e)
        {
            // Control Level, Control Type, Control Text
            int iLevel = (int)numLevel.Value;

            enControlType enControlType = enControlType.Unknown;
            if (rdoLabel.Checked) enControlType = enControlType.Label;
            else if(rdoTextbox.Checked) enControlType = enControlType.Textbox;
            else if(rdoButton.Checked) enControlType = enControlType.Button;      
                
           
            
            ControlSearch(gboxCheckList, 2, enControlType.Label, "");
        }

        private void ControlSearch(GroupBox CheckList, int iLevel, enControlType eType, string strChangeText)
        {
            foreach (var item in this.Controls)
            {

            }
        }


    }
}
