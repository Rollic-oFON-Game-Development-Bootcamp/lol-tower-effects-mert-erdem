using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] private Wall wall;
    [SerializeField] private ParticleSystem explosionParticle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wall.Explode(transform.position);
            explosionParticle.Play();
            Destroy(gameObject);
        }
    }
}
