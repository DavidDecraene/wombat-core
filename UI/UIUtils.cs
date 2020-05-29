using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Wombat
{
    public static class UIUtils
    {

        public static void CreateListener(EventTrigger trigger, EventTriggerType type, UnityEngine.Events.UnityAction<BaseEventData> action)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = type;
            entry.callback.AddListener(action);
            trigger.triggers.Add(entry);

        }

        public static Vector3 UIToMouseOverlay(RectTransform tf, Camera bounds)
        {
            Vector3 point = bounds.ScreenToViewportPoint(Input.mousePosition);
            point.x = Mathf.Clamp(point.x, 0, 1);
            point.y = Mathf.Clamp(point.y, 0, 1);
            Vector3 boundedPosition = bounds.ViewportToScreenPoint(point);
            return new Vector3(boundedPosition.x, boundedPosition.y);
            // RectTransformUtility.ScreenPointToWorldPointInRectangle(tf, Input.mousePosition, bounds, out Vector3 target);
            // return target;
        }

        public static string RenderNumber(int amount, Text count, bool showZero)
        {
            string result = null;
            if(amount >= 1 || (amount == 0 && showZero))
                {
                result = amount.ToString("G2");
            }
            else
                {
                result = null;
            }
            if (count == null) return result;
            count.text = result;
            return result;
        }

        public static void RenderSprite(Image image, Sprite sprite, bool disableWhenNull)
        {
            if (image == null) return;
            image.sprite = sprite;
            if (sprite == null)
            {
                if (disableWhenNull) image.enabled = false;

            }
            else image.enabled = true;
        }
    }
}
