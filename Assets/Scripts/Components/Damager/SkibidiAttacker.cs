using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkibidiAttacker : MonoBehaviour, IDamager, IHaveHealth
{
    public bool IsAlive => UnitHealth.IsAlive;
    
    private Enemy _skibidi;
    
    public int Damage { get; set; }

    public float AttackInterval { get; set; }
    public UnitHealth UnitHealth { get; set; } = new UnitHealth();

    private bool _isAttackAvailable;
    private bool _isAttackInProcess;

    public string attackName;
    public AudioSource musicSource;

    private void Awake()
    {
        if (!_skibidi) _skibidi = GetComponent<Enemy>();
        
        attackName = "Attack" + Random.Range(1, 3);
    }

    private void OnEnable()
    {
        UnitHealth.OnDeath += Death;
    }

    private void OnDisable()
    {
        UnitHealth.OnDeath -= Death;
    }

    public void Update()
    {
        if (_isAttackAvailable && !_isAttackInProcess)
        {
            PerformAttack();
        }
    }

    public void StartAttacking()
    {
        _skibidi.animatorController.DoIdle();
        _isAttackAvailable = true;
    }

    public void EndAttacking()
    {
        _isAttackAvailable = false;
    }

    public void PerformAttack()
    {
        if (_isAttackAvailable && !_isAttackInProcess && IsAlive)
        {
            _skibidi.animatorController.DoAttack(attackName);
            Debug.Log("Skibidi Attacked!");
            StartCoroutine(PerformAttackInterval());
        }
    }

    private IEnumerator PerformAttackInterval()
    {
        _isAttackInProcess = true;
        yield return new WaitForSeconds(_skibidi.animatorController.skibidiAttackClip.length + 2f);
        _isAttackInProcess = false;
    }

    private void Death()
    {
        _skibidi.animatorController.DoDeath();
        musicSource.Stop();
        musicSource.PlayOneShot(GameSingleton.Instance.skibidiDeath);
    }
}
