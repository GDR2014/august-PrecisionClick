using UnityEngine;

namespace TargetClicker
{
    public class SoundManager : MonoBehaviour
    {
        private const float BULLSEYE_THRESHOLD = 10;

        [SerializeField] private AudioClip HitNormal;
        [SerializeField] private AudioClip HitBullseye;

        public float Volume { get; set; }
        public bool Muted { get; set; }

        private const string PREFKEY_VOLUME = "soundmanager volume";
        private const string PREFKEY_MUTED = "soundmanager muted";

        void Start()
        {
            Target.TargetClickedEvent += OnTargetClicked;
            Volume = PlayerPrefs.GetFloat( PREFKEY_VOLUME, 1 );
            Muted = PlayerPrefs.GetInt( PREFKEY_MUTED, 0 ) > 0;
        }

        private void OnTargetClicked( Target target, float percentageFromCenter )
        {
            if( Muted ) return;
            bool isBullseye = percentageFromCenter < BULLSEYE_THRESHOLD;
            AudioClip clip = isBullseye ? HitBullseye : HitNormal;
            AudioSource.PlayClipAtPoint(clip, target.transform.position, Volume);
        }
    }
}
