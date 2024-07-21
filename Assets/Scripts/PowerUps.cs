using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] string typeOfPowerup;
    [SerializeField] int shieldModifier;
    [SerializeField] float attackSpeedModifier;
    [SerializeField] int hpModifier;
    [SerializeField] float moveSpeedModifier;
    [SerializeField] int damageModifier;
   
   public void GetPowerUp(GameObject player)
   {
        Health health = player.GetComponent<Health>();
        GameObject projectilePlayer = GameObject.FindWithTag("Playerbullet");
        DamageDealer damageDealer = projectilePlayer.GetComponent<DamageDealer>();
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        Shooter shooter = player.GetComponent<Shooter>();

        switch (typeOfPowerup)
        {
            case "Shield":
            health.AddShield(shieldModifier);
            break;

            case "Attackspeed":
            shooter.AddAttackSpeed(attackSpeedModifier);
            
            break;

            case "Health":
            health.AddHealth(hpModifier);
            
            break;

            case "Movespeed":
            playerMovement.AddPlayerMovement(moveSpeedModifier);
            
            break;

            case "Damage":
            damageDealer.AddDamage(damageModifier);
            
            break;
        }
   }

   public void PowerUpGathered()
   {
    Destroy(gameObject);
   }
}
