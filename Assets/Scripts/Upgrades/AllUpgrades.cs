using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllUpgrades", menuName = "Scriptable Objects/Upgrade/AllUpgrades")]
public class AllUpgrades : ScriptableObject
{
    public Upgrade[] allUpgrades;

    public IEnumerator<Upgrade> GetEnumerator()
    {
        foreach (var upgrade in allUpgrades)
            yield return upgrade;
    }
}
