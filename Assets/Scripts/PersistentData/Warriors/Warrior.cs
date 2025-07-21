using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(fileName = "Warrior", menuName = "Scriptable Objects/Warrior")]
    public class Warrior : ScriptableObject
    {
        public string displayName;
        public string description;
        //TODO: Skills, etc.

        public override string ToString()
        {
            return "DisplayName: " + displayName;
        }
    }
}
