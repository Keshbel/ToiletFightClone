using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected Player Player => GameSingleton.Instance.player;
    public AnimatorController animatorController;
    
    public SkibidiAttacker skibidiAttacker;
}
