using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Connection
{
    public class WinLoseUIPanel : UIPanel
    {
        [SerializeField] private UIPanel _winPreview;
        [SerializeField] private UIPanel _losePreview;
        [SerializeField] private Button _closeWinPreview;
        [SerializeField] private Button _closeLosePreview;
        [SerializeField] private ResultUIPanel _resultPanel;

        public event Action ExitClicked;

        private void OnEnable()
        {
            _closeWinPreview.onClick.AddListener(CloseWinAndOpenResult);
            _closeLosePreview.onClick.AddListener(CloseLoseAndOpenResult);

            _resultPanel.ExitButtonClicked += RaiseExitClicked;
        }

        private void OnDisable()
        {
            _closeWinPreview.onClick.RemoveListener(CloseWinAndOpenResult);
            _closeLosePreview.onClick.RemoveListener(CloseLoseAndOpenResult);

            _resultPanel.ExitButtonClicked -= RaiseExitClicked;
        }

        public void Initialize(bool isWin)
        {
            if (isWin)
            {
                _winPreview.Open();
                _losePreview.Close();
                _resultPanel.Close();
            }
            else
            {
                _winPreview.Close();
                _losePreview.Open();
                _resultPanel.Close();
            }

            var resultText = isWin ? "Победа" : "Поражение";
            _resultPanel.Initialize(resultText);
        }

        private void CloseWinAndOpenResult()
        {
            _winPreview.Close();
            _resultPanel.Open();
        }

        private void CloseLoseAndOpenResult()
        {
            _losePreview.Close();
            _resultPanel.Open();
        }

        private void RaiseExitClicked()
        {
            ExitClicked?.Invoke();
        }
    }
}