using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public event Action OnBattleBegan;
    public event Action OnBattleEnd;
    public event Action OnFinishDestination;
    
    private Transform FinishPoint => GameSingleton.Instance.finishPoint;
    
    [Header("Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Moveable moveable;
    public PlayerAttacker playerAttacker;
    public AnimatorController animatorController;

    [Header("Fight")]
    public bool isChasing;
    public bool isBattle;
    public List<Enemy> targetsInBattle = new List<Enemy>();
    public List<Enemy> targetsInChasing = new List<Enemy>();
    public Enemy currentTarget;

    [Header("Finish")] 
    private bool _isFinishDestination;

    private void Awake()
    {
        if (!animatorController) animatorController = gameObject.GetComponentInChildren<AnimatorController>();
        
        UpdateData();
    }

    private void Start()
    {
        moveable.Agent = agent;
        SetPlayerToFinish();
    }

    private void OnEnable()
    {
        OnBattleBegan += playerAttacker.StartAttacking;
        OnBattleBegan += moveable.StopChasing;

        OnBattleEnd += playerAttacker.EndAttacking;
        OnBattleEnd += SetPlayerToFinish;

        OnFinishDestination += FinishDestination;
    }

    private void OnDisable()
    {
        OnBattleBegan -= playerAttacker.StartAttacking;
        OnBattleBegan -= moveable.StopChasing;

        OnBattleEnd -= playerAttacker.EndAttacking;
        OnBattleEnd -= SetPlayerToFinish;

        OnFinishDestination -= FinishDestination;
    }

    private void Update()
    {
        CheckEnemyHealth();
        if (isChasing)
        {
            moveable.ChaseTarget(currentTarget.transform);
            playerAttacker.enemyUnitHealth = currentTarget.skibidiAttacker.UnitHealth;
        }
    }

    private async void SetPlayerToFinish()
    {
        isChasing = false;
        isBattle = false;
        
        moveable.RunningToFinish();
        //moveable.ChaseTarget(FinishPoint);
        agent.SetDestination(FinishPoint.position);
        animatorController.DoRunning();
        
        await Task.Yield();
        
        moveable.RunningToFinish();
    }

    public void StopMoving()
    {
        moveable.StopChasing();
    }

    public void OnPlayerFinishDestination()
    {
        if (!_isFinishDestination) OnFinishDestination?.Invoke();
    }
    
    private void FinishDestination()
    {
        _isFinishDestination = true;
        moveable.StopChasing();
        animatorController.DoDancing();
    }

    private void UpdateData()
    {
        playerAttacker.Damage = (int)ProjectController.Instance.damage;
        playerAttacker.UnitHealth.MaxHealth = (int)ProjectController.Instance.health;
        playerAttacker.UnitHealth.Health = (int)ProjectController.Instance.health;
    }

    #region Trigger Checks

    public void SetChasingStatus(bool value, Enemy target = null)
    {
        isChasing = value;
        currentTarget = target;
    }

    public void SetBattleStatus(bool value, Enemy target = null)
    {
        isBattle = value;
        
        if (value)
        {
            AddEnemyToBattle(target);
            
            currentTarget = targetsInBattle.ElementAt(0);
            OnBattleBegan?.Invoke();
            animatorController.DoFightingIdle();
        }
        else targetsInBattle.Remove(target);
    }
    
    public void CheckEnemyHealth()
    {
        isChasing = targetsInChasing.Any(target => target.skibidiAttacker.IsAlive);
        
        if (!isBattle) return;

        if (currentTarget && !currentTarget.skibidiAttacker.UnitHealth.IsAlive)
        {
            targetsInBattle.Remove(currentTarget);
            targetsInChasing.Remove(currentTarget);

            if (targetsInBattle.Count > 0)
            {
                var nearestEnemy = targetsInBattle.OrderBy(target => Vector3.Distance(target.transform.position, transform.position)).First(target => target.skibidiAttacker.IsAlive);
                SetChasingStatus(true, nearestEnemy);
            }
            else
            {
                OnBattleEnd?.Invoke();
            }
        }
    }

    public void AddEnemyToBattle(Enemy target)
    {
        if (!targetsInBattle.Contains(target))
        {
            isChasing = true;
            targetsInBattle.Add(target);
        }
    }

    #endregion
}
