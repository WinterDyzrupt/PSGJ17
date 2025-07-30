using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersistentData;
using PersistentData.Bosses;
using UnityEngine;

namespace Arena
{
    public class BossController : CombatantController
    {
        public float minimumTimeBetweenActionsInSeconds = .5f;
        public float minimumDistanceForMeleeAttacks = 260f;
        public int currentPhaseNumber = 1;

        private Boss _boss;
        private BossState _bossState;
        /// <summary>
        /// The last type of action (non-waiting state) that the boss took. 
        /// </summary>
        private BossState _previousBossAction;
        private GameObject _player;
        private float _timeOfLastAction;
        private bool _isMoving;
        private Task _attackTask;
        private List<Skill> _meleeSkills = new();
        private List<Skill> _rangedSkills = new();
        private BossPhaseMonitor _bossPhaseMonitor;

        private void Start()
        {
            Debug.Assert(currentCombatant is Boss);
            _boss = currentCombatant as Boss;

            _bossPhaseMonitor = GetComponent<BossPhaseMonitor>();
            Debug.Assert(_bossPhaseMonitor, $"Expected {nameof(_bossPhaseMonitor)} to be found.");
            currentPhaseNumber = _bossPhaseMonitor.currentPhaseNumber;

            Debug.Assert((_boss.allSkillsForAllPhases?.Count ?? 0) > 0,
                "Expected boss skills to have at least one phase worth of skills.");
            PopulateSkillsForPhase(currentPhaseNumber);

            _timeOfLastAction = Time.time;
            _bossState = BossState.Thinking;
        }

        private void PopulateSkillsForPhase(int phaseNumber)
        {
            var skills = new List<Skill>();
            if ((_boss?.allSkillsForAllPhases?.Count ?? 0) >= phaseNumber)
            {
                var skillsForPhase = _boss.allSkillsForAllPhases
                    .FirstOrDefault(skillSet => skillSet.phaseNumber == phaseNumber);
                Debug.Log($"SkillSet for phase {phaseNumber}: {skillsForPhase}");
                if (skillsForPhase != null)
                {
                    skills = skillsForPhase.skillSet;
                    _meleeSkills = new List<Skill>();
                    _rangedSkills = new List<Skill>();
                    foreach (var skill in skills)
                    {
                        if (skill.isRanged)
                        {
                            _rangedSkills.Add(skill);
                        }
                        else
                        {
                            _meleeSkills.Add(skill);
                        }
                    }
                }
            }

            Debug.Assert(skills.Count > 0, $"Expected skills for {nameof(phaseNumber)}: {phaseNumber} to be populated.");
        }

        protected override void FixedUpdate()
        {
            if (_player == null)
            {
                _player = FindNewPlayerObject();
            }
            else
            {
                if (_isMoving)
                {
                    base.FixedUpdate();
                }

                if (Time.time - _timeOfLastAction > minimumTimeBetweenActionsInSeconds)
                {
                    _timeOfLastAction = Time.time;
                    _isMoving = false;
                    switch (_bossState)
                    {
                        case BossState.Thinking:
                            Debug.Log("Thinking...");
                            // Because the boss spends some time thinking, it needs to do another turn-towards-
                            // player here for its combat to feel more responsive.  Note: it's important to not
                            // turn once preparation has started, since the turn will cause the warning-box to
                            // not line up with the attack-box
                            UpdateAimDirection(_player.transform.position - transform.position);

                            var distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
                            if (distanceToPlayer > minimumDistanceForMeleeAttacks)
                            {
                                if (IsAnySkillReadyToExecute(_rangedSkills))
                                {
                                    // Alternate between ranged attack and moving closer if there are melee attacks.
                                    if (IsAnySkillReadyToExecute(_meleeSkills) && _previousBossAction == BossState.StartingRangedAttack)
                                    {
                                        Debug.Log("Out of melee range, and just did range attack; must get closer...");
                                        _bossState = BossState.MovingTowardsPlayer;
                                    }
                                    else
                                    {
                                        Debug.Log("Out of melee range, but I have a ranged attack...");
                                        _bossState = BossState.StartingRangedAttack;
                                    }
                                }
                                else
                                {
                                    Debug.Log("Out of melee range, and no ranged skills; must get closer...");
                                    _bossState = BossState.MovingTowardsPlayer;
                                }
                            }
                            else if (IsAnySkillReadyToExecute(_meleeSkills))
                            {
                                Debug.Log("Close enough to attack...");
                                _bossState = BossState.StartingMeleeAttack;
                            }
                            else
                            {
                                Debug.Log("Close enough, no skills ready.");
                                _bossState = BossState.MovingTowardsPlayer;
                            }

                            _previousBossAction = BossState.Thinking;

                            break;
                        case BossState.StartingRangedAttack:
                            Debug.Log("Preparing ranged attack");
                            ExecuteSkillOffCooldown(_rangedSkills);

                            _previousBossAction = BossState.StartingRangedAttack;

                            break;
                        case BossState.MovingTowardsPlayer:
                            Debug.Log("Moving towards player");

                            UpdateMoveDirection((_player.transform.position - transform.position));
                            UpdateAimDirection(_player.transform.position - transform.position);
                            _isMoving = true;
                            _bossState = BossState.Thinking;

                            _previousBossAction = BossState.MovingTowardsPlayer;
                            break;
                        case BossState.StartingMeleeAttack:
                            Debug.Log("Preparing to attack");
                            ExecuteSkillOffCooldown(_meleeSkills);

                            _previousBossAction = BossState.StartingMeleeAttack;
                            break;
                        case BossState.WaitingForAttackToComplete:
                            // if (_attackTask.IsCompleted)
                            // {
                            Debug.Log("Done waiting for attack to complete");
                            _bossState = BossState.Thinking;
                            // }
                            // else
                            // {
                            //  Debug.Log("...Still waiting for attack to complete...");
                            // }

                            break;
                        default:
                            Debug.LogWarning("Unexpected boss state: " + _bossState);
                            break;
                    }
                }
                else
                {
                    //Debug.Log($"Boss doing nothing; {Time.time} - {_timeOfLastAction} > {minimumTimeBetweenActionsInSeconds}");
                }
            }
        }

        private bool IsAnySkillReadyToExecute(IEnumerable<Skill> skills)
        {
            return skills.Any(skill => skill.IsOffCooldown);
        }

        private void ExecuteSkillOffCooldown(IEnumerable<Skill> skills)
        {
            var skillToUse = skills.FirstOrDefault(skill => skill.IsOffCooldown);
            if (skillToUse != null)
            {
                Debug.Log($"Executing skill: {skillToUse.displayName}.");
                StartCoroutine(ExecuteSkillAsync(skillToUse));
                _bossState = BossState.WaitingForAttackToComplete;
            }
            else
            {
                Debug.Log("No skill off cooldown...");
                _bossState = BossState.Thinking;
            }
        }

        public void OnPlayerDeath()
        {
            _player = null;
        }

        public void OnPhaseChange()
        {
            Debug.Log($"OnPhaseChange {currentPhaseNumber} -> {_bossPhaseMonitor.currentPhaseNumber}");
            currentPhaseNumber = _bossPhaseMonitor.currentPhaseNumber;
            PopulateSkillsForPhase(currentPhaseNumber);
        }

        private GameObject FindNewPlayerObject()
        {
            return GameObject.FindWithTag(Tags.Player);
        }

        private enum BossState
        {
            MovingTowardsPlayer,
            StartingRangedAttack,
            StartingMeleeAttack,
            WaitingForAttackToComplete,
            Thinking
        }
    }
}
