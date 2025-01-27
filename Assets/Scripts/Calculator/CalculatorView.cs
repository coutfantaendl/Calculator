using Calculator.Abstraction;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
namespace Calculator
{
    public class CalculatorView : MonoBehaviour, ICalculatorView
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private TextMeshProUGUI resultText;
        [SerializeField] private TextMeshProUGUI historyText;
        [SerializeField] private Button calculateButton;

        private CalculatorPresenter _presenter;

        private void Start()
        {
            var model = new CalculatorModel();
            _presenter = new CalculatorPresenter(this, model);

            _presenter.OnLoad();
            calculateButton.onClick.AddListener(() => _presenter.OnCalculate(inputField.text));
        }

        public void ShowResult(string result)
        {
            resultText.text = result;
        }

        public void ShowError(string errorMessage)
        {
            Debug.LogError(errorMessage);
        }

        public void UpdateHistory(List<string> history)
        {
            historyText.text = string.Join("\n", history);
        }

        public void SetExpression(string expression)
        {
            inputField.text = expression;
        }
    }
}