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
        if (!IsValidExpression(expression))
        {
            return "Error";
        }

        var (num1, num2) = ParseExpression(expression);
        return num1.HasValue && num2.HasValue
            ? (num1.Value + num2.Value).ToString()
            : "Error";
    }

    public void AddToHistory(string entry)
    {
        History.Add(entry);
    }
    
    public void SetState(string currentExpression, List<string> history)
    {
        CurrentExpression = currentExpression;
        History = new List<string>(history);
    }

    private static bool IsValidExpression(string expression)
    {
        return Regex.IsMatch(expression, @"^\d+\+\d+$");
    }

    private static (int? num1, int? num2) ParseExpression(string expression)
    {
        var parts = expression.Split('+');
        return (int.TryParse(parts[0], out var num1) ? num1 : null,
            int.TryParse(parts[1], out var num2) ? num2 : null);
    }
}
