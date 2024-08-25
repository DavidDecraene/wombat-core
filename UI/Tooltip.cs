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

        [SerializeField]
        private KeyValueView amountView;
        [SerializeField]
        private KeyValueView stackSizeView;
        [SerializeField]
        private KeyValueView fuelValueView;
        [SerializeField]
        private KeyValueView analysisLevelView;
        [SerializeField]
        private KeyValueView rpValueView;

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

        private void ShowTooltip(string title)
        {
            if (gameObject == null) return;
            tooltipText.text = title;
            gameObject.SetActive(true);
        }

        private void HideTooltip()
        {
            gameObject?.SetActive(false);
        }

        void RenderStats(ResourceCard card)
        {
            if (card == null)
            {
                analysisLevelView.Toggle(false);
                rpValueView.Toggle(false);
                return;
            }
            rpValueView.SetValue(NumberFormats.instance.Format(card.researchPoints));
            rpValueView.Toggle(true);
            if (card.researchPoints <= 0)
            {
                analysisLevelView.Toggle(false);
                return;
            }
            ResourceStat stat = GameManager.instance?.Stats.GetResourceStat(card);
            AnalysisStat aStat = stat.GetAnalysis();
            analysisLevelView.SetValue(NumberFormats.instance.Format(aStat.completed));
            analysisLevelView.Toggle(true);
        }

        void RenderFuelValue(int amount)
        {
            if (amount <= 0)
            {
                fuelValueView.Toggle(false);
                return;
            }
            fuelValueView.SetValue($"{amount} kW");
            fuelValueView.Toggle(true);
        }

        void RenderStackSize(int amount)
        {
            if (amount <= 0)
            {
                stackSizeView.Toggle(false);
                return;
            }
            stackSizeView.SetValue(NumberFormats.instance.Format(amount));
            stackSizeView.Toggle(true);
        }

        void RenderAmount(int amount)
        {
            if (amount <= 0)
            {
                amountView.Toggle(false);
                return;
            }
            amountView.SetValue(NumberFormats.instance.Format(amount));
            amountView.Toggle(true);
        }

        public static ResourceCard GetInventoryCard()
        {
            if (singleton == null) return null;
            return singleton.InventoryCard;
        }


        public static void ShowEmpty(string message)
        {
            if (singleton == null) return;
            singleton.ShowTooltip(message);
            singleton.InventoryCard = null;
            singleton.RenderAmount(0);
            singleton.RenderStackSize(0);
            singleton.RenderFuelValue(0);
            singleton.RenderStats(null);
        }

        public static void ShowCard(ResourceCard card)
        {
            if (singleton == null) return;
            singleton.InventoryCard = card;
            singleton.ShowTooltip(card.Label);
            singleton.RenderAmount(0);
            singleton.RenderStackSize(card.stackSize);
            singleton.RenderFuelValue(card.fuelValue);
            singleton.RenderStats(card);

        }

        public static void ShowInventory(InventorySlot<ResourceCard> slot)
        {
            if (singleton == null || slot == null) return;
            singleton.InventoryCard = slot.Resource;
            singleton.ShowTooltip(slot.Resource.Label);
            singleton.RenderAmount(slot.Count);
            singleton.RenderStackSize(slot.Resource.stackSize);
            singleton.RenderFuelValue(slot.Resource.fuelValue);
            singleton.RenderStats(slot.Resource);
        }

        public static void ShowLiquid(ResourceCard card, LiquidSlot slot, int avgValue = 0)
        {
            if (singleton == null || card == null) return;
            singleton.InventoryCard = card;
            singleton.ShowTooltip(card.Label);
            singleton.RenderAmount(avgValue);
            singleton.RenderStackSize(card.stackSize);
            singleton.RenderFuelValue(0);
            singleton.RenderStats(null);

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
