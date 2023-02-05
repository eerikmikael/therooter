using UnityEngine;
using Random = UnityEngine.Random;

public class BouncingEnemy : MonoBehaviour
{
    [SerializeField]
    private float bounceStrength;
    private Rigidbody myBody;
    private bool hitGround = true;
    
    private void Start()
    {
        myBody = GetComponent<Rigidbody>();
        InvokeRepeating(nameof(Bounce), Random.Range(0.2f, 2f), Random.Range(1.4f, 1.6f));
    }

    private void Bounce()
    {
        if(!hitGround)
        {
            return;
        }
        
        Vector3 force = new Vector3(Random.Range(-1, 1), 5, Random.Range(-1, 1)) * bounceStrength;
        myBody.AddForce(force);
        hitGround = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.name == "Floor")
        {
            hitGround = true;
        }
    }

    public void AddBounceStrength()
    {
        bounceStrength += LevelManager.Instance.GetCurrentLevelIndex() * 10;
    }
}
