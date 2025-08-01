using UnityEngine;
using System;
using System.Diagnostics;
using PersistentData;
using PersistentData.Bosses;
using PersistentData.Warriors;
using Debug = UnityEngine.Debug;
using UnityEngine.SceneManagement;
using TMPro;

namespace Arena
{
    public class ArenaManager : MonoBehaviour
    {
        public GameObject bossPrefab;

        public CombatantGroup allBosses;

        public IntVariable selectedBossIndex;

        public Boss currentBoss;

        public CombatantGroup currentParty;

        public Warrior currentWarrior;

        public GameObject currentWarriorPrefab;

        public Canvas introMessage;

        public int introMessageDisplayTimeInSeconds = 2;

        public Canvas warriorDeathMessage;

        public int warriorDeathMessageDisplayTimeInSeconds = 2;

        public CanvasGroup settingsPanel;
        public CanvasGroup resultsVictory;
        public TMP_Text victoryFame;
        public CanvasGroup resultsDefeat;
        public TMP_Text defeatFame;

        public IntVariable currentFame;
        public IntVariable lifetimeFame;

        public RectTransform bossHealthBarRectTransform;
        public GameObject healthBarPipPrefab;

        private ArenaState _currentState;
        private ArenaState _stateBeforePause;
        private float _unpausedTimeScale;
        private bool _isShowingMessage;
        private bool _isSettingsShown;
        private Stopwatch _messageTimer;
        private TimeSpan _introMessageDisplayTime;
        private TimeSpan _warriorDeathMessageDisplayTime;
        private float _totalPlayerMaxHealth = 0;

        private void Awake()
        {
            Debug.Log("ArenaManager Awake start");
            Debug.Assert(bossPrefab != null, nameof(bossPrefab) + " expected to be not null.");
            Debug.Assert(allBosses != null, nameof(allBosses) + " expected to be not null.");
            Debug.Assert(currentBoss != null, nameof(currentBoss) + " expected to be not null");
            Debug.Assert(selectedBossIndex != null, nameof(selectedBossIndex) + " expected to be not null.");
            Debug.Assert(currentParty != null, nameof(currentParty) + " expected to be not null.");
            Debug.Assert(currentParty.combatants != null,
                nameof(currentParty.combatants) + " expected to be not null.");
            Debug.Assert(currentParty.combatants.Count > 0, "Expected more than 0 warriors in current party");
            Debug.Assert(currentWarrior != null, nameof(currentWarrior) + " expected to be not null.");
            Debug.Assert(currentWarrior.currentHealth != null,
                nameof(currentWarrior.currentHealth) + " expected to be not null.");
            Debug.Assert(currentWarriorPrefab != null, nameof(currentWarriorPrefab) + " expected to be not null.");
            Debug.Assert(introMessage != null, nameof(introMessage) + " expected to be not null.");
            Debug.Assert(warriorDeathMessage != null, nameof(warriorDeathMessage) + " expected to be not null.");

            // Calculates and stores total health in party
            foreach (Combatant combatant in currentParty.combatants)
            {
                _totalPlayerMaxHealth += combatant.MaxHealth;
            }

            InitializeBoss(selectedBossIndex, allBosses);
            SendNextWarriorIntoArena(currentWarrior, currentParty, currentWarriorPrefab);

            _messageTimer = new Stopwatch();
            _introMessageDisplayTime = TimeSpan.FromSeconds(introMessageDisplayTimeInSeconds);
            _warriorDeathMessageDisplayTime = TimeSpan.FromSeconds(warriorDeathMessageDisplayTimeInSeconds);
            _unpausedTimeScale = Time.timeScale;
            _currentState = ArenaState.Awake;

            Debug.Log("ArenaManager Awake end");
        }

        private void Start()
        {
            Debug.Log("ArenaManager Start start");


            Debug.Log("ArenaManager Start end");
        }

        private void InitializeBoss(int bossIndex, CombatantGroup bosses)
        {
            Debug.Log("Initializing boss; boss index: " + bossIndex);
            var bossTemplate = bosses.combatants[bossIndex];
            InitializeCombatant(currentBoss, bossTemplate, bossPrefab, Tags.Boss);
            Debug.Log("Boss: " + currentBoss);

            // install pips
            int numberOfSections = currentBoss.numberOfPhases;
            float width = bossHealthBarRectTransform.rect.width;

            for (int i = 1; i < numberOfSections; i++)
            {
                float normalizedSectionWidth = (float)i / numberOfSections;
                float positionX = -width / 2f + width * normalizedSectionWidth;

                GameObject pip = Instantiate(healthBarPipPrefab, bossHealthBarRectTransform);
                RectTransform pipRectTransform = pip.GetComponent<RectTransform>();
                pipRectTransform.anchoredPosition = new(positionX, 0);
            }
        }

        /// <summary>
        /// Removes a warrior from the part and sets the placeholder to the removed warrior.
        /// </summary>
        /// <param name="currentWarriorPlaceholder"></param>
        /// <param name="party"></param>
        private void SendNextWarriorIntoArena(Warrior currentWarriorPlaceholder, CombatantGroup party,
            GameObject prefab)
        {
            var nextWarrior = PrepareNextWarriorFromParty(party);
            InitializeCombatant(currentWarriorPlaceholder, nextWarrior, prefab, Tags.Player);
        }

        private void InitializeCombatant(Combatant combatantPlaceholder, Combatant template, GameObject prefab,
            string tagForCombatant)
        {
            combatantPlaceholder.ResetValues();
            combatantPlaceholder.SetValues(template);
            var newCombatant = Instantiate(prefab);
            newCombatant.GetComponent<CombatantController>().combatantSprite.sprite = combatantPlaceholder.sprite;
            newCombatant.tag = tagForCombatant;
        }

        private void Update()
        {
            switch (_currentState)
            {
                case ArenaState.Awake:
                    ShowMessage(introMessage, _messageTimer);
                    _currentState = ArenaState.DisplayingIntroMessage;
                    break;
                case ArenaState.DisplayingIntroMessage:
                    if (HideMessageAfterTime(introMessage, _messageTimer, _introMessageDisplayTime))
                    {
                        // TODO: Enable player/boss movement (and disable by default)
                        _currentState = ArenaState.CombatStart;
                    }

                    break;
                case ArenaState.CombatStart:
                    // nothing for now, let combat play out
                    // Player death and boss death are triggered by those events, and OnWarriorDeath and OnBossDeath
                    // are wired up in Unity.
                    break;
                case ArenaState.WarriorDeath:
                    // Warrior gets disabled and left as a corpse in PersistAfterDeath attached to the warrior.
                    if (currentParty.combatants.Count > 0)
                    {
                        ShowMessage(warriorDeathMessage, _messageTimer);
                        _currentState = ArenaState.DisplayingWarriorDeathMessage;
                    }
                    else
                    {
                        _currentState = ArenaState.DisplayingPartyDeathMessage;
                    }

                    break;
                case ArenaState.DisplayingWarriorDeathMessage:
                    if (HideMessageAfterTime(warriorDeathMessage, _messageTimer, _warriorDeathMessageDisplayTime))
                    {
                        // TODO: reset boss position/logic? (not current health)
                        SendNextWarriorIntoArena(currentWarrior, currentParty, currentWarriorPrefab);
                        _currentState = ArenaState.CombatStart;
                    }

                    break;

                case ArenaState.DisplayingPartyDeathMessage:
                    ShowResults(resultsDefeat, defeatFame);
                    _currentState = ArenaState.DisplayingResults;
                    break;

                case ArenaState.BossDeath:
                    _currentState = ArenaState.DisplayingBossDeathMessage;
                    break;

                case ArenaState.DisplayingBossDeathMessage:
                    ShowResults(resultsVictory, victoryFame);
                    _currentState = ArenaState.DisplayingResults;
                    break;

                case ArenaState.DisplayingResults:
                    // Do nothing, player should click a button to go back to title or party select.
                    break;
                case ArenaState.CombatPaused:
                    // Do nothing, wait for unpause and state change.
                    break;
                case ArenaState.Unknown:
                default:
                    Debug.LogError("Unexpected state: " + _currentState);
                    // SceneManager.LoadScene(SceneData.MainMenuSceneIndex);
                    break;
            }
        }

        /// <summary>
        /// Removes and returns the next combatant from the party.
        /// </summary>
        /// <param name="party"></param>
        /// <returns></returns>
        private Combatant PrepareNextWarriorFromParty(CombatantGroup party)
        {
            Debug.Assert(party.combatants.Count > 0);

            var nextWarrior = party.combatants[0];
            party.combatants.RemoveAt(0);
            Debug.Log("Removed warrior; remaining party: " + party);

            return nextWarrior;
        }

        private void ShowMessage(Canvas message, Stopwatch timer)
        {
            Pause();
            _isShowingMessage = true;
            timer.Restart();
            message.enabled = true;
            message.gameObject.SetActive(true);
            Debug.Assert(message.isActiveAndEnabled, "expected message to be active an enabled");
        }

        private void ShowResults(CanvasGroup canvasGroup, TMP_Text fameText)
        {
            // Pause the combat?
            Helper.CanvasHelper.ToggleCanvasGroup(canvasGroup);

            // Calculate Fame Earned Ratios
            float currentPartyTotalRemainingHealth = 0;
            foreach (Combatant combatant in currentParty.combatants)
            {
                currentPartyTotalRemainingHealth += (int)combatant.MaxHealth;
            }

            currentPartyTotalRemainingHealth += (int)currentWarrior.currentHealth;
            float playerRatio = currentPartyTotalRemainingHealth / _totalPlayerMaxHealth;
            playerRatio = playerRatio > 0.0f ? playerRatio : 0.0f;
            // Debug.Log($"Fame earned from remaining Player Health\nRemaining Health: {currentPartyTotalRemainingHealth}, Total Party Health: {_totalPlayerMaxHealth}\nPlayer Ratio: {playerRatio}");

            float bossRatio = currentBoss.currentHealth / currentBoss.MaxHealth;
            bossRatio = bossRatio > 0.0f ? bossRatio : 0.0f;

            int fameEarned = (int)((1f + playerRatio - bossRatio) * currentBoss.fameValue);

            currentFame.value += fameEarned;
            lifetimeFame.value += fameEarned;

            fameText.text = $"{fameEarned}\n{currentFame.value}";
        }

        public void LoadMainMenuScene()
        {
            SceneManager.LoadScene(SceneData.MainMenuSceneIndex);
        }

        /// <summary>
        /// Hides the specified message after the timer has measured the specified timeToShowMessage 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="timer"></param>
        /// <param name="timeToShowMessage"></param>
        /// <returns>
        /// true if the time has elapsed and the message has been hid.
        /// false otherwise
        /// </returns>
        private bool HideMessageAfterTime(Canvas message, Stopwatch timer, TimeSpan timeToShowMessage)
        {
            if (timer.Elapsed > timeToShowMessage)
            {
                timer.Stop();
                message.enabled = false;
                _isShowingMessage = false;
                Unpause();
                return true;
            }

            return false;
        }

        public void OnWarriorDeath()
        {
            Debug.Log("Warrior death.");
            _currentState = ArenaState.WarriorDeath;
        }

        public void OnBossDeath()
        {
            Debug.Log("Boss death.");
            _currentState = ArenaState.BossDeath;
        }


        public void ToggleSettings()
        {
            Debug.Log("Toggled settings.");
            if (_isSettingsShown)
            {
                OnSettingsClosed();
                _isSettingsShown = false;
            }
            else
            {
                _isSettingsShown = true;
                OnSettingsOpened();
            }
        }

        public void OnSettingsOpened()
        {
            Pause();
            Helper.CanvasHelper.ToggleCanvasGroup(settingsPanel);
            if (_isShowingMessage)
            {
                _messageTimer.Stop();
            }
            _stateBeforePause = _currentState;
            _currentState = ArenaState.CombatPaused;
            Debug.Log("Opened settings.");
        }

        public void OnSettingsClosed()
        {
            Debug.Log("Closed settings.");
            Helper.CanvasHelper.ToggleCanvasGroup(settingsPanel);
            _currentState = _stateBeforePause;
            if (_isShowingMessage)
            {
                _messageTimer.Start();
            }
            Unpause();
        }

        private void Pause()
        {
            // Protection against accidental repeated pauses
            if (Time.timeScale != 0f)
            {
                _unpausedTimeScale = Time.timeScale;
                Time.timeScale = 0f;
            }
            else
            {
                Debug.LogWarning("Trying to pause while already paused");
            }
            Debug.Log("Paused; unpausedTimeScale: " + _unpausedTimeScale);
        }

        private void Unpause()
        {
            Debug.Log("Unpausing, setting timeScale to: " + _unpausedTimeScale);
            Time.timeScale = _unpausedTimeScale;
        }

        private enum ArenaState
        {
            Unknown = 0,
            Awake,
            DisplayingIntroMessage,
            CombatStart,
            CombatPaused,
            WarriorDeath,
            DisplayingWarriorDeathMessage,
            DisplayingPartyDeathMessage,
            BossDeath,
            DisplayingBossDeathMessage,
            DisplayingResults
        }
    }
}
