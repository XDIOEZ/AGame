using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackStarSpawn : MonoBehaviour
{
    public GameObject blackStar; // 要生成的预制体
    public int minCount = 1; // 每次生成的最小数量
    public int maxCount = 5; // 每次生成的最大数量
    public float minX = -5f; // 生成区域的最小 x 坐标
    public float maxX = 5f; // 生成区域的最大 x 坐标
    public float minY = -5f; // 生成区域的最小 y 坐标
    public float maxY = 5f; // 生成区域的最大 y 坐标

    void Start()
    {
        // 每 10 秒调用一次 RandomSpawnObjects 方法
        InvokeRepeating("RandomSpawnObjects", 0f, 5f);
    }

    void RandomSpawnObjects()
    {
        // 生成随机数量的物体
        int count = Random.Range(minCount, maxCount + 1);

        for (int i = 0; i < count; i++)
        {
            // 在随机位置生成物体
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            Vector3 position =transform.position+new Vector3(x, y, 0f);

            Instantiate(blackStar, position, Quaternion.identity);
        }
    }
}
