using UnityEngine;
using System.Collections;

public class AddLife : MonoBehaviour
{
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
        if (other.tag == "Player 1" || other.tag == "Player 2")
        {
            Destroy(gameObject);
            gameController.UpdateLifes(1, other);
        }
    }
}
