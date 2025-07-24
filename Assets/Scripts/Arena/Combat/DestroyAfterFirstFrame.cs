using UnityEngine;

namespace Arena.Combat
{
    public class DestroyAfterFirstFrame : MonoBehaviour
    {
        private bool nextFrame = false;

        void FixedUpdate()
        {
            if (nextFrame)
            {
                Destroy(gameObject);
            }
            if (!nextFrame) nextFrame = true;
        }
    }
}