using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu]
    public class IntVariable : ScriptableObject
    {
        public int value;

        public void ResetValue()
        {
            value = 0;
        }
    }
}
