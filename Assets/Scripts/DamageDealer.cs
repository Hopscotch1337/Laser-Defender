using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 10;
        public int GetDamage()
    {
        Debug.Log(damage + " done");
        return damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
    public void AddDamage(int addDamage)
    {
        damage += addDamage;
        Debug.Log(damage);
    }



    

}
