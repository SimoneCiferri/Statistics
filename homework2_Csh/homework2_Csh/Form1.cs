using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            //retrieve relative paths
            String currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            String relativePath = Directory.GetParent(currentDirectory).Parent.Parent.FullName;
            String afterLifeSourveyPath = System.IO.Path.Combine(relativePath, "SurveyAfterLife-Sheet1.csv");
            String profLifeSourveyPath = System.IO.Path.Combine(relativePath, "ProfessionalLife-Sheet1.csv");

            //def dictionary to save distributions
            Dictionary<string, int> religionDistrib = new Dictionary<string, int>();
            Dictionary<string, int> remunerationDistrib = new Dictionary<string, int>();
            Dictionary<string, int> heightDistrib = new Dictionary<string, int>();

            try
            {
                //retrieve values from .cvs file
                List<String> religion_col = extractValuesFromCSV(afterLifeSourveyPath, 3);
                List<String> remuneration_col = extractValuesFromCSV(profLifeSourveyPath, 6);
                List<String> height_col = extractValuesFromCSV(profLifeSourveyPath, 16);

                //create distribution
                religionDistrib = createDistrib(religion_col);
                remunerationDistrib = createDistrib(remuneration_col);
                heightDistrib = createDistrib(height_col);

                //calculate frequency distributions
                frequencyDistrib(religionDistrib);
                frequencyDistrib(remunerationDistrib);
                frequencyDistrib(heightDistrib);

                //print distributions
                /*
                label4.Text = " ";
                printDistrib(religionDistrib);
                printDistrib(remunerationDistrib);
                printDistrib(heightDistrib);
                */

            }
            catch (Exception ex)
            {
                label1.Text = afterLifeSourveyPath;
            }

        }


        //This function extracts the values from the .csv file with relative path " relativePathpath " and returns it as a List of strings
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
                        //label1.Text = label1.Text + " " + row[3];
                    }
                }

                return col_values;
            }
        }

        //this function creates a distribution from in put data "col" and returns it as a Dictionary variable
        private Dictionary<string, int> createDistrib(List<string> col)
        {
            Dictionary<string, int> distrib = new Dictionary<string, int>();

            col.RemoveAt(0);

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


        //This function calculates the frequency distribution of " inputDistrib "
        private void frequencyDistrib(Dictionary<string, int> inputDistrib)
        {
            int totalCateg = 0;
            foreach (var coppia in inputDistrib)
            {
                totalCateg += coppia.Value;
            }

            Console.WriteLine("Category      |   absolute    |   Relative    |   Percentage%     |");
            label2.Text += "\n\n\nCategory      |   absolute    |   Relative    |   Percentage%     |\n";

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

                Console.WriteLine($"{cat,-10}     {absoluteFreq,-18}     {relativeFrequency,-18:F4}     {percentageFrequency,-18:F2}%");
                label2.Text += " \n " + $"{cat,-10}     {absoluteFreq,-18}     {relativeFrequency,-18:F4}     {percentageFrequency,-18:F2}%";

            }
        }

        //This function print the distribution on a label
        private void printDistrib(Dictionary<string, int> distrib)
        {

            foreach (var coppia in distrib)
            {
                label4.Text += $"{coppia.Key}: {coppia.Value}" + "\n";
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

            // Aggiungi l'ultimo valore dopo l'ultima virgola
            string lastValue = line.Substring(start);
            values.Add(lastValue.Trim('"'));

            return values;
        }

    }
}
