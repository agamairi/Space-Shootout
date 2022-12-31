using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    private float _speed = 5f;
    private float _speedMultiplier = 2; 
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private float _firerate = 0.15f;
    [SerializeField]
    private float _canfire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    [SerializeField]
    private bool _isShieldsActive = false;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private int _score;
    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _audioSource;
    private GameManager _gameManager;
    private UiManager _uiManager;
    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _laserSoundClip;

        if(_gameManager.isCoopmmode != true){
        transform.position = new Vector3(0,-1,0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerOne == true){
            PlayerOneMovement();
             if ((Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire)&& isPlayerOne == true){
                 Lasershoot();
             }
        }
        if(isPlayerTwo == true){
            PlayerTwoMove();
           if(Input.GetKeyDown(KeyCode.KeypadEnter))
           {
                Lasershoot();
            }
        }
    }
    private void PlayerOneMovement()
    {
        /*float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);*/
         if(Input.GetKey(KeyCode.W)){
            transform.Translate(Vector3.up *_speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.S)){
            transform.Translate(Vector3.down *_speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D)){
            transform.Translate(Vector3.right *_speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.A)){
            transform.Translate(Vector3.left *_speed * Time.deltaTime);
        }
        
        //Vertical Barrier
        if (transform.position.y >= 1)
          {
                transform.position = new Vector3(transform.position.x, 1, 0);
          }
        else if (transform.position.y <= -3.2f)
        {
          transform.position = new Vector3(transform.position.x, -3.2f, 0);
        }
        //Horizontal teleport
        if (transform.position.x > 8.3f)
        {
            transform.position = new Vector3(-8.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -8.3f)
        {
            transform.position = new Vector3(8.3f, transform.position.y, 0);
        }
    }
    void Lasershoot()
    {
        //space spawns the lazer
        _canfire = Time.time + _firerate;

        if(_isTripleShotActive == true)
        {
            //instantiate triple shot
            Instantiate(_tripleShotPrefab, transform.position , Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        }

        _audioSource.Play();
    }

    public void Damage()
    {
        if(_isShieldsActive == true)
        {
            _isShieldsActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }


        _lives -= 1;
        _uiManager.UpdateLives(_lives);

        if(_lives == 2)
        {
            _leftEngine.SetActive(true);
        }
        else if(_lives == 1)
        {
            _rightEngine.SetActive(true);
        }

        if (_lives <= 0){
            //tell spawn manager you ded lol
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
    public void TripleShotActive()
    {
        //tripleshot active = true
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
//IEnumerator to end the duration
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }
    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }
    public void ShieldsActive()
    {
        _isShieldsActive = true;
        _shieldVisualizer.SetActive(true);
    }
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
      private  void PlayerTwoMove()
    {
        if(Input.GetKey(KeyCode.Keypad8)){
            transform.Translate(Vector3.up *_speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.Keypad2)){
            transform.Translate(Vector3.down *_speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.Keypad6)){
            transform.Translate(Vector3.right *_speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.Keypad4)){
            transform.Translate(Vector3.left *_speed * Time.deltaTime);
        }
        //vertical barrier
         if (transform.position.y >= 1)
          {
                transform.position = new Vector3(transform.position.x, 1, 0);
          }
        else if (transform.position.y <= -3.2f)
        {
          transform.position = new Vector3(transform.position.x, -3.2f, 0);
        }
        //horizontal teleport
        if (transform.position.x > 8.3f)
        {
            transform.position = new Vector3(-8.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -8.3f)
        {
            transform.position = new Vector3(8.3f, transform.position.y, 0);
        }
    }
}