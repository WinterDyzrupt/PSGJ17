using UnityEngine;

namespace Helper
{
    public class CanvasHelper
    {
        public static void ToggleCanvasGroup(CanvasGroup canvasGroup)
        {
            bool state = !canvasGroup.interactable;
            canvasGroup.interactable = state;
            canvasGroup.blocksRaycasts = state;
            canvasGroup.alpha = state ? 1 : 0;
        }
    }
}
