using System;
using UnityEngine;

namespace Storage
{
    [Serializable]
    public class LevelSettings
    {
        public string Name;
        public EnemyUnitSettings[] EnemyUnits;
    }
    
    [Serializable]
    public class EnemyUnitSettings
    {
        public Vector3 Position;
        public int UnitId;
    }
}