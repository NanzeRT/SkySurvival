using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using InventoryItems;

namespace InventorySystem
{
    public class CellBehaviour : MonoBehaviour, IPointerClickHandler, ICellHandler
    {
        protected Cell cell;
        public Cell Cell => cell;
        public List<Cell> Cells => new List<Cell> { cell };
        public void SetCell(Cell c) => cell = c;
        [SerializeField]
        private Image background;
        [SerializeField]
        private Image itemImage;
        [SerializeField]
        protected Sprite blankItem;
        [SerializeField]
        private Text amountText;

        protected virtual void Start()
        {
            //itemImage.sprite = blankItem;
        }

        public virtual void OnPointerClick(PointerEventData eventData)
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
            if (item is null) return;
            SetAmount(item.Amount);
            SetSprite(item.GetSprite());
        }

        public virtual void SetSprite(Sprite sprite)
        {
            itemImage.sprite = sprite;
        }

        public virtual void SetBackground(Sprite sprite)
        {
            background.sprite = sprite;
        }

        public virtual void SetAmount(int amount)
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

        public virtual void Reset()
        {
            itemImage.sprite = blankItem;
            amountText.text = "";
        }

        public void Destroy()
        {
            cell.RemoveBehaviour(this);
            Destroy(gameObject);
        }
    }
}