using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using InventoryItems;

namespace InventorySystem
{
    public class DropedItemBehaviour : CellBehaviour
    {
        [SerializeField]
        private Renderer _renderer;
        private MaterialPropertyBlock _propBlock;

        void Awake()
        {
            _propBlock = new MaterialPropertyBlock();
        }

        public override void SetSprite(Sprite sprite)
        {
            _renderer.GetPropertyBlock(_propBlock);
            _propBlock.SetTexture("_ItemTex", sprite.texture);
            _renderer.SetPropertyBlock(_propBlock);
        }

        public override void SetBackground(Sprite sprite)
        {
            _renderer.GetPropertyBlock(_propBlock);
            _propBlock.SetTexture("_MainTex", sprite.texture);
            _renderer.SetPropertyBlock(_propBlock);
        }

        public override void Reset()
        {
            _renderer.GetPropertyBlock(_propBlock);
            _propBlock.SetTexture("_ItemTex", blankItem.texture);
            _renderer.SetPropertyBlock(_propBlock);
        }

        public override void SetAmount(int amount) { }
    }
}