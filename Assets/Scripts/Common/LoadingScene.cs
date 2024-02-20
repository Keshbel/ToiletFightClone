using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private Image image;

    private void Awake()
    {
        Loading();
    }

    private void Loading()
    {
        image.transform.localScale = Vector3.zero;
        image.transform.DOScale(1, 2f).OnComplete(()=>SceneManager.LoadSceneAsync("Game"));
    }
}
