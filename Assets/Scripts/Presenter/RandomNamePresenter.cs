using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Presenter
{
    public class RandomNamePresenter : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _nameInputField;
        [SerializeField] private Button _randomizeButton;

        private void OnEnable()
        {
            _randomizeButton.onClick.AddListener(RandomizeName);
        }

        private void OnDisable()
        {
            _randomizeButton.onClick.RemoveListener(RandomizeName);
        }

        public void RandomizeName()
        {
            var randomName = GetRandomName();
            _nameInputField.text = randomName;
        }

        public string GetName()
        {
            return _nameInputField.text;
        }

        public bool IsNameEmpty()
        {
            return string.IsNullOrEmpty(_nameInputField.text);
        }

        private static string GetRandomName()
        {
            const int stringNameLength = 5;
            const int numberNameLength = 3;

            return GetRandomString(stringNameLength) + GetRandomNumber(numberNameLength);
        }

        private static string GetRandomString(int length)
        {
            const int minCharCode = 97;
            const int maxCharCode = 122;

            var chars = new char[length];
            for (var index = 0; index < chars.Length; index++)
                chars[index] = (char)Random.Range(minCharCode, maxCharCode);

            return new string(chars);
        }

        private static string GetRandomNumber(int length)
        {
            const int minNumber = 0;
            const int maxNumber = 9;

            var chars = new char[length];
            for (var index = 0; index < chars.Length; index++)
                chars[index] = char.Parse(Random.Range(minNumber, maxNumber).ToString());

            return new string(chars);
        }
    }
}