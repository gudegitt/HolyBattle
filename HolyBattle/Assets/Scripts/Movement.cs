using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class Movement : NetworkBehaviour
{
    private NavMeshAgent _agent;
    private bool _isGameStarted = false;
    private Animator _animator;

    [SerializeField] private Transform _agroTransform;
    [SerializeField] private GameObject _stopPrefab;
    
    public string _gridPos, _gridTeam;
    public GameObject _gridParent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        GameEventManager.StartMovementAction += ChangeStartBool;
        
        Invoke("CheckGridCount", 2f);
    }

    void Update()
    {
        if (!_isGameStarted)
        {
            return;
        }

        if (_agroTransform)
        {
            _agent.SetDestination(_agroTransform.position);
            _animator.SetFloat("Movement", _agent.remainingDistance);
            transform.LookAt(new Vector3(_agroTransform.position.x, 0f, _agroTransform.position.z));
        }
        else
        {
            _animator.SetFloat("Movement", 0);
            FindClosestEnemy();
        }
    }

    private void FindClosestEnemy() //����� �� ������ ���������� �����, ������� ���� ����� �� ����� �����
    {
        GameObject[] enemy = GameObjectsManager.GetNPCGridByName(_gridTeam, _gridPos);

        float distance = 10000f;
        Vector3 position = transform.position;

        foreach (GameObject go in enemy)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                _agroTransform = go.transform;
                distance = curDistance;
            }
        }
    }

    private void CheckGridCount() //����� �� �������� ����������� ����� (�������) � ����� ����������� ������ ����������� ������
    {
        if (GameObjectsManager.TakeGridCount() > 1) //������ (3) ����� �������� �� ����������, ������� ������� �� ������� ������� ��������� ���� ���
        {
            Invoke("StartEventInitialize", 5f);
        }
    }

    private void StartEventInitialize()
    {
        GameEventManager.StartMovementAction?.Invoke();
    }

    private void ChangeStartBool() //������ ��� true ��������� ��� ������
    {
        _isGameStarted = true;
        GameEventManager.StartMovementAction -= ChangeStartBool;
    }

    private void OnDestroy()
    {
        GameEventManager.StartMovementAction -= ChangeStartBool;
        GameEventManager.GridDestroy?.Invoke(_gridParent, gameObject);
    }

    private void OnApplicationQuit()
    {
        Destroy(gameObject);
    }
}
