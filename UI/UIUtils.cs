
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Wombat
{
    public static class UIUtils
    {

        public static string RenderDate(float time, string zero, int hoursInDay)
        {
            float hours = 60 * 60;
            float days = hours * hoursInDay;
            int second = 0;
            int hour = 0;
            int minute = 0;
            int day = 0;
            if (time <= 0)
            {
                return zero;
            }
            if (time > days)
            {
                float rest = time % (days);
                day = Mathf.FloorToInt((time - rest) / days);
                time = rest;
            }
            if (time > hours)
            {
                float rest = time % (hours);
                hour = Mathf.FloorToInt((time - rest) / hours);
                time = rest;
            }
            if (time > 60)
            {
                float rest = time % 60;
                minute = Mathf.FloorToInt((time - rest) / 60);
                time = rest;
            }
            if (time >= 1)
            {
                float rest = time % 1;
                second = Mathf.FloorToInt(time - rest);
            }
            if (day > 1)
            {
                if (hour == 0 && minute == 0 && second == 0) return string.Format("{0} days", day);
                return string.Format("{3} days + {0:D2}:{1:D2}:{2:D2}", hour, minute, second, day);
            }
            if (day == 1)
            {
                if (hour == 0 && minute == 0 && second == 0) return "1 day";
                return string.Format("1 day + {0:D2}:{1:D2}:{2:D2}", hour, minute, second);

            }
            if (minute > 0)
            {
                return string.Format("{0:D2}:{1:D2}:{2:D2}", hour, minute, second);
            }
            return string.Format("{0:D2} seconds", second);
        }
        

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
        }

        public static string RenderNumber(int amount, TextMeshProUGUI count, bool showZero)
        {
            string result = null;
            if (amount == 0)
            {
                if (!showZero)
                {
                    if (count != null) count.text = null;
                    return null;
                }
            }
            result = NumberFormats.instance.Format(amount);
            if (count == null) return result;
            count.text = result;
            return result;
        }

        public static string RenderNumber(int amount, Text count, bool showZero)
        {
            string result = null;
            if (amount == 0)
            {
                if (!showZero)
                {
                    if (count != null) count.text  = null;
                    return null;
                }
            }
            result = NumberFormats.instance.Format(amount);
            if (count == null) return result;
            count.text = result;
            return result;
        }

        public static void RenderSprite(Image image, Sprite sprite, bool disableWhenNull)
        {
            if (image == null) return;
            if (image.sprite == sprite) return;
            image.sprite = sprite;
            if (sprite == null)
            {
                if (disableWhenNull) image.enabled = false;

            }
            else image.enabled = true;
        }

        public static bool UIManager(out UIManager mgr)
        {
            mgr = GameManager.instance?.ui;
            return mgr != null;
        }
    }
}
