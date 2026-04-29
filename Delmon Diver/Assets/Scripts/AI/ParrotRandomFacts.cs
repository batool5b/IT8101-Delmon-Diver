using UnityEngine;
using System.Collections;

public class ParrotRandomFacts : MonoBehaviour
{
    public Transform player; //player reference

    public float minFactTime = 30f; //minimum time between facts
    public float maxFactTime = 100f; //maximum time between facts

    //sound
    public AudioSource audioSource; //audio source on parrot
    public AudioClip parrotSound;

    //distance
    public float farDistance = 7f; //when player is far
    public float closeDistance = 5f; //when player is close again

    //last indexes
    private int lastFactIndex = -1; //last fact
    private int lastReactionIndex = -1; //last reaction

    private bool alreadyReactedToFar = false; //checks if parrot already talked

    public string[] facts =
    {
        "Did you know? Bahrain was once home to the ancient Dilmun civilization.",
        "Did you know? Bahrain is an archipelago made up of many islands.",
        "Did you know? Pearl diving was once a major tradition and source of income.",
        "Did you know? The Tree of Life is a rare living tree found in Bahrain’s desert.",
        "Did you know? Bahrain has a rich pearl-diving history from the days of natural pearls.",
        "Did you know? Bahrain is famous for its traditional textile and craftwork.",
        "Did you know? Dilmun is believed to be mentioned in some of the world’s oldest stories.",
        "Did you know? Pearl diving shaped Bahrain’s history and economy for centuries.",
        "Did you know? Bahrain’s pearl-diving era left stories and traditions that still influence culture today.",
        "Did you know? Traditional crafts in Bahrain were passed down through generations of artisans.",
        "Did you know? Bahrain was one of the first places in the Gulf to discover oil.",
        "Did you know? Before oil, Bahrain’s economy depended heavily on the sea.",
        "Did you know? The Tree of Life can survive without an obvious water source.",
        "Did you know? Bahrain’s location made it an important trading center in the Gulf.",
        "Did you know? Muharraq was once the capital of Bahrain before Manama.",
        "Did you know? Bahrain has over 30 natural and man-made islands.",
        "Did you know? The Dilmun civilization made Bahrain a key trade hub in ancient times.",
        "Did you know? Traditional Bahraini houses were designed to stay cool in hot weather.",
        "Did you know? The pearl industry declined after the discovery of oil.",
        "Did you know? Bahrain has a history that goes back more than 4,000 years."
    };

    public string[] reactionLines =
    {
        "Hey! Wait for me!",
        "You are getting too far!",
        "Slow down, I am coming!",
        "Do not leave me behind!",
        "I will fly closer to you!",
        "Stay close, I have more facts to tell you!",
        "Careful, I am following you!",
        "Wait! I am still here!"
    };


    void Update()
    {
        if (player == null) return;

        //distance between parrot and player
        float distance = Vector3.Distance(transform.position, player.position);

        //if player becomes far, parrot talks once
        if (distance >= farDistance && alreadyReactedToFar == false)
        {
            SayRandomReaction();
            alreadyReactedToFar = true;
        }

        //if player comes close again, allow parrot to react next time
        if (distance <= closeDistance)
        {
            alreadyReactedToFar = false;
        }
    }

    void Start()
    {
        StartCoroutine(RandomFactLoop());
    }

    public void SayRandomFact()
    {
        //check facts
        if (facts.Length == 0) return;

        int randomIndex;
        int attempts = 0;

        //prevent same fact
        do
        {
            randomIndex = Random.Range(0, facts.Length);
            attempts++;
        }
        while (randomIndex == lastFactIndex && attempts < 10);

        lastFactIndex = randomIndex;

        string fact = facts[randomIndex];

        Debug.Log("Parrot: " + fact); //until ui added

        PlaySound();
    }

    public void SayRandomReaction()
    {
        //check reactions
        if (reactionLines.Length == 0) return;

        int randomIndex;
        int attempts = 0;

        //prevent same reaction
        do
        {
            randomIndex = Random.Range(0, reactionLines.Length);
            attempts++;
        }
        while (randomIndex == lastReactionIndex && attempts < 10);

        lastReactionIndex = randomIndex;

        string reaction = reactionLines[randomIndex];

        Debug.Log("Parrot: " + reaction); //until ui added

        PlaySound();
    }

    void PlaySound() //play sound when the parrot say random fact 
    {
        //play sound
        if (audioSource != null && parrotSound != null)
        {
            audioSource.PlayOneShot(parrotSound);
        }
    }

    IEnumerator RandomFactLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minFactTime, maxFactTime));

            SayRandomFact();
        }
    }
}