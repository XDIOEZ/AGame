using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackStarSpawn : MonoBehaviour
{
    public GameObject blackStar; // Ҫ���ɵ�Ԥ����
    public int minCount = 1; // ÿ�����ɵ���С����
    public int maxCount = 5; // ÿ�����ɵ��������
    public float minX = -5f; // �����������С x ����
    public float maxX = 5f; // ������������ x ����
    public float minY = -5f; // �����������С y ����
    public float maxY = 5f; // ������������ y ����
    public float cycleTimer;
    public float GameTimerStart;

    void Start()
    {
        // ÿ x �����һ�� RandomSpawnObjects ����
        InvokeRepeating("RandomSpawnObjects", GameTimerStart, cycleTimer);
    }

    void RandomSpawnObjects()
    {
        // �����������������
        int count = Random.Range(minCount, maxCount + 1);

        for (int i = 0; i < count; i++)
        {
            // �����λ����������
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            Vector3 position =transform.position+new Vector3(x, y, 0f);

            Instantiate(blackStar, position, Quaternion.identity);
        }
    }
}
