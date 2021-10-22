using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerControl : MonoBehaviour
{
    public AudioSource weaponAudio;
    public GameObject bullet, bullet_position;
    public GameObject ammo,spawnEnemyPos,Enemy;
    
    float moveRotZ = 0;
    Color[] colors = { Color.red, Color.blue, Color.green, Color.yellow };
    int[] ammoSprite = new int[4];
    public float fireTimer = 0,spawnTimer=0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)ammoSprite[i] = Random.Range(0, 4);
        randomAmmo();
    }

    void randomAmmo()
    {
        for (int i = 0; i < 4; i++) ammo.transform.GetChild(i).GetComponent<SpriteRenderer>().color = colors[ammoSprite[i]];
    }

    //Sıradaki mermi Random
    void randomNext()
    {
        for (int i = 0; i < 3; i++) ammoSprite[i] = ammoSprite[i + 1];
        ammoSprite[3] = Random.Range(0, 4);
    }
    void fire()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            //Ateş edildiğinde...
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                weaponAudio.Play();
                GameObject bullet_go = Instantiate(bullet, bullet_position.transform.position, Quaternion.identity);
                bullet_go.GetComponent<SpriteRenderer>().color = ammo.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                randomNext();
                randomAmmo();
                bullet_go.GetComponent<Rigidbody2D>().AddForce(transform.right * 2000);
                fireTimer = 1f;
            }
        }
        moveRotZ += Input.GetAxis("Mouse Y") * Time.deltaTime * 5000;
        transform.localEulerAngles = new Vector3(0, 0, moveRotZ);
    }
    // Update is called once per frame
    void Update()
    {
        fire();
        spawnTimer -= Time.deltaTime;
        if (spawnTimer<0f)
        {
            GameObject enemy_go = Instantiate(Enemy, new Vector3( spawnEnemyPos.transform.position.x,Random.Range(-4, 4)),Quaternion.identity);
            enemy_go.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, 4)];
            spawnTimer = 2f;
        }
    }

    
}
