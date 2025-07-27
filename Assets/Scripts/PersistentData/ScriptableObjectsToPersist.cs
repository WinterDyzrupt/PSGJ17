using PersistentData.Warriors;
using UnityEngine;

namespace PersistentData
{
    /// <summary>
    /// A strange workaround to tell UnityEditor to persist these ScriptableObjects across scenes.
    /// If a ScriptableObject is used in scene1 and scene3, with scene2 in the middle, Unity will reset the
    /// ScriptableObject to default value.
    /// This class should have all the ScriptableObjects that we modify and want persisted across scenes.
    /// NOTE: this does not include ScriptableObjects that are not modified at runtime.
    /// </summary>
    public class ScriptableObjectsToPersist : MonoBehaviour
    {
        public IntVariable selectedBossIndex;
        public IntVariable currentFame;
        public IntVariable lifetimeFame;
        public Warrior warrior1;
        public Warrior warrior2;
        public Warrior warrior3;
        public Warrior warrior4;
        public AgilityUpgrade agility;
        public DefenseUpgrade defense;
        public EvasionUpgrade evasion;
        public HealthUpgrade health;
        public SpeedUpgrade speed;
        // TODO: non-stat upgrades

        private static GameObject _instance;
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
            }
            else
            {
                DontDestroyOnLoad(this);
                _instance = gameObject;
            }
        }
    }
}
