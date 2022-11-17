using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour

{
    public ParticleSystem DieParticle;
    bool Die = false;
    public float count;
    public Transform aim;
    public GameObject bulletPrefab;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame

    int speed = 10; 

    float xMove, yMove;

    void Update()

    {
        xMove = 0;
        yMove = 0;

        if (Input.GetKey(KeyCode.D))

            xMove = speed * Time.deltaTime;

        else if (Input.GetKey(KeyCode.A))

            xMove = -speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))

            yMove = speed * Time.deltaTime;

        else if (Input.GetKey(KeyCode.S))

            yMove = -speed * Time.deltaTime;

        this.transform.Translate(new Vector3(xMove, yMove, 0));

        Vector3 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0;

        aim.position = worldPos;

        if (Input.GetMouseButtonDown(0))
        {

            GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector3 direction = (aim.position - transform.position).normalized;
            Bullet bullet = bulletObj.GetComponent<Bullet>();

            bullet.direction = direction;
            bullet.speed = 20;
            GetComponent<AudioSource>().Play();
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (count == 3)
        {
            Die = true;
        }

        if (other.gameObject.tag.Equals("enemy"))
        {
            count += 1;

            if (Die == true)
            {
                ParticleSystem instance = Instantiate(DieParticle, transform.position, Quaternion.identity);
                instance.Play();
                Destroy(instance.gameObject, instance.main.duration);
                Destroy(this.gameObject);
            }
        }

    }

}
