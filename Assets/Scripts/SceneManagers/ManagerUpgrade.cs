using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SceneManagers
{
    public class ManagerUpgrade : MonoBehaviour
    {
        public static ManagerUpgrade Instance;
        public UpgradeButton[] upgradeButtons;
        public UpgradeProgress selectedUpgrade;
        public TMP_Text fameAmount;
        public TMP_Text selectedName;
        public TMP_Text selectedDescription;
        public Button purchaseButton;
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
            foreach (UpgradeButton button in upgradeButtons) button.UpdateButton();
        }


        public void SelectUpgrade(UpgradeProgress upgradeProgress)
        {
            selectedUpgrade = upgradeProgress;
            UpdateUpgradeScene();
        }

        public void UpdateUpgradeScene()
        {
            fameAmount.text = PlayerUpgrades.Instance.Fame.ToString();
            // If nothing selected, blank text fields and hide buy button
            if (selectedUpgrade.upgrade == null)
            {
                selectedName.text = "";
                selectedDescription.text = "";
                purchaseButton.gameObject.SetActive(false);
                return;
            }

            // populate Name and description
            selectedName.text = selectedUpgrade.upgrade.upgradeName;
            selectedDescription.text = selectedUpgrade.upgrade.upgradeDescription[selectedUpgrade.currentRank];
            // If max rank, hide button
            if (selectedUpgrade.IsMaxed) purchaseButton.gameObject.SetActive(false);
            // else show button and update button with price
            else
            {
                purchaseButton.gameObject.SetActive(true);
                purchaseButton.GetComponentInChildren<TMP_Text>().text = selectedUpgrade.GetCostForNextRank().ToString();
            }
            // if can't afford, disable button
            if (selectedUpgrade.CanAffordNextRank()) purchaseButton.enabled = true;
            // else enable button
            else purchaseButton.enabled = false;
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



}