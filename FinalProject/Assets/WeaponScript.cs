using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject weaponPrefab;
    public Transform firePoint;
    public float fireSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.J))
        {
            FireWeapon();
        }
    }

    public void FireWeapon()
    {
        Vector3 fireDirection = firePoint.forward;
        float angleOffset = 0f;
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) angleOffset = -45f;  // Shoot upward
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) angleOffset = 45f; // Shoot downward

        fireDirection = Quaternion.AngleAxis(angleOffset, firePoint.right) * fireDirection;


        // Spawns Projectile, and lets the dedicated scripts handle the rest
        GameObject projectile = Instantiate(weaponPrefab, firePoint.position, Quaternion.LookRotation(fireDirection));
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        ProjectileScript ps = projectile.GetComponent<ProjectileScript>();

    }

    public void SelectWeapon()
    {

    }
}
