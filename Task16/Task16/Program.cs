using System;
using System.Windows.Forms;

namespace SumWindow
{
    static class Program // Main entry point for the application
    {
        [STAThread]// Single-threaded apartment model for Windows Forms
        static void Main()
        {
            // Enable visual styles for controls
            Application.EnableVisualStyles();// Enable visual styles for controls
            Application.SetCompatibleTextRenderingDefault(false); // Use default text rendering

            // Start the application with Form1
            Application.Run(new Form1());
        }
    }
}
