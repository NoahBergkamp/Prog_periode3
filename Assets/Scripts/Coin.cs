using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            if (collision.gameObject.tag == "Player")
            {
                MovementScript player = collision.gameObject.GetComponent<MovementScript>();
                player.coins += 1;
                Destroy(gameObject);
            }
        }
    }

}
