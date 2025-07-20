using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    public static PlayerUpgrades Instance { get; private set; }

    [SerializeField]
    private UpgradeSO[] allUpgrades;

    public readonly List<UpgradeProgress> upgradesProgress = new();

    public int Fame = 0;
    public int TotalFame = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeStates();
    }

    private void InitializeStates()
    {
        upgradesProgress.Clear();
        foreach (UpgradeSO upgradeSO in allUpgrades)
        {
            upgradesProgress.Add(new UpgradeProgress { upgrade = upgradeSO, currentRank = 0 });
        }
    }

    public UpgradeProgress GetUpgradeProgress(UpgradeSO upgradeSO)
    {
        return upgradesProgress.Find(u => u.upgrade == upgradeSO);
    }
}