using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour
{
    #region References
    public GameObject _zombiePrefab;
    [SerializeField] private Transform _spawnPoint;
    #endregion

    #region Functions

    #region Unity Built-in
    private void Start()
    {
        RoundManager.RoundOver += RoundManager_RoundOver;
        StartCoroutine(SpawnZombie());
    }

    private void OnDisable()
    {
        RoundManager.RoundOver -= RoundManager_RoundOver;
    }

    private void RoundManager_RoundOver()
    {
        StartCoroutine(SpawnZombie());
    }
    #endregion

    IEnumerator SpawnZombie()
    {
        static IEnumerator Delay()
        {
            int seconds = Random.Range(5, 11);
            yield return new WaitForSeconds(seconds);
        }

        for(int i = RoundManager.Instance.ZombiesToSpawn; i > 0; --i)
        {
            if (RoundManager.Instance.ZombiesToSpawn == 0) break;

            //Create zombie at spawnpoint and add it to the zombies list
            GameObject z = Instantiate(_zombiePrefab, new Vector3(_spawnPoint.position.x, _spawnPoint.position.y, _spawnPoint.position.z), Quaternion.identity);
            RoundManager.Instance.Zombies.Add(z.GetComponent<Zombie>());

            //Remove the amount of zombies to spawn by 1 so we don't go over the intended amount
            --RoundManager.Instance.ZombiesToSpawn;
            yield return StartCoroutine(Delay());
        }
        RoundManager.Instance.AllZombiesSpawned = true;
    }
    #endregion
}
