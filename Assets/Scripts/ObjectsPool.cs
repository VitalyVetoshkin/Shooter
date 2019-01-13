using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class ObjectsPool : MonoBehaviour
    {
        public static ObjectsPool Instance { set; get; }

        [SerializeField] 
        private GameObject[] objects;
        private Dictionary<string, Queue<IPoolable>> objectsDict = new Dictionary<string, Queue<IPoolable>>();
        
        private void Awake()
        {
            if (Instance) DestroyImmediate(gameObject);
            else Instance = this;
        }

        private void Start()
        {
            foreach (var o in objects)
            {
                var poolObj = o.GetComponent<IPoolable>();
                if (poolObj == null) continue;
                
                var queue = new Queue<IPoolable>();

                for (int i = 0; i < poolObj.ObjectsCount; i++)
                {
                    GameObject go = Instantiate(o);
                    go.SetActive(false);
                    queue.Enqueue(go.GetComponent<IPoolable>());
                }

                if (!objectsDict.ContainsKey(poolObj.PoolID))
                objectsDict.Add(poolObj.PoolID, queue);                              
            }
        }

        public IPoolable GetObject(string poolID)
        {
            if (string.IsNullOrEmpty(poolID)) return null;
            if (!objectsDict.ContainsKey(poolID)) return null;

            if (objectsDict[poolID].Count == 0)
            {
                foreach (var o in objects)
                {
                    if (o.GetComponent<IPoolable>().PoolID == poolID)
                    {
                        GameObject go = Instantiate(o);
                        go.SetActive(false);
                        objectsDict[poolID].Enqueue(go.GetComponent<IPoolable>());                                               
                        break;
                    }
                }
            }

            IPoolable p = objectsDict[poolID].Dequeue();
            objectsDict[poolID].Enqueue(p);
            
            return p;
        }
    }
}