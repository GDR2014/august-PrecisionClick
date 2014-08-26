using UnityEngine;

namespace TargetClicker
{
    public class Target : MonoBehaviour
    {
        // TODO: Maybe remove "pointAmount"-parameter and calculate points in the pointmanager via the event.
        public delegate void TargetClickedHandler( Target target, float percentageFromCenter );
        public static event TargetClickedHandler TargetClickedEvent;

        public int BaseValue{get { return 100; }}
        public int PenaltyIncrement{get { return 10; }}

        void OnMouseDown()
        {
            HandleClick();
        }

        private void HandleClick()
        {
            if( GameManager.Instance.IsPaused || TargetClickedEvent == null ) return;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
            TargetClickedEvent( this, calculatePercentFromCenter( mousePosition ) );
        }

        private float calculatePercentFromCenter(Vector2 mousePosition)
        {
            float distanceFromCenter = Vector2.Distance( transform.position, mousePosition );
            float percentageFromCenter = distanceFromCenter / (transform.localScale.x / 2) * 100;
            return percentageFromCenter;
        }
    }
}
