using UnityEngine;
using System;
using System.Diagnostics;

namespace Arena
{
    public class IntroMessageTimer : MonoBehaviour
    {
        /// <summary>
        /// How long to display the message.
        /// </summary>
        public int secondsToDisplay = 2;
        
        private Stopwatch _timeDisplayed;
        private TimeSpan _maxTimeToDisplay;
        private Canvas _parentCanvas;

        private void Awake()
        {
            _timeDisplayed = Stopwatch.StartNew();
            _maxTimeToDisplay = TimeSpan.FromSeconds(secondsToDisplay);
        }
        private void Start()
        {
            _parentCanvas = GetComponent<Canvas>();
        }

        private void Update()
        {
            if (_timeDisplayed.Elapsed > _maxTimeToDisplay)
            {
                _timeDisplayed.Stop();
                _parentCanvas.enabled = false;
                enabled = false;

                UnityEngine.Debug.Log("Disabled intro message");
            }
        }
    }

}