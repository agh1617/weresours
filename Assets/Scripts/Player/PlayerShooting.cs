using UnityEngine;
using System;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float startTimeBetweenBullets = 0.35f;
    public float minTimeBetweenBullets = 0.03f;
    public float range = 100f;

    public Transform bulletTrailPrefab;
    public Transform muzzleFlashPrefabl;

    int playerId;
    PlayerState playerState;
    PlayerHealth playerHealth;
    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    float effectsDisplayTime = 0.1f;
    AudioSource gunAudio;
    Animator animator;
    float timeBetweenBullets;

    public void DoubleTap(float duration)
    {
        timeBetweenBullets = Math.Max(timeBetweenBullets / 2, minTimeBetweenBullets);

        CancelInvoke("ResetShooting");
        Invoke("ResetShooting", duration);
    }

    void Awake()
    {
        playerState = transform.parent.parent.gameObject.GetComponent<PlayerState>();
        playerHealth = transform.parent.parent.gameObject.GetComponent<PlayerHealth>();
        animator = GetComponentInChildren<Animator>();

        playerId = playerState.playerId;
        shootableMask = LayerMask.GetMask("Shootable");
        gunAudio = GetComponent<AudioSource>();
        timeBetweenBullets = startTimeBetweenBullets;
    }

    void Update()
    {
        timer += Time.deltaTime;

		if (playerHealth.currentHealth > 0 && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            if (Input.GetButton("Fire_" + playerId) || Input.GetAxis("Fire_" + playerId) > 0 || (playerId == 1 && Input.GetButton("Fire1")))
               Shoot();
        }
    }

    void ResetShooting()
    {
        timeBetweenBullets = startTimeBetweenBullets;
    }

    void Shoot()
    {
        Effect();
        // animator.Play("Shoot");

        timer = 0f;

        gunAudio.Play();

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            ZombieHealth zombieHealth = shootHit.collider.GetComponent<ZombieHealth>();
            if (zombieHealth != null)
            {
                zombieHealth.TakeDamage(playerState, damagePerShot);
            }
        }
    }

    void Effect()
    {
        BulletTrail();
        MuzzleFlash();
    }

    void BulletTrail()
    {
        Instantiate(bulletTrailPrefab, transform.position, transform.rotation);
    }

    void MuzzleFlash()
    {

    }
}
