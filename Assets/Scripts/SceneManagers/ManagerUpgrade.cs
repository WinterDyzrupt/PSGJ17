using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SceneManagers
{
    public class ManagerUpgrade : MonoBehaviour
    {
        public int test;
        public UpgradeButton[] upgradeButtons;
        public Upgrade selectedUpgrade;
        public TMP_Text fameAmount;
        public TMP_Text selectedName;
        public TMP_Text selectedDescription;
        public Button purchaseButton;
        public CanvasGroup[] panels;
        public Button[] toggleButtons;
        public Image[] tabImages;

        void Start()
        {
            RefreshButtons();
            RefreshUpgradeTextFields();
        }

        public void LoadTitleScene()
        {
            SceneManager.LoadScene("Title");
        }

        private void RefreshButtons()
        {
            foreach (UpgradeButton button in upgradeButtons) button.UpdateButton();
        }


        public void SelectUpgrade(UpgradeButton upgradeButton)
        {
            selectedUpgrade = upgradeButton.upgrade;
            RefreshUpgradeTextFields();
        }

        public void RefreshUpgradeTextFields()
        {
            fameAmount.text = PlayerUpgrades.Instance.Fame.ToString();

            // If nothing selected, blank text fields and hide buy button
            if (selectedUpgrade == null)
            {
                selectedName.text = "";
                selectedDescription.text = "";
                purchaseButton.interactable = false;
                return;
            }

            // populate Name and description
            selectedName.text = selectedUpgrade.upgradeName;

            if ((selectedUpgrade.upgradeDescription?.Length ?? 0) < selectedUpgrade.currentRank)
            {
                Debug.LogError($"{selectedUpgrade.name} does not have a valid description or have enough elements.");
                return;
            }

            selectedDescription.text = selectedUpgrade.upgradeDescription[selectedUpgrade.currentRank];

            // If max rank, hide button
            if (selectedUpgrade.IsMaxed)
            {
                purchaseButton.interactable = false;
            }

            // else show button and update button with price
            else
            {
                purchaseButton.interactable = true;
                purchaseButton.GetComponentInChildren<TMP_Text>().text = selectedUpgrade.GetCostForNextRank().ToString();
            }

            // if can afford, enable button
            if (selectedUpgrade.CanAffordNextRank()) purchaseButton.interactable = true;
            // else disable button
            else purchaseButton.interactable = false;
        }

        public void PurchaseUpgrade()
        {
            // Deduct Fame
            PlayerUpgrades.Instance.Fame -= selectedUpgrade.GetCostForNextRank();
            // Rank up Progress
            selectedUpgrade.IncreaseRank();

            RefreshButtons();
            RefreshUpgradeTextFields();
        }

        //Panel Methods
        public void TogglePanels()
        {
            foreach (CanvasGroup panel in panels) Helper.CanvasHelper.ToggleCanvasGroup(panel);
            foreach (Button button in toggleButtons) button.enabled = !button.enabled;

            (tabImages[1].color, tabImages[0].color) = (tabImages[0].color, tabImages[1].color);
        }
    }



}