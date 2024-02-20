using UnityEngine;

public class ChasingTrigger : MonoBehaviour
{
    private Player Player => GameSingleton.Instance.player;
    private bool IsChasing => Player.isChasing;
    
    private void OnTriggerEnter(Collider target)
    {
        if (target.CompareTag("Enemy"))
        {
            Enemy currentTarget = target.gameObject.GetComponent<Enemy>();
            if (currentTarget.skibidiAttacker.IsAlive)
            {
                if (!IsChasing) Player.SetChasingStatus(true, currentTarget);
                if (!Player.targetsInChasing.Contains(currentTarget)) Player.targetsInChasing.Add(currentTarget);
            }
        }
    }
}
