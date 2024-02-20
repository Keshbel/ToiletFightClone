using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class BasicSkibidi : Enemy
{
    private ProjectController Project => ProjectController.Instance;
    
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected Moveable moveable;

    private void Awake()
    {
        if (!animatorController) gameObject.GetComponentInChildren<AnimatorController>();
    }

    public void Start()
    {
        moveable.Agent = agent;
        moveable.ChaseTarget(Player.transform);
        animatorController.DoRunning();
        
        CheckDistanceAsync();
    }

    public void UpdateData(EnemyData enemyData)
    {
        var modifier = (int)(Project.level * Random.Range(1,3));
        
        skibidiAttacker.Damage = enemyData.damage + modifier;
        skibidiAttacker.UnitHealth.MaxHealth = enemyData.health + modifier;
        skibidiAttacker.UnitHealth.Health = enemyData.health + modifier;
        agent.speed = enemyData.speed;
    }

    public async void CheckDistanceAsync()
    {
        await Task.Delay(100);
        
        while (this != null && !agent.isStopped) //battle begun
        {
            if (agent.remainingDistance < 1f)
            {
                skibidiAttacker.StartAttacking();
                moveable.StopChasing();
            }

            await Task.Delay(100);
        }
    }
}
