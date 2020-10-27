using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; set; }

    [SerializeField] GameObject _pooledObject;
    Queue<GameObject> _poolOfObjects = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    public GameObject Get()
    {
        if (_poolOfObjects.Count == 0)
        {
            AddShots(1);
        }

        return _poolOfObjects.Dequeue();
    }

    private void AddShots(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject _projectile = Instantiate(_pooledObject);
            if(!_projectile.activeSelf)
            _projectile.SetActive(true);
            _poolOfObjects.Enqueue(_pooledObject);
        }
    }

    public void ReturnToPool(GameObject _pooledObject)
    {
        _pooledObject.SetActive(false);
        _poolOfObjects.Enqueue(_pooledObject);
    }
}
