using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : Singleton<ObjectPooler>
{
    [System.Serializable]
    public class Pool {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public override void Awake() {
        base.Awake();
        gameObject.name = "ObjectPooler";
    }

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        
        foreach(var pool in pools) {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (var i = 0; i < pool.size; i++) {
                var obj = Instantiate(pool.prefab, gameObject.transform, true);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject Spawn(string objectTag, Vector3 position, Quaternion rotation) {
        if(!poolDictionary.ContainsKey(objectTag)) {
            Debug.LogError("Pool with tag" + objectTag + "doesn't exist");
            return null;
        }
        var objectToSpawn = poolDictionary[objectTag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        poolDictionary[objectTag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }


}
