using System.Text;
using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(fileName = "Party", menuName = "Scriptable Objects/Party")]
    public class Party : ScriptableObject
    {
        public Warrior[] warriors;

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            if ((warriors?.Length ?? 0) == 0)
            {
                stringBuilder.Append("Party size: zero.");
            }
            else
            {
                stringBuilder.Append("Party size: " + warriors.Length);
                foreach (var warrior in warriors)
                {
                    stringBuilder.AppendLine("\tParty member: " + warrior);
                }
            }
            
            return stringBuilder.ToString();
        }
    }
}
