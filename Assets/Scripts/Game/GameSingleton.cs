using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-9999)]
public class GameSingleton : MonoBehaviour
{
    [Header("Prefabs")]
    public Transform startPoint;
    public Transform finishPoint;
    public Player playerPrefab;

    [Header("Data")]
    [HideInInspector] public Player player;
    public EnemyData[] enemyData; 
    public List<EnemyMarker> enemyMarkers;

    [Header("Components")] 
    public GameController gameController;
    public SceneChanger sceneChanger;
    
    [Header("Musics")] 
    public AudioClip skibidiDeath;

    #region Singleton

    public static GameSingleton Instance;
    
    private void Awake()
    {
        if (Instance) Destroy(gameObject);
        else Instance = this;
    }
    
    #endregion
}
