using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {
    ParticleSystem explosion;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            explosion = other.GetComponentInChildren<ParticleSystem>();
            explosion.Play();


            Destroy(other, .1f);
            Destroy(other.transform.parent.gameObject, .1f);


        }
    }
}
