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
        public UpgradeSelection currentUpgradeSelection;
        public TMP_Text fameAmount;
        public TMP_Text selectedName;
        public TMP_Text selectedDescription;
        public Button purchaseButton;
        public CanvasGroup[] panels;
        public Button[] toggleButtons;
        public Image[] tabImages;
        public IntVariable currentFame;
        public CombatantGroup allWarriors;
        
        public string maxedDisplayString = "Maxed";

        private void Awake()
        {
            Debug.Assert(currentUpgradeSelection != null,  nameof(currentUpgradeSelection) + " expected to be non null.");
        }

        private void Start()
        {
            RefreshButtons();
            RefreshFameTextFields(currentFame);
            InitializeUpgradeTextFields();
        }

        public void LoadTitleScene()
        {
            SceneManager.LoadScene("Title");
        }

        private void RefreshButtons()
        {
            foreach (UpgradeButton button in upgradeButtons) button.UpdateButton();
        }

        public void OnUpgradeSelected()
        {
            Debug.Log("OnUpgradeSelected; upgrade selection: " + currentUpgradeSelection?.upgrade);
            RefreshUpgradeTextFields(currentUpgradeSelection?.upgrade);
        }

        private void RefreshFameTextFields(int fame)
        {
            fameAmount.text = fame.ToString();
        }

        private void InitializeUpgradeTextFields()
        {
            selectedName.text = "";
            selectedDescription.text = "";
            purchaseButton.interactable = false;
        }

        private void RefreshUpgradeTextFields(Upgrade upgrade)
        {
            Debug.Assert(upgrade != null, nameof(upgrade) + " expected to be non-null.");
            Debug.Assert(upgrade.upgradeDescription != null, nameof(upgrade.upgradeDescription) + " expected to be non-null.");
            Debug.Assert(upgrade.upgradeDescription.Length > upgrade.currentRank, "Expected upgrade to have more descriptions.");

            if (upgrade)
            {
                selectedName.text = upgrade.upgradeName;
                if (upgrade.currentRank < upgrade.upgradeDescription.Length)
                {
                    selectedDescription.text = upgrade.upgradeDescription[upgrade.currentRank];
                }
                else
                {
                    Debug.LogError($"{upgrade.name} does not have a valid description or have enough elements.");
                }
                
                if (upgrade.IsMaxed)
                {
                    purchaseButton.interactable = false;
                    purchaseButton.GetComponentInChildren<TMP_Text>().text = maxedDisplayString;
                }
                else
                {
                    purchaseButton.interactable = upgrade.CanAffordNextRank(currentFame);
                    purchaseButton.GetComponentInChildren<TMP_Text>().text = upgrade.GetCostForNextRank().ToString();
                }
            }
            else
            {
                Debug.LogWarning("RefreshUpgradeTextFields called for null upgrade.");
            }
        }

        public void PurchaseUpgrade()
        {
            var upgrade = currentUpgradeSelection.upgrade;
            // Deduct Fame
            currentFame.value -= upgrade.GetCostForNextRank();
            // Rank up Progress
            upgrade.IncreaseRank(allWarriors);

            RefreshButtons();
            RefreshFameTextFields(currentFame);
            RefreshUpgradeTextFields(upgrade);
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