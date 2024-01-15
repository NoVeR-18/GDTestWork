using UnityEngine;
using UnityEngine.AI;

public class EnemyController : OverlapAttack
{
    public float Damage;
    public float AtackSpeed;
    public float AttackRange = 2;

    public Animator AnimatorController;
    public NavMeshAgent Agent;

    private float lastAttackTime = 0;
    private void Start()
    {
        Agent.SetDestination(GameManager.Instance.Player.transform.position);
    }

    private void Update()
    {
        var distance = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position);

        if (distance <= AttackRange)
        {
            //Agent.isStopped = true;
            if (Time.time - lastAttackTime > AtackSpeed)
            {
                lastAttackTime = Time.time;
                AnimatorController.SetTrigger("Attack");
                PerformAttack(Damage);
            }
        }
        else
        {
            Agent.SetDestination(GameManager.Instance.Player.transform.position);
        }
        AnimatorController.SetFloat("Speed", Agent.speed);
    }

}
