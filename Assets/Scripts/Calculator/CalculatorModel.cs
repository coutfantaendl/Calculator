using System.Text.RegularExpressions;
using Calculator.Abstraction;
using System.Collections.Generic;
public class CalculatorModel : ICalculatorModel
{
    public string CurrentExpression { get; set; }
    public List<string> History { get; private set; }

    public CalculatorModel()
    {
        History = new List<string>();
        CurrentExpression = string.Empty;
    }

    public string Calculate(string expression)
    {
        if (!Regex.IsMatch(expression, @"^\d+\+\d+$"))
        {
            return "Error";
        }

        var parts = expression.Split('+');
        if (int.TryParse(parts[0], out var num1) && int.TryParse(parts[1], out var num2))
        {
            return (num1 + num2).ToString();
        }

        return "Error";
    }

    public void AddToHistory(string entry)
    {
        History.Add(entry);
    }
}
