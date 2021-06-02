using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Wombat
{


    public class Tooltip : MonoBehaviour
    {
        private static Tooltip singleton;
        public TextMeshProUGUI tooltipText;
        public RectTransform background;
        public float padding = 4f;
        public Vector3 offset = new Vector3(12, -12, 0);
        private Camera mainCam;

        public ResourceCard InventoryCard { get; private set; }

        void Awake()
        {
            singleton = this;
            mainCam = Camera.main;
            HideTooltip();
        }

        void Update()
        {
            this.transform.position = UIUtils.UIToMouseOverlay(this.background, mainCam) + offset;
        }

        private void OnDestroy()
        {
            singleton = null;
        }

        private void ShowTooltip(string message)
        {
            if (gameObject == null) return;
            if (message == tooltipText.text)
            {
                if (!gameObject.activeSelf) gameObject.SetActive(true);
                return;
            }
            tooltipText.text = message;
            Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + padding * 2f, 
                tooltipText.preferredHeight + padding * 2f);
            background.sizeDelta = backgroundSize;
            gameObject.SetActive(true);
        }

        private void HideTooltip()
        {
            gameObject?.SetActive(false);
        }

        public static ResourceCard GetInventoryCard()
        {
            if (singleton == null) return null;
            return singleton.InventoryCard;
        }


        public static void Show(string message, ResourceCard inventoryCard = null)
        {
            if (singleton == null) return;
            singleton.ShowTooltip(message);
            singleton.InventoryCard = inventoryCard;
        }


        public static void Hide()
        {
            if (singleton != null)
            {
                singleton.InventoryCard = null;
                singleton.HideTooltip();
            }
        }
    }
}
