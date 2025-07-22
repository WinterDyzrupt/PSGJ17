using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PersistentData.Warriors
{
    [CreateAssetMenu(fileName = "Party", menuName = "Scriptable Objects/Party")]
    public class Party : ScriptableObject
    {
        public List<Warrior> warriors;

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Party size: " + (warriors?.Count ?? 0));
            if (warriors != null)
            {
                foreach (var warrior in warriors)
                {
                    stringBuilder.AppendLine("\tParty member: " + warrior);
                }
            }

            return stringBuilder.ToString();
        }
    }
}
