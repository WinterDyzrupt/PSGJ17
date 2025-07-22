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
        
        public static implicit operator int(IntVariable intVariable) => intVariable.value;
    }
}