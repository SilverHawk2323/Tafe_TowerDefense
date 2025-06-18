using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    public float attackCooldown = 2f;
    public float currentAttackCooldown;
    public bool canAttack;

    private void Start()
    {
        currentAttackCooldown = attackCooldown;
    }

    private void Update()
    {
        if (!canAttack)
        {
            currentAttackCooldown -= Time.deltaTime;
            if(currentAttackCooldown <= 0)
            {
                canAttack = true;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && canAttack)
        {
            Destroy(other.gameObject);
            canAttack = false;
            currentAttackCooldown = attackCooldown;
        }
    }
}
