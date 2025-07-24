
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

        private void Awake()
        {
            if (currentWarrior == null)
            {
                Debug.LogError($"Current Warrior hasn't been assigned to the PlayerController");
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ExecuteSkill(currentWarrior.skill1BasicAttack);
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                ExecuteSkill(currentWarrior.skill2Ability1);
            }
        }


        public void ExecuteSkill(Skill skill)
        {
            skill.ExecuteSkill(transform);
        }
    }
}
