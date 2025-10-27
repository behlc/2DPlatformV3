using UnityEngine;

public class Stars : MonoBehaviour
{
    public AudioClip starsClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // access to the player script
            Player player = collision.gameObject.GetComponent<Player>();
            player.stars += 1;
            player.PlaySFX(starsClip, 0.1f);

            Destroy(gameObject);
        }
    }
}
