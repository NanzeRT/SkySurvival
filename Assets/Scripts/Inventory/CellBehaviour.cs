using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using InventoryItems;

namespace InventorySystem
{
    public class CellBehaviour : MonoBehaviour, IPointerClickHandler
    {
        private Cell cell;
        public void SetCell(Cell c) => cell = c;
        [SerializeField]
        private Image background;
        [SerializeField]
        private Image itemImage;
        [SerializeField]
        private Sprite blankItem;
        [SerializeField]
        private Text amountText;

        void Start()
        {
            //itemImage.sprite = blankItem;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    cell.OnLeftClick();
                    break;
                case PointerEventData.InputButton.Right:
                    cell.OnRightClick();
                    break;
            }
        }

        public void SetItem(ItemBase item)
        {
            SetAmount(item.Amount);
            SetSprite(item.GetSprite());
        }

        public void SetSprite(Sprite sprite)
        {
            itemImage.sprite = sprite;
        }

        public void SetBackground(Sprite sprite)
        {
            background.sprite = sprite;
        }

        public void SetAmount(int amount)
        {
            if (amount != 1)
            {
                amountText.text = amount.ToString();
            }
            else
            {
                amountText.text = "";
            }
        }

        public void SetAmount(ItemBase item) => SetAmount(item.Amount);

        public void Reset()
        {
            itemImage.sprite = blankItem;
            amountText.text = "";
        }
    }
}