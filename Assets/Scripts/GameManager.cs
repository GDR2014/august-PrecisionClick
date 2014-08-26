using TargetClicker.UI;
using UnityEngine;

namespace TargetClicker
{
    public class GameManager : MonoBehaviour
    {
        public delegate void GameStateChangedHandler( GameStateEvent gameStateEvent );
        public static event GameStateChangedHandler GameStateChangedEvent;

        #region Singleton
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if(_instance == null) _instance = FindObjectOfType<GameManager>();
                return _instance;
            }
        }
        #endregion

        public bool IsPaused { get; private set; }

        public enum GameStateEvent
        {
            START, GAMEOVER, PAUSED, UNPAUSED 
        }

        void Start()
        {
            IsPaused = false;
            UIManager.Instance.ShowLayout(UIManager.LAYOUT.MAIN_MENU);
        }

        public void StartNewGame()
        {
            if(GameStateChangedEvent != null) GameStateChangedEvent(GameStateEvent.START);
            UIManager.Instance.ShowLayout(UIManager.LAYOUT.GAME_HUD);
        }
    }
}
