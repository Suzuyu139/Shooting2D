using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private float _spawnTime = 0.0f;
    [SerializeField] private GameObject _spawnEnemy = null;

    public float SpawnTime => _spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        Instantiate(_spawnEnemy, this.transform.position, Quaternion.identity);
    }
}
