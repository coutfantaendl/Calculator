using System.Collections.Generic;

namespace Calculator.Abstraction
{
    public interface ICalculatorModel
    {
        string CurrentExpression { get; set; }
        List<string> History { get; }
        string Calculate(string expression);
        void AddToHistory(string entry);
        void SetState(string currentExpression, List<string> history);
    }
}