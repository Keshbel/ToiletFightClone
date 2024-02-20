using UnityEngine;

public class EnemyMarker : MonoBehaviour
{
    public EnemyType enemyType;
    public int group;
    public int count;

    private void Awake()
    {
        if (!GameSingleton.Instance.enemyMarkers.Contains(this)) GameSingleton.Instance.enemyMarkers.Add(this);
        
        RandomCount();
    }

    private void RandomCount()
    {
        if (group == 1 && ProjectController.Instance.level % 10 == 0) enemyType = EnemyType.BossSkibidi;

        if (enemyType == EnemyType.BasicSkibidi) count = (int)(Random.Range(1, 2) + ProjectController.Instance.level / 10);
        else count = 1;
    }
}
