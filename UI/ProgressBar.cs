using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wombat
{
    [RequireComponent(typeof(Slider))]
    public class ProgressBar : MonoBehaviour
    {

        private Slider slider;
        public Color fillColor;

        private float startValue = 0;

        void Awake()
        {
            slider = GetComponent<Slider>();
            slider.fillRect.GetComponent<Image>().color = fillColor;
            slider.value = startValue;
        }

        public void SetValue(float n)
        {
            if (slider == null)
            {
                startValue = Mathf.Clamp(n, 0, 1);
                return;
            }
            if (n == slider.value) return;
            this.slider.value = Mathf.Clamp(n, 0, 1);
        }

        void Update()
        {
            if (slider == null) return;
            if (slider.fillRect.gameObject.activeSelf)
            {
                if (slider.value <= 0) slider.fillRect.gameObject.SetActive(false);
            }
            else
            {
                if (slider.value > 0) slider.fillRect.gameObject.SetActive(true);
            }

        }
    }
}
