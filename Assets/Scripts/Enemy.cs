using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4.0f;
    // Start is called before the first frame update
    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;

    void Start()
    {
        transform.position = new Vector3(Random.Range(-8.2f, 8.2f), 8, 0);
        _player = GameObject.Find("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down *_speed* Time.deltaTime);

        if(transform.position.y < -5f)
        {
            float randomx = Random.Range(-8.2f, 8.2f);
            transform.position = new Vector3(randomx, 9f, 0);
        }
      
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if other = player
        //damage player and destroy enemy
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 1f);
        }
        //if other = laser
        else if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.AddScore(10);
                
                
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 1f);
        }
        //destroy laser, then destroy us
    }
}

