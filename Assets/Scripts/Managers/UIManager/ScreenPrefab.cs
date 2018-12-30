using System;
using UnityEngine;

namespace Managers.UIManager
{
    [Serializable]
    public class Screen
    {
        public ScreenType Type;
        public GameObject Prefab;
    }
}