using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeGroup", menuName = "Scriptable Objects/Upgrades/UpgradeGroup")]
public class UpgradeGroup : ScriptableObject
{
    public List<Upgrade> upgrades;
}