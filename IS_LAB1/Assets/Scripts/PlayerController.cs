using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public Vector3 initial_pos;
    public Vector3 checkpoint;
    public TextMeshProUGUI countText;
    public GameObject door;

    public GameObject winObject;

    private int count;
    private int total_pickups;

    private Rigidbody rb;
    private float movementX;
    private float movementY;

    void ResetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 12;
        total_pickups = GameObject.FindGameObjectsWithTag("PickUp").Length;

        checkpoint = new Vector3(2.0f, 0.5f, 16.0f);

        SetCountText();
        winObject.SetActive(false);
    }

    void Update()
    {
        if (transform.position.y < 0) ResetPosition(checkpoint);
    }

        void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Collected: " + count.ToString() + "/ " + total_pickups;
        if (count >= total_pickups)
        {
            door.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Damage"))
        {
            ResetPosition(initial_pos);
        }
        else if (other.gameObject.CompareTag("Goal"))
        {
            winObject.SetActive(true);
            Time.timeScale = 0;
        }
        
    }
}
