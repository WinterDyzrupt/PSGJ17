using UnityEngine;

namespace Arena.Combat
{
    public class DestroyAfterElapsedFrames : MonoBehaviour
    {
        public int framesToKeepAlive;
        public float secondsToKeepAlive;
        private float _startTime;
        private int _framesSpentAlive;

        private void Start()
        {
            _startTime = Time.time;
        }

        private void FixedUpdate()
        {
            if (secondsToKeepAlive > 0 && Time.time - _startTime > secondsToKeepAlive)
            {
                Destroy(gameObject);
            } 
            else if (secondsToKeepAlive == 0 && _framesSpentAlive >= framesToKeepAlive)
            {
                Destroy(gameObject);
            }
            _framesSpentAlive++;
        }
    }
}