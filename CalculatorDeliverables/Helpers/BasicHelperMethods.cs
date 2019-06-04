using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CalculatorDeliverables.Helpers
{
    class BasicHelperMethods
    {
        public MainWindow MainWindow { get; set; }

        public BasicHelperMethods(MainWindow mainwindow)
        {
            MainWindow = mainwindow;
        }

        public bool IsInputEmpty()
        {
            if (MainWindow.CalcInput.Text.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsInputInvalid()
        {
            if (MainWindow.CalcInput.Text.Contains(","))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsInputLeftUnused()
        {
            return true;
        }

        public bool InputHasLeadingZeroWithoutDecimals(string input)
        {
            if (input.StartsWith("0") && !InputHasDecimal(input))
            {
                return true;
            }

            return false;
        }

        public int NumberOfLeadingZeroes(string input)
        {
            var result = 0;
            bool leadingZeroOngoing = true;

            for (var i = 0; i < input.Length; i++)
            {
                if (leadingZeroOngoing == true)
                {
                    if (input[i] == '0')
                    {
                        result += 1;
                    }
                    else
                    {
                        leadingZeroOngoing = false;
                    }
                }
            }

            return result;
        }

        public bool InputHasDecimal(string input)
        {
            if (input.Contains("."))
            {
                return true;
            }

            return false;
        }

        public decimal DetermineOperatorAndCalculate(string input, decimal referenceNumber, decimal latestInput)
        {
            if (input == "+")
            {
                return referenceNumber += latestInput;
            }
            else if (input == "-")
            {
                return referenceNumber -= latestInput;
            }
            else if (input == "*")
            {
                return referenceNumber *= latestInput;
            }
            else if (input == "/")
            {
                return referenceNumber / latestInput;
            }
            else if (input == "%")
            {
                return referenceNumber % latestInput;
            }
            else if (input == "^")
            {
                var baseNumber = Convert.ToDouble(referenceNumber);
                var power = Convert.ToDouble(latestInput);

                var finalResult = Math.Pow(baseNumber, power);

                var finalResultInDecimal = Convert.ToDecimal(finalResult);

                return finalResultInDecimal;
            }
            //else if(input == "")
            //{
            //    var numberToBeRooted = Convert.ToDouble(latestInput);
            //    var finalResult = Math.Sqrt(numberToBeRooted);
            //    var finalResultInDecimal = Convert.ToDecimal(finalResult);

            //    return finalResultInDecimal;
            //}

            return referenceNumber;
        }


        public decimal ConvertNumberToRoot(string number)
        {
            var indexOfRoot = number.IndexOf('√');
            var numberWithRoot = number.Substring(indexOfRoot + 1);



            var numberToBeRooted = Convert.ToDouble(numberWithRoot);
            var finalResult = Math.Sqrt(numberToBeRooted);
            var finalResultInDecimal = Convert.ToDecimal(finalResult);

            decimal numberWithoutRoot;
            string numberWithoutRootAsString;
            if (indexOfRoot != 0)
            {
                numberWithoutRootAsString = number.Substring(0, indexOfRoot);
                numberWithoutRoot = Convert.ToDecimal(numberWithoutRootAsString);

                return finalResultInDecimal *= numberWithoutRoot;
            }

            return finalResultInDecimal;
        }


        public decimal? DetermineFactorial(string number)
        {
            var indexOfFactorial = number.IndexOf('!');
            var numberWithFactorial = number.Substring(0, indexOfFactorial);


            var numberInInt = Convert.ToInt32(numberWithFactorial);

            int? result;
            if (numberInInt == 0)
            {
                result = 1;
            }
            else if (numberInInt > 0)
            {
                result = numberInInt;
                for (var i = numberInInt - 1; i > 0; i--)
                {
                    result *= i;
                }
            }
            else
            {
                result = null;
            }

            if (result != null)
            {
                decimal finalResultInDecimal = Convert.ToDecimal(result);
                decimal numberWithoutFactorial;
                string numberWithoutFactorialAsString;
                if (indexOfFactorial != number.Length - 1)
                {
                    numberWithoutFactorialAsString = number.Substring(indexOfFactorial + 1);
                    numberWithoutFactorial = Convert.ToDecimal(numberWithoutFactorialAsString);

                    return finalResultInDecimal *= numberWithoutFactorial;
                }
                else
                {
                    return finalResultInDecimal;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
