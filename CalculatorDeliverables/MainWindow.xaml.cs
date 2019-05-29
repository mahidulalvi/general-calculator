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

        public MainWindow()
        {
            InitializeComponent();
            BasicHelper = new BasicHelperMethods(this);
            InputNumbers = new List<decimal>();
            Result = 0;
            Operator = "";
            OperatorAllowed = false;
            ResultShowingInCalcInput = true;
            IsCalcInResetState = true;
        }        

        private void BtnNums_On_Click(object sender, RoutedEventArgs e)
        {
            Button button = e.Source as Button;
            if(button != null)
            {
                if(button.Content.ToString() == ".")
                {
                    if (!BasicHelper.InputHasDecimal(CalcInput.Text) && ResultShowingInCalcInput == false)
                    {                     
                        CalcInput.Text += button.Content;
                        OperatorAllowed = true;                        
                    }
                }    
                else
                {
                    if(ResultShowingInCalcInput/* || IsCalcInResetState*/)
                    {
                        CalcInput.Text = "";
                    }

                    if (BasicHelper.InputHasLeadingZeroWithoutDecimals(CalcInput.Text))
                    {
                        var totalLeadingZeroes = BasicHelper.NumberOfLeadingZeroes(CalcInput.Text);

                        var textWithoutLeadingZero = CalcInput.Text.Substring(totalLeadingZeroes);

                        CalcInput.Text = "";
                        CalcInput.Text = textWithoutLeadingZero;
                        OperatorAllowed = true;                        
                    }
                    else
                    {
                        CalcInput.Text += button.Content;
                        OperatorAllowed = true;                        
                    }
                }
                ResultShowingInCalcInput = false;
                IsCalcInResetState = false;
            }
        }



        private void BtnOperators_On_Click(object sender, RoutedEventArgs e)
        {
            Button button = e.Source as Button;
            if (button != null && OperatorAllowed)
            {                
                if (BasicHelper.InputHasLeadingZeroWithoutDecimals(CalcInput.Text))
                {
                    var totalLeadingZeroes = BasicHelper.NumberOfLeadingZeroes(CalcInput.Text);

                    var textWithoutLeadingZero = CalcInput.Text.Substring(totalLeadingZeroes);
                    CalcInput.Text = textWithoutLeadingZero;
                }

                var inputText = CalcInput.Text;
                var inputTextConvertedToDecimal = Convert.ToDecimal(inputText);

                if (Operator != "-")
                {
                    CalcDisplay.Text += inputText;
                }

                InputNumbers.Add(inputTextConvertedToDecimal);
                CalcDisplay.Text += " " + button.Content + " ";

                if (InputNumbers.Count() > 1)
                {
                    Result = BasicHelper.DetermineOperatorAndCalculate(Operator, Result, inputTextConvertedToDecimal);

                    CalcInput.Text = $"{Result}";
                    ResultShowingInCalcInput = true;
                }
                else if(InputNumbers.Count() > 0 && InputNumbers.Count() < 2)
                {
                    Result += InputNumbers[0];
                    ResultShowingInCalcInput = true;
                }

                Operator = button.Content.ToString();
            }
        }                                     
    }
}
