using PersistentData;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SceneManagers
{
    public class ManagerUpgrade : MonoBehaviour
    {
        public UpgradeButton[] upgradeButtons;
        private Upgrade _selectedUpgrade;
        public TMP_Text fameAmount;
        public TMP_Text selectedName;
        public TMP_Text selectedDescription;
        public Button purchaseButton;
        public CanvasGroup[] panels;
        public Button[] toggleButtons;
        public Image[] tabImages;
        public IntVariable currentFame;
        public AllWarriors allWarriors;

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
            _selectedUpgrade = upgradeButton.upgrade;
            RefreshUpgradeTextFields();
        }

        public void RefreshUpgradeTextFields()
        {
            fameAmount.text = currentFame.value.ToString();

            // If nothing selected, blank text fields and hide buy button
            if (_selectedUpgrade == null)
            {
                selectedName.text = "";
                selectedDescription.text = "";
                purchaseButton.interactable = false;
                return;
            }

            // populate Name and description
            selectedName.text = _selectedUpgrade.upgradeName;
            Debug.Log($"Upgrade being used to populate data: {_selectedUpgrade}."); 

            if ((_selectedUpgrade.upgradeDescription?.Length ?? 0) < _selectedUpgrade.currentRank)
            {
                Debug.LogError($"{_selectedUpgrade.name} does not have a valid description or have enough elements.");
                return;
            }

            selectedDescription.text = _selectedUpgrade.upgradeDescription[_selectedUpgrade.currentRank];

            // If max rank, hide button
            if (_selectedUpgrade.IsMaxed)
            {
                purchaseButton.interactable = false;
                purchaseButton.GetComponentInChildren<TMP_Text>().text = "Maxed";
            }

            // else show button and update button with price
            else
            {
                purchaseButton.interactable = true;
                purchaseButton.GetComponentInChildren<TMP_Text>().text = _selectedUpgrade.GetCostForNextRank().ToString();
            }

            // if can afford, enable button
            if (_selectedUpgrade.CanAffordNextRank(currentFame)) purchaseButton.interactable = true;
            // else disable button
            else purchaseButton.interactable = false;
        }

        public void PurchaseUpgrade()
        {
            // Deduct Fame
            currentFame.value -= _selectedUpgrade.GetCostForNextRank();
            // Rank up Progress
            _selectedUpgrade.IncreaseRank(allWarriors);

            RefreshButtons();
            RefreshUpgradeTextFields();
        }

        //Panel Methods
        public void TogglePanels()
        {
            foreach (CanvasGroup panel in panels) Helper.CanvasHelper.ToggleCanvasGroup(panel);
            foreach (Button button in toggleButtons) button.interactable = !button.interactable;

            (tabImages[1].color, tabImages[0].color) = (tabImages[0].color, tabImages[1].color);
        }
    }
}