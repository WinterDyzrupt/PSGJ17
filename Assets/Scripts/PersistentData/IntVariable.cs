using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu]
    public class IntVariable : ScriptableObject
    {
        public int value;

        public static implicit operator int(IntVariable intVariable) => intVariable.value;
    }
}