using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homework2_Csh
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //retrieve relative document paths
            String currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            String relativePath = Directory.GetParent(currentDirectory).Parent.Parent.FullName;
            String afterLifeSourveyPath = System.IO.Path.Combine(relativePath, "SurveyAfterLife-Sheet1.csv");
            String profLifeSourveyPath = System.IO.Path.Combine(relativePath, "ProfessionalLife-Sheet1.csv");

            //def dictionary to save distributions
            Dictionary<string, int> backgroundDistrib = new Dictionary<string, int>();
            Dictionary<string, int> remunerationDistrib = new Dictionary<string, int>();
            Dictionary<string, int> hardWorkerDistrib = new Dictionary<string, int>();

            try
            {
                //retrieve values from .cvs file
                List<String> background_col = extractValuesFromCSV(profLifeSourveyPath, 1);
                List<String> remuneration_col = extractValuesFromCSV(profLifeSourveyPath, 6);
                List<String> hardWorker_col = extractValuesFromCSV(profLifeSourveyPath, 4);

                //create single distributions
                backgroundDistrib = createDistrib(background_col);
                remunerationDistrib = createDistribFromContinuous(remuneration_col);
                hardWorkerDistrib = createDistrib(hardWorker_col);

                
                //calculate frequency distributions
                frequencyDistrib(backgroundDistrib);
                frequencyDistrib(remunerationDistrib);
                frequencyDistrib(hardWorkerDistrib);

                //calculate Bivariate distribution

                //first specify the columns
                List<int> variables = new List<int>();
                variables.Add(4);
                variables.Add(16);

                //if you add the following line of code you will calculate Multivariate distribution of columns 4, 16 and 2
                //variables.Add(2);

                calculateMultivariateDistrib(profLifeSourveyPath,variables);

            }
            catch (Exception ex)
            {
                label1.Text = "Exception thrown: " + ex.Message;
            }

        }


        //This function extracts the values of the i-column from the .csv file with relative path " relativePathpath " and returns it as a List of strings
        private List<String> extractValuesFromCSV(string relativePath, int i)
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

        //this function creates a distribution from input data "col" (list of strings)  and returns it as a Dictionary variable
        private Dictionary<string, int> createDistrib(List<string> col)
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
        private Dictionary<string, int> createDistribFromContinuous(List<string> col)
        {
            Dictionary<string, int> distrib = new Dictionary<string, int>();
            

            // In this point I'll ask user to insert k (in this Windows form application i choose manually the k but i can simply parse
            // it with:
            // int k = int.Parse(Console.ReadLine()) from the console);
            
            int k = 3;

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
           
           // Calculate the lenght of each intervall
           double intervallenght = (double_col.Max() - double_col.Min()) / k;

            //calculate distribution
            foreach (double val in double_col)
            {

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
           }
           
            return distrib;
        }
        


        //This function calculates the frequency distribution of " inputDistrib "
        private void frequencyDistrib(Dictionary<string, int> inputDistrib)
        {
            int totalCateg = 0;
            foreach (var coppia in inputDistrib)
            {
                totalCateg += coppia.Value;
            }

            //From here I start printing resuslts inside a label on the Windows Form and on the Debug Output view
            Debug.WriteLine("Category      |      absolute      |      Relative      |      Percentage%      |");
            labelCat.Text += ("\n\nCATEGORY");
            labelAbs.Text += ("\n\nABSOLUTE");
            labelRel.Text += ("\n\nRELATIVE");
            labelPerc.Text += ("\n\nPERCENTAGE");

            // Calculate and print all freq. distrib.
            foreach (var coppia in inputDistrib)
            {
                string cat = coppia.Key;

                //absolute
                int absoluteFreq = coppia.Value;
                //relative
                double relativeFrequency = (double)absoluteFreq / totalCateg;
                //percentage
                double percentageFrequency = relativeFrequency * 100;

                Debug.WriteLine($"{cat,-10}      {absoluteFreq,-18}      {relativeFrequency,-18:F4}      {percentageFrequency,-18:F2}%");
                labelCat.Text += ("\n" + cat);
                labelAbs.Text += ("\n" + absoluteFreq);
                labelRel.Text += ("\n" + relativeFrequency);
                labelPerc.Text += ("\n" + percentageFrequency);

            }
        }

        //This function calculare the multivariate distribution of variables specified in the variables input list (in that list the
        // user has to specify the columns of the variables) 
        private void calculateMultivariateDistrib(String relativePath, List<int> variables)
        {
            using (StreamReader reader1 = new StreamReader(relativePath))
            {
                List<String> row;
                string line;
                int variables_number = variables.Count;
                bool first = true;

                //multivariable distribution will be a Dictionary variable with a list of string as a key. Because of that i
                // need to define implement the class ArrayComparer that compare two different list of strings
                // (otherwise the .ContainsKey method of the dictionary doesn't work)
                //
                Dictionary<string[], int> multivariateDistribution = new Dictionary<string[], int>(new ArrayComparer());

                //Start parsing
                while ((line = reader1.ReadLine()) != null)
                {
                    //this if is to delete the columns names (on .csv file the first row parsed is exactly the columns names)
                    if (first) { 
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

                // Calculate total count
                int totalCount = multivariateDistribution.Values.Sum();

                //From here I start printing resuslts inside a label on the Windows Form and on the Debug Output view
                Debug.WriteLine("Multivariate Distribution: ");
                labelMultivVariables.Text += ("\n\nVARIABLES");
                labelMultivAbs.Text += ("\n\nABSOLUTE");
                labelMultivRel.Text += ("\n\nRELATIVE");
                labelMultiPerc.Text += ("\n\nPERCENTAGE");

                //calculate absolute/relative/frequency distribution
                foreach (var pair in multivariateDistribution)
                {
                    // Absolute frequency
                    int absoluteFrequency = pair.Value;

                    // Relative frequency
                    double relativeFrequency = (double)absoluteFrequency / totalCount;

                    // Percentage frequency
                    double percentageFrequency = relativeFrequency * 100;

                    // Print the results
                    Debug.WriteLine($"Variables: {string.Join(", ", pair.Key)} -> Absolute Frequency: {absoluteFrequency}, Relative Frequency: {relativeFrequency:F2}, Percentage Frequency: {percentageFrequency:F2}%");
                    labelMultivVariables.Text += ("\n" + string.Join(", ", pair.Key));
                    labelMultivAbs.Text += ("\n" + absoluteFrequency);
                    labelMultivRel.Text += ("\n" + relativeFrequency);
                    labelMultiPerc.Text += ("\n" + percentageFrequency + "%");
                }
            }
            
        }


        // Comparer for arrays, used to compare arrays of strings with .ContainsKet dictionary method (For Multivariate Distribution)
        class ArrayComparer : IEqualityComparer<string[]>
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

        //This function is to perform the row split avoiding errors on multivalues (in .csv files the " " are used to represents multivalues in a cell)
        private List<string> ParseCsvLine(string line)
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

    }
}
