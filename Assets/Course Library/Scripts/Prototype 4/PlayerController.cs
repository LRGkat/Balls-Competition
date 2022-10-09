using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focusPoint;
    private float powerupStrength = 15.0f;
    public float forwardSpeed = 5.0f;
    public float horizontalSpeed = 5.0f;
    public bool hasPowerup;
    public GameObject powerupIndicator;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerRb = GetComponent<Rigidbody>();
        focusPoint = GameObject.Find("Focus");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        playerRb.AddForce(focusPoint.transform.forward * forwardInput * forwardSpeed);
        playerRb.AddForce(focusPoint.transform.right * horizontalInput * horizontalSpeed);

        powerupIndicator.transform.position = transform.position;
        if (transform.position.y < -10)
        {
            gameManager.GameOver();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);

            Destroy(other.gameObject);
            StopCoroutine("powerupCountdownRoutine");
            StartCoroutine("powerupCountdownRoutine");            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")&& hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
    IEnumerator powerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
}
