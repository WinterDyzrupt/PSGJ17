using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    public static PlayerUpgrades Instance { get; private set; }

    [SerializeField]
    private List<Upgrade> allUpgrades;

    public readonly List<Upgrade> upgrades = new();

    public int Fame = 0;
    public int TotalFame = 0;
}