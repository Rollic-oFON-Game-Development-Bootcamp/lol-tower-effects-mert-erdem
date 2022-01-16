using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> wallCubes;
    [SerializeField] private float explosionForce;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float upwardsModifier;

    public void Explode(Vector3 explosionPoint)
    {
        foreach (var cube in wallCubes)
        {
            cube.AddExplosionForce(explosionForce, explosionPoint, explosionRadius, upwardsModifier, ForceMode.VelocityChange);
        }
    }
}
