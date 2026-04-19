using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using NCalc;

namespace WindowsFormsApp1
{
    public partial class calculator : Form
    {
        double num1, num2;
        string operation;
        bool isSingleInputOperation = false;

        public calculator()
        {
            InitializeComponent();
        }

        void NumberPressed(object x, EventArgs y)
        {
            textBox1.Text += (x as Button).Text;
        }

        void opeatorpressed(object z, EventArgs l)
        {
            string op = (z as Button).Text;

            if (op == "sin" || op == "cos" || op == "tan" || op == "√" || op == "log")
            {
                operation = op;
                isSingleInputOperation = true;
            }
            else
            {
                if (!double.TryParse(textBox1.Text, out num1))
                {
                    MessageBox.Show("Invalid number input.");
                    return;
                }
                textBox1.Clear();
                operation = op;
                isSingleInputOperation = false;
            }
        }

        void PressEqual(object haha, EventArgs hehe)
        {
            if (!double.TryParse(textBox1.Text, out num2))
            {
                MessageBox.Show("Invalid number input.");
                return;
            }

            if (isSingleInputOperation)
            {
                if (operation == "sin")
                    num2 = Math.Sin(ToRadians(num2));
                else if (operation == "cos")
                    num2 = Math.Cos(ToRadians(num2));
                else if (operation == "tan")
                    num2 = Math.Tan(ToRadians(num2));
                else if (operation == "√")
                    num2 = Math.Sqrt(num2);
                else if (operation == "log")
                    num2 = Math.Log10(num2);
            }
            else
            {
                if (operation == "+")
                    num2 = num1 + num2;
                else if (operation == "-")
                    num2 = num1 - num2;
                else if (operation == "×")
                    num2 = num1 * num2;
                else if (operation == "÷")
                {
                    if (num2 == 0)
                    {
                        MessageBox.Show("Division by zero is not allowed.");
                        return;
                    }
                    num2 = num1 / num2;
                }
                else if (operation == "^")
                    num2 = Math.Pow(num1, num2);
            }

            textBox1.Text = num2.ToString();

            
            num1 = 0;
            num2 = 0;
            operation = "";
            isSingleInputOperation = false;
        }

        private void neg(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox1.Text, out double value))
            {
                MessageBox.Show("Invalid input.");
                return;
            }
            value *= -1;
            textBox1.Text = value.ToString();
        }

        private void buttonAC_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            num1 = 0;
            num2 = 0;
            operation = "";
            isSingleInputOperation = false;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
            }
        }

        private void point(object sender, EventArgs e)
        {
            if (!textBox1.Text.Contains("."))
            {
                textBox1.Text += ".";
            }
        }

        private void buttonOff_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private double ToRadians(double angleInDegrees)
        {
            return (Math.PI / 180) * angleInDegrees;
        }

        private void graph_Click(object sender, EventArgs e)
        {
            GraphForm graphForm = new GraphForm();
            graphForm.Show();
        }

        
        private void buttonPi_Click(object sender, EventArgs e)
        {
            textBox1.Text = Math.PI.ToString();
        }

        private void buttonE_Click(object sender, EventArgs e)
        {
            textBox1.Text = Math.E.ToString();
        }
    }
}
