using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnOnGrid : NetworkBehaviour
{
    /*private void Awake()
    {
        GameEventManager.SpawnOnStartInGrid += SpawnNPCInManager; print("��������");
    }

    public void SpawnNPCInManager(GameObject _whoSpawn, GameObject _whereSpawn)
    {
        Instantiate(_whoSpawn, _whereSpawn.transform.position, _whereSpawn.transform.rotation); print("�����");
    }

    private void OnDestroy()
    {
        GameEventManager.SpawnOnStartInGrid -= SpawnNPCInManager;
    }

    private void OnApplicationQuit()
    {
        print("�����"); Destroy(gameObject);
    }*/
}
