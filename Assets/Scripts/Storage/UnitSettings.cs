using System;

namespace Storage
{
    [Serializable]
    public class UnitSettings
    {
        public int Id;
        public string Name;
        public string PrefabPath;
        public string Icon;
        public float BaseHealth;
        public AttackSettings AttackSettings;
        public MovementSettings MovementSettings;
    }
    
    [Serializable]
    public class AttackSettings
    {
        public float AttackDistance;
        public float AttackPower;
        public float AttackCooldown;
    }
    
    [Serializable]
    public class MovementSettings
    {
        public float MovementSpeed;
        public float StoppingDistance;
    }
}