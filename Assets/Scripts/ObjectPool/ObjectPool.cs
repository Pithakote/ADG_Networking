using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; set; }

    [SerializeField] GameObject _pooledObject;
    LinkedList<GameObject> _poolOfObjects;
    private void Awake()
    {
        _poolOfObjects = new LinkedList<GameObject>();

        Instance = this;
    }

    public GameObject Get()
    {
        if (_poolOfObjects.Count == 0)
        {
            AddShots(1);
        }

        GameObject pooledObject = _poolOfObjects.First.Value;
        _poolOfObjects.RemoveFirst();
        return pooledObject;
    }

    private void AddShots(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject _projectile = Instantiate(_pooledObject);
            if(!_projectile.activeSelf)
            _projectile.SetActive(true);
            _poolOfObjects.AddFirst(_pooledObject);
        }
    }

    public void ReturnToPool(GameObject _pooledObject)
    {
        _pooledObject.SetActive(false);
        _poolOfObjects.AddFirst(_pooledObject);
    }
}
