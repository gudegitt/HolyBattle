using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;

public class Player : NetworkBehaviour
{
    [SyncVar]
    [SerializeField] private float _speedMove;

    private Vector3 _moveVector;
    private CharacterController _chController;
    private NavMeshAgent _agent;


    private GameObject[] _tp;

    private void Awake()
    {
        _chController = GetComponent<CharacterController>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _moveVector = Vector3.zero;
    }

    private void Update()
    {
        //_tp = GameObjectsManager.GetGameObjectByName("TargetPoint");


        //_agent.SetDestination(_tp[0].transform.position);
        //transform.LookAt(_tp[0].transform.position);
    }

    
}
