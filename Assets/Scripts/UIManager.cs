using System.Collections.Generic;
using UnityEngine;

namespace TargetClicker.UI
{
    public class UIManager
    {
        #region Singleton
        private static UIManager _instance;
        public static UIManager Instance
        {
            get
            {
                if( _instance == null ) _instance = new UIManager();
                return _instance;
            }
        }
        #endregion

        public enum LAYOUT
        {
            MAIN_MENU,
            GAME_HUD,
        }

        private readonly Canvas _uiCanvas;
        private GameObject _currentLayout;

        private Dictionary<LAYOUT, GameObject> _layouts; 

        private UIManager()
        {
            _uiCanvas = GameObject.FindGameObjectWithTag("uiCanvas").GetComponent<Canvas>();
            _currentLayout = initializeLayouts();
        }

        /// <summary>
        /// Initializes the layouts map.
        /// </summary>
        /// <returns>The starting layout.</returns>
        private GameObject initializeLayouts()
        {
            _layouts = new Dictionary<LAYOUT, GameObject>();
            GameObject mainMenu = _uiCanvas.transform.FindChild( "pnlMainMenu" ).gameObject;
            _layouts.Add(LAYOUT.MAIN_MENU, mainMenu);
            GameObject gameHud = _uiCanvas.transform.FindChild( "pnlHud" ).gameObject;
            _layouts.Add(LAYOUT.GAME_HUD, gameHud);
            return mainMenu;
        }

        /// <summary>
        /// Changes the UI layout.
        /// </summary>
        /// <param name="layoutType">Then new layout to show.</param>
        public void ShowLayout(LAYOUT layoutType)
        {
            GameObject newLayout;
            bool exists = _layouts.TryGetValue( layoutType, out newLayout );
            if( !exists || newLayout == _currentLayout) return;
            _currentLayout.SetActive( false );
            _currentLayout = newLayout;
            _currentLayout.SetActive(true);
        }
    }
}