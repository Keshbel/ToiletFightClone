using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData", menuName = "EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public GameObject enemyPrefab;

    public int health;

    public float speed;

    public int damage;
}
