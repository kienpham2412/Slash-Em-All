using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRB;
    private GameManager gameManager;
    public ParticleSystem explosionParticle;

    [SerializeField] private float minSpeed = 12, maxSpeed = 20;
    private float maxTorque = 10;
    private float xRange = 4, ySpawnPos = -6;
    public int pointValue;

    // Start is called before the first frame update
    void Start()
    {
        // make target fly up to the screen and torque around itself at the begining
        targetRB = GetComponent<Rigidbody>();
        targetRB.AddForce(RandomForce(), ForceMode.Impulse);
        targetRB.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();

        // add score mechanic
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private Vector3 RandomForce()
    {
        Vector3 force = Vector3.up * Random.Range(minSpeed, maxSpeed);
        return force;
    }

    private Vector3 RandomSpawnPos()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
        return spawnPos;
    }

    private float RandomTorque()
    {
        float torque = Random.Range(-maxTorque, maxTorque);
        return torque;
    }

    // hover the mouse onto the object to trigger this event
    private void OnMouseOver()
    {
        // if the game is not over then do the following things
        if (gameManager.isGameActive)
        {
            if(gameManager.isPaused) return;
            Instantiate(explosionParticle, transform.position, transform.rotation);
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sensor"))
        {
            Destroy(gameObject);
            if (gameObject.CompareTag("Good"))
            {
                if(gameManager.lives > 0)
                {
                    gameManager.lives--;
                }
            }
        }
    }
}
