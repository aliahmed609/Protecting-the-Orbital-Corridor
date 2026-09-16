using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Pool")]
    [SerializeField] private GameObject prefab;

    [SerializeField] private int initialSize = 5;

    [SerializeField] private Transform poolParent;

    private readonly Queue<GameObject> availableObjects =
        new Queue<GameObject>();

    private void Awake()
    {
        if (prefab == null)
        {
            Debug.LogError(
                gameObject.name +
                " ObjectPool has no prefab assigned!"
            );

            return;
        }

        if (poolParent == null)
        {
            poolParent = transform;
        }

        Prewarm();
    }

    private void Prewarm()
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = CreateObject();

            if (obj != null)
            {
                availableObjects.Enqueue(obj);
            }
        }
    }

    private GameObject CreateObject()
    {
        GameObject obj =
            Instantiate(
                prefab,
                poolParent
            );

        obj.name = prefab.name + "(Clone)";

        PoolReference reference =
            obj.GetComponent<PoolReference>();

        if (reference == null)
        {
            reference =
                obj.AddComponent<PoolReference>();
        }

        reference.SetPool(this);

        IPoolable poolable =
            obj.GetComponent<IPoolable>();

        if (poolable != null)
        {
            poolable.OnDespawned();
        }

        obj.SetActive(false);

        return obj;
    }

    public GameObject Get()
    {
        GameObject obj;

        if (availableObjects.Count > 0)
        {
            obj = availableObjects.Dequeue();
        }
        else
        {
            obj = CreateObject();
        }

        if (obj == null)
        {
            return null;
        }

        obj.SetActive(true);

        // Make absolutely sure the reference points
        // to THIS pool.
        PoolReference reference =
            obj.GetComponent<PoolReference>();

        if (reference == null)
        {
            reference =
                obj.AddComponent<PoolReference>();
        }

        reference.SetPool(this);

        IPoolable poolable =
            obj.GetComponent<IPoolable>();

        if (poolable != null)
        {
            poolable.OnSpawned();
        }

        return obj;
    }

    public void Return(GameObject obj)
    {
        if (obj == null)
        {
            return;
        }

        IPoolable poolable =
            obj.GetComponent<IPoolable>();

        if (poolable != null)
        {
            poolable.OnDespawned();
        }

        obj.SetActive(false);

        obj.transform.SetParent(poolParent);

        availableObjects.Enqueue(obj);
    }
}