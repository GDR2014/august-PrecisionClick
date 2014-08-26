
using UnityEngine;

namespace TargetClicker
{
    public class ScoreManager : MonoBehaviour
    {

        public delegate void ScoreChangedHandler( int previousScore );
        public static event ScoreChangedHandler ScoreChangedEvent;

        public int CurrentScore { get; private set; }

        #region Singleton
        private static ScoreManager _instance;
        public static ScoreManager Instance
        {
            get
            {
                if(_instance == null) _instance = FindObjectOfType<ScoreManager>();
                return _instance;
            }
        }
        #endregion

        void Start()
        {
            Target.TargetClickedEvent += OnTargetClicked;
        }

        private void OnTargetClicked( Target target, float percentageFromCenter )
        {
            int points = calculatePoints( target, percentageFromCenter );
            updateScore(points);
        }

        private int calculatePoints( Target target, float percentageFromCenter )
        {
            int penaltyPoints = Mathf.FloorToInt(percentageFromCenter / target.PenaltyIncrement) * target.PenaltyIncrement;
            return target.BaseValue - penaltyPoints;
        }

        private void updateScore( int deltaScore )
        {
            int previousScore = CurrentScore;
            CurrentScore += deltaScore;
            if( ScoreChangedEvent != null ) ScoreChangedEvent( previousScore );
        }
    }
}
