using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance => _instance;
    private static RoundManager _instance;

    #region Getters & Setters
    public List<Zombie> Zombies { get { return _zombies; } set { _zombies = value; } }
    public int Round { get { return _round; } set { _round = value; } }
    public bool AllZombiesSpawned { get { return _allZombiesSpawned; } set { _allZombiesSpawned= value; } }
    public int ZombiesToSpawn { get { return _zombiesToSpawn; } set { _zombiesToSpawn = value; } }
    #endregion

    #region Variables
    [SerializeField]private List<Zombie> _zombies = new List<Zombie>();
    [SerializeField] private int _round = 1;
    [SerializeField] private bool _allZombiesSpawned = false;
    [SerializeField] private int _zombiesToSpawn;
    #endregion

    public static System.Action RoundOver;

    #region Functions

    #region Unity Built-in
    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else _instance = this;

        _zombiesToSpawn = _round * 6;
    }

    private void Update()
    {
        CheckIfRoundOver();
    }
    #endregion

    void CheckIfRoundOver()
    {
        if(_allZombiesSpawned == true && _zombies.Count <= 0)
        {
            ++_round;
            _zombiesToSpawn = _round * 6;
            _allZombiesSpawned = false;

            //Let our subscribers know the round is over
            RoundOver?.Invoke();
        }
    }
    #endregion

}
