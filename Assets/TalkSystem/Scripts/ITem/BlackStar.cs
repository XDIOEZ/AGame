using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackStar : MonoBehaviour
{
    public float speed = 5f;
    public float angle = 45f; // 角度，单位是度
    public float rotationSpeed = 100f; // 旋转速度
    public float lifeTimer;
    public int damage;


    private GameObject blackStars;
    public GameObject vlight;
    private GameObject effect;


    void Start()
    {
        blackStars = transform.Find("BlackStar").gameObject;
        effect= transform.Find("ParticleWakeline").gameObject;
        // 将角度转换为弧度
        float radians = angle * Mathf.Deg2Rad;

        // 计算水平和垂直速度分量
        float horizontalSpeed = speed * Mathf.Cos(radians);
        float verticalSpeed = speed * Mathf.Sin(radians);

        // 设置物体的初始速度
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(horizontalSpeed, verticalSpeed);
    }

    void Update()
    {
        // 根据旋转速度更新物体的旋转角度
        blackStars.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent(out PlayerData_Temp playerData))
            {
                playerData.ChangeHealth(-damage);
                Debug.Log("玩家受伤: BlackStar.cs: OnCollisionEnter2D: Player");
                Destroy(gameObject);
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("BlackStar"))
            {
                //不反应
            }
            else
            {
                Debug.Log(collision.gameObject);
                Invoke("EnemyDead", lifeTimer);
                // 否则，停止物体的移动
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.velocity = Vector2.zero;
                rotationSpeed = 0;
                Destroy(effect, 0.05f);
            }
        }
    }


    public void EnemyDead()
    {
        Instantiate(vlight,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}
