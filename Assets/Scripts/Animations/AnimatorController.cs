using System;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Player Player => GameSingleton.Instance.player;
    
    private static readonly int Running = Animator.StringToHash("Running");
    private static readonly int FightingIdle = Animator.StringToHash("FightingIdle");
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Kick = Animator.StringToHash("Kick");
    private static readonly int StartBlock = Animator.StringToHash("StartBlock");
    private static readonly int StopBlock = Animator.StringToHash("StopBlock");
    private static readonly int Dancing = Animator.StringToHash("Dancing");
    private static readonly int Death = Animator.StringToHash("Death");

    [Header("Components")] 
    public SkibidiAttacker skibidiAttacker;
    public Animator animator;

    [Header("Animations")] 
    public AnimationClip playerAttackClip;
    public AnimationClip skibidiAttackClip;
    
    [Header("Audio")] 
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if (!animator) animator = GetComponent<Animator>();
        if (!skibidiAttacker) skibidiAttacker = gameObject.GetComponentInParent<SkibidiAttacker>();
        if (!skibidiAttackClip && skibidiAttacker) skibidiAttackClip = FindAnimation(animator, skibidiAttacker.attackName);
        if (!playerAttackClip && !skibidiAttacker) playerAttackClip = FindAnimation(animator, "Kick");
    }

    private void OnEnable()
    {
        if (playerAttackClip && playerAttackClip.events.Length == 0) playerAttackClip.AddEvent(new AnimationEvent()
        {
            time = playerAttackClip.length/2,
            functionName = "PlayerDealDamage"
        });
        
        if (skibidiAttackClip && skibidiAttackClip.events.Length == 0) skibidiAttackClip.AddEvent(new AnimationEvent()
        {
            time = skibidiAttackClip.length/2,
            functionName = "PlayerApplyDamage"
        });
    }

    private void OnDisable()
    {
        if (skibidiAttackClip) skibidiAttackClip.events = Array.Empty<AnimationEvent>();
        if (playerAttackClip) playerAttackClip.events = Array.Empty<AnimationEvent>();
    }

    public AnimationClip FindAnimation(Animator animatorComponent, string animationName)
    {
        foreach (AnimationClip clip in animatorComponent.runtimeAnimatorController.animationClips)
        {
            if (clip.name == animationName)
            {
                return clip;
            }
        }

        Debug.Log("Clip with name = " + animationName + " not found");
        return null;
    }

    public void DoRunning()
    {
        animator.SetTrigger(Running);
    }

    public void DoFightingIdle()
    {
        animator.SetTrigger(FightingIdle);
    }
    
    public void DoIdle()
    {
        animator.SetTrigger(Idle);
    }
    
    public void DoKick()
    {
        animator.SetTrigger(Kick);
    }
    
    public void DoStartBlock()
    {
        animator.ResetTrigger(StopBlock);
        animator.SetTrigger(StartBlock);
    }
    
    public void DoStopBlock()
    {
        animator.ResetTrigger(StartBlock);
        animator.SetTrigger(StopBlock);
    }
    
    public void DoDeath()
    {
        animator.SetTrigger(Death);
    }
    
    public void DoDancing()
    {
        animator.SetTrigger(Dancing);
    }
    
    public void DoAttack(string attackName)
    {
        animator.SetTrigger(attackName);
    }
    
    //пришлось сюда вынести, т.к анимация только для чтения и нужен скрипт, висящий на объекте с аниматором, чтобы применился метод
    public void PlayerDealDamage() 
    {
        Player.playerAttacker.enemyUnitHealth.ApplyDamage(Player.playerAttacker.Damage);
        audioSource.Play();
    }

    public void PlayerApplyDamage()
    {
        if (!GameSingleton.Instance.player.playerAttacker.isBlockInProcess) Player.playerAttacker.UnitHealth.ApplyDamage(skibidiAttacker.Damage);
        audioSource.Play();
    }
}
