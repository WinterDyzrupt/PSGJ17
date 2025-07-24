using System.Collections.Generic;
using UnityEngine;

namespace PersistentData.Warriors
{
    [CreateAssetMenu(fileName = "AllWarriors", menuName = "Warriors/AllWarriors")]
    public class AllWarriors : ScriptableObject
    {
        public List<Warrior> warriors;
    }
}
