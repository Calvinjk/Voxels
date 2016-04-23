using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour {

    public static ObjectPooler current;

    public GameObject pooledObject;
    public int pooledAmount = 0;
    public bool willGrow = true;

    List<GameObject> pooledObjects;

    void Awake() {
        current = this;
    }

	void Start () {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < pooledAmount; ++i) {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
	}

    public GameObject GetPooledObject() {
        //Try to find an inactive pooled object
        for (int i = 0; i < pooledObjects.Count; ++i) {
            if (!pooledObjects[i].activeInHierarchy) {
                return pooledObjects[i];
            }
        }

        //If not inactive objects can be found, make one if we are allowing the pool to grow
        if (willGrow) {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            return obj;
        }

        //If we cannot find an inactive object AND we are not allowing the pool to grow, return null
        return null;
    }
}
