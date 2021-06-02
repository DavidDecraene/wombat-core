using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wombat
{
    public class ImageSwap 
    {
        private readonly Image image;
        private readonly Sprite defaultSprite;
        private readonly Sprite alternativeSprite;
        public bool ShowAlternative { get; private set; }

        public ImageSwap(Image image, Sprite alternative)
        {
            this.image = image;
            this.defaultSprite = image?.sprite;
            this.alternativeSprite = alternative;
        }

        public void Toggle(bool showAlternative)
        {
            this.ShowAlternative = showAlternative;
            if (image == null) return;
            image.sprite = showAlternative ? alternativeSprite : defaultSprite;
        }
    }
}
