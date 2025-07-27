using UnityEngine;

/// <summary>
/// A reference to the currently-selected upgrade.
/// </summary>
[CreateAssetMenu(fileName = "UpgradeSelection", menuName = "Scriptable Objects/Upgrades/UpgradeSelection")]
public class UpgradeSelection : ScriptableObject
{
    public Upgrade upgrade;
}
