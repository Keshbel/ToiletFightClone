using UnityEngine;

public class SkibidiFactory : MonoBehaviour, ISkibidiFactory
{
    private const string BasicSkibidi = "Prefabs/BasicSkibidi";
    private const string BossSkibidi = "Prefabs/BossSkibidi";
    private EnemyData[] EnemyData => GameSingleton.Instance.enemyData;

    public int currentLevel = 1;

    private GameObject _basicSkibidiPrefab;
    private GameObject _bossSkibidiPrefab;

    public void Load()
    {
        _basicSkibidiPrefab = Resources.Load<GameObject>(BasicSkibidi);
        _bossSkibidiPrefab = Resources.Load<GameObject>(BossSkibidi);
    }

    public void Create(EnemyType enemyType, Vector3 spawnPos)
    {
        GameObject go;
        BasicSkibidi newSkibidi;
        
        switch(enemyType)
        {
            case EnemyType.BasicSkibidi:
                go = Instantiate(_basicSkibidiPrefab, spawnPos, Quaternion.identity);
                newSkibidi = go.GetComponent<BasicSkibidi>();
                FillWithSkibidiParameters(newSkibidi, enemyType);
                break;
            case EnemyType.BossSkibidi:
                go = Instantiate(_bossSkibidiPrefab, spawnPos, Quaternion.identity);
                newSkibidi = go.GetComponent<BasicSkibidi>();
                FillWithSkibidiParameters(newSkibidi, enemyType);
                break;
        }
    }

    private void FillWithSkibidiParameters(BasicSkibidi newSkibidi, EnemyType enemyType)
    {
        var enemyData = GetEnemyData(enemyType.ToString());
        newSkibidi.UpdateData(enemyData);
    }

    public int CountEntities()
    {
        return 0;
    }

    public EnemyData GetEnemyData(string enemyType)
    {
        foreach (var data in EnemyData)
        {
            if (data.name == enemyType + "Data")
            {
                return data;
            }
        }
        Debug.LogError("Enemy data not found: " + enemyType);
        return null;
    }
}
