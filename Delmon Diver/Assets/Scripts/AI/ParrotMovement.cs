using UnityEngine;

public class ParrotMovement : MonoBehaviour
{
    //the diver
    public Transform player;

    //the parrot is following the player
    public Vector3 followOffset = new Vector3(1.5f, 2f, 0f); //distace between both

    public float followSpeed = 3f; //movemnet speed

    public float catchUpSpeed = 7f;  //faster when player is far away

    public float maxDistance = 5f;  //distance to catchUpSpeed

    void Update()
    {
        if (player == null) return; //check for player

        //distance between parrot and player
        float distance = Vector3.Distance(transform.position, player.position);

        //If far → move faster, else -> normal speed
        float currentSpeed = distance > maxDistance ? catchUpSpeed : followSpeed;

        //position (player + offset)
        Vector3 targetPosition = player.position + followOffset;

        // Smooth movement towards player
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            currentSpeed * Time.deltaTime
        );

        // Make parrot face the player direction
        FacePlayer();
    }

    void FacePlayer()
    {
        // If player is on the right → face right
        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            // If player is on the left → flip
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

}
    
