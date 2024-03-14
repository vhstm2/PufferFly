using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TextLogManager : MonoBehaviour
{
    public static TextLogManager Instance;


    public ObjectPool<LogText> Pool;

    [SerializeField] private LogText logPrefabs;
    [SerializeField] private Transform parent;
    
    
    private void Awake()
    {
        Instance = this;

        Pool = new ObjectPool<LogText>(
            () =>
            {
                var log = Instantiate(logPrefabs, parent);
                return log;
            },
            (log) => { log.gameObject.SetActive(true); },
            (log) => { log.gameObject.SetActive(false); },
            (log) => { Destroy(log.gameObject); },
            maxSize : 10
            );

    }

}
