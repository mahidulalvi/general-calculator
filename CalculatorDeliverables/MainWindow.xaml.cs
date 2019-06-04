using CalculatorDeliverables.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorDeliverables
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BasicHelperMethods BasicHelper { get; set; }
        private List<decimal> InputNumbers { get; set; }
        public decimal Result { get; set; }
        public string Operator { get; set; }
        public bool OperatorAllowed { get; set; }
        public bool ResultShowingInCalcInput { get; set; }
        public bool IsCalcInResetState { get; set; }
        public bool InputHasRoot { get; set; }
        public bool InputHasFactorial { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            BasicHelper = new BasicHelperMethods(this);
            InputNumbers = new List<decimal>();
            Result = 0;
            Operator = "";
            ResultShowingInCalcInput = true;
            IsCalcInResetState = true;
            InputHasRoot = false;
            InputHasFactorial = false;
        }

        private void BtnNums_On_Click(object sender, RoutedEventArgs e)
        {
            Button button = e.Source as Button;
            if (button != null)
            {
                if (button.Content.ToString() == ".")
                {
                    if (!BasicHelper.InputHasDecimal(CalcInput.Text) && !ResultShowingInCalcInput)
                    {
                        CalcInput.Text += button.Content;
                    }
                    else if (IsCalcInResetState && ResultShowingInCalcInput)
                    {
                        CalcDisplay.Text = "";
                        CalcInput.Text = "0";

                        CalcInput.Text += button.Content;
                    }
                    else if (ResultShowingInCalcInput)
                    {
                        CalcInput.Text = "0";

                        CalcInput.Text += button.Content;
                    }
                }
                else
                {
                    if (ResultShowingInCalcInput)
                    {
                        CalcInput.Text = "";
                    }

                    if (BasicHelper.InputHasLeadingZeroWithoutDecimals(CalcInput.Text))
                    {
                        var totalLeadingZeroes = BasicHelper.NumberOfLeadingZeroes(CalcInput.Text);

                        var textWithoutLeadingZero = CalcInput.Text.Substring(totalLeadingZeroes);

                        CalcInput.Text = "";
                        CalcInput.Text = textWithoutLeadingZero;
                    }
                    else
                    {
                        CalcInput.Text += button.Content;
                    }
                }

                if (IsCalcInResetState)
                {
                    CalcDisplay.Text = "";
                }

                if(button.Content.ToString() == "√")
                {
                    InputHasRoot = true;
                }

                if(button.Content.ToString() == "!")
                {
                    InputHasFactorial = true;
                }

                ResultShowingInCalcInput = false;
                IsCalcInResetState = false;
            }
        }



        private void BtnOperators_On_Click(object sender, RoutedEventArgs e)
        {
            Button button = e.Source as Button;
            if (button != null)
            {
                //When the user inputs too many leading zeroes, this if function trims them
                if (BasicHelper.InputHasLeadingZeroWithoutDecimals(CalcInput.Text) && !IsCalcInResetState)
                {
                    var totalLeadingZeroes = BasicHelper.NumberOfLeadingZeroes(CalcInput.Text);

                    var textWithoutLeadingZero = CalcInput.Text.Substring(totalLeadingZeroes);
                    CalcInput.Text = textWithoutLeadingZero;
                }

                //Checking to see if Calculator has been reseted
                string inputText;
                if (IsCalcInResetState)
                {
                    CalcInput.Text = "0";
                    CalcDisplay.Text = "";
                }

                if (CalcInput.Text.Contains("√"))
                {
                    inputText = BasicHelper.ConvertNumberToRoot(CalcInput.Text).ToString();
                }
                else if (CalcInput.Text.Contains("!"))
                {
                    inputText = BasicHelper.DetermineFactorial(CalcInput.Text).ToString();
                }
                else
                {
                    inputText = CalcInput.Text;
                }
                var inputTextConvertedToDecimal = Convert.ToDecimal(inputText);


                CalcDisplay.Text += inputText + " ";


                InputNumbers.Add(inputTextConvertedToDecimal);
                if (button.Content.ToString() != "=")
                {
                    CalcDisplay.Text += " " + button.Content + " ";
                }
                

                if (InputNumbers.Count() > 1)
                {
                    if(InputHasRoot == true)
                    {
                        Result = BasicHelper.DetermineOperatorAndCalculate(Operator, Result, inputTextConvertedToDecimal);
                    }
                    else
                    {
                        Result = BasicHelper.DetermineOperatorAndCalculate(Operator, Result, inputTextConvertedToDecimal);
                    }


                    CalcInput.Text = $"{Result}";
                    ResultShowingInCalcInput = true;
                }
                else if (InputNumbers.Count() > 0 && InputNumbers.Count() < 2)
                {
                    Result += InputNumbers[0];
                    ResultShowingInCalcInput = true;
                }

                if (button.Content.ToString() != "=")
                {
                    Operator = button.Content.ToString();
                    IsCalcInResetState = false;                    
                }
                else
                {
                    InputNumbers = new List<decimal>();
                    Result = 0;
                    Operator = "";
                    ResultShowingInCalcInput = true;
                    IsCalcInResetState = true;
                }
            }
        }
    }
}
