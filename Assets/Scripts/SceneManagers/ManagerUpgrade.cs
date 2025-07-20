using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerUpgrade : MonoBehaviour
{
    public static ManagerUpgrade Instance;
    public UpgradeButton[] UpgradeButtons;
    public UpgradeProgress selectedUpgrade;
    public TMP_Text FameAmount;
    public TMP_Text SelectedName;
    public TMP_Text SelectedDescription;
    public Button PurchaseButton;
    public CanvasGroup[] panels;
    public Button[] toggleButtons;
    public Image[] tabImages;

    void Start()
    {
        Instance = this;
        UpdateButtons();
        UpdateUpgradeScene();
    }
    public void LoadTitleScene()
    {
        SceneManager.LoadScene("Title");
    }

    private void UpdateButtons()
    {
        foreach (UpgradeButton button in UpgradeButtons) button.UpdateButton();
    }


    public void SelectUpgrade(UpgradeProgress upgradeProgress)
    {
        selectedUpgrade = upgradeProgress;
        UpdateUpgradeScene();
    }

    public void UpdateUpgradeScene()
    {
        FameAmount.text = PlayerUpgrades.Instance.Fame.ToString();
        // If nothing selected, blank text fields and hide buy button
        if (selectedUpgrade.upgrade == null)
        {
            SelectedName.text = "";
            SelectedDescription.text = "";
            PurchaseButton.gameObject.SetActive(false);
            return;
        }

        // populate Name and description
        SelectedName.text = selectedUpgrade.upgrade.upgradeName;
        SelectedDescription.text = selectedUpgrade.upgrade.upgradeDescription[selectedUpgrade.currentRank];
        // If max rank, hide button
        if (selectedUpgrade.IsMaxed) PurchaseButton.gameObject.SetActive(false);
        // else show button and update button with price
        else
        {
            PurchaseButton.gameObject.SetActive(true);
            PurchaseButton.GetComponentInChildren<TMP_Text>().text = selectedUpgrade.GetCostForNextRank().ToString();
        }
        // if can't afford, disable button
        if (selectedUpgrade.CanAffordNextRank()) PurchaseButton.enabled = true;
        // else enable button
        else PurchaseButton.enabled = false;
    }

    public void PurchaseUpgrade()
    {
        // Deduct Fame
        PlayerUpgrades.Instance.Fame -= selectedUpgrade.GetCostForNextRank();
        // Rank up Progress
        selectedUpgrade.IncreaseRank();

        UpdateButtons();
        UpdateUpgradeScene();
    }

    //Panel Methods
    public void TogglePanels()
    {
        foreach (CanvasGroup panel in panels) Helper.CanvasHelper.ToggleCanvasGroup(panel);
        foreach (Button button in toggleButtons) button.enabled = !button.enabled;

        Color colorPlaceholder = tabImages[0].color;
        tabImages[0].color = tabImages[1].color;
        tabImages[1].color = colorPlaceholder;
    }
}



