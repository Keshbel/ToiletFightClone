using UnityEngine;

public class BattleTrigger : MonoBehaviour
{
    private Player Player => GameSingleton.Instance.player;
    private bool IsBattle => Player.isBattle;

    private void OnTriggerEnter(Collider target)
    {
        if (target.CompareTag("Finish")) Player.OnPlayerFinishDestination();
        
        if (!target.CompareTag("Enemy")) return;
        
        Enemy currentTarget = target.gameObject.GetComponent<Enemy>();
        if (!currentTarget.skibidiAttacker.IsAlive) return;
            
        if (IsBattle) Player.AddEnemyToBattle(currentTarget); 
        else Player.SetBattleStatus(true, currentTarget);
    }
}
