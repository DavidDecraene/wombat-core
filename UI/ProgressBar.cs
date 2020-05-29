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

        void Start()
        {
            slider = GetComponent<Slider>();
            slider.fillRect.GetComponent<Image>().color = fillColor;

        }

        public void SetValue(float n)
        {
            this.slider.value = Mathf.Clamp(n, 0, 1);
        }

        void Update()
        {
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
