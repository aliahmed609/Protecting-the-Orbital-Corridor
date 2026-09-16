using UnityEngine;

public class PoolReference : MonoBehaviour
{
    private ObjectPool pool;

    public void SetPool(ObjectPool newPool)
    {
        pool = newPool;
    }

    public void ReturnToPool()
    {
        if (pool == null)
        {
            Debug.LogError(
                gameObject.name +
                " PoolReference has no pool!"
            );

            return;
        }

        pool.Return(gameObject);
    }
}