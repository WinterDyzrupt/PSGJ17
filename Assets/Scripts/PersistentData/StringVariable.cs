using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu]
    public class StringVariable : ScriptableObject
    {
        public string value;

        public void ResetValue()
        {
            value = string.Empty;
        }
        
        public static implicit operator string(StringVariable stringVariable) => stringVariable.value;
    }
}
