using UnityEngine;

/*
public class ParrotChatAI : MonoBehaviour
{
    public Transform player; //player reference
    public KeyCode chatKey = KeyCode.E; //key to open the chat
    public float chatRange = 3f; //distance to allow chat

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chatRange && Input.GetKeyDown(chatKey))
        {
            Debug.Log("AI Chat opens"); //ui will be implemented later
        }
    }
}*/
using UnityEngine.InputSystem;

public class ParrotChatAI : MonoBehaviour
{
    public Transform player;
    public float chatRange = 3f;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chatRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("Open Neocortex + Ollama chat here.");
        }
    }
}
