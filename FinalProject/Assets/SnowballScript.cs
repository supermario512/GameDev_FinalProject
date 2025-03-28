using UnityEngine;

public class SnowballScript : ProjectileScript
{
    public float maxDistance = 160f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        MoveProjectile();
        
        if (Vector3.Distance(spawnPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    public override void  MoveProjectile()
    {
        rb.linearVelocity = transform.forward * 40f;
    }
}
