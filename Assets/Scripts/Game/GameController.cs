using System.Threading.Tasks;
using UnityEngine;

[DefaultExecutionOrder(-999)]
public class GameController : MonoBehaviour
{
    private GameSingleton Game => GameSingleton.Instance;
    private ProjectController Project => ProjectController.Instance;

    [SerializeField] private GameControllerView gameControllerView;
    
    private void Awake()
    {
        var player = Instantiate(Game.playerPrefab, Game.startPoint);
        
        Game.player = player;
    }

    private void OnEnable()
    {
        Game.player.OnFinishDestination += WinGame;
        Game.player.playerAttacker.UnitHealth.OnDeath += LoseGame;
    }

    private void OnDisable()
    {
        Game.player.OnFinishDestination -= WinGame;
        Game.player.playerAttacker.UnitHealth.OnDeath -= LoseGame;
    }

    public async void WinGame()
    {
        await Task.Delay(2000);
        
        Project.GiveAward();
        gameControllerView.ShowEndPanel(true);

        await Task.Yield();
        
        Project.NewLevel();
    }

    public async void LoseGame()
    {
        await Task.Delay(2000);
        
        gameControllerView.ShowEndPanel(false);
    }

    public void ResetGame()
    {
        LoadScene("Loading");
    }
    
    public void NextGame()
    {
        LoadScene("Loading");
    }

    public void ReturnInMenu()
    {
        LoadScene("MainMenu");
    }

    private void LoadScene(string sceneName)
    {
        Game.sceneChanger.sceneName = sceneName;
        Game.sceneChanger.LoadSceneAsync();
    }
}
