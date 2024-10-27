using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemy;
    public int spawnTime = 20;
    private float spawnTiming = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform transform = GetComponent<Transform>();
        spawnTiming+= Time.deltaTime;
        if (spawnTiming > 10) {
            spawnTiming = 0;
            float x = transform.position.x;
            float y = transform.position.y;
            float z = transform.position.z;
            Vector3 pos = new Vector3(x, y, z);
            Instantiate(enemy, pos, Quaternion.identity);
        }
    }
}
