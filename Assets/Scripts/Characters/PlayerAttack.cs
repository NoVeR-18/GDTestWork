using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : OverlapAttack
{
    [Header("Common")]
    public float Damage = 10f;
    public float AtackSpeed = 1f;
    public float AttackRange = 2f;
    public float doubleAttackCooldown = 2f;
    private float lastAttackTime = 0;
    private float lastDoubleAttackTime = 0;

    public Animator AnimatorController;
    [Header("UIButtons")]
    public Button AttackButton;
    public Button DoubleAttackButton;

    void Start()
    {
        if (TryGetComponent<Animator>(out Animator AnimatorController))
        {
            this.AnimatorController = AnimatorController;
        }
        else
        {
            Debug.Log("PlayerAttack not found 'Animator'");
        }
        AttackButton.onClick.AddListener(Attack);
        DoubleAttackButton.onClick.AddListener(DoubleAttact);
        AttackButton.interactable = false;
        DoubleAttackButton.interactable = false;
    }

    private void Update()
    {
        SkillUIUpdate();
    }
    void SkillUIUpdate()
    {
        AttackButton.image.fillAmount = Mathf.Min(1, (Time.time - lastAttackTime) / AtackSpeed);
        if ((Time.time - lastAttackTime) > AtackSpeed)
        {
            AttackButton.interactable = true;
        }
        DoubleAttackButton.image.fillAmount = Mathf.Min(1, (Time.time - lastDoubleAttackTime) / doubleAttackCooldown);
        if ((Time.time - lastDoubleAttackTime) > doubleAttackCooldown)
        {

            if (TryFindEnemies())
                DoubleAttackButton.interactable = true;
        }
    }
    public void Attack()
    {
        AttackButton.interactable = false;
        lastAttackTime = Time.time;
        AnimatorController.SetTrigger("Attack");
        PerformAttack(Damage);
    }
    public void DoubleAttact()
    {
        DoubleAttackButton.interactable = false;
        lastDoubleAttackTime = Time.time;
        AnimatorController.SetTrigger("DoubleAttack");
        PerformAttack(Damage * 2);
    }


}
