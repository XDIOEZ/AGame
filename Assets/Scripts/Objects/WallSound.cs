using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSound : MonoBehaviour
{
    public Transform player;

    public AudioSource audioSource;
    public float minDistance = 1f;
    public float maxDistance = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        float volume = Mathf.Clamp((maxDistance - distance) / (maxDistance - minDistance), 0f, 1f);
        audioSource.volume = volume;
    }
}
