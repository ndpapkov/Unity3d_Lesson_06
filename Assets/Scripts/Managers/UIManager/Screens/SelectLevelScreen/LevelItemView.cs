using Storage;
using UnityEngine;
using UnityEngine.UI;

namespace Managers.UIManager.Screens.SelectLevelScreen
{
    public class LevelItemView : MonoBehaviour
    {
        [SerializeField] 
        private Text _name;

        public void Initialize(LevelSettings level)
        {
            _name.text = level.Name;
        }
    }
}