using UnityEngine;
using TMPro;

public class Stars : MonoBehaviour
{
    public AudioClip starsClip;
    private TextMeshProUGUI starsText;
    public int starsToGive = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the Stars Text game object; then get its component
        starsText = GameObject.FindWithTag("StarsText").GetComponent<TextMeshProUGUI>();
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
            player.stars += starsToGive;
            player.PlaySFX(starsClip, 0.1f);
            starsText.text = player.stars.ToString();

            Destroy(gameObject);
        }
    }
}
