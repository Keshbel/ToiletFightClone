using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private Player Player => GameSingleton.Instance.player;
    
    [SerializeField] private CinemachineVirtualCamera virtualCamera1;
    [SerializeField] private CinemachineVirtualCamera virtualCamera2;
    [SerializeField] private CinemachineTargetGroup targetGroup;
    
    private CinemachineTargetGroup.Target _target;

    private void OnEnable()
    {
        Player.OnBattleBegan += StartFilmingBattle;
        Player.OnBattleEnd += StopFilmingBattle;
    }

    private void OnDisable()
    {
        Player.OnBattleBegan -= StartFilmingBattle;
        Player.OnBattleEnd -= StopFilmingBattle;
    }

    private void Start()
    {
        var playerTransform = Player.transform;
        virtualCamera1.Follow = playerTransform;
        virtualCamera1.LookAt = playerTransform;

        AddNewTarget(playerTransform, 0);
    }

    private void StartFilmingBattle()
    {
        virtualCamera2.Priority += 2;

        AddNewTarget(Player.currentTarget.transform, 1);
    }

    private void StopFilmingBattle()
    {
        virtualCamera2.Priority -= 2;
    }

    private void AddNewTarget(Transform target, int number)
    {
        _target.target = target;
        _target.weight = number == 0 ? 10 : 1;
        targetGroup.m_Targets.SetValue(_target, number);
    }

}
