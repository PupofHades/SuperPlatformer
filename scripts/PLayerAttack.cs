using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerAttack : MonoBehaviour
{
    public Animator animator;
    public Transform AttackPoint;
    public float attackRange;
    public LayerMask Enemylayer;
    public int attackDamage = 40;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Attack();
        }
    }
    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, Enemylayer);

        foreach(Collider2D enemy in hitEnemies)
        {
            
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            
        }
        
        
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }
}
