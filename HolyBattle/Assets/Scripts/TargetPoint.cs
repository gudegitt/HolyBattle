using UnityEngine;

public class TargetPoint : MonoBehaviour
{

    private void Awake()
    {
        GameObjectsManager.Register(gameObject);
        
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameObjectsManager.Unregister(gameObject);
    }
}
