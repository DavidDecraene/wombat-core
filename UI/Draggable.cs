using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Wombat
{
    [RequireComponent(typeof(EventTrigger))]
    public class Draggable : MonoBehaviour
    {
        public bool useDragGhost = false;
        public GameObject dragTarget;
        private RectTransform rectTransform;
        private Vector3 offset;
        private Camera mainCam;


        void Start()
        {
            EventTrigger trigger = GetComponent<EventTrigger>();
            UIUtils.CreateListener(trigger, EventTriggerType.Drag, (data) => OnDrag((PointerEventData)data));
            UIUtils.CreateListener(trigger, EventTriggerType.EndDrag, (data) => OnEndDrag((PointerEventData)data));
            UIUtils.CreateListener(trigger, EventTriggerType.BeginDrag, (data) => OnStartDrag((PointerEventData)data));


            rectTransform = dragTarget != null ? dragTarget.GetComponent<RectTransform>() : GetComponent<RectTransform>();
            offset = dragTarget != null ? dragTarget.transform.position - this.transform.position : Vector3.zero;
            mainCam = Camera.main;
        }

        public void Update()
        {
        }

        public void OnStartDrag(PointerEventData eventData)
        {
           // Debug.Log("start");
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // Debug.Log("stopped");
        }

        public void OnDrag(PointerEventData eventData)
        {
            // dragging = true;
            if (!useDragGhost)
            {
                if (dragTarget != null)
                {
                    // dragTarget.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y) + offset;
                    dragTarget.transform.position = UIUtils.UIToMouseOverlay(this.rectTransform, mainCam) + offset;
                    //  VertiFact.UIUtils.UIToMouse(this.rectTransform);
                    // new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                }
                else
                {
                    //transform.position  = new Vector3(Input.mousePosition.x, Input.mousePosition.y) + offset;
                    transform.position = UIUtils.UIToMouseOverlay(this.rectTransform, mainCam) + offset;
                }
            }
        }
    }
}
