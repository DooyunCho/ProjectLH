using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionLog : MonoBehaviour, IConsumable
{
    public void Consume(Player player)
    {
        player.recoveryHealth(10);
        Destroy(gameObject);
    }

    public void Consume(Player player, CharacterStats stats)
    {
        // do nothing
        Destroy(gameObject);
    }
}
