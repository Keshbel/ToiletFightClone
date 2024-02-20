using UnityEngine;

public interface ISkibidiFactory
{
    void Load();
    void Create(EnemyType enemyType, Vector3 spawnPos);
}