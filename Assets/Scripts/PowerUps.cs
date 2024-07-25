using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private string typeOfPowerup;
    [SerializeField] private int shieldModifier;
    [SerializeField] private float attackSpeedModifier;
    [SerializeField] private int hpModifier;
    [SerializeField] private float moveSpeedModifier;
    [SerializeField] private int damageModifier;

    public void GetPowerUp(GameObject player)
    {
        Health health = player.GetComponent<Health>();
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
                //ProjectileManager.instance.ApplyDamageModifier(damageModifier);
                break;
        }
    }

    public void PowerUpGathered()
    {
        Destroy(gameObject);
    }
}
