using PersistentData;
using UnityEngine;

  //[CreateAssetMenu(fileName = "Warrior1", menuName = "Scriptable Objects/Upgrades/Warrior1")]
  public class Warrior1Upgrade : Upgrade
  {
      protected override void ApplyUpgrade(Combatant combatant, int rank)
      {
          // warrior.health = rank * 10 + warrior.baseHealth;
      }
  }