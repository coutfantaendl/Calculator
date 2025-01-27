using System;
using System.Collections.Generic;

namespace Calculator.Abstraction
{
    public interface ICalculatorView
    {
        event Action<string> OnCalculatePressed;
        event Action OnViewLoaded;
        
        void ShowResult(string result);
        void ShowError(string errorMessage);
        void UpdateHistory(List<string> history);
        void SetExpression(string expression);
    }
}