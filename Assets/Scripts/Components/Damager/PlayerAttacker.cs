using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAttacker : MonoBehaviour, IDamager, IHaveHealth
{
    private Player Player => GameSingleton.Instance.player;
    private bool IsAlive => UnitHealth.IsAlive;
    
    //data
    public int Damage { get; set; }
    public float AttackInterval { get; set; }
    
    //components
    public UnitHealth UnitHealth { get; set; } = new UnitHealth();
    public UnitHealth enemyUnitHealth;

    [Header("States")]
    public bool isBlockInProcess;
    private bool _isAttackAvailable;
    private bool _isAttackInProcess;

    private void OnEnable()
    {
        UnitHealth.OnDeath += Death;
    }

    private void OnDisable()
    {
        UnitHealth.OnDeath -= Death;
    }

    public void StartAttacking() // on fight begin
    {
        Debug.Log("Start Attacking");
        
        _isAttackAvailable = true;
    }

    public void EndAttacking() // on fight end
    {
        _isAttackAvailable = false;
        
        GameSingleton.Instance.player.animatorController.DoRunning();
    }

    public void PerformAttack()
    {
        if (!IsAlive) return;
        
        if (_isAttackAvailable && !_isAttackInProcess && !isBlockInProcess)
        {
            if (enemyUnitHealth == null) return;
            
            GameSingleton.Instance.player.animatorController.DoKick();
            Debug.Log("Player Attacked!");
            StartCoroutine(PerformAttackInterval());
        }
    }

    public void StartBlock(BaseEventData data)
    {
        if (!IsAlive || !Player.isBattle) return;
        
        if(_isAttackAvailable && !isBlockInProcess)
        {
            GameSingleton.Instance.player.animatorController.DoStartBlock();
            Debug.Log("Player Started Blocking!");
            isBlockInProcess = true;
        }
    }

    public void EndBlock(BaseEventData data)
    {
        if (!IsAlive || !Player.isBattle) return;
        
        if (_isAttackAvailable)
        {
            GameSingleton.Instance.player.animatorController.DoStopBlock();
            Debug.Log("Player Stopped Blocking!");
            isBlockInProcess = false;
        }
    }

    private IEnumerator PerformAttackInterval()
    {
        _isAttackInProcess = true;
        yield return new WaitForSeconds(GameSingleton.Instance.player.animatorController.playerAttackClip.length);
        _isAttackInProcess = false;
    }

    private void Death()
    {
        Player.StopMoving();
        Player.animatorController.DoDeath();
        UnitHealth.OnDeath -= Death;
    }
}
