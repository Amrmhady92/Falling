using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGust : MonoBehaviour
{
    public float gustSpeed;
    public Vector3 gustDirection;
    public float timeToGustFullStrenght = .25f;
    Vector3 windVelocity;

    public List<GameObject> windParticles = new List<GameObject>();
    private ParticleSystem windParticleSystem;

    private void Start()
    {
        windVelocity = Vector3.zero;
        if (gustDirection == Vector3.zero)
        {
            gustDirection = Vector3.up;
        }
        if (gustDirection.magnitude > 1)
        {
            gustDirection = gustDirection.normalized;
        }
        if ((int)gustSpeed == 0) {
            gustSpeed = 2;
        }

        CreateParticleEffect(transform.position, gustDirection, gustSpeed);
    }

    private void OnTriggerStay(Collider other)
    {
        windVelocity = Vector3.Lerp(windVelocity, gustDirection * gustSpeed, timeToGustFullStrenght *Time.fixedDeltaTime);
        other.GetComponent<Rigidbody>().AddForce(windVelocity, ForceMode.Impulse);
    }

    void CreateParticleEffect(Vector3 spawnPoint, Vector3 direction, float _gustSpeed)
    {
        GameObject newWindParticle = Instantiate(windParticles[Random.Range(0, windParticles.Count)], spawnPoint, Quaternion.LookRotation(direction));
        newWindParticle.transform.eulerAngles = new Vector3(
            newWindParticle.transform.eulerAngles.x,
            newWindParticle.transform.eulerAngles.y - 90,
            newWindParticle.transform.eulerAngles.z);

        windParticleSystem = newWindParticle.GetComponent<ParticleSystem>();
        windParticleSystem.Play();
        var windparticleVelocity = windParticleSystem.velocityOverLifetime;
        windparticleVelocity.speedModifier = (gustSpeed + 2) / 10;

        Destroy(newWindParticle, 5f);

    }
}
