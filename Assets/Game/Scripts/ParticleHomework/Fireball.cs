using UnityEngine;

[SelectionBase]
public class Fireball : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Transform target;


    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.LookAt(target);
    }

    public void SetTarget(Transform target) => this.target = target;

    private void ActivateExplosionEffect()
    {
        var explosionEffect = ExplosionPool.Instance.GetObject();
        explosionEffect.transform.position = transform.position;
        explosionEffect.gameObject.SetActive(true);
        explosionEffect.Deactivate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Minion") || other.CompareTag("TeamPlayer"))
            ActivateExplosionEffect();
    }
}
