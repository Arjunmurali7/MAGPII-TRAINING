using System;
using System.Windows.Forms;

namespace SumWindow
{
    public partial class Form1 : Form  // Inherits from Form
    {
        public Form1() // Constructor
        {
            InitializeComponent(); // Initializes controls from Designer
        }

        
        private void btnsum_Click(object sender, EventArgs e)// Event handler for button click
        {
            try
            {
                // Read numbers from TextBoxes
                double num1 = double.Parse(txtFirst.Text.Trim());
                double num2 = double.Parse(txtSecond.Text.Trim());

                // Calculate sum
                double sum = num1 + num2;

                // Show result in Label
                lblResult.Text = $"Sum: {sum}";
            }
            catch
            {
                MessageBox.Show(
                    "Please enter valid numbers",
                    "Input Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning // Show warning icon
                );
            }
        }
    }
}
