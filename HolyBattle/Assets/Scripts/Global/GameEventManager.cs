using System;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static Action<GameObject, GameObject> GridDestroy; //��� ������ ���, �������, �������� ��� ������ �����, �� ������� ��� �� ������ ����� � ���������� ���� ���� ���� ������
    public static Action StartMovementAction;
}
