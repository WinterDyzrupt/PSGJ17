using UnityEngine;
using UnityEngine.SceneManagement;
using Helper;
using PersistentData;
using UnityEngine.UI;

public class ManagerTitle : MonoBehaviour
{
    // Canvas Groups
    public CanvasGroup levelSelectPanel;
    public CanvasGroup creditsPanel;

    public IntVariable selectedBossIndex;
    public UpgradeGroup upgradesForAllWarriors;
    public CombatantGroup allWarriors;

    public Button[] _mainButtons;

    private void Awake()
    {
        Debug.Assert(upgradesForAllWarriors != null, nameof(upgradesForAllWarriors) + " expected to be non-null.");
    }

    private void Start()
    {
        LoadData();
        ApplyUpgrades(upgradesForAllWarriors, allWarriors);
    }

    private void LoadData()
    {
        Debug.LogWarning("Load Data method is not implemented yet.");
    }

    private void ApplyUpgrades(UpgradeGroup upgrades, CombatantGroup combatants)
    {
        foreach (var upgrade in upgrades.upgrades)
        {
            upgrade.ApplyToCombatantGroup(combatants);
        }
    }

    public void LoadUpgradeScene()
    {
        SceneManager.LoadScene(SceneData.UpgradeSceneIndex);
    }

    public void LoadPartySelectScene(int bossIndex)
    {
        selectedBossIndex.value = bossIndex;
        Debug.Log("Selected boss index: " + selectedBossIndex.value);

        SceneManager.LoadScene(SceneData.PartySelectSceneIndex);
    }

    public void ToggleLevelSelect()
    {
        ToggleTitleButtons();
        CanvasHelper.ToggleCanvasGroup(levelSelectPanel);
    }

    public void ToggleCredits()
    {
        ToggleTitleButtons();
        CanvasHelper.ToggleCanvasGroup(creditsPanel);
    }

    private void ToggleTitleButtons()
    {
        foreach (Button button in _mainButtons) { button.interactable = !button.interactable; }
    }
}
