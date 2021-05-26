using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class ItemDetector : MonoBehaviour
    {
        public delegate void CellDelegate(Cell c);

        public CellDelegate OnItemEnter;
        public CellDelegate OnItemExit;

        void OnTriggerEnter(Collider collider)
        {
            Debug.Log(12);
            CellBehaviour bh = collider.GetComponent<CellBehaviour>();

            if (bh)
            {
                Debug.Log(13);
                OnItemEnter(bh.Cell);
            }
        }

        void OnTriggerExit(Collider collider)
        {
            CellBehaviour bh = collider.GetComponent<CellBehaviour>();

            if (bh)
            {
                OnItemExit(bh.Cell);
            }
        }
    }
}