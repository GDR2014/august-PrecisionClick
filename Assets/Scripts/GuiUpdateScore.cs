using UnityEngine;
using UnityEngine.UI;

namespace TargetClicker.UI
{
    [RequireComponent(typeof(Text))]
    public class GuiUpdateScore : MonoBehaviour
    {
        private Text _text;

        void Start()
        {
            _text = GetComponent<Text>();
            ScoreManager.ScoreChangedEvent += OnScoreChanged;
        }

        private void OnScoreChanged( int previousscore )
        {
            _text.text = ScoreManager.Instance.CurrentScore + "";
        }
    }
}
