public partial class EnemyHealth : Health
{
    public int healtToBuff = 2;
    public override void Die()
    {
        base.Die();
        if (GameManager.Instance.Player.TryGetComponent(out IHealeble damageable) == true)
        {
            damageable.ApplyHealing(healtToBuff);
        }

        GameManager.Instance.Enemies.Remove(this.transform);
        GameManager.Instance.RemoveEnemie();


        gameObject.SetActive(false);
    }
}
