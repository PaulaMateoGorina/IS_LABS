using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayMachine : MonoBehaviour
{
    public float movementSpeed;
    public float horizontalBoundary = 22;

    public GameObject hayBalePrefab; // ref to hayBale prefab
    public Transform haySpawnpoint; // point from where the hay will be shot
    public float shootInterval; // smallest amount of time between shots
    private float shootTimer; // cooldown between shots


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();   
        UpdateShooting();
    }

    private void UpdateMovement()
    {
        // This gives us whether we have pressed the left or righ arrow (also A / D)
        // negative for left, positive for right, 0 for nothing
        float horizontalInput = Input.GetAxisRaw("Horizontal"); 

        // going left
        if (horizontalInput < 0 && transform.position.x > -horizontalBoundary) 
        {
            transform.Translate(transform.right * -movementSpeed * Time.deltaTime);
        }
        // going right
        else if (horizontalInput > 0 && transform.position.x < horizontalBoundary) 
        {
            transform.Translate(transform.right * movementSpeed * Time.deltaTime);
        }
    }


    private void UpdateShooting(){
    shootTimer -= Time.deltaTime;
    if(shootTimer <= 0 && Input.GetKey(KeyCode.Space)){
        shootTimer = shootInterval;
        ShootHay();
    }
    }
    
    private void ShootHay()
    {
        Instantiate(hayBalePrefab, haySpawnpoint.position, Quaternion.identity);
    }
}
