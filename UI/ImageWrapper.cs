using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wombat
{

    [RequireComponent(typeof(Image))]
    public class ImageWrapper : MonoBehaviour
    {
        private Image image;
        private Color imageColor;
        public PlaceHolderIcon placeHolder;
        public bool disableWhenEmpty = false;
        private bool rendered = false;

        void Awake()
        {
            GetImage();
            if(!rendered) RenderSprite(image.sprite);
            rendered = true;

        }

        private Image GetImage()
        {
            if (image == null)
            {
                image = GetComponent<Image>();
                imageColor = image.color;
            }
            return image;
        }

        public void RenderSprite(Sprite sprite)
        {
            rendered = true;
            Image image = GetImage();
            if (sprite == null)
            {
                
                if (placeHolder != null && placeHolder.icon != null)
                {
                    image.enabled = true;
                    image.sprite = placeHolder.icon;
                    image.color = placeHolder.color;
                } else if (disableWhenEmpty)
                {
                    image.enabled = false;
                }

            }
            else
            {
                image.sprite = sprite;
                image.enabled = true;
                image.color = imageColor;
            }
        }

        public bool Toggle(bool state)
        {
            if (this.gameObject.activeSelf == state) return false;
            this.gameObject.SetActive(state);
            return true;
        }
    }
}
