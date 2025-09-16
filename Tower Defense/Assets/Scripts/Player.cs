using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb; //Se inicia en el inspector
    private Vector2 movement; //Se inicia en el inspector
    [SerializeField] private float speed = 20f; //Velocidad de movimiento, se modificar� en el inspector

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

}