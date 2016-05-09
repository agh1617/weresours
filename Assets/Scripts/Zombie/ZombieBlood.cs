using UnityEngine;
using System.Collections;

public class ZombieBlood : MonoBehaviour {

    private Transform zombieTransform;

    private ParticleSystem zombieBlood;

    private bool isBleeding;

    public ParticleSystem bloodParticleSystem;
	
    void Start()
    {
        this.zombieTransform = GetComponent<Transform>();
        this.isBleeding = false;
    }

    public void Bleed()
    {
        if (isBleeding) return;

        this.zombieBlood = (ParticleSystem) Instantiate(
            bloodParticleSystem,
            zombieTransform.position,
            Quaternion.identity
        );

        zombieBlood.transform.parent = zombieTransform;
        this.isBleeding = true;

        zombieBlood.Play();
    }
}
