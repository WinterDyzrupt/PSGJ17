using PersistentData;
using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public class VolumeSetting : MonoBehaviour
    {
        public Slider volumeSlider;
        public FloatVariable persistentVolume;

        private void Start()
        {
            Debug.Assert(volumeSlider != null,  nameof(volumeSlider) + " expected to be non-null.");
            Debug.Assert(persistentVolume, nameof(persistentVolume) + " expected to be non-null.");

            volumeSlider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });

            volumeSlider.value = persistentVolume;
            AudioListener.volume = persistentVolume;

            Debug.Log("Volume: " + AudioListener.volume);
        }

        private void OnSliderValueChanged()
        {
            persistentVolume.value = volumeSlider.value;
            AudioListener.volume = volumeSlider.value;
            Debug.Log("Volume: " + AudioListener.volume);
        }
    }
}
