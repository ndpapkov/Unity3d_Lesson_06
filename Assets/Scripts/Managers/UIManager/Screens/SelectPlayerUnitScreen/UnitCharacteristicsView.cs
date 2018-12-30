using Storage;
using UnityEngine;
using UnityEngine.UI;

namespace Managers.UIManager.Screens.SelectPlayerUnitScreen
{
    public class UnitCharacteristicsView : MonoBehaviour
    {
        [SerializeField]
        private Text _name;

        [SerializeField] 
        private Text _power;
        
        [SerializeField] 
        private Text _attackSpeed;
        
        [SerializeField] 
        private Text _movementSpeed;

        public void Initialize(UnitSettings unit)
        {
            _name.text = unit.Name;
            _power.text = unit.AttackSettings.AttackPower.ToString();
            var attackSpeed = 1 / unit.AttackSettings.AttackCooldown;
            _attackSpeed.text = attackSpeed.ToString("F2");
            _movementSpeed.text = unit.MovementSettings.MovementSpeed.ToString();
        }
    }
}