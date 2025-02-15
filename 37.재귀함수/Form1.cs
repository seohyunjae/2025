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

            string strChangeText = tboxChangeText.Text;

            ControlSearch(gboxCheckList, iLevel, enControlType, strChangeText);
        }

        private void ControlSearch(GroupBox CheckList, int iLevel, enControlType eType, string strChangeText)
        {
            foreach (var item in CheckList.Controls)
            {
                if (CheckList.Text.Equals("Level " + iLevel.ToString()))
                { 
                     switch (eType)
                     {
                         case enControlType.Label:
                            if (item is Label)
                            {
                                ((Label)item).Text = strChangeText;
                                lboxResult.Items.Add(string.Format("현재 GROUPBOX : {0}, Label Text : {1}로 변경", CheckList.Text, strChangeText));
                            }
                             break;
                         case enControlType.Textbox:
                            if (item is TextBox)
                            {
                                ((TextBox)item).Text = strChangeText;
                                lboxResult.Items.Add(string.Format("현재 GROUPBOX : {0}, Label Text : {1}로 변경", CheckList.Text, strChangeText));
                            }
                            break;
                         case enControlType.Button:
                            if (item is Button) 
                            { 
                                ((Button)item).Text = strChangeText;
                                lboxResult.Items.Add(string.Format("현재 GROUPBOX : {0}, Label Text : {1}로 변경", CheckList.Text, strChangeText));
                            }
                            break;
                     }
                }
                if (item is GroupBox)
                {
                    lboxResult.Items.Add(string.Format("현재 GROUPBOX : {0}, Label Text : {1}로 변경", CheckList.Text, strChangeText));
                    ControlSearch((GroupBox)item, iLevel, eType, strChangeText);
                }
            }

            if (CheckList == gboxCheckList)
            {
                lboxResult.Items.Add(string.Format("END"));
            }
        }
    }
}
