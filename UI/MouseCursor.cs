using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wombat
{
    [RequireComponent(typeof(RectTransform))]
    public class MouseCursor : MonoBehaviour
    {

        public bool locked = true;
        public Vector3 offset = new Vector3();
        public RectTransform RectTransform { get; private set; }

        void Start()
        {
            Cursor.visible = false;
            RectTransform = GetComponent<RectTransform>();
            RectTransform.position = new Vector3(Screen.width / 2, Screen.height / 2) - offset;
        }

        void Update()
        {
            if (!locked)
            {
                RectTransform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, RectTransform.position.z) - offset;
            }
        }

        public bool Toggle(bool state)
        {
            if (this.gameObject.activeSelf == state) return false;
            this.gameObject.SetActive(state);
            return true;
        }


        public Ray ToRay(Camera bounds)
        {
            return bounds.ScreenPointToRay(transform.position);
        }

    }
}
