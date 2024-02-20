using DG.Tweening;
using UnityEngine;

public class ScalingAnimation : MonoBehaviour
{
    public GameObject target;

    [Header("Options")] 
    public float duration = 1f;
    public float maxScale;
    public float minScale;

    private void Awake()
    {
        ScalingIn();
    }

    private void ScalingIn()
    {
        target.transform.DOScale(minScale, duration).OnComplete(ScalingOut).SetEase(Ease.Linear);
    }

    private void ScalingOut()
    {
        target.transform.DOScale(maxScale, duration).OnComplete(ScalingIn).SetEase(Ease.Linear);
    }
}
