using System;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static Action<GameObject, GameObject> GridDestroy; //��� ������ ���, �������, �������� ��� ������ �����, �� ������� ��� �� ������ ����� � ���������� ���� ���� ���� ������
    public static Action StartMovementAction; //��������� � ������ ����, ������ ������ � Movement
    //public static Action <GameObject, GameObject> SpawnOnStartInGrid; //�������� �������� � GameObjectsManager �� ��� ������� �������� �� �����
    public static Action StopPrefabEvent;
}
