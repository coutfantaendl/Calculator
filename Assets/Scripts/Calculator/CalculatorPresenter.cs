using System;
using Calculator.Abstraction;
using UnityEngine;
namespace Calculator
{
    public class CalculatorPresenter : MonoBehaviour
    {
        [SerializeField] private CalculatorView _calculatorView;
        
        private ICalculatorView _view;
        private ICalculatorModel _model;
        
        private void Awake()
        {
            _view = _calculatorView;
            _model = new CalculatorModel();
        }

        private void OnEnable()
        {
            _view.OnViewLoaded += OnLoad;
            _view.OnCalculatePressed += HandleCalculatePressed;
        }

        private void OnDisable()
        {
            _view.OnViewLoaded -= OnLoad;
            _view.OnCalculatePressed -= HandleCalculatePressed;
        }

        private void OnLoad()
        {
            Debug.Log("OnLoad called");
            var state = CalculatorPersistence.LoadState();
            
            _model.SetState(state.CurrentExpression, state.History);

            _view.SetExpression(_model.CurrentExpression);
            _view.UpdateHistory(_model.History);
        }

        private void HandleCalculatePressed(string expression)
        {
            _model.CurrentExpression = expression;

            var result = _model.Calculate(expression);
            if (result is "Error")
            {
                HandleError(expression);
                return;
            }

            AddToHistoryAndUpdateView(expression, result);
        }
        
        private void HandleError(string expression)
        {
            _view.ShowError("Error");
            _view.SetExpression(expression);
        }

        private void AddToHistoryAndUpdateView(string expression, string result)
        {
            var entry = $"{expression} = {result}";
            _model.AddToHistory(entry);

            CalculatorPersistence.SaveState(_model.CurrentExpression, _model.History);

            _view.ShowResult(result);
            _view.UpdateHistory(_model.History);
        }
    }
}