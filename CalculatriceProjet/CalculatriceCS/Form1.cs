using CalculatriceCS.Classes;
using System;
using System.Data;
using System.Windows.Forms;

namespace CalculatriceCS
{
    public partial class Form1 : Form
    {
        private string currentCalculation = "";
        Calculator Calc = new Calculator(); 

        public Form1()
        {
            InitializeComponent();
        }

        private string EvaluateFunctions(string expression)
        {
            expression = expression.Replace("π", Math.PI.ToString(System.Globalization.CultureInfo.InvariantCulture));
            while (expression.Contains("cos(") || expression.Contains("sin(") || expression.Contains("tan(") ||
                   expression.Contains("acos(") || expression.Contains("asin(") || expression.Contains("atan(") ||
                   expression.Contains("ln(") || expression.Contains("log(") || 
                   expression.Contains("sqrt(") || expression.Contains("cbrt("))
            {
                int startIndex = expression.IndexOf("cos(");
                if (startIndex == -1 || (expression.IndexOf("sin(") != -1 && expression.IndexOf("sin(") < startIndex))
                    startIndex = expression.IndexOf("sin(");
                if (startIndex == -1 || (expression.IndexOf("tan(") != -1 && expression.IndexOf("tan(") < startIndex))
                    startIndex = expression.IndexOf("tan(");
                if (startIndex == -1 || (expression.IndexOf("acos(") != -1 && expression.IndexOf("acos(") < startIndex))
                    startIndex = expression.IndexOf("acos(");
                if (startIndex == -1 || (expression.IndexOf("asin(") != -1 && expression.IndexOf("asin(") < startIndex))
                    startIndex = expression.IndexOf("asin(");
                if (startIndex == -1 || (expression.IndexOf("atan(") != -1 && expression.IndexOf("atan(") < startIndex))
                    startIndex = expression.IndexOf("atan(");
                if (startIndex == -1 || (expression.IndexOf("ln(") != -1 && expression.IndexOf("ln(") < startIndex))
                    startIndex = expression.IndexOf("ln(");
                if (startIndex == -1 || (expression.IndexOf("log(") != -1 && expression.IndexOf("log(") < startIndex))
                    startIndex = expression.IndexOf("log(");
                if (startIndex == -1 || (expression.IndexOf("sqrt(") != -1 && expression.IndexOf("sqrt(") < startIndex))
                    startIndex = expression.IndexOf("sqrt(");
                if (startIndex == -1 || (expression.IndexOf("cbrt(") != -1 && expression.IndexOf("cbrt(") < startIndex))
                    startIndex = expression.IndexOf("cbrt(");

                int endIndex = expression.IndexOf(")", startIndex);
                if (endIndex == -1)
                {
                    throw new Exception("Erreur : Parenthèse fermante manquante.");
                }

                string functionCall = expression.Substring(startIndex, endIndex - startIndex + 1);
                string functionName = functionCall.Substring(0, functionCall.IndexOf('('));
                string argument = functionCall.Substring(functionCall.IndexOf('(') + 1, functionCall.Length - functionName.Length - 2);

                argument = argument.Trim();

                double argumentValue;
                try
                {
                    argumentValue = double.Parse(EvaluateFunctions(argument), System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (FormatException)
                {
                    throw new Exception($"Erreur : L'argument '{argument}' n'est pas un nombre valide.");
                }

                double result;
                switch (functionName)
                {
                    case "cos":
                        result = Calc.Cos(argumentValue);
                        break;
                    case "sin":
                        result = Calc.Sin(argumentValue);
                        break;
                    case "tan":
                        result = Calc.Tan(argumentValue);
                        break;
                    case "acos":
                        result = Calc.Acos(argumentValue);
                        break;
                    case "asin":
                        result = Calc.Asin(argumentValue);
                        break;
                    case "atan":
                        result = Calc.Atan(argumentValue);
                        break;
                    case "ln":
                        result = Calc.Ln(argumentValue);
                        break;
                    case "log":
                        result = Calc.Log(argumentValue);
                        break;
                    case "sqrt":
                        result = Calc.RootOf2(argumentValue);
                        break;
                    case "cbrt":
                        result = Calc.RootOf3(argumentValue);
                        break;
                    default:
                        throw new Exception("Erreur : Fonction inconnue.");
                }

                expression = expression.Replace(functionCall, result.ToString());
            }

            while (expression.Contains("exp("))
            {
                int startIndex = expression.IndexOf("exp(");
                int endIndex = expression.IndexOf(")", startIndex);
                if (endIndex == -1)
                {
                    throw new Exception("Erreur : Parenthèse fermante manquante pour exp.");
                }

                string argument = expression.Substring(startIndex + 4, endIndex - startIndex - 4).Trim();

                double argumentValue;
                try
                {
                    argumentValue = double.Parse(EvaluateFunctions(argument), System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (FormatException)
                {
                    throw new Exception($"Erreur : L'argument '{argument}' pour exp() n'est pas valide.");
                }

                double result = Calc.Exponential(argumentValue);

                expression = expression.Replace("exp(" + argument + ")", result.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }

            while (expression.Contains("^2"))
            {
                int index = expression.IndexOf("^2");

                int startIndex = index - 1;

                while (startIndex > 0 && (Char.IsDigit(expression[startIndex - 1]) || expression[startIndex - 1] == '.'))
                {
                    startIndex--;
                }

                string baseOperand = expression.Substring(startIndex, index - startIndex);

                double baseValue = double.Parse(baseOperand, System.Globalization.CultureInfo.InvariantCulture);
                double result = Calc.PowerOf2(baseValue); 

                expression = expression.Replace(baseOperand + "^2", result.ToString());
            }

            while (expression.Contains("^"))
            {
                int index = expression.IndexOf("^");

                int startIndex = index - 1;

                while (startIndex > 0 && (Char.IsDigit(expression[startIndex - 1]) || expression[startIndex - 1] == '.'))
                {
                    startIndex--;
                }

                string baseOperand = expression.Substring(startIndex, index - startIndex);

                int endIndex = index + 1;
                while (endIndex < expression.Length && (Char.IsDigit(expression[endIndex]) || expression[endIndex] == '.'))
                {
                    endIndex++;
                }

                string exponentOperand = expression.Substring(index + 1, endIndex - index - 1);

                double baseValue = double.Parse(baseOperand, System.Globalization.CultureInfo.InvariantCulture);
                double exponentValue = double.Parse(exponentOperand, System.Globalization.CultureInfo.InvariantCulture);
                double result = Calc.PowerOf(baseValue, exponentValue); 

                expression = expression.Replace(baseOperand + "^" + exponentOperand, result.ToString());
            }

            return expression;
        }




        private bool AreParenthesesBalanced(string expression)
        {
            int count = 0;
            foreach (char c in expression)
            {
                if (c == '(') count++;
                else if (c == ')') count--;
                if (count < 0) return false;
            }
            return count == 0;
        }

        private void button_Click(object sender, EventArgs e)
        {
            string buttonValue = (sender as Button).Text;
            currentCalculation += buttonValue;
            textBoxOutput.Text = currentCalculation;
            Console.WriteLine($"Expression actuelle : {currentCalculation}");
        }


        private void button_Equals_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(currentCalculation))
                {
                    textBox1.Text = "Erreur : Expression vide.";
                    Console.WriteLine("Aucune expression à évaluer.");
                    return;
                }

                if (!AreParenthesesBalanced(currentCalculation))
                {
                    throw new Exception("Erreur : Parenthèses non équilibrées.");
                }

                string formattedCalculation = EvaluateFunctions(currentCalculation.Replace("X", "*").Replace("÷", "/"));
                Console.WriteLine($"Expression avant évaluation : {formattedCalculation}");

                formattedCalculation = formattedCalculation.Replace(",", ".");

                string result = new DataTable().Compute(formattedCalculation, null).ToString();

                currentCalculation = result;  
                textBox1.Text = result;
            }
            catch (Exception ex)
            {
                textBox1.Text = "Erreur";  
                currentCalculation = "";
                Console.WriteLine($"Erreur détectée : {ex.Message}");
            }
        }




        private void button_Clear_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = "0";
            currentCalculation = "";
        }

        private void button_Clear_Last_Entry(object sender, EventArgs e)
        {
            if (currentCalculation.Length > 0)
            {
                currentCalculation = currentCalculation.Remove(currentCalculation.Length - 1, 1);
            }
            textBoxOutput.Text = currentCalculation;
        }

        private void button_Tan_Click(object sender, EventArgs e)
        {
            currentCalculation += "tan(";
            textBoxOutput.Text = currentCalculation;
        }

        private void button_Sin_Click(object sender, EventArgs e)
        {
            currentCalculation += "sin(";
            textBoxOutput.Text = currentCalculation;
        }

        private void button_Cos_Click(object sender, EventArgs e)
        {
            currentCalculation += "cos(";
            textBoxOutput.Text = currentCalculation;
        }

        private void button_Atan_Click(object sender, EventArgs e)
        {
            currentCalculation += "atan(";
            textBoxOutput.Text = currentCalculation;
        }

        private void button_Asin_Click(object sender, EventArgs e)
        {
            currentCalculation += "asin(";
            textBoxOutput.Text = currentCalculation;
        }

        private void button_Acos_Click(object sender, EventArgs e)
        {
            currentCalculation += "acos(";
            textBoxOutput.Text = currentCalculation;
        }

        private void button_ln_Click(object sender, EventArgs e)
        {
            currentCalculation += "ln(";
            textBoxOutput.Text = currentCalculation;
        }

        private void button_Log_Click(object sender, EventArgs e)
        {
            currentCalculation += "log(";
            textBoxOutput.Text = currentCalculation;
        }

        private void button_Sqrt_Click(object sender, EventArgs e)
        {
            currentCalculation += "sqrt(";
            textBoxOutput.Text = currentCalculation;
        }

        private void button_Cbrt_Click(object sender, EventArgs e)
        {
            currentCalculation += "cbrt(";
            textBoxOutput.Text = currentCalculation;
        }

        private void button_Power2_Click(object sender, EventArgs e)
        {
            currentCalculation += "^2";
            textBoxOutput.Text = currentCalculation;
        }

        private void button_PowerN_Click(object sender, EventArgs e)
        {
            currentCalculation += "^";
            textBoxOutput.Text = currentCalculation;
        }

        private void button_PI_Click(object sender, EventArgs e)
        {
            currentCalculation += "π";
            textBoxOutput.Text = currentCalculation;
        }

        private void button_Exponancial_Click(object sender, EventArgs e)
        {
            currentCalculation += "exp(";
            textBoxOutput.Text = currentCalculation;
        }
    }

}
