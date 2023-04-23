using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float init_speed;
    public Vector3 initial_pos;

    public AudioSource pickup_sound;
    public AudioSource win_sound;


    public GameObject door;
    public TextMeshProUGUI countText;
    public GameObject winObject;

    private float speed;
    private int count;
    private int total_pickups;
    private Vector3 checkpoint;

    private bool finished;
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
        rb.isKinematic = false;
        count = 0;
        total_pickups = GameObject.FindGameObjectsWithTag("PickUp").Length;

        finished = false;
        speed = init_speed;
        checkpoint = new Vector3(2.0f, 0.5f, 16.0f);

        pickup_sound = pickup_sound.GetComponent<AudioSource>();
        win_sound = win_sound.GetComponent<AudioSource>();

        SetCountText();
        winObject.SetActive(false);
    }

    void Update()
    {
        if (transform.position.y < 0) ResetPosition(checkpoint);
        if((finished) && (Input.GetKeyDown(KeyCode.R))){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
       
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
            pickup_sound.Play();
            count++;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Damage"))
        {
            ResetPosition(initial_pos);
        }
        else if (other.gameObject.CompareTag("Goal"))
        {
            win_sound.Play();
            finished = true;
            rb.isKinematic = true;
            winObject.SetActive(true);
        }
        
    }
}
