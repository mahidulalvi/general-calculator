using GeneralCalculator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GeneralCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BasicHelperMethods BasicHelper { get; set; }
        private ConversionHelper ConversionHelper { get; set; }
        private List<double> InputNumbers { get; set; }
        public double? Result { get; set; }
        public string Operator { get; set; }
        public bool OperatorAllowed { get; set; }
        public bool ResultShowingInCalcInput { get; set; }
        public bool IsCalcInResetState { get; set; }
        public bool InputHasRoot { get; set; }
        public bool InputHasFactorial { get; set; }
        public bool ConvertInputBoxInResetState { get; set; }
        public bool OnlyEqualSignAllowed { get; set; }
        public object UIHelper { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            BasicHelper = new BasicHelperMethods(this);
            ConversionHelper = new ConversionHelper(this);
            InputNumbers = new List<double>();
            Result = 0;
            Operator = "";
            ResultShowingInCalcInput = false;
            IsCalcInResetState = true;
            InputHasRoot = false;
            InputHasFactorial = false;
            ConvertInputBoxInResetState = true;
            OperatorAllowed = true;
            OnlyEqualSignAllowed = false;
        }

        private void BtnNums_On_Click(object sender, RoutedEventArgs e)
        {
            Button button = e.Source as Button;
            if (button != null)
            {
                if (button.Content.ToString() == ".")
                {
                    if (!IsCalcInResetState && !ResultShowingInCalcInput && !BasicHelper.InputHasDecimal(CalcInput.Text))
                    {
                        CalcInput.Text += button.Content;
                    }
                    else if (IsCalcInResetState || ResultShowingInCalcInput)
                    {
                        CalcDisplay.Text = "";

                        CalcInput.Text = "0" + button.Content;
                    }
                }
                else
                {
                    if (ResultShowingInCalcInput)
                    {
                        CalcInput.Text = "";
                    }

                    if (IsCalcInResetState)
                    {
                        CalcDisplay.Text = "";
                        if(button.Content.ToString() != "!")
                        {
                            CalcInput.Text = "";
                        }
                    }

                    var buttonContent = button.Content.ToString();

                    if (buttonContent == "0")
                    {
                        if (BasicHelper.ValidateInputOfZero(CalcInput.Text))
                        {
                            CalcInput.Text += button.Content;
                        }
                    }
                    else if (buttonContent == "√")
                    {
                        if (!InputHasRoot && !InputHasFactorial && BasicHelper.ValidateSqrtInput(CalcInput.Text))
                        {
                            CalcInput.Text += button.Content;
                        }
                    }
                    else if (buttonContent == "!")
                    {
                        if (!InputHasFactorial && !InputHasRoot && !BasicHelper.InputHasDecimal(CalcInput.Text))
                        {
                            if (BasicHelper.CheckIfANumberExistsBefore(CalcInput.Text))
                            {
                                CalcInput.Text += button.Content;
                            }
                        }
                    }
                    else
                    {
                        CalcInput.Text += button.Content;
                    }
                }


                if (CalcInput.Text.Contains("√"))
                {
                    InputHasRoot = true;
                }

                if (CalcInput.Text.Contains("!"))
                {
                    InputHasFactorial = true;
                }

                if(Operator == "^" && CalcInput.Text != "")                    
                {
                    OperatorAllowed = false;
                    if (Char.IsDigit(CalcInput.Text[CalcInput.Text.Length - 1]) || CalcInput.Text[CalcInput.Text.Length - 1] == '!')
                    {
                        OnlyEqualSignAllowed = true;
                    }
                }
                else if(Operator == "^-" && CalcInput.Text != "")
                {
                    OperatorAllowed = false;
                    if (Char.IsDigit(CalcInput.Text[CalcInput.Text.Length - 1]) || CalcInput.Text[CalcInput.Text.Length - 1] == '!')
                    {
                        OnlyEqualSignAllowed = true;
                    }
                }
                else if(Operator != "^-" && Operator != "^")
                {
                    OperatorAllowed = true;
                }
                ResultShowingInCalcInput = false;
                IsCalcInResetState = false;
            }
        }


        private void BtnEditionsOnClick(object sender, RoutedEventArgs e)
        {
            Button button = e.Source as Button;
            if (button != null)
            {
                if (CalcInput.Text.Any() && button.Name == "btnBackspace")
                {
                    var inputTextLength = CalcInput.Text.Length;
                    var editedText = CalcInput.Text.Remove(inputTextLength - 1);
                    if(CalcInput.Text.EndsWith("!"))
                    {
                        InputHasFactorial = false;
                    }
                    else if (CalcInput.Text.EndsWith("√"))
                    {
                        InputHasRoot = false;
                    }
                    else if ((Operator == "^" || Operator == "^-") && (CalcInput.Text.EndsWith("!") || (Char.IsDigit(CalcInput.Text[CalcInput.Text.Length - 1]))))
                    {
                        OnlyEqualSignAllowed = false;
                    }
                    CalcInput.Text = editedText;
                }
                else if(button.Name == "btnClear")
                {
                    CalcInput.Text = "";
                }
                else if (button.Name == "btnReset")
                {
                    CalcInput.Text = "0";
                    CalcDisplay.Text = "";


                    InputNumbers = new List<double>();
                    Result = 0;
                    Operator = "";
                    OperatorAllowed = true;
                    ResultShowingInCalcInput = true;
                    IsCalcInResetState = true;
                    InputHasRoot = false;
                    InputHasFactorial = false;
                    ConvertInputBoxInResetState = true;
                    OnlyEqualSignAllowed = false;
                }
            }
        }


        private void BtnOperators_On_Click(object sender, RoutedEventArgs e)
        {
            Button button = e.Source as Button;
            if (button != null && (OperatorAllowed || (OnlyEqualSignAllowed && button.Content.ToString() == "=")))
            {
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

                var inputTextConvertedToDouble = Convert.ToDouble(inputText);

                CalcDisplay.Text += inputText + " ";

                InputNumbers.Add(inputTextConvertedToDouble);
                if (button.Content.ToString() != "=")
                {
                    CalcDisplay.Text += " " + button.Content + " ";
                }

                if (InputNumbers.Count() > 1)
                {                    
                    Result = BasicHelper.DetermineOperatorAndCalculate(Operator, Result, inputTextConvertedToDouble);

                    if (Result == null)
                    {
                        if(Operator == "/")
                        {
                            CalcInput.Text = $"∞";
                        }
                        else
                        {
                            CalcInput.Text = "Syntax Error!";
                        }
                    }
                    else
                    {
                        CalcInput.Text = $"{Result}";
                    }

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
                    if(Operator == "^" || Operator == "^-")
                    {
                        OperatorAllowed = false;
                        OnlyEqualSignAllowed = false;
                    }
                }
                else
                {
                    InputNumbers = new List<double>();
                    Result = 0;
                    Operator = "";
                    ResultShowingInCalcInput = true;
                    IsCalcInResetState = true;
                }
            }
        }
        

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }


        private void MenuItem_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }


        private void ChildMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = e.Source as MenuItem;
            if (item != null)
            {
                if (item.IsChecked)
                {
                    ConversionHelper.EnableDisableMenues(item.Name, true);
                }
                else
                {
                    ConversionHelper.EnableDisableMenues(item.Name, false);
                }
            }
        }


        private void Convert_BtnNums_On_Click(object sender, RoutedEventArgs e)
        {

            Button button = e.Source as Button;

            if (button != null)
            {
                if (ConvertInputBoxInResetState)
                {
                    convert_inputBox.Text = "";
                }

                var stringContent = button.Content.ToString();
                var stringLength = button.Content.ToString().Length;

                var inputBoxStringContent = convert_inputBox.Text;
                var inputBoxStringLength = convert_inputBox.Text.Length;

                if (stringContent == "+" || stringContent == "-")
                {
                    if (!inputBoxStringContent.Contains("+") || !inputBoxStringContent.Contains("-"))
                    {
                        if (ConvertInputBoxInResetState)
                        {
                            convert_inputBox.Text += button.Content;
                        }
                    }
                }
                else if (stringContent == ".")
                {
                    if (!inputBoxStringContent.Contains("."))
                    {
                        convert_inputBox.Text += button.Content;
                    }
                }
                else if (stringContent == "0")
                {
                    if (inputBoxStringContent != "0")
                    {
                        convert_inputBox.Text += button.Content;
                    }
                }
                else if (stringContent == "<-")
                {
                    if (inputBoxStringLength == 1)
                    {
                        var startIndex = inputBoxStringLength - 1;
                        convert_inputBox.Text = inputBoxStringContent.Remove(startIndex);
                        ConversionHelper.ChangeConvertInputBoxInResetState();
                    }
                    else if (inputBoxStringLength > 1)
                    {
                        var startIndex = inputBoxStringLength - 1;
                        convert_inputBox.Text = inputBoxStringContent.Remove(startIndex);
                    }
                }
                else
                {
                    convert_inputBox.Text += button.Content;
                }

                if (convert_inputBox.Text.Length != 0)
                {
                    ConvertInputBoxInResetState = false;
                }
            }
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = e.Source as Button;

            if (button != null)
            {
                var menuItemString = menuItemDefault.Header.ToString();

                var valueToBeConverted = convert_inputBox.Text;

                if (menuItemString != "Unit")
                {
                    if (ConversionHelper.DetermineCategoryOfUnit(menuItemString) == "BaseConversion")
                    {
                        if ((menuItemString == "hexadecimal" && ConversionHelper.IsValidHexString(valueToBeConverted)) || (menuItemString == "integer" && ConversionHelper.IsValidIntegerString(valueToBeConverted)) || (menuItemString == "octal" && ConversionHelper.IsValidOctalString(valueToBeConverted)) || (menuItemString == "binary" && ConversionHelper.IsValidBinaryString(valueToBeConverted)))
                        {
                            BaseConversionUnits baseConversionUnits = new BaseConversionUnits(menuItemString);

                            var results = baseConversionUnits.CalculatedValues(valueToBeConverted);

                            for (var i = 0; i < results.Count(); i++)
                            {
                                var resultLabel = (Label)this.FindName($"resultLabel{i + 1}");
                                resultLabel.Content = $"{results[i].Unit} = {results[i].Value}";
                            }
                        }
                        else
                        {
                            resultLabel9.Content = "Invalid input format for the selected unit!";
                            resultLabel9.Visibility = Visibility.Visible;
                        }
                    }
                    else if (ConversionHelper.DetermineCategoryOfUnit(menuItemString) == "PercentConversion")
                    {
                        if ((menuItemString == "decimal" && ConversionHelper.IsValidDecimalString(valueToBeConverted)) || (menuItemString == "percent" && ConversionHelper.IsValidDecimalString(valueToBeConverted)))
                        {
                            PercentConversionUnits percentConversionUnits = new PercentConversionUnits(menuItemString);

                            var results = percentConversionUnits.CalculatedValues(valueToBeConverted);

                            for (var i = 0; i < results.Count(); i++)
                            {
                                var resultLabel = (Label)this.FindName($"resultLabel{i + 1}");
                                resultLabel.Content = $"{results[i].Unit} = {results[i].Value}";
                            }
                        }
                    }
                    else if (ConversionHelper.DetermineCategoryOfUnit(menuItemString) == "WeightConversion")
                    {
                        if ((menuItemString == "mg" && ConversionHelper.IsValidDecimalString(valueToBeConverted)) || (menuItemString == "gram" && ConversionHelper.IsValidDecimalString(valueToBeConverted)) || (menuItemString == "kg" && ConversionHelper.IsValidDecimalString(valueToBeConverted)) || (menuItemString == "ounce" && ConversionHelper.IsValidDecimalString(valueToBeConverted)) || (menuItemString == "pound" && ConversionHelper.IsValidDecimalString(valueToBeConverted)))
                        {
                            WeightConversionUnits weightConversionUnits = new WeightConversionUnits(menuItemString);

                            var results = weightConversionUnits.CalculatedValues(valueToBeConverted);

                            for (var i = 0; i < results.Count(); i++)
                            {
                                var resultLabel = (Label)this.FindName($"resultLabel{i + 1}");
                                resultLabel.Content = $"{results[i].Unit} = {results[i].Value}";
                            }
                        }
                    }
                    else if (ConversionHelper.DetermineCategoryOfUnit(menuItemString) == "LengthConversion")
                    {
                        if ((menuItemString == "mm" && ConversionHelper.IsValidDecimalString(valueToBeConverted)) || (menuItemString == "cm" && ConversionHelper.IsValidDecimalString(valueToBeConverted)) || (menuItemString == "meter" && ConversionHelper.IsValidDecimalString(valueToBeConverted)) || (menuItemString == "km" && ConversionHelper.IsValidDecimalString(valueToBeConverted)) || (menuItemString == "inch" && ConversionHelper.IsValidDecimalString(valueToBeConverted)) || (menuItemString == "feet" && ConversionHelper.IsValidDecimalString(valueToBeConverted)))
                        {
                            LengthConversionUnits lengthConversionUnits = new LengthConversionUnits(menuItemString);

                            var results = lengthConversionUnits.CalculatedValues(valueToBeConverted);

                            for (var i = 0; i < results.Count(); i++)
                            {
                                var resultLabel = (Label)this.FindName($"resultLabel{i + 1}");
                                resultLabel.Content = $"{results[i].Unit} = {results[i].Value}";
                            }
                        }
                    }
                    else if (ConversionHelper.DetermineCategoryOfUnit(menuItemString) == "DigitalSizeConversion")
                    {
                        if ((menuItemString == "byte" && ConversionHelper.IsValidDecimalString(valueToBeConverted)) || (menuItemString == "kb" && ConversionHelper.IsValidDecimalString(valueToBeConverted)) || (menuItemString == "mb" && ConversionHelper.IsValidDecimalString(valueToBeConverted)) || (menuItemString == "gb" && ConversionHelper.IsValidDecimalString(valueToBeConverted)) || (menuItemString == "tb" && ConversionHelper.IsValidDecimalString(valueToBeConverted)))
                        {
                            DigitalSizeConversionUnits digitalSizeConversionUnits = new DigitalSizeConversionUnits(menuItemString);

                            var results = digitalSizeConversionUnits.CalculatedValues(valueToBeConverted);

                            for (var i = 0; i < results.Count(); i++)
                            {
                                var resultLabel = (Label)this.FindName($"resultLabel{i + 1}");
                                resultLabel.Content = $"{results[i].Unit} = {results[i].Value}";
                            }
                        }
                    }
                    else if (ConversionHelper.DetermineCategoryOfUnit(menuItemString) == "TimeConversion")
                    {
                        if ((menuItemString == "hour" && ConversionHelper.IsValidDecimalString(valueToBeConverted)) || (menuItemString == "min" && ConversionHelper.IsValidDecimalString(valueToBeConverted)) || (menuItemString == "sec" && ConversionHelper.IsValidDecimalString(valueToBeConverted)))
                        {
                            TimeConversionUnits timeConversionUnits = new TimeConversionUnits(menuItemString);

                            var results = timeConversionUnits.CalculatedValues(valueToBeConverted);

                            for (var i = 0; i < results.Count(); i++)
                            {
                                var resultLabel = (Label)this.FindName($"resultLabel{i + 1}");
                                resultLabel.Content = $"{results[i].Unit} = {results[i].Value}";
                            }
                        }
                    }
                    else if (ConversionHelper.DetermineCategoryOfUnit(menuItemString) == "TemperatureConversion")
                    {
                        if ((menuItemString == "celsius" && ConversionHelper.IsValidDecimalString(valueToBeConverted)) || (menuItemString == "farenheit" && ConversionHelper.IsValidDecimalString(valueToBeConverted)))
                        {
                            TemperatureConversionUnits temperatureConversionUnits = new TemperatureConversionUnits(menuItemString);

                            var results = temperatureConversionUnits.CalculatedValues(valueToBeConverted);

                            for (var i = 0; i < results.Count(); i++)
                            {
                                var resultLabel = (Label)this.FindName($"resultLabel{i + 1}");
                                resultLabel.Content = $"{results[i].Unit} = {results[i].Value}";
                            }
                        }
                    }
                }
            }
        }
    }
}
