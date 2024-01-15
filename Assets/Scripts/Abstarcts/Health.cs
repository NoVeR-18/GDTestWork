using UnityEngine;

public partial class Health : MonoBehaviour, IDamageable
{
    public float maxHp = 100;
    public float curHp;
    public Animator AnimatorController;
    private void Start()
    {
        if (TryGetComponent<Animator>(out Animator AnimatorController))
        {
            this.AnimatorController = AnimatorController;
        }
        else
        {
            Debug.Log("Health not found 'Animator'");
        }
    }
    public void ApplyDamage(float _damage)
    {
        curHp -= _damage;
        if (curHp <= 0)
            Die();
    }
    public virtual void Die()
    {
        AnimatorController.SetTrigger("Die");
    }
}
