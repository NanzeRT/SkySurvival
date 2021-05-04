using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public class SelectableCellBehaviour : CellBehaviour
    {
        [SerializeField]
        private Sprite selectedSprite;
        [SerializeField]
        private Sprite unselectedSprite;

        public void Select()
        {
            SetBackground(selectedSprite);
        }

        public void Unselect()
        {
            SetBackground(unselectedSprite);
        }
    }
}