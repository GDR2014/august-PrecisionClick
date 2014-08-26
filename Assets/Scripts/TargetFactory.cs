using System.Collections.Generic;
using UnityEngine;

namespace TargetClicker
{
    public class TargetFactory
    {
        public enum TargetType
        {
            NORMAL
        }

        private Dictionary<TargetType, Target> _targetTypeLookup;

        #region Singleton
        private static TargetFactory _instance;
        public static TargetFactory Instance
        {
            get
            {
                if(_instance == null) _instance = new TargetFactory();
                return _instance;
            }
        }
        #endregion

        private TargetFactory()
        {
            initializeTargetLookup();
        }

        private void initializeTargetLookup()
        {
            _targetTypeLookup = new Dictionary<TargetType, Target>();
            _targetTypeLookup.Add( TargetType.NORMAL, Resources.Load<Target>( "Prefabs/Target" ) );
        }

        public Target GetTarget( TargetType targetType )
        {
            Target target;
            bool exists = _targetTypeLookup.TryGetValue( targetType, out target );
            if( exists ) return Object.Instantiate( target ) as Target;
            
            Debug.LogWarning(string.Format("No target loaded for enum '{0}'", targetType));
            return null;
        }
    }
}
