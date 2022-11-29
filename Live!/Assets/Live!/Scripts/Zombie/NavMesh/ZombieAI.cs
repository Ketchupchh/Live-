using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    #region References
    public NavMeshAgent Agent => _agent;
    [SerializeField] private NavMeshAgent _agent;
    #endregion

    #region Variables
    [SerializeField] private Animator _animator;
    #endregion

    #region Functions

    #region Unity Built-in
    private void Update()
    {
        //Please take this out of update future-self (thank you)
        SetDestination();
    }
    #endregion

    private void SetDestination()
    {
        _agent.SetDestination(Weapon.Instance.Hand.transform.position);
        _animator.SetBool("isWalking", true);
    }
    #endregion
}