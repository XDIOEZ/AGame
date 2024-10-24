using UnityEngine;

public class MovableRock : MonoBehaviour
{
    public float speed = 2f;
    private float oldSpeed = 0f;
    private GameObject player;
    private Vector3 offset;
    private bool isPlayerInRange = false;
    private bool isInteracting = false;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isPlayerInRange && player != null && isInteracting)
        {
            float targetX = player.transform.position.x + offset.x;
            // Vector2 currentVelocity = rb.velocity;
            // rb.velocity = new Vector2(targetX - transform.position.x, currentVelocity.y);
            rb.MovePosition(new Vector2(targetX, transform.position.y));
        }

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            isInteracting = !isInteracting;

            if (player != null && player.TryGetComponent(out PlayerMove playerMove))
            {
                oldSpeed = playerMove.moveSpeed;
                playerMove.moveSpeed = speed;
            }

            if (isInteracting)
            {
                offset = transform.position - player.transform.position;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (
                player != null
                && player.TryGetComponent(out PlayerMove playerMove)
                && oldSpeed != 0f
            )
            {
                playerMove.moveSpeed = oldSpeed;
                oldSpeed = 0f;
            }
            isPlayerInRange = false;
            isInteracting = false;
            player = null;
        }
    }
}
