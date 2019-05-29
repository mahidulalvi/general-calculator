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

            for(var i = 0; i < input.Length; i++)
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
            if(input.Contains("."))
            {
                return true;
            }

            return false;
        }

        public decimal DetermineOperatorAndCalculate(string input, decimal referenceNumber, decimal latestInput)
        {
            if(input == "+")
            {
                return referenceNumber += latestInput;
            }
            else if(input == "-")
            {
                return referenceNumber -= latestInput;
            }
            else if(input == "*")
            {
                return referenceNumber *= latestInput;
            }
            else if (input == "/")
            {
                return referenceNumber / latestInput;
            }

            return referenceNumber;
        }

        //public void ButtonOnMouseDown(object sender, MouseEventArgs e)
        //{
        //    Button button = e.Source as Button;

        //    if(button != null)
        //    {
        //        button.CaptureMouse();
        //    }
        //}


        //public void ButtonOnMouseUp(object sender, MouseEventArgs e)
        //{
        //    Button button = e.Source as Button;

        //    if (button != null)
        //    {
        //        button.ReleaseMouseCapture();
        //        MainWindow.CalcInput.Text += button.Content;
        //    }
        //}


    }
}
