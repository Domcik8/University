using UnityEngine;
using System.Collections;

public class AddSpeed : MonoBehaviour
{
    public float speed;
    public float time;

    private Player1Controller player1Controller;
    private Player2Controller player2Controller;

    void Start()
    {
        GameObject playerControllerObject = GameObject.FindWithTag("Player 1");
        if (playerControllerObject != null)
        {
            player1Controller = playerControllerObject.GetComponent<Player1Controller>();
        }
        if (player1Controller == null)
        {
            Debug.Log("Cannot find 'Player1Controller' script");
        }

        playerControllerObject = GameObject.FindWithTag("Player 2");
        if (playerControllerObject != null)
        {
            player2Controller = playerControllerObject.GetComponent<Player2Controller>();
        }
        if (player2Controller == null)
        {
            Debug.Log("Cannot find 'Player1Controller' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("I was in trigger");
        if (other.tag == "Player 1")
        {
            Debug.Log("I entered first trigger");
            Destroy(gameObject);
            player1Controller.AddSpeed(speed, time);
        }

        if (other.tag == "Player 2")
        {
            Debug.Log("I entered second trigger");
            Destroy(gameObject);
            player2Controller.AddSpeed(speed, time);
        }
    }
}
