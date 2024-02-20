using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Player Player => GameSingleton.Instance.player;
    private List<EnemyMarker> EnemyMarkers => GameSingleton.Instance.enemyMarkers;
    
    [SerializeField] private SkibidiFactory skibidiFactory;
    
    private int _currentGroup = 0;

    private void OnEnable()
    {
        Player.OnBattleEnd += SpawnEnemiesInGroup;
    }

    private void Awake()
    {
        if (!skibidiFactory) FindObjectOfType<SkibidiFactory>();
        
        skibidiFactory.Load();
        SpawnEnemiesInGroup();
    }

    public async void SpawnEnemiesInGroup()
    {
        await Task.Delay(100);
        
        foreach(EnemyMarker marker in EnemyMarkers)
        {
            if (_currentGroup == marker.group)
            {
                for (int i = 0; i < marker.count; i++)
                {
                    skibidiFactory.Create(marker.enemyType, marker.transform.position);

                    await Task.Delay(100);
                }
            }
        }
        _currentGroup++;
    }
}
