using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TowerStates
{
    SeekTarget,
    AttackMinion,
    AttackEnemy
}

public class LolTower : MonoBehaviour
{
    [SerializeField] private Transform towerTop;

    [ReadOnly] public TowerStates currentState;

    private Minion currentTargetMinion;
    private TeamPlayer currentTargetPlayer;

    private float towerRange => SettingsManager.GameSettings.TowerRange;

    private static Collider[] overlapResults = new Collider[100];

    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private LineRenderer lineRenderer;

    private void OnDrawGizmos()
    {
        var col = Gizmos.color;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, towerRange);

        if (currentTargetMinion != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(towerTop.position, currentTargetMinion.transform.position);
        }

        if (currentTargetPlayer != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(towerTop.position, currentTargetPlayer.transform.position);
        }

        Gizmos.color = col;
    }

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.SetPosition(0, towerTop.position);
        StartCoroutine(RunMachine());
    }

    private IEnumerator GetStateRoutine()
    {
        IEnumerator result = null;
        switch (currentState)
        {
            case TowerStates.SeekTarget:
                result = SeekTarget();
                break;
            case TowerStates.AttackMinion:
                result = AttackMinion();
                break;
            case TowerStates.AttackEnemy:
                result = AttackEnemy();
                break;
            default:
                break;
        }
        return result;
    }

    private IEnumerator RunMachine()
    {
        while (true)
        {
            var stateRoutine = GetStateRoutine();

            yield return stateRoutine;
        }
    }

    private IEnumerator SeekTarget()
    {
        //State Enter

        while (currentState == TowerStates.SeekTarget)
        {
            //State Loop
            var hitCount = Physics.OverlapSphereNonAlloc(transform.position, towerRange, overlapResults, LayerMask.GetMask("Player"));
            var results = overlapResults.Take(hitCount);

            var minions = results.Where(o => o.CompareTag("Minion"))
                .Select(o => o.attachedRigidbody.GetComponent<Minion>());
            
            if (minions.Any(o => !o.IsDead))
            {
                var minion = minions.First(o => !o.IsDead);
                //currentTarget = results.First(o => o.CompareTag("Minion")).transform;
                currentTargetMinion = minion;
                currentState = TowerStates.AttackMinion;
            }
            else if (results.Any(o => o.CompareTag("TeamPlayer")))
            {
                currentTargetPlayer = results.First(o => o.CompareTag("TeamPlayer")).attachedRigidbody.GetComponent<TeamPlayer>();
                currentState = TowerStates.AttackEnemy;
            }

            yield return null;
        }
        //State Exit

    }

    private IEnumerator AttackMinion()
    {
        //State Enter
        float timer = 0f;
        while (currentState == TowerStates.AttackMinion)
        {
            if(currentTargetMinion != null)
               lineRenderer.SetPosition(1, currentTargetMinion.transform.position);

            timer += Time.deltaTime;

            //if (timer >= attackCooldown)
            //{
            //    if (currentTargetMinion.GetHit())
            //    {
            //        currentTargetMinion = null;
            //        currentState = TowerStates.SeekTarget;
            //        continue;
            //    }
            //    timer -= attackCooldown;
            //}

            if (timer >= attackCooldown)
            {
                LaunchFireball(currentTargetMinion.transform);
                timer -= attackCooldown;
            }

            if (currentTargetMinion.IsDead)
            {
                currentTargetMinion = null;
                currentState = TowerStates.SeekTarget;
                continue;
            }

            var sqrDistanceToTarget = (currentTargetMinion.transform.position - transform.position).sqrMagnitude;
            if (sqrDistanceToTarget > towerRange * towerRange)
            {
                currentTargetMinion = null;
                currentState = TowerStates.SeekTarget;
                continue;
            }

            //State Loop
            yield return null;
        }
        //State Exit

    }

    private IEnumerator AttackEnemy()
    {
        //State Enter
        float timer = 0f;
        while (currentState == TowerStates.AttackEnemy)
        {
            if (currentTargetPlayer != null)
                lineRenderer.SetPosition(1, currentTargetPlayer.transform.position);

            //State Loop
            timer += Time.deltaTime;

            //if (timer >= attackCooldown)
            //{
            //    if (currentTargetPlayer.GetHit())
            //    {
            //        currentTargetPlayer = null;
            //        currentState = TowerStates.SeekTarget;
            //        continue;
            //    }
            //    timer -= attackCooldown;
            //}

            if (timer >= attackCooldown)
            {
                LaunchFireball(currentTargetPlayer.transform);
                timer -= attackCooldown;
            }

            var sqrDistanceToTarget = (currentTargetPlayer.transform.position - transform.position).sqrMagnitude;
            if (sqrDistanceToTarget > towerRange * towerRange)
            {
                currentTargetPlayer = null;
                currentState = TowerStates.SeekTarget;
                continue;
            }

            yield return null;
        }
        //State Exit

    }

    private void LaunchFireball(Transform target)
    {
        var fireballClone = FireballPool.Instance.GetObject();
        fireballClone.transform.position = towerTop.position;
        fireballClone.SetTarget(target);
        fireballClone.gameObject.SetActive(true);
    }

    public void Complain(TeamPlayer teamPlayer)
    {
        if (currentState == TowerStates.AttackMinion)
        {
            currentTargetMinion = null;
            currentTargetPlayer = teamPlayer;
            currentState = TowerStates.AttackEnemy;
        }
    }
}
