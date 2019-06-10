using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CalculatorDeliverables.Helpers
{
    class ConversionHelper
    {
        public MainWindow MainWindow { get; set; }
        public List<MenuItem> AllMenuItems { get; set; }
        public List<MenuItem> BaseConversionItems { get; set; }
        public List<MenuItem> PercentConversionItems { get; set; }
        public List<MenuItem> WeightConversionItems { get; set; }
        public List<MenuItem> TemperatureConversionItems { get; set; }
        public List<MenuItem> LengthConversionItems { get; set; }
        public List<MenuItem> DigitalSizeConversionItems { get; set; }
        public List<MenuItem> TimeConversionItems { get; set; }
        public List<Label> ResultLabels { get; set; }
        public UnitCalculationHelper UnitCalculationHelper { get; set; }
        public bool ButtonsDisabled { get; set; }

        public ConversionHelper(MainWindow mainwindow)
        {
            MainWindow = mainwindow;
            UnitCalculationHelper = new UnitCalculationHelper();
            ButtonsDisabled = false;
            AllMenuItems = new List<MenuItem>
            {
                MainWindow.intMenuItem, MainWindow.hexaMenuItem, MainWindow.octalMenuItem, MainWindow.binaryMenuItem, MainWindow.mgMenuItem, MainWindow.gramMenuItem, MainWindow.kgMenuItem, MainWindow.ounceMenuItem, MainWindow.poundMenuItem, MainWindow.celsiusMenuItem, MainWindow.farenheitMenuItem, MainWindow.mmMenuItem, MainWindow.cmMenuItem, MainWindow.meterMenuItem, MainWindow.kmMenuItem, MainWindow.inchMenuItem, MainWindow.feetMenuItem, MainWindow.byteMenuItem, MainWindow.kbMenuItem, MainWindow.mbMenuItem, MainWindow.gbMenuItem, MainWindow.tbMenuItem, MainWindow.hourMenuItem, MainWindow.minMenuItem, MainWindow.secMenuItem, MainWindow.percentMenuItem, MainWindow.decimalMenuItem
            };
            BaseConversionItems = new List<MenuItem>
            {
                MainWindow.intMenuItem, MainWindow.hexaMenuItem, MainWindow.octalMenuItem, MainWindow.binaryMenuItem
            };
            PercentConversionItems = new List<MenuItem>
            {
                MainWindow.percentMenuItem, MainWindow.decimalMenuItem
            };
            WeightConversionItems = new List<MenuItem>
            {
                MainWindow.mgMenuItem, MainWindow.gramMenuItem, MainWindow.kgMenuItem, MainWindow.ounceMenuItem, MainWindow.poundMenuItem
            };
            TemperatureConversionItems = new List<MenuItem>
            {
                MainWindow.celsiusMenuItem, MainWindow.farenheitMenuItem
            };
            LengthConversionItems = new List<MenuItem>
            {
                MainWindow.mmMenuItem, MainWindow.cmMenuItem, MainWindow.meterMenuItem, MainWindow.kmMenuItem, MainWindow.inchMenuItem, MainWindow.feetMenuItem
            };
            DigitalSizeConversionItems = new List<MenuItem>
            {
                MainWindow.byteMenuItem, MainWindow.kbMenuItem, MainWindow.mbMenuItem, MainWindow.gbMenuItem, MainWindow.tbMenuItem
            };
            TimeConversionItems = new List<MenuItem>
            {
                MainWindow.hourMenuItem, MainWindow.minMenuItem, MainWindow.secMenuItem
            };
            ResultLabels = new List<Label>
            {
                MainWindow.resultLabel1, MainWindow.resultLabel2, MainWindow.resultLabel3, MainWindow.resultLabel4, MainWindow.resultLabel5, MainWindow.resultLabel6, MainWindow.resultLabel7, MainWindow.resultLabel8, MainWindow.resultLabel9,
            };
        }

        public void ResetConversionSystem()
        {
            MainWindow.convert_inputBox.Text = "0.00";
            MainWindow.ConvertInputBoxInResetState = true;
        }


        public void EnableDisableRelatedButtons(string menuElementHeader, bool Revert)
        {
            var inputText = MainWindow.convert_inputBox.Text;
            var nonBinaryRelatedControls = new List<Button> { MainWindow.convertBtn2, MainWindow.convertBtn3, MainWindow.convertBtn4, MainWindow.convertBtn5, MainWindow.convertBtn6, MainWindow.convertBtn7, MainWindow.convertBtn8, MainWindow.convertBtn9, MainWindow.convertBtnA, MainWindow.convertBtnB, MainWindow.convertBtnC, MainWindow.convertBtnD, MainWindow.convertBtnE, MainWindow.convertBtnF, MainWindow.convertBtnDot };

            var nonHexadecimalRelatedControls = new List<Button> { MainWindow.convertBtnDot };

            var nonIntegerRelatedControls = new List<Button> { MainWindow.convertBtnA, MainWindow.convertBtnB, MainWindow.convertBtnC, MainWindow.convertBtnD, MainWindow.convertBtnE, MainWindow.convertBtnF, MainWindow.convertBtnDot };

            var nonOctalRelatedControls = new List<Button> { MainWindow.convertBtn8, MainWindow.convertBtn9, MainWindow.convertBtnA, MainWindow.convertBtnB, MainWindow.convertBtnC, MainWindow.convertBtnD, MainWindow.convertBtnE, MainWindow.convertBtnF, MainWindow.convertBtnDot };

            var nonPercentRelatedControls = new List<Button> { MainWindow.convertBtnA, MainWindow.convertBtnB, MainWindow.convertBtnC, MainWindow.convertBtnD, MainWindow.convertBtnE, MainWindow.convertBtnF, MainWindow.convertBtnNegSign, MainWindow.convertBtnPosSign };

            var nonWeightRelatedControls = new List<Button> { MainWindow.convertBtnA, MainWindow.convertBtnB, MainWindow.convertBtnC, MainWindow.convertBtnD, MainWindow.convertBtnE, MainWindow.convertBtnF, MainWindow.convertBtnNegSign, MainWindow.convertBtnPosSign };

            var nonLengthRelatedControls = new List<Button> { MainWindow.convertBtnA, MainWindow.convertBtnB, MainWindow.convertBtnC, MainWindow.convertBtnD, MainWindow.convertBtnE, MainWindow.convertBtnF, MainWindow.convertBtnNegSign, MainWindow.convertBtnPosSign };

            var nonDigitalSizeRelatedControls = new List<Button> { MainWindow.convertBtnA, MainWindow.convertBtnB, MainWindow.convertBtnC, MainWindow.convertBtnD, MainWindow.convertBtnE, MainWindow.convertBtnF, MainWindow.convertBtnNegSign, MainWindow.convertBtnPosSign };

            var nonTimeRelatedControls = new List<Button> { MainWindow.convertBtnA, MainWindow.convertBtnB, MainWindow.convertBtnC, MainWindow.convertBtnD, MainWindow.convertBtnE, MainWindow.convertBtnF, MainWindow.convertBtnNegSign, MainWindow.convertBtnPosSign };

            var nonTemperatureRelatedControls = new List<Button> { MainWindow.convertBtnA, MainWindow.convertBtnB, MainWindow.convertBtnC, MainWindow.convertBtnD, MainWindow.convertBtnE, MainWindow.convertBtnF, MainWindow.convertBtnNegSign, MainWindow.convertBtnPosSign };

            if (menuElementHeader == "binary" && !Revert)
            {
                if (!IsValidBinaryString(inputText))
                {
                    ResetConversionSystem();
                    foreach (var button in nonBinaryRelatedControls)
                    {
                        button.IsEnabled = false;
                    }
                }
                else
                {
                    foreach (var button in nonBinaryRelatedControls)
                    {
                        button.IsEnabled = false;
                    }
                }
            }
            else if (menuElementHeader == "hexadecimal" && !Revert)
            {
                if (!IsValidHexString(inputText))
                {
                    ResetConversionSystem();
                    foreach (var button in nonHexadecimalRelatedControls)
                    {
                        button.IsEnabled = false;
                    }
                }
                else
                {
                    foreach (var button in nonHexadecimalRelatedControls)
                    {
                        button.IsEnabled = false;
                    }
                }
            }
            else if (menuElementHeader == "integer" && !Revert)
            {
                if (!IsValidIntegerString(inputText))
                {
                    ResetConversionSystem();
                    foreach (var button in nonIntegerRelatedControls)
                    {
                        button.IsEnabled = false;
                    }
                }
                else
                {
                    foreach (var button in nonIntegerRelatedControls)
                    {
                        button.IsEnabled = false;
                    }
                }
            }
            else if (menuElementHeader == "octal" && !Revert)
            {
                if (!IsValidOctalString(inputText))
                {
                    ResetConversionSystem();
                    foreach (var button in nonOctalRelatedControls)
                    {
                        button.IsEnabled = false;
                    }
                }
                else
                {
                    foreach (var button in nonOctalRelatedControls)
                    {
                        button.IsEnabled = false;
                    }
                }
            }
            else if(menuElementHeader == "decimal" || menuElementHeader == "percent")
            {
                if (!IsValidDecimalString(inputText))
                {
                    ResetConversionSystem();
                    foreach (var button in nonPercentRelatedControls)
                    {
                        button.IsEnabled = false;
                    }
                }
                else
                {
                    foreach (var button in nonPercentRelatedControls)
                    {
                        button.IsEnabled = false;
                    }
                }
            }
            else if ((menuElementHeader == "mg" || menuElementHeader == "gram" || menuElementHeader == "kg" || menuElementHeader == "ounce" || menuElementHeader == "pound") && !Revert)
            {
                if (!IsValidDecimalString(inputText))
                {
                    ResetConversionSystem();
                    foreach (var button in nonWeightRelatedControls)
                    {
                        button.IsEnabled = false;
                    }
                }
                else
                {
                    foreach (var button in nonWeightRelatedControls)
                    {
                        button.IsEnabled = false;
                    }
                }
            }
            else if ((menuElementHeader == "mm" || menuElementHeader == "cm" || menuElementHeader == "meter" || menuElementHeader == "km" || menuElementHeader == "inch" || menuElementHeader == "feet") && !Revert)
            {
                if (!IsValidDecimalString(inputText))
                {
                    ResetConversionSystem();
                    foreach (var button in nonLengthRelatedControls)
                    {
                        button.IsEnabled = false;
                    }
                }
                else
                {
                    foreach (var button in nonLengthRelatedControls)
                    {
                        button.IsEnabled = false;
                    }
                }
            }
            else if ((menuElementHeader == "byte" || menuElementHeader == "kb" || menuElementHeader == "mb" || menuElementHeader == "gb" || menuElementHeader == "tb") && !Revert)
            {
                if (!IsValidDecimalString(inputText))
                {
                    ResetConversionSystem();
                    foreach (var button in nonDigitalSizeRelatedControls)
                    {
                        button.IsEnabled = false;
                    }
                }
                else
                {
                    foreach (var button in nonDigitalSizeRelatedControls)
                    {
                        button.IsEnabled = false;
                    }
                }
            }
            else if ((menuElementHeader == "sec" || menuElementHeader == "min" || menuElementHeader == "hour") && !Revert)
            {
                if (!IsValidDecimalString(inputText))
                {
                    ResetConversionSystem();
                    foreach (var button in nonTimeRelatedControls)
                    {
                        button.IsEnabled = false;
                    }
                }
                else
                {
                    foreach (var button in nonTimeRelatedControls)
                    {
                        button.IsEnabled = false;
                    }
                }
            }
            else if ((menuElementHeader == "celsius" || menuElementHeader == "farenheit") && !Revert)
            {
                if (!IsValidDecimalString(inputText))
                {
                    ResetConversionSystem();
                    foreach (var button in nonTemperatureRelatedControls)
                    {
                        button.IsEnabled = false;
                    }
                }
                else
                {
                    foreach (var button in nonTemperatureRelatedControls)
                    {
                        button.IsEnabled = false;
                    }
                }
            }
            else if (Revert)
            {
                foreach (var button in nonBinaryRelatedControls)
                {
                    button.IsEnabled = true;
                }

                foreach (var button in nonIntegerRelatedControls)
                {
                    button.IsEnabled = true;
                }

                foreach (var button in nonHexadecimalRelatedControls)
                {
                    button.IsEnabled = true;
                }

                foreach (var button in nonOctalRelatedControls)
                {
                    button.IsEnabled = true;
                }

                foreach (var button in nonWeightRelatedControls)
                {
                    button.IsEnabled = true;
                }

                foreach (var button in nonDigitalSizeRelatedControls)
                {
                    button.IsEnabled = true;
                }

                foreach (var button in nonTimeRelatedControls)
                {
                    button.IsEnabled = true;
                }

                foreach (var button in nonTemperatureRelatedControls)
                {
                    button.IsEnabled = true;
                }

                foreach (var button in nonPercentRelatedControls)
                {
                    button.IsEnabled = true;
                }

                ButtonsDisabled = false;
            }
        }


        public void EnableDisableMenues(string nameOfItem, bool isChecked)
        {
            if (isChecked)
            {
                foreach (var element in AllMenuItems)
                {
                    if (element.Name != nameOfItem)
                    {
                        element.IsEnabled = false;
                    }
                    else
                    {
                        MainWindow.menuItemDefault.Header = element.Header.ToString();

                        if (BaseConversionItems.Contains(element))
                        {
                            for (var i = 0; i < BaseConversionItems.Count() - 1; i++)
                            {
                                ResultLabels[i].IsEnabled = true;
                                ResultLabels[i].Visibility = Visibility.Visible;
                                MainWindow.convertButton.IsEnabled = true;

                                var viableResultLabelNames = new List<string>();

                                foreach (var baseItem in BaseConversionItems)
                                {
                                    if (baseItem.Name != element.Name)
                                    {
                                        viableResultLabelNames.Add(baseItem.Header.ToString());
                                    }
                                    else
                                    {
                                        if (!ButtonsDisabled)
                                        {
                                            EnableDisableRelatedButtons(element.Header.ToString(), false);
                                            ButtonsDisabled = true;
                                        }
                                    }
                                }




                                ResultLabels[i].Content = viableResultLabelNames[i];
                            }
                        }
                        else if (PercentConversionItems.Contains(element))
                        {
                            for (var i = 0; i < PercentConversionItems.Count() - 1; i++)
                            {
                                ResultLabels[i].IsEnabled = true;
                                ResultLabels[i].Visibility = Visibility.Visible;
                                MainWindow.convertButton.IsEnabled = true;


                                var viableResultLabelNames = new List<string>();

                                foreach (var percentItem in PercentConversionItems)
                                {
                                    if (percentItem.Name != element.Name)
                                    {
                                        viableResultLabelNames.Add(percentItem.Header.ToString());
                                    }
                                    else
                                    {
                                        if (!ButtonsDisabled)
                                        {
                                            EnableDisableRelatedButtons(element.Header.ToString(), false);
                                            ButtonsDisabled = true;
                                        }
                                    }
                                }


                                ResultLabels[i].Content = viableResultLabelNames[i];
                            }
                        }
                        else if (WeightConversionItems.Contains(element))
                        {
                            for (var i = 0; i < WeightConversionItems.Count() - 1; i++)
                            {
                                ResultLabels[i].IsEnabled = true;
                                ResultLabels[i].Visibility = Visibility.Visible;
                                MainWindow.convertButton.IsEnabled = true;


                                var viableResultLabelNames = new List<string>();

                                foreach (var weightItem in WeightConversionItems)
                                {
                                    if (weightItem.Name != element.Name)
                                    {
                                        viableResultLabelNames.Add(weightItem.Header.ToString());
                                    }
                                    else
                                    {
                                        if (!ButtonsDisabled)
                                        {
                                            EnableDisableRelatedButtons(element.Header.ToString(), false);
                                            ButtonsDisabled = true;
                                        }
                                    }
                                }


                                ResultLabels[i].Content = viableResultLabelNames[i];
                            }
                        }
                        else if (TemperatureConversionItems.Contains(element))
                        {
                            for (var i = 0; i < TemperatureConversionItems.Count() - 1; i++)
                            {
                                ResultLabels[i].IsEnabled = true;
                                ResultLabels[i].Visibility = Visibility.Visible;
                                MainWindow.convertButton.IsEnabled = true;

                                var viableResultLabelNames = new List<string>();

                                foreach (var temperatureItem in TemperatureConversionItems)
                                {
                                    if (temperatureItem.Name != element.Name)
                                    {
                                        viableResultLabelNames.Add(temperatureItem.Header.ToString());
                                    }
                                    else
                                    {
                                        if (!ButtonsDisabled)
                                        {
                                            EnableDisableRelatedButtons(element.Header.ToString(), false);
                                            ButtonsDisabled = true;
                                        }
                                    }
                                }


                                ResultLabels[i].Content = viableResultLabelNames[i];
                            }
                        }
                        else if (LengthConversionItems.Contains(element))
                        {
                            for (var i = 0; i < LengthConversionItems.Count() - 1; i++)
                            {
                                ResultLabels[i].IsEnabled = true;
                                ResultLabels[i].Visibility = Visibility.Visible;
                                MainWindow.convertButton.IsEnabled = true;

                                var viableResultLabelNames = new List<string>();

                                foreach (var lengthItem in LengthConversionItems)
                                {
                                    if (lengthItem.Name != element.Name)
                                    {
                                        viableResultLabelNames.Add(lengthItem.Header.ToString());
                                    }
                                    else
                                    {
                                        if (!ButtonsDisabled)
                                        {
                                            EnableDisableRelatedButtons(element.Header.ToString(), false);
                                            ButtonsDisabled = true;
                                        }
                                    }
                                }


                                ResultLabels[i].Content = viableResultLabelNames[i];
                            }
                        }
                        else if (DigitalSizeConversionItems.Contains(element))
                        {
                            for (var i = 0; i < DigitalSizeConversionItems.Count() - 1; i++)
                            {
                                ResultLabels[i].IsEnabled = true;
                                ResultLabels[i].Visibility = Visibility.Visible;
                                MainWindow.convertButton.IsEnabled = true;


                                var viableResultLabelNames = new List<string>();

                                foreach (var digitalSizeItem in DigitalSizeConversionItems)
                                {
                                    if (digitalSizeItem.Name != element.Name)
                                    {
                                        viableResultLabelNames.Add(digitalSizeItem.Header.ToString());
                                    }
                                    else
                                    {
                                        if (!ButtonsDisabled)
                                        {
                                            EnableDisableRelatedButtons(element.Header.ToString(), false);
                                            ButtonsDisabled = true;
                                        }
                                    }
                                }


                                ResultLabels[i].Content = viableResultLabelNames[i];
                            }
                        }
                        else if (TimeConversionItems.Contains(element))
                        {
                            for (var i = 0; i < TimeConversionItems.Count() - 1; i++)
                            {
                                ResultLabels[i].IsEnabled = true;
                                ResultLabels[i].Visibility = Visibility.Visible;
                                MainWindow.convertButton.IsEnabled = true;

                                var viableResultLabelNames = new List<string>();

                                foreach (var timeItem in TimeConversionItems)
                                {
                                    if (timeItem.Name != element.Name)
                                    {
                                        viableResultLabelNames.Add(timeItem.Header.ToString());
                                    }
                                    else
                                    {
                                        if (!ButtonsDisabled)
                                        {
                                            EnableDisableRelatedButtons(element.Header.ToString(), false);
                                            ButtonsDisabled = true;
                                        }
                                    }
                                }


                                ResultLabels[i].Content = viableResultLabelNames[i];
                            }
                        }


                    }
                }
            }
            else
            {
                foreach (var element in AllMenuItems)
                {
                    element.IsEnabled = true;
                }
                foreach (var element in ResultLabels)
                {
                    element.IsEnabled = false;
                    element.Visibility = Visibility.Collapsed;
                }
                MainWindow.convertButton.IsEnabled = false;
                MainWindow.menuItemDefault.Header = "Unit";

                EnableDisableRelatedButtons(null, true);                
            }
        }


        public void ChangeConvertInputBoxInResetState()
        {
            //MainWindow.convert_inputBox.Text = "0.00";
            MainWindow.ConvertInputBoxInResetState = true;
        }

        public bool IsValidBinaryString(string binaryString)
        {
            NumberStyles styles = NumberStyles.AllowLeadingSign;

            var nonbinaryNumbers = new int[8] { 2, 3, 4, 5, 6, 7, 8, 9 };

            for (var i = 0; i < nonbinaryNumbers.Count(); i++)
            {
                if (binaryString.Contains($"{nonbinaryNumbers[i]}"))
                {
                    return false;
                }
            }


            bool success = Int32.TryParse(binaryString, styles,
                                   null, out int number);

            return success;
        }


        public bool IsValidHexString(string hexString)
        {
            //NumberStyles styles = NumberStyles.HexNumber | NumberStyles.AllowLeadingSign;            

            bool success;

            string hexStringWithoutSign;
            
            if (hexString.StartsWith("-") || hexString.StartsWith("+"))
            {
                //int number;
                hexStringWithoutSign = hexString.Substring(1);
                //success = Int32.TryParse(hexStringWithoutSign, styles,
                                   //null, out number);

                success = hexStringWithoutSign.Select(currentCharacter =>
                (currentCharacter >= '0' && currentCharacter <= '9') ||
                (currentCharacter >= 'A' && currentCharacter <= 'F')).All(isHexCharacter => isHexCharacter);
            }
            else
            {
                //int number;
                //success = Int32.TryParse(hexString, styles,
                //null, out number);

                success = hexString.Select(currentCharacter =>
                (currentCharacter >= '0' && currentCharacter <= '9') ||
                (currentCharacter >= 'A' && currentCharacter <= 'F')).All(isHexCharacter => isHexCharacter);
            }

            return success;
        }

        public bool IsValidOctalString(string octalString)
        {
            NumberStyles styles = NumberStyles.Number | NumberStyles.AllowLeadingSign | NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite;

            if (octalString.Contains('8') || octalString.Contains('9'))
            {
                return false;
            }

            bool success;

            string octalStringWithoutSign;
            if(octalString.StartsWith("-") || octalString.StartsWith("+"))
            {
                octalStringWithoutSign = octalString.Substring(1);
                success = Int32.TryParse(octalStringWithoutSign, styles,
                                   null, out int number);
            }
            else
            {
                success = Int32.TryParse(octalString, styles,
                                   null, out int number);
            }

            return success;
        }

        public bool IsValidIntegerString(string integerString)
        {
            NumberStyles styles = NumberStyles.Integer;

            bool success = Int32.TryParse(integerString, styles,
                                   null, out int number);

            return success;
        }


        public bool IsValidDecimalString(string decimalString)
        {
            NumberStyles styles = NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingWhite | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;

            bool success = decimal.TryParse(decimalString, styles,
                                   null, out decimal number);

            return success;
        }



        public string DetermineCategoryOfUnit(string nameOfUnit)
        {
            List<string> returnValues = new List<string> { "BaseConversion", "PercentConversion", "WeightConversion", "TemperatureConversion", "LengthConversion", "DigitalSizeConversion", "TimeConversion" };

            if (BaseConversionItems.Select(p => p.Header).ToList().Contains(nameOfUnit))
            {
                return returnValues[0];
            }
            else if(PercentConversionItems.Select(p => p.Header).ToList().Contains(nameOfUnit))
            {
                return returnValues[1];
            }
            else if (WeightConversionItems.Select(p => p.Header).ToList().Contains(nameOfUnit))
            {
                return returnValues[2];
            }
            else if (TemperatureConversionItems.Select(p => p.Header).ToList().Contains(nameOfUnit))
            {
                return returnValues[3];
            }
            else if (LengthConversionItems.Select(p => p.Header).ToList().Contains(nameOfUnit))
            {
                return returnValues[4];
            }
            else if (DigitalSizeConversionItems.Select(p => p.Header).ToList().Contains(nameOfUnit))
            {
                return returnValues[5];
            }
            else if (TimeConversionItems.Select(p => p.Header).ToList().Contains(nameOfUnit))
            {
                return returnValues[6];
            }
            else
            {
                return null;
            }
        }




    }
}
