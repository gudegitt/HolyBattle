using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class Movement : NetworkBehaviour
{
    private NavMeshAgent _agent;
    private bool _isGameStarted = false;

    [SerializeField] private Transform _agroTransform;
    
    public string _gridPos, _gridTeam;
    public GameObject _gridParent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

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
            transform.LookAt(new Vector3(_agroTransform.position.x, 0f, _agroTransform.position.z));
        }
        else
        {
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
        if (GameObjectsManager.TakeGridCount() > 3) //������ (3) ����� �������� �� ����������, ������� ������� �� ������� ������� ��������� ���� ���
        {
            GameEventManager.StartMovementAction?.Invoke();
        }
    }

    private void ChangeStartBool() //������ ��� true ��������� ��� ������
    {
        _isGameStarted = true;
    }

    private void OnDestroy()
    {
        GameEventManager.GridDestroy?.Invoke(_gridParent, gameObject);
    }

    private void OnApplicationQuit()
    {
        Destroy(gameObject);
    }
}
