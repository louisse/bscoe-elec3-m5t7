using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {
    [SerializeField] float speed = 15f;
    [SerializeField] float xRange = 9f;
    [SerializeField] float yRange = 3f;

    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float controlRollFactor = -20f;
    public List<ParticleCollisionEvent> collisionEvents;

    ParticleSystem explosion;
    ParticleSystem bullet;
    float xThrow, yThrow;

    // Use this for initialization
    void Start()
    {
        explosion = GetComponentInChildren<ParticleSystem>();
        bullet = GetComponentInChildren<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        bullet.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        HVMovement();
        RotationalMovements();
    }

    private void RotationalMovements()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void HVMovement()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * speed * Time.deltaTime;
        float yOffset = yThrow * speed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void OnTriggerEnter(Collider collider) {
        explosion.Play();
        Invoke("DestroyThenRestart", 0.05f);
    }

    private void DestroyThenRestart() {
        Destroy(gameObject);
        Application.LoadLevel(1);
    }
    private void OnParticleCollision(GameObject other) {
        print("OnParticleCollision");
        int numCollisionEvents = bullet.GetCollisionEvents(other, collisionEvents);

        Rigidbody rb = other.GetComponent<Rigidbody>();
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (rb)
            {
                Vector3 pos = collisionEvents[i].intersection;
                Vector3 force = collisionEvents[i].velocity * 10;
                rb.AddForce(force);
            }
            i++;
        }
    }
}
