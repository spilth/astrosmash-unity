using UnityEngine;

public class AmmoCrate : MonoBehaviour
{
    private AmmoCrateSpawner _spawner;

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.name == "Ground")
        {
            Destroy(gameObject, 3.0f);
        }
        
        if (collision2D.gameObject.name == "Player")
        {
            FindObjectOfType<Player>().AddMissiles(10);
            _spawner.PlayPickupSound();
            Destroy(gameObject);
        }
    }

    public void SetSpawner(AmmoCrateSpawner ammoCrateSpawner)
    {
        _spawner = ammoCrateSpawner;
    }
}