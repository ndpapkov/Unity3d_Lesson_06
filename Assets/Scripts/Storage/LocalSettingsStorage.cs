using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Storage
{
    [CreateAssetMenu(fileName = "settingsStorage", menuName = "course_example_05/SettingsStorage")]
    public class LocalSettingsStorage : ScriptableObject, ISettingsStorage
    {
        public UnitSettings[] Units => _units;
        public LevelSettings[] Levels => _levels;

        [SerializeField] private UnitSettings[] _units;
        [SerializeField] private LevelSettings[] _levels;

        [CanBeNull]
        public UnitSettings GetUnitById(int id)
        {
            return _units.FirstOrDefault(unit => unit.Id == id);
        }
    }
}