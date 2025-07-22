using System.Collections.Generic;
using UnityEngine;

namespace PersistentData.Warriors
{
    [CreateAssetMenu(fileName = "AllWarriors", menuName = "Scriptable Objects/AllWarriors")]
    public class AllWarriors : ScriptableObject
    {
        public List<Warrior> warriors;
    }
}
