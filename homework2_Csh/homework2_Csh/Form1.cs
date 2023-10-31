using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
            Dictionary<string, double> absFreqDistrib = new Dictionary<string, double>();
            Dictionary<string, double> relFreqDistrib = new Dictionary<string, double>();
            Dictionary<string, double> percFreqDistrib = new Dictionary<string, double>();
            Dictionary<string[], int> multivariateDistribution = new Dictionary<string[], int>(new Distributions.ArrayComparer());
            Dictionary<string[], double> multiAbsFreqDistrib = new Dictionary<string[], double>();
            Dictionary<string[], double> multiRelFreqDistrib = new Dictionary<string[], double>();
            Dictionary<string[], double> multiPercFreqDistrib = new Dictionary<string[], double>();

            //Use the class Distribution
            Distributions distribution = new Distributions();

            try
            {
                //retrieve values from .cvs file
                List<String> background_col = distribution.extractValuesFromCSV(profLifeSourveyPath, 1);
                List<String> remuneration_col = distribution.extractValuesFromCSV(profLifeSourveyPath, 6);
                List<String> hardWorker_col = distribution.extractValuesFromCSV(profLifeSourveyPath, 4);

                //create single distributions
                backgroundDistrib = distribution.createDistrib(background_col);

                int k = 2;
                remunerationDistrib = distribution.createDistribFromContinuous(remuneration_col, k);

                hardWorkerDistrib = distribution.createDistrib(hardWorker_col);


                //calculate frequency distributions
                absFreqDistrib = distribution.absFrequencyDistrib(backgroundDistrib);
                relFreqDistrib = distribution.relativeFrequencyDistrib(backgroundDistrib);
                percFreqDistrib = distribution.percFrequencyDistrib(backgroundDistrib);
                printAllFrequencyDistributions(absFreqDistrib, relFreqDistrib, percFreqDistrib);

                absFreqDistrib = distribution.absFrequencyDistrib(remunerationDistrib);
                relFreqDistrib = distribution.relativeFrequencyDistrib(remunerationDistrib);
                percFreqDistrib = distribution.percFrequencyDistrib(remunerationDistrib);
                printAllFrequencyDistributions(absFreqDistrib, relFreqDistrib, percFreqDistrib);

                absFreqDistrib = distribution.absFrequencyDistrib(hardWorkerDistrib);
                relFreqDistrib = distribution.relativeFrequencyDistrib(hardWorkerDistrib);
                percFreqDistrib = distribution.percFrequencyDistrib(hardWorkerDistrib);
                printAllFrequencyDistributions(absFreqDistrib, relFreqDistrib, percFreqDistrib);

                //calculate Bivariate distribution
                //first specify the columns
                List<int> variables = new List<int>();
                variables.Add(4);
                variables.Add(16);
                //if you add the following line of code you will calculate Multivariate distribution of columns 4, 16 and 2
                //variables.Add(2);
                multivariateDistribution = distribution.calculateMultivariateDistrib(profLifeSourveyPath, variables);
                multiAbsFreqDistrib = distribution.multiAbsFrequencyDistrib(multivariateDistribution);
                multiRelFreqDistrib = distribution.multiRelativeFrequencyDistrib(multivariateDistribution);
                multiPercFreqDistrib = distribution.multiPercFrequencyDistrib(multivariateDistribution);
                printAllMultivariateFrequencyDistributions(multiAbsFreqDistrib, multiRelFreqDistrib, multiPercFreqDistrib);

            }
            catch (Exception ex)
            {
                label1.Text = "Exception thrown: " + ex.Message;
            }

        }


        //print all frequency distributions
        private void printAllFrequencyDistributions(Dictionary<string, double> absoluteFreq, Dictionary<string, double> relativeFreq, Dictionary<string, double> percFreq) {

            //From here I start printing resuslts inside a label on the Windows Form
            Debug.WriteLine("Category      |      absolute      |      Relative      |      Percentage%      |");
            labelCat.Text += ("\n\nCATEGORY");
            labelAbs.Text += ("\n\nABSOLUTE");
            labelRel.Text += ("\n\nRELATIVE");
            labelPerc.Text += ("\n\nPERCENTAGE");

            // Print all distrib.
            for (int i = 0; i < absoluteFreq.Keys.Count; i++)
            {
                Debug.WriteLine(message: $"({absoluteFreq.Keys.ElementAt(i),-10})      ({absoluteFreq.Values.ElementAt(i),-18})      {relativeFreq.Values.ElementAt(i),-18:F4}      {percFreq.Values.ElementAt(i),-18:F2}%");
                labelCat.Text += ("\n" + absoluteFreq.Keys.ElementAt(i));
                labelAbs.Text += ("\n" + absoluteFreq.Values.ElementAt(i));
                labelRel.Text += ("\n" + relativeFreq.Values.ElementAt(i));
                labelPerc.Text += ("\n" + percFreq.Values.ElementAt(i));
            }

        }

        //print all frequency distributions
        private void printAllMultivariateFrequencyDistributions(Dictionary<string[], double> absoluteFreq, Dictionary<string[], double> relativeFreq, Dictionary<string[], double> percFrequency)
        {

            //From here I start printing resuslts inside a label on the Windows Form
            Debug.WriteLine("Multivariate Distribution: ");
            labelMultivVariables.Text += ("\n\nVARIABLES");
            labelMultivAbs.Text += ("\n\nABSOLUTE");
            labelMultivRel.Text += ("\n\nRELATIVE");
            labelMultiPerc.Text += ("\n\nPERCENTAGE");

            // Print all multivariate distrib.
            for (int i = 0; i < absoluteFreq.Keys.Count; i++)
            {
                // Print the results
                Debug.WriteLine($"Variables: {string.Join(", ", absoluteFreq.Keys.ElementAt(i))} -> Absolute Frequency: {absoluteFreq.Values.ElementAt(i)}, Relative Frequency: {relativeFreq.Values.ElementAt(i):F2}, Percentage Frequency: {percFrequency.Values.ElementAt(i):F2}%");
                labelMultivVariables.Text += ("\n" + string.Join(", ", absoluteFreq.Keys.ElementAt(i)));
                labelMultivAbs.Text += ("\n" + absoluteFreq.Values.ElementAt(i));
                labelMultivRel.Text += ("\n" + relativeFreq.Values.ElementAt(i));
                labelMultiPerc.Text += ("\n" + percFrequency.Values.ElementAt(i) + "%");
            }

        }

    }
}
