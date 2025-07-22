using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class ResetPersistentDataOnRun
{
    static ResetPersistentDataOnRun()
    {
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    private static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            ResetAllUpgradeRanks();
        }
    }

    private static void ResetAllUpgradeRanks()
    {
        // Load AllUpgrades asset here instead of in the static constructor
        string[] guids = AssetDatabase.FindAssets("t:AllUpgrades");
        if (guids.Length == 0)
        {
            Debug.LogWarning("[UpgradeResetter] No AllUpgrades asset found.");
            return;
        }

        string path = AssetDatabase.GUIDToAssetPath(guids[0]);
        AllUpgrades allUpgrades = AssetDatabase.LoadAssetAtPath<AllUpgrades>(path);

        if (allUpgrades == null)
        {
            Debug.LogWarning("[UpgradeResetter] Failed to load AllUpgrades asset.");
            return;
        }

        foreach (Upgrade upgrade in allUpgrades)
        {
            upgrade.currentRank = 0;
            EditorUtility.SetDirty(upgrade);
        }

        AssetDatabase.SaveAssets();
        Debug.Log("[UpgradeResetter] All Upgrade runtime fields reset.");
    }
}