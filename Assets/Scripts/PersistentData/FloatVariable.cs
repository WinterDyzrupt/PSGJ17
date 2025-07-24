using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu]
    public class FloatVariable : ScriptableObject
    {
        public float value;

        public void ResetValue()
        {
            value = 0f;
        }
        
        public static implicit operator float(FloatVariable floatVariable) => floatVariable.value;
    }
}
