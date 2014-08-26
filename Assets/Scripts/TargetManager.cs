using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TargetClicker
{
    public class TargetManager : MonoBehaviour
    {
        #region Singleton
        private static TargetManager _instance;
        public static TargetManager Instance
        {
            get
            {
                if(_instance == null) _instance = FindObjectOfType<TargetManager>();
                return _instance;
            }
        }
        #endregion

        private Target _activeTarget;
        private bool _shouldSpawn = false;

        void Start ()
        {
            Target.TargetClickedEvent += OnTargetClicked;
            GameManager.GameStateChangedEvent += OnGameStateChanged;
        }

        private void OnTargetClicked( Target target, float percentageFromCenter )
        {
            Destroy(target.gameObject);
            if( !_shouldSpawn || object.ReferenceEquals( _activeTarget, null )) return;
            _activeTarget = spawnNewTarget();
        }

        private void OnGameStateChanged( GameManager.GameStateEvent gameStateEvent )
        {
            switch( gameStateEvent )
            {
                case GameManager.GameStateEvent.START:
                    startSpawning();
                    break;
                case GameManager.GameStateEvent.GAMEOVER:
                    stopSpawning();
                    break;
            }
        }

        private void startSpawning()
        {
            _shouldSpawn = true;
            if( _activeTarget != null ) return;
            _activeTarget = spawnNewTarget();
        }

        private void stopSpawning()
        {
            _shouldSpawn = false;
            if( _activeTarget == null ) return;
            Destroy(_activeTarget.gameObject);
            _activeTarget = null;
        }

        private Target spawnNewTarget()
        {
            Target target = TargetFactory.Instance.GetTarget( TargetFactory.TargetType.NORMAL );
            setRandomTargetPosition( target );
            return target;
        }

        private void setRandomTargetPosition( Target target )
        {
            int screenX = Random.Range(0, Screen.width);
            int screenY = Random.Range(0, Screen.height);
            Vector2 screenPosition = new Vector2(screenX, screenY);
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPosition);
            worldPoint.z = 0;
            target.transform.position = worldPoint;
        }
    }
}
