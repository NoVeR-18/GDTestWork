using System;

public class PlayerHealth : Health, IHealeble
{
    public void ApplyHealing(float _health)
    {
        if (curHp < maxHp)
        {
            curHp = MathF.Min(curHp + _health, maxHp);
        }
    }

    public override void Die()
    {
        base.Die();
        GameManager.Instance.GameOver();
    }
}
