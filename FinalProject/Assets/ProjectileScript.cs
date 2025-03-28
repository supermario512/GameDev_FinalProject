using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float lifetime = 5f;
    public float atk = 10f;
    protected Vector3 spawnPosition;
    protected Rigidbody rb;

    public void Start()
    {
        spawnPosition = transform.position;
        spawnPosition.y += .5f;
        transform.position = spawnPosition;

        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifetime); // Automatically destroy after lifetime
    }

    public void Update()
    {
        
    }

    public virtual void MoveProjectile()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Weapon"))
        {
            Destroy(gameObject);
        }
    }
}
