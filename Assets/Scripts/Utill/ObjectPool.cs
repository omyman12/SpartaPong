using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools = new List<Pool>();
    public Dictionary<string, Queue<GameObject>> PoolDictionanry;

    private void Awake()
    {
        PoolDictionanry = new Dictionary<string, Queue<GameObject>>();

        foreach(var pool in pools)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }

            PoolDictionanry.Add(pool.tag, queue);
        }
    }

    public GameObject spqwnFromPool(string tag)
    {
        if(!PoolDictionanry.ContainsKey(tag))
        {
            return null;
        }
        GameObject obj = PoolDictionanry[tag].Dequeue();
        PoolDictionanry[tag].Enqueue(obj);

        obj.SetActive(true);
        return obj;
    }
}