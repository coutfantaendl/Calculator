using Calculator.Abstraction;

namespace Calculator
{
    public class CalculatorPresenter
    {
        private readonly ICalculatorView _view;
        private readonly ICalculatorModel _model;

        public CalculatorPresenter(ICalculatorView view, ICalculatorModel model)
        {
            _view = view;
            _model = model;
        }

        public void OnLoad()
        {
            var state = CalculatorPersistence.LoadState();
            _model.CurrentExpression = state.CurrentExpression;
            _model.History.AddRange(state.History);

            _view.SetExpression(_model.CurrentExpression);
            _view.UpdateHistory(_model.History);
        }

        public void OnCalculate(string expression)
        {
            _model.CurrentExpression = expression;

            var result = _model.Calculate(expression);
            if (result == "Error")
            {
                _view.ShowError("Invalid expression");
                _view.SetExpression(expression);
                return;
            }

            _model.AddToHistory($"{expression} = {result}");
            CalculatorPersistence.SaveState(_model.CurrentExpression, _model.History);

            _view.ShowResult(result);
            _view.UpdateHistory(_model.History);
        }
    }
}