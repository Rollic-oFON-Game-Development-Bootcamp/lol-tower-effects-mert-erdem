using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public void Deactivate()
    {
        StartCoroutine(DeactivateEnumerator());
    }

    private IEnumerator DeactivateEnumerator()
    {
        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }
}
