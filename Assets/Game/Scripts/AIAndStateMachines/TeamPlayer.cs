using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class TeamPlayer : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyPlayer"))
        {
            var enemy = other.attachedRigidbody.GetComponent<EnemyPlayer>();
            enemy.GetHitByEnemy(this);
        }

        if(other.CompareTag("Fireball"))
        {
            other.gameObject.SetActive(false);
            GetHit();
        }
    }

    public bool GetHit(int damage = 1)
    {
        return false;
    }
}
