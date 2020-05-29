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

        private void Awake()
        {
            this.gameObject.SetActive(false);
            baseSprite = image.sprite;
            rectTransform = GetComponent<RectTransform>();
        }

        public void Update()
        {
            if (dragging)
            {
                transform.position = UIUtils.UIToMouseOverlay(this.rectTransform, Camera.main);
            }
        }

        public void StartDrag(Sprite icon)
        {
            this.image.sprite = icon ?? baseSprite;
            this.Toggle(true);
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
