using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace _30.Lambda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Action<string> _aStepCheck = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            _aStepCheck = (test) => lblStepCheck.Text = string.Format(" - 다음 Step은 {0}.{1}", iNowStep,
                ((enumLambdaCase)iNowStep).ToString());
            _aStepCheck("test");

            ButtonColorChange();
        }
        private void ButtonColorChange()
        {
            // Event 함수에서 색상 변경
            btnColorChange_1.Click += BtnColorChange_1_Click;

            // 무명 메서드 
            btnColorChange_2.Click += delegate (object sender, EventArgs e)
            {
                btnColorChange_2.BackColor = Color.Brown;
            };

            // 람다식
            //btnColorChange_3.Click += (sender, e) => btnColorChange_3.BackColor = Color.Coral;
            btnColorChange_3.Click += (a, b) => btnColorChange_3.BackColor = Color.Coral;

        }

        private void BtnColorChange_1_Click(object sender, EventArgs e)
        {
            btnColorChange_1.BackColor = Color.Aqua;
        }


        int iNowStep = 0;
        delegate int delIntFunc(int a, int b);
        delegate string delStringFunc(); 

        private void btnNext_Click(object sender, EventArgs e)
        {
            Lambda(iNowStep);
            iNowStep++;
            _aStepCheck("test");
        }

        private void Lambda(int iCase)
        {
            switch (iCase)
            {
                case (int)enumLambdaCase.식형식_람다식:
                    // 식형식 람다식
                    delIntFunc dint = (a, b) => a * b;
                    int iRet = dint(4, 5);
                    lboxResult.Items.Add(iRet.ToString());

                    delStringFunc dString = () => string.Format("Lambda Sample 식형식");
                    string strRet = dString();
                    lboxResult.Items.Add(strRet.ToString());


                    break;
                case (int)enumLambdaCase.문형식_람다식:
                    // 문형식 람다식
                    delStringFunc dstrSeqment = () =>
                    {
                        return string.Format("Lambda Sample 문형식");
                    };
                    string strSeqment = dstrSeqment();
                    lboxResult.Items.Add(strSeqment.ToString());


                    break;
                case (int)enumLambdaCase.제네릭_형태의_무명메서드_Func:
                    // 제네릭 형태의 무명 메서드
                    // return을 해야함

                    // Func : 반환 값이 있는 형태
                    Func<int, int, int>  fint = (a, b) => a * b;
                    int fintRet = fint(4, 5);
                    lboxResult.Items.Add(fintRet.ToString());
                     
                    break;

                case (int)enumLambdaCase.제네릭_형태의_무명메서드_Action:
                    // return을 하지 않아도 됨
                    Action<string, string> aString = (a, b) =>
                    {
                        string strText = String.Format("인자 값 {0}와 {1}을 받았습니다.", a, b);
                        lboxResult.Items.Add(strText.ToString());
                    };
                    aString("시간", "금");

                    break;
                case (int)enumLambdaCase.제네릭_형태의인자_반환_예제:

                    int[] iGroup = { 1, 3, 5, 7, 9 };
                    int iNumSum = iGroup.Sum(x => x);
                    lboxResult.Items.Add(iNumSum.ToString());

                    string[] strGroup = { "XXX", "TTTT", "YYY" };
                    int ilengthSum = strGroup.Sum(x => x.Length);
                    lboxResult.Items.Add(ilengthSum.ToString());

                    break;
            }
        }

        enum enumLambdaCase
        {
            식형식_람다식 = 0,
            문형식_람다식 = 1,
            제네릭_형태의_무명메서드_Func = 2,
            제네릭_형태의_무명메서드_Action = 3,
            제네릭_형태의인자_반환_예제 = 4
        }
    }
}
