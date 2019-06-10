using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorDeliverables.Helpers
{
    class UnitCalculationHelper
    {

    }

    class UnitValue
    {
        public string Value { get; set; }
        public string Unit { get; set; }
    }

    class BaseConversionUnits
    {
        public string GivenUnitName { get; set; }
        public List<string> AllRelatedUnitNames { get; set; }

        public BaseConversionUnits(string unit)
        {
            AllRelatedUnitNames = new List<string> { "integer", "hexadecimal", "octal", "binary" };
            GivenUnitName = unit;
        }

        public List<UnitValue> CalculatedValues(string value/*, string unit*/)
        {
            var listOfConvertibleUnits = AllRelatedUnitNames;
            listOfConvertibleUnits.Remove(GivenUnitName);
            int unitInInteger;
            bool hasNegativeSignInTheBeginning = false;
            bool hasPositiveSignInTheBeginning = false;

            if (GivenUnitName == "integer")
            {
                string valueWithoutSign;
                if (value.StartsWith("-") || value.StartsWith("+"))
                {
                    if (value[0] == '-')
                    {
                        hasNegativeSignInTheBeginning = true;
                    }
                    else
                    {
                        hasPositiveSignInTheBeginning = true;
                    }
                    valueWithoutSign = value.Substring(1);
                    unitInInteger = Convert.ToInt32(valueWithoutSign);
                }
                else
                {
                    unitInInteger = Convert.ToInt32(value);
                }
            }
            else if (GivenUnitName == "octal")
            {
                string valueWithoutSign;
                if (value.StartsWith("-") || value.StartsWith("+"))
                {
                    if (value[0] == '-')
                    {
                        hasNegativeSignInTheBeginning = true;
                    }
                    else
                    {
                        hasPositiveSignInTheBeginning = true;
                    }
                    valueWithoutSign = value.Substring(1);
                    unitInInteger = Convert.ToInt32(valueWithoutSign, 8);
                }
                else
                {
                    unitInInteger = Convert.ToInt32(value, 8);
                }

            }
            else if (GivenUnitName == "binary")
            {

                string valueWithoutSign;
                if (value[0] == '-' || value[1] == '+')
                {
                    valueWithoutSign = value.Substring(1);
                    unitInInteger = Convert.ToInt32(valueWithoutSign, 2);

                    if (value[0] == '-')
                    {
                        hasNegativeSignInTheBeginning = true;
                    }
                    else
                    {
                        hasPositiveSignInTheBeginning = true;
                    }
                }
                else
                {
                    unitInInteger = Convert.ToInt32(value, 2);
                }
            }
            else
            {
                string valueWithoutSign;
                if (value.StartsWith("-") || value.StartsWith("+"))
                {
                    if (value[0] == '-')
                    {
                        hasNegativeSignInTheBeginning = true;
                    }
                    else
                    {
                        hasPositiveSignInTheBeginning = true;
                    }

                    valueWithoutSign = value.Substring(1);
                    unitInInteger = Convert.ToInt32(valueWithoutSign, 16);
                }
                else
                {
                    unitInInteger = Convert.ToInt32(value, 16);
                }
            }

            var integerUnit = new UnitValue
            {
                Unit = "integer",
                Value = hasNegativeSignInTheBeginning ? "-" + unitInInteger.ToString() : hasPositiveSignInTheBeginning ? "+" + unitInInteger.ToString() : unitInInteger.ToString()
            };

            var binaryUnit = new UnitValue
            {
                Unit = "binary",
                Value = hasNegativeSignInTheBeginning ? "-" + Convert.ToString(unitInInteger, 2) : hasPositiveSignInTheBeginning ? "+" + Convert.ToString(unitInInteger, 2) : Convert.ToString(unitInInteger, 2)        /*Convert.ToString(unitInInteger, 2)*/
            };

            var octalUnit = new UnitValue
            {
                Unit = "octal",
                Value = hasNegativeSignInTheBeginning ? "-" + Convert.ToString(unitInInteger, 8) : hasPositiveSignInTheBeginning ? "+" + Convert.ToString(unitInInteger, 8) : Convert.ToString(unitInInteger, 8)
            };

            var hexadecimalUnit = new UnitValue
            {
                Unit = "hexadecimal",
                Value = hasNegativeSignInTheBeginning ? "-" + unitInInteger.ToString("X") : hasPositiveSignInTheBeginning ? "+" + unitInInteger.ToString("X") : unitInInteger.ToString("X")
            };

            List<UnitValue> preResults = new List<UnitValue> { integerUnit, binaryUnit, octalUnit, hexadecimalUnit };
            List<UnitValue> results = new List<UnitValue>();

            foreach (var preResult in preResults)
            {
                if (listOfConvertibleUnits.Contains(preResult.Unit))
                {
                    results.Add(preResult);
                }
            }

            return results;
        }
    }


    class WeightConversionUnits
    {
        public string GivenUnitName { get; set; }
        public List<string> AllRelatedUnitNames { get; set; }

        public WeightConversionUnits(string unit)
        {
            AllRelatedUnitNames = new List<string> { "mg", "gram", "kg", "ounce", "pound" };
            GivenUnitName = unit;
        }

        public List<UnitValue> CalculatedValues(string value)
        {
            var listOfConvertibleUnits = AllRelatedUnitNames;
            listOfConvertibleUnits.Remove(GivenUnitName);
            double unitInDouble;

            if (GivenUnitName == "mg")
            {
                unitInDouble = Convert.ToDouble(value);
            }
            else if (GivenUnitName == "gram")
            {
                unitInDouble = Convert.ToDouble(value);
            }
            else if (GivenUnitName == "kg")
            {
                unitInDouble = Convert.ToDouble(value);
            }
            else if (GivenUnitName == "ounce")
            {
                unitInDouble = Convert.ToDouble(value);
            }
            else
            {
                unitInDouble = Convert.ToDouble(value);
            }

            double valueResult = unitInDouble;

            var mgUnit = new UnitValue
            {
                Unit = "mg",
                Value = GivenUnitName == "gram" ? (valueResult * 1000).ToString() : GivenUnitName == "kg" ? (valueResult * 1000000).ToString() : GivenUnitName == "ounce" ? (valueResult * 28349.523
                    ).ToString() : GivenUnitName == "pound" ? (valueResult * 453592.37).ToString() : valueResult.ToString() 
            };            

            var gramUnit = new UnitValue
            {
                Unit = "gram",
                Value = GivenUnitName == "mg" ? (valueResult / 1000).ToString() : GivenUnitName == "kg" ? (valueResult * 1000).ToString() : GivenUnitName == "ounce" ? (valueResult * 28.35).ToString() : GivenUnitName == "pound" ? (valueResult * 453.592).ToString() : valueResult.ToString()
            };

            var kgUnit = new UnitValue
            {
                Unit = "kg",
                Value = GivenUnitName == "mg" ? (valueResult / 1000000).ToString() : GivenUnitName == "gram" ? (valueResult / 1000).ToString() : GivenUnitName == "ounce" ? (valueResult / 35.274).ToString() : GivenUnitName == "pound" ? (valueResult / 2.205).ToString() : valueResult.ToString()
            };

            var ounceUnit = new UnitValue
            {
                Unit = "ounce",
                Value = GivenUnitName == "mg" ? (valueResult / 28349.523).ToString() : GivenUnitName == "gram" ? (valueResult / 28.35).ToString() : GivenUnitName == "kg" ? (valueResult * 35.274).ToString() : GivenUnitName == "pound" ? (valueResult * 16).ToString() : valueResult.ToString()
            };

            var poundUnit = new UnitValue
            {
                Unit = "pound",
                Value = GivenUnitName == "mg" ? (valueResult / 453592.37).ToString() : GivenUnitName == "gram" ? (valueResult / 453.592).ToString() : GivenUnitName == "kg" ? (valueResult * 2.205).ToString() : GivenUnitName == "ounce" ? (valueResult / 16).ToString() : valueResult.ToString()
            };

            List<UnitValue> preResults = new List<UnitValue> { mgUnit, gramUnit, kgUnit, ounceUnit, poundUnit };
            List<UnitValue> results = new List<UnitValue>();

            foreach (var preResult in preResults)
            {
                if (listOfConvertibleUnits.Contains(preResult.Unit))
                {
                    results.Add(preResult);
                }
            }

            return results;
        }
    }

    class LengthConversionUnits
    {
        public string GivenUnitName { get; set; }
        public List<string> AllRelatedUnitNames { get; set; }

        public LengthConversionUnits(string unit)
        {
            AllRelatedUnitNames = new List<string> { "mm", "cm", "meter", "km", "inch", "feet" };
            GivenUnitName = unit;
        }

        public List<UnitValue> CalculatedValues(string value)
        {
            var listOfConvertibleUnits = AllRelatedUnitNames;
            listOfConvertibleUnits.Remove(GivenUnitName);
            double unitInDouble;

            unitInDouble = Convert.ToDouble(value);

            double valueResult = unitInDouble;

            var mmUnit = new UnitValue
            {
                Unit = "mm",
                Value = GivenUnitName == "cm" ? (valueResult * 10).ToString() : GivenUnitName == "meter" ? (valueResult * 1000).ToString() : GivenUnitName == "km" ? (valueResult * 1000000).ToString() : GivenUnitName == "inch" ? (valueResult * 25.4).ToString() : GivenUnitName == "feet" ? (valueResult * 304.8).ToString() : valueResult.ToString()
            };

            var cmUnit = new UnitValue
            {
                Unit = "cm",
                Value = GivenUnitName == "mm" ? (valueResult / 10).ToString() : GivenUnitName == "meter" ? (valueResult * 100).ToString() : GivenUnitName == "km" ? (valueResult * 100000).ToString() : GivenUnitName == "inch" ? (valueResult * 2.54).ToString() : GivenUnitName == "feet" ? (valueResult * 30.48).ToString() : valueResult.ToString()
            };

            var meterUnit = new UnitValue
            {
                Unit = "meter",
                Value = GivenUnitName == "mm" ? (valueResult / 1000).ToString() : GivenUnitName == "cm" ? (valueResult / 100).ToString() : GivenUnitName == "km" ? (valueResult * 1000).ToString() : GivenUnitName == "inch" ? (valueResult / 39.37).ToString() : GivenUnitName == "feet" ? (valueResult / 3.281).ToString() : valueResult.ToString()
            };

            var kmUnit = new UnitValue
            {
                Unit = "km",
                Value = GivenUnitName == "mm" ? (valueResult / 1000000).ToString() : GivenUnitName == "cm" ? (valueResult / 100000).ToString() : GivenUnitName == "meter" ? (valueResult / 1000).ToString() : GivenUnitName == "inch" ? (valueResult / 39370.079).ToString() : GivenUnitName == "feet" ? (valueResult / 3280.84).ToString() : valueResult.ToString()
            };

            var inchUnit = new UnitValue
            {
                Unit = "inch",
                Value = GivenUnitName == "mm" ? (valueResult / 25.4).ToString() : GivenUnitName == "cm" ? (valueResult / 2.54).ToString() : GivenUnitName == "meter" ? (valueResult * 39.37).ToString() : GivenUnitName == "km" ? (valueResult * 39370.079).ToString() : GivenUnitName == "feet" ? (valueResult * 12).ToString() : valueResult.ToString()
            };

            var feetUnit = new UnitValue
            {
                Unit = "feet",
                Value = GivenUnitName == "mm" ? (valueResult / 304.8).ToString() : GivenUnitName == "cm" ? (valueResult / 30.48).ToString() : GivenUnitName == "meter" ? (valueResult * 3.281).ToString() : GivenUnitName == "km" ? (valueResult * 3280.84).ToString() : GivenUnitName == "inch" ? (valueResult / 12).ToString() : valueResult.ToString()
            };

            List<UnitValue> preResults = new List<UnitValue> { mmUnit, cmUnit, meterUnit, kmUnit, inchUnit, feetUnit };
            List<UnitValue> results = new List<UnitValue>();

            foreach (var preResult in preResults)
            {
                if (listOfConvertibleUnits.Contains(preResult.Unit))
                {
                    results.Add(preResult);
                }
            }

            return results;
        }
    }












    class DigitalSizeConversionUnits
    {
        public string GivenUnitName { get; set; }
        public List<string> AllRelatedUnitNames { get; set; }

        public DigitalSizeConversionUnits(string unit)
        {
            AllRelatedUnitNames = new List<string> { "byte", "kb", "mb", "gb", "tb" };
            GivenUnitName = unit;
        }

        public List<UnitValue> CalculatedValues(string value)
        {
            var listOfConvertibleUnits = AllRelatedUnitNames;
            listOfConvertibleUnits.Remove(GivenUnitName);
            double unitInDouble;
            
            unitInDouble = Convert.ToDouble(value);            

            double valueResult = unitInDouble;

            var byteUnit = new UnitValue
            {
                Unit = "byte",
                Value = GivenUnitName == "kb" ? (valueResult * 1000).ToString() : GivenUnitName == "mb" ? (valueResult * 1000000).ToString() : GivenUnitName == "gb" ? (valueResult * 1000000000).ToString() : GivenUnitName == "tb" ? (valueResult * 1000000000000).ToString() : valueResult.ToString()
            };

            var kbUnit = new UnitValue
            {
                Unit = "kb",
                Value = GivenUnitName == "byte" ? (valueResult / 1000).ToString() : GivenUnitName == "mb" ? (valueResult * 1000).ToString() : GivenUnitName == "gb" ? (valueResult * 1000000).ToString() : GivenUnitName == "tb" ? (valueResult * 1000000000).ToString() : valueResult.ToString()
            };

            var mbUnit = new UnitValue
            {
                Unit = "mb",
                Value = GivenUnitName == "byte" ? (valueResult / 1000000).ToString() : GivenUnitName == "kb" ? (valueResult / 1000).ToString() : GivenUnitName == "gb" ? (valueResult * 1000).ToString() : GivenUnitName == "tb" ? (valueResult * 1000000).ToString() : valueResult.ToString()
            };

            var gbUnit = new UnitValue
            {
                Unit = "gb",
                Value = GivenUnitName == "byte" ? (valueResult / 1000000000).ToString() : GivenUnitName == "kb" ? (valueResult / 1000000).ToString() : GivenUnitName == "mb" ? (valueResult / 1000).ToString() : GivenUnitName == "tb" ? (valueResult * 1000).ToString() : valueResult.ToString()
            };

            var tbUnit = new UnitValue
            {
                Unit = "tb",
                Value = GivenUnitName == "byte" ? (valueResult / 1000000000000).ToString() : GivenUnitName == "kb" ? (valueResult / 1000000000).ToString() : GivenUnitName == "mb" ? (valueResult / 1000000).ToString() : GivenUnitName == "gb" ? (valueResult / 1000).ToString() : valueResult.ToString()
            };

            List<UnitValue> preResults = new List<UnitValue> { byteUnit, kbUnit, mbUnit, gbUnit, tbUnit };
            List<UnitValue> results = new List<UnitValue>();

            foreach (var preResult in preResults)
            {
                if (listOfConvertibleUnits.Contains(preResult.Unit))
                {
                    results.Add(preResult);
                }
            }

            return results;
        }
    }















    class TimeConversionUnits
    {
        public string GivenUnitName { get; set; }
        public List<string> AllRelatedUnitNames { get; set; }

        public TimeConversionUnits(string unit)
        {
            AllRelatedUnitNames = new List<string> { "sec", "min", "hour" };
            GivenUnitName = unit;
        }

        public List<UnitValue> CalculatedValues(string value)
        {
            var listOfConvertibleUnits = AllRelatedUnitNames;
            listOfConvertibleUnits.Remove(GivenUnitName);
            double unitInDouble;
            
            unitInDouble = Convert.ToDouble(value);

            double valueResult = unitInDouble;

            var secUnit = new UnitValue
            {
                Unit = "sec",
                Value = GivenUnitName == "min" ? (valueResult * 60).ToString() : GivenUnitName == "hour" ? (valueResult * 3600).ToString() : valueResult.ToString()
            };

            var minUnit = new UnitValue
            {
                Unit = "min",
                Value = GivenUnitName == "sec" ? (valueResult / 60).ToString() : GivenUnitName == "hour" ? (valueResult * 60).ToString() : valueResult.ToString()
            };

            var hourUnit = new UnitValue
            {
                Unit = "hour",
                Value = GivenUnitName == "sec" ? (valueResult / 3600).ToString() : GivenUnitName == "min" ? (valueResult / 60).ToString() : valueResult.ToString()
            };            

            List<UnitValue> preResults = new List<UnitValue> { secUnit, minUnit, hourUnit };
            List<UnitValue> results = new List<UnitValue>();

            foreach (var preResult in preResults)
            {
                if (listOfConvertibleUnits.Contains(preResult.Unit))
                {
                    results.Add(preResult);
                }
            }

            return results;
        }
    }



    class TemperatureConversionUnits
    {
        public string GivenUnitName { get; set; }
        public List<string> AllRelatedUnitNames { get; set; }

        public TemperatureConversionUnits(string unit)
        {
            AllRelatedUnitNames = new List<string> { "celsius", "farenheit" };
            GivenUnitName = unit;
        }

        public List<UnitValue> CalculatedValues(string value)
        {
            var listOfConvertibleUnits = AllRelatedUnitNames;
            listOfConvertibleUnits.Remove(GivenUnitName);
            double unitInDouble;

            unitInDouble = Convert.ToDouble(value);

            double valueResult = unitInDouble;

            var celsiusUnit = new UnitValue
            {
                Unit = "celsius",
                Value = GivenUnitName == "farenheit" ? ((valueResult - 32) * (Convert.ToDouble((decimal)5 / (decimal) 9))).ToString() : valueResult.ToString()
            };

            var farenheitUnit = new UnitValue
            {
                Unit = "farenheit",
                Value = GivenUnitName == "celsius" ? ((valueResult * (Convert.ToDouble((decimal)9 / (decimal)5))) + 32).ToString() : valueResult.ToString()
            };            

            List<UnitValue> preResults = new List<UnitValue> { celsiusUnit, farenheitUnit };
            List<UnitValue> results = new List<UnitValue>();

            foreach (var preResult in preResults)
            {
                if (listOfConvertibleUnits.Contains(preResult.Unit))
                {
                    results.Add(preResult);
                }
            }

            return results;
        }
    }











    class PercentConversionUnits
    {
        public string GivenUnitName { get; set; }
        public List<string> AllRelatedUnitNames { get; set; }

        public PercentConversionUnits(string unit)
        {
            AllRelatedUnitNames = new List<string> { "decimal", "percent" };
            GivenUnitName = unit;
        }

        public List<UnitValue> CalculatedValues(string value)
        {
            var listOfConvertibleUnits = AllRelatedUnitNames;
            listOfConvertibleUnits.Remove(GivenUnitName);
            double unitInDouble;

            unitInDouble = Convert.ToDouble(value);

            double valueResult = unitInDouble;

            var decimalUnit = new UnitValue
            {
                Unit = "decimal",
                Value = GivenUnitName == "percent" ? (valueResult * 100).ToString() : valueResult.ToString()
            };

            var percentUnit = new UnitValue
            {
                Unit = "percent",
                Value = GivenUnitName == "decimal" ? (valueResult / 100).ToString() : valueResult.ToString()
            };

            List<UnitValue> preResults = new List<UnitValue> { decimalUnit, percentUnit };
            List<UnitValue> results = new List<UnitValue>();

            foreach (var preResult in preResults)
            {
                if (listOfConvertibleUnits.Contains(preResult.Unit))
                {
                    results.Add(preResult);
                }
            }

            return results;
        }
    }
}