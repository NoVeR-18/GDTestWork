using UnityEngine;

public class MegaGoblinHealth : EnemyHealth
{
    GameObject smallGoblin;
    public int childernsCount = 2;
    public void Start()
    {
        //smallGoblin = Resources.Load("SmallGoblin") as GameObject;
        //GameManager.Instance.Enemies.Add(this.gameObject.transform);
    }
    public override void Die()
    {

        for (int i = 0; i < childernsCount; i++)
        {
            GameManager.Instance.Enemies.Add(Instantiate(GameManager.Instance.Config.Waves[0].Characters[0].transform, gameObject.transform.position, gameObject.transform.rotation)); ;
        }
        base.Die();
    }
}
