using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;

    int playerId;
    PlayerState playerState;
    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    LineRenderer gunLine;
    float effectsDisplayTime = 0.2f;
    
    void Awake()
    {
        playerState = transform.parent.parent.gameObject.GetComponent<PlayerState>();
        playerId = playerState.playerId;
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;

		if (Input.GetButton("Fire_" + playerId) && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
    }

    void Shoot()
    {
        timer = 0f;

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            ZombieHealth zombieHealth = shootHit.collider.GetComponent<ZombieHealth>();
            if (zombieHealth != null)
            {
                zombieHealth.TakeDamage(playerState, damagePerShot);
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}
