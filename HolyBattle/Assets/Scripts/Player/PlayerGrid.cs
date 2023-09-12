//Функция работающая с сеткой спавна игрока

//позиция спавна должна быть определена системой на этапе регистрации в бой
//при пве у игрока точка №1, при пвп рандом
//команды поделены следующим спавном: команда 1 - точки спавна 1, 2, 3
//команда 2 - точки спавна 2, 4, 6
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerGrid : NetworkBehaviour
{
    public string _teamNum { get; private set; }
    public string _xPos { get; private set; }

    [SerializeField] public List<GameObject> _NPCGrid;

    private Transform _cameraTransform;

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void Start()
    {
        float _zPos = transform.position.z;
        _xPos = transform.position.x.ToString();

        if (_zPos == 15 )
        {
            _teamNum = "Team1";
            SetCameraForPlayer(new Vector3(40f, 13.7200003f, 3f), new Vector3(33.45f, 0f, 0f));
        }
        else
        {
            _teamNum = "Team2";
            SetCameraForPlayer(new Vector3(40f, 13.7200003f, 41f), new Vector3(33.45f, 180f, 0f));
        }

        GameObjectsManager.RegisterGrid(gameObject);
        GameEventManager.GridDestroy += NPCGridRemove;

        Invoke("ColliderDisable", 1f);
    }

    private void SetCameraForPlayer(Vector3 _pos, Vector3 _rotat) //метод определяет камеру для игрока в зависимости от его команды
    {
        if(!isLocalPlayer)
        {
            return;
        }

        _cameraTransform.SetPositionAndRotation(_pos, Quaternion.Euler(_rotat));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            _NPCGrid.Add(other.gameObject);
            Movement _otherParamMovement = other.gameObject.GetComponent<Movement>();

            _otherParamMovement._gridTeam = _teamNum;
            _otherParamMovement._gridPos = _xPos;
            _otherParamMovement._gridParent = gameObject;
        }
    }

    private void NPCGridRemove(GameObject _name, GameObject _NPCRemove) //метод убирает НПС из листа сетки
    {
        if (_name == gameObject)
        {
            _NPCGrid.Remove(_NPCRemove);
        }
    }

    private void ColliderDisable()//выключаем коллайдер, так как он нужен только на старте для добавления НПС в лист, Invoke в Start
    {
        GetComponent<Collider>().enabled = false;
    }

    private void OnDestroy()
    {
        GameEventManager.GridDestroy -= NPCGridRemove;
        GameObjectsManager.UnregisterGrid(gameObject);
    }
}
