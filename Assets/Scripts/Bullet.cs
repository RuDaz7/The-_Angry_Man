using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem PowerShoot;
    public Vector3 direction;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ParticleSystem instance = Instantiate(PowerShoot, transform.position, Quaternion.identity);
        instance.Play();
        Destroy(instance.gameObject, instance.main.duration);

        if (other.gameObject.tag.Equals("enemy"))
        {
            Destroy(this.gameObject);
        }
    }

}