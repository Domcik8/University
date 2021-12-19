using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public int scoreValue;
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }
        Destroy(gameObject);
        Instantiate(explosion, transform.position, transform.rotation);
        gameController.AddScore(scoreValue);
        if (other.tag == "Player 1" || other.tag == "Player 2")
        {
            gameController.UpdateLifes(-1, other);
            return;
        }
        Destroy(other.gameObject);
    }
}