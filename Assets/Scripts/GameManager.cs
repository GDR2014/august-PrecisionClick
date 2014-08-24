using TargetClicker.UI;
using UnityEngine;

namespace TargetClicker
{
    public class GameManager : MonoBehaviour
    {

        void Start()
        {
            UIManager.Instance.ShowLayout(UIManager.LAYOUT.MAIN_MENU);
        }

        public void StartNewGame()
        {
            UIManager.Instance.ShowLayout(UIManager.LAYOUT.GAME_HUD);
        }
    }
}
