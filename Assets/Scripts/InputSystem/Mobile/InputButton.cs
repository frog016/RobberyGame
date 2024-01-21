using UnityEngine;
using UnityEngine.EventSystems;

namespace InputSystem.Mobile
{
    public class InputButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool IsPressed { get; private set; }
        public bool IsPressedThisFrame { get; private set; }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsPressed = true;
            IsPressedThisFrame = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsPressed = false;
        }

        private void LateUpdate()
        {
            IsPressedThisFrame = false;
        }
    }
}