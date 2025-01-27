using System.IO;
using UnityEngine;
using System.Collections.Generic;

namespace Calculator
{
    public static class CalculatorPersistence
    {
        private static readonly string FilePath = Application.persistentDataPath + "/calculator_data.json";

        public static void SaveState(string currentExpression, List<string> history)
        {
            var data = new CalculatorState
            {
                CurrentExpression = currentExpression,
                History = history
            };

            File.WriteAllText(FilePath, JsonUtility.ToJson(data));
        }

        public static CalculatorState LoadState()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                return JsonUtility.FromJson<CalculatorState>(json);
            }

            return new CalculatorState();
        }
    }

    [System.Serializable]
    public class CalculatorState
    {
        public string CurrentExpression = string.Empty;
        public List<string> History = new List<string>();
    }
}