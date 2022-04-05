using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public int maxHealth = 10;
    public int CurrentHealth;

   void Start() 
    {
        CurrentHealth = maxHealth;  
        
    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        
        

        if(CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
     void Die() {
        {
            Debug.Log("dewath");
        }
    }
}
