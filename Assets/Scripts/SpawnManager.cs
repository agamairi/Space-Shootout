using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemycontainer;
    [SerializeField]
    private GameObject [] powerups;


    private bool _stopSpawn = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }
    //spawn objects every 5 seconds
    //create coroutine of type IEnumerator  -> yield events
    //while loop
    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawn == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.2f, 8.2f), 9f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemycontainer.transform;
            yield return new WaitForSeconds(2.5f);
        }
    }
    IEnumerator SpawnPowerupRoutine()
    {
        //every 5-7 seconds, spawn apower up
        while(_stopSpawn == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.2f, 8.2f), 9f, 0);
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5, 10));

        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawn = true; 
    }

}
