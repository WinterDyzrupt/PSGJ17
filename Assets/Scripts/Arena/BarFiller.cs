using UnityEngine;
using UnityEngine.UI;

namespace Arena
{
    public class BarFiller : MonoBehaviour
    {
        public Image image;
        public float currentValue;
        public float maxValue;

        private void Update()
        {
            image.fillAmount = currentValue / maxValue;
        }
    }
}