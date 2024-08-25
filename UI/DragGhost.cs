using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wombat
{
    [RequireComponent(typeof(RectTransform))]
    public class DragGhost : MonoBehaviour
    {
        private bool dragging = false;
        public Image image;
        private Sprite baseSprite;
        private RectTransform rectTransform;
        private Camera mainCam;
        private InventorySlot<ResourceCard> slot;

        private void Awake()
        {
            this.gameObject.SetActive(false);
            baseSprite = image.sprite;
            rectTransform = GetComponent<RectTransform>();
            mainCam = Camera.main;
        }

        public void Update()
        {
            if (dragging)
            {
                transform.position = UIUtils.UIToMouseOverlay(this.rectTransform, mainCam);
            }
        }

        public void StartDrag(InventorySlot<ResourceCard> slot)
        {
            this.slot = slot;
            this.image.sprite = slot?.Resource?.icon ?? baseSprite;
            this.Toggle(true);
        }

        public void ClearInventoryView(Inventory<ResourceCard> inventory)
        {
            if (slot == null) return;
            if (slot.Parent != inventory)
            {
                return;
            }
            slot = null;
            Toggle(false);

        }

        public void Toggle(bool state)
        {
            this.gameObject.SetActive(state);
            if (state)
            {
                this.dragging = true;
            }
            else
            {
                this.dragging = false;
            }
        }
    }
}
