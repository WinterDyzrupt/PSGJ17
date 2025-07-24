
using PersistentData.Warriors;
using UnityEngine;

namespace Arena
{
    /// <summary>
    /// Something to activate skills and pass information down to the skills
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        public Warrior currentWarrior;
        private FactionType _faction;

        private void Awake()
        {
            if (currentWarrior == null)
            {
                Debug.LogError($"Current Warrior hasn't been assigned to the PlayerController");
            }
        }

        private void Start()
        {
            if (TryGetComponent(out Faction factionComponent))
            {
                _faction = factionComponent.faction;
            }
            else
            {
                Debug.LogError($"{gameObject.name} doesn't have a faction.");
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ExecuteSkill(currentWarrior.basicAttack);
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                ExecuteSkill(currentWarrior.ability1);
            }
        }


        public void ExecuteSkill(Skill skill)
        {
            skill.ExecuteSkill(transform, _faction);
        }
    }
}
