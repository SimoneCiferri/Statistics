using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework2_Csh
{
    internal class Distributions
    {
        //This function extracts the values of the i-column from the .csv file with relative path " relativePathpath " and returns it as a List of strings
        public List<String> extractValuesFromCSV(string relativePath, int i)
        {
            using (StreamReader reader1 = new StreamReader(relativePath))
            {
                List<String> col_values = new List<String>();
                List<String> row;
                string line;
                while ((line = reader1.ReadLine()) != null)
                {
                    row = ParseCsvLine(line);

                    if (!((row[i] == "") || (row[i] == "-")))
                    {
                        col_values.Add(row[i]);
                    }
                }
                col_values.RemoveAt(0);
                return col_values;
            }
        }

        //This function is to perform the row split avoiding errors on multivalues (in .csv files the " " are used to represents multivalues in a cell)
        public List<string> ParseCsvLine(string line)
        {
            List<string> values = new List<string>();
            bool inQuotes = false;
            int start = 0;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    string value = line.Substring(start, i - start);
                    values.Add(value.Trim('"'));
                    start = i + 1;
                }
            }

            // add last value after last " , "
            string lastValue = line.Substring(start);
            values.Add(lastValue.Trim('"'));

            return values;
        }

        
        //this function creates a distribution from input data "col" (list of strings)  and returns it as a Dictionary variable
        public Dictionary<string, int> createDistrib(List<string> col)
        {
            Dictionary<string, int> distrib = new Dictionary<string, int>();

            foreach (string val in col)
            {
                // if exists inc. counter
                if (distrib.ContainsKey(val))
                {
                    distrib[val]++;
                }
                else // if not then add and set counter = 1
                {
                    distrib[val] = 1;
                }
            }
            return distrib;
        }

        
        //this function creates a distribution from input data "col" (continuous values), calculates the intervals and returns it as a Dictionary variable
        public Dictionary<string, int> createDistribFromContinuous(List<string> col, int k)
        {
            Dictionary<string, int> distrib = new Dictionary<string, int>();


            // In this point I'll ask user to insert k (in this Windows form application i choose manually the k but i can simply parse
            // it with:
            // int k = int.Parse(Console.ReadLine()) from the console);

            // Convert String to Double to calculate intervalls
            List<double> double_col = new List<double>();

            foreach (string value in col)
            {
                
                if (double.TryParse(value, out double doubleValue))
                {
                    double_col.Add(doubleValue);
                }
                else
                {
                    Debug.WriteLine($"The value '{value}' is not valid (It will be ignored).");
                }
            }
            double_col.Sort();

            // Calculate the lenght of each intervall
            double intervallenght = (double_col.Max() - double_col.Min()) / k;


            //calculate distribution
            foreach (double val in double_col)
            {

                if (val != 0)
                {
                    int interval = (int)((val - double_col.Min()) / intervallenght)+1;
                    if (interval >= 0)
                    {
                        string string_interval;
                        if (interval <= k) {
                            string_interval = interval.ToString();
                        }
                        else {
                            string_interval = (interval-1).ToString();
                        }

                        // if exists inc. counter
                        if (distrib.ContainsKey(string_interval))
                        {
                            distrib[string_interval]++;
                        }
                        else // if not then add and set counter = 1
                        {
                            distrib[string_interval] = 1;
                        }

                    }
                }

                /*
                int interval = (int)((val - double_col.Min()) / intervallenght) + 1;
                string string_interval = interval.ToString();

                // if exists inc. counter
                if (distrib.ContainsKey(string_interval))
                {
                    distrib[string_interval]++;
                }
                else // if not then add and set counter = 1
                {
                    distrib[string_interval] = 1;
                }
                */
            }

            return distrib;
        }

        //This function calculates the absolute frequency distribution of " inputDistrib "
        public Dictionary<string, double> absFrequencyDistrib(Dictionary<string, int> inputDistrib)
        {
            Dictionary<string, double> absFreqDistrib = new Dictionary<string, double>();

            int totalCateg = 0;
            foreach (var coppia in inputDistrib)
            {
                totalCateg += coppia.Value;
            }

            // Calculate all freq. distrib.
            foreach (var coppia in inputDistrib)
            {
                string cat = coppia.Key;

                //absolute
                int absoluteFreq = coppia.Value;

                absFreqDistrib.Add(cat, absoluteFreq);

            }
            return absFreqDistrib;
        }

        //This function calculates the relative frequency distribution of " inputDistrib "
        public Dictionary<string, double> relativeFrequencyDistrib(Dictionary<string, int> inputDistrib)
        {
            Dictionary<string, double> relFreqDistrib = new Dictionary<string, double>();

            int totalCateg = 0;
            foreach (var coppia in inputDistrib)
            {
                totalCateg += coppia.Value;
            }

            // Calculate all rel. freq. distrib.
            foreach (var coppia in inputDistrib)
            {
                string cat = coppia.Key;

                //absolute
                int absoluteFreq = coppia.Value;
                //relative
                double relativeFrequency = (double)absoluteFreq / totalCateg;

                relFreqDistrib.Add(cat, relativeFrequency);

            }
            return relFreqDistrib;
        }

        //This function calculates the percentage frequency distribution of " inputDistrib "
        public Dictionary<string, double> percFrequencyDistrib(Dictionary<string, int> inputDistrib)
        {
            Dictionary<string, double> percFreqDistrib = new Dictionary<string, double>();

            int totalCateg = 0;
            foreach (var coppia in inputDistrib)
            {
                totalCateg += coppia.Value;
            }

            // Calculate all perc. freq. distrib.
            foreach (var coppia in inputDistrib)
            {
                string cat = coppia.Key;

                //absolute
                int absoluteFreq = coppia.Value;
                //relative
                double relativeFrequency = (double)absoluteFreq / totalCateg;
                //percentage
                double percentageFrequency = relativeFrequency * 100;

                percFreqDistrib.Add(cat, percentageFrequency);

            }
            return percFreqDistrib;
        }

        //This function calculare the multivariate distribution of variables specified in the variables input list (in that list the
        // user has to specify the columns of the variables) 
        public Dictionary<string[], int> calculateMultivariateDistrib(String relativePath, List<int> variables)
        {
            //multivariable distribution will be a Dictionary variable with a list of string as a key. Because of that i
            // need to define implement the class ArrayComparer that compare two different list of strings
            // (otherwise the .ContainsKey method of the dictionary doesn't work)
            //
            Dictionary<string[], int> multivariateDistribution = new Dictionary<string[], int>(new ArrayComparer());

            using (StreamReader reader1 = new StreamReader(relativePath))
            {
                List<String> row;
                string line;
                int variables_number = variables.Count;
                bool first = true;

                //Start parsing
                while ((line = reader1.ReadLine()) != null)
                {
                    //this if is to delete the columns names (on .csv file the first row parsed is exactly the columns names)
                    if (first)
                    {
                        first = false;
                        continue;
                    }

                    row = ParseCsvLine(line);

                    //calculate distribution
                    if (variables.All(colIndex => colIndex < row.Count))
                    {
                        string[] selectedValues = variables.Select(colIndex => row[colIndex]).ToArray();
                        if (multivariateDistribution.ContainsKey(selectedValues))
                        {
                            multivariateDistribution[selectedValues]++;
                        }
                        else
                        {
                            multivariateDistribution[selectedValues] = 1;
                        }
                    }

                }
            }
            return multivariateDistribution;

        }

        //This function calculates the absolute frequency distribution of multivariate input distrib
        public Dictionary<string[], double> multiAbsFrequencyDistrib(Dictionary<string[], int> multivariateDistribution)
        {
            Dictionary<string[], double> absFreqDistrib = new Dictionary<string[], double>();

            // Calculate total count
            int totalCount = multivariateDistribution.Values.Sum();

            //calculate absolute distribution
            foreach (var pair in multivariateDistribution)
            {
                // Absolute frequency
                int absoluteFrequency = pair.Value;

                absFreqDistrib.Add(pair.Key, absoluteFrequency);
            }
            return absFreqDistrib;
        }

        //This function calculates the relative frequency distribution of multivariate input distrib
        public Dictionary<string[], double> multiRelativeFrequencyDistrib(Dictionary<string[], int> multivariateDistribution)
        {
            Dictionary<string[], double> relFreqDistrib = new Dictionary<string[], double>();

            // Calculate total count
            int totalCount = multivariateDistribution.Values.Sum();

            //calculate absolute distribution
            foreach (var pair in multivariateDistribution)
            {
                // Absolute frequency
                int absoluteFrequency = pair.Value;
                // Relative frequency
                double relativeFrequency = (double)absoluteFrequency / totalCount;

                relFreqDistrib.Add(pair.Key, relativeFrequency);
            }
            return relFreqDistrib;
        }

        //This function calculates the percentage frequency distribution of multivariate input distrib
        public Dictionary<string[], double> multiPercFrequencyDistrib(Dictionary<string[], int> multivariateDistribution)
        {
            Dictionary<string[], double> percFreqDistrib = new Dictionary<string[], double>();

            // Calculate total count
            int totalCount = multivariateDistribution.Values.Sum();

            //calculate absolute distribution
            foreach (var pair in multivariateDistribution)
            {
                // Absolute frequency
                int absoluteFrequency = pair.Value;
                // Relative frequency
                double relativeFrequency = (double)absoluteFrequency / totalCount;
                // Percentage frequency
                double percentageFrequency = relativeFrequency * 100;

                percFreqDistrib.Add(pair.Key, percentageFrequency);
            }
            return percFreqDistrib;
        }

        // Comparer for arrays, used to compare arrays of strings with .ContainsKet dictionary method (For Multivariate Distribution)
        public class ArrayComparer : IEqualityComparer<string[]>
        {
            public bool Equals(string[] x, string[] y)
            {
                return x.SequenceEqual(y);
            }

            public int GetHashCode(string[] obj)
            {
                return obj.Aggregate(0, (current, val) => current ^ val.GetHashCode());
            }
        }
    }
}
