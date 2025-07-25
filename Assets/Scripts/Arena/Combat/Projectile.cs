using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string displayName;
    public bool isDying = false;
    public float deathTime = 0.0f;
    private float timeElapsed;
    public float instantiateOffset = 100;
    public float projectileSpeed = 300;

    // Stretch
    // public Animator animator;
    // public Animation animation;

    void Update()
    {
        if (isDying)
        {
            if (timeElapsed >= deathTime)
            {
                Destroy(gameObject);
            }

            // can handle 'lerp' death animation here with a DeathAnimate() or something.
            timeElapsed = Time.deltaTime;
        }
    }

    public void InitializeProjectile(FactionType faction)
    {
        transform.SetParent(null);
        transform.localScale = Vector3.one;
        Vector3 rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z) * Vector3.right;
        transform.position += instantiateOffset * rotation;
        GetComponent<Rigidbody2D>().linearVelocity = projectileSpeed * (Vector2)rotation;

        gameObject.AddComponent<Faction>().faction = faction;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        ExecuteCollisionAction(collider);
    }

    public virtual void ExecuteCollisionAction(Collider2D collider)
    {
        DestroyProjectile();
    }

    internal void DestroyProjectile()
    {
        isDying = true;
        timeElapsed = 0;
        // TODO: start animation, even if it's a simple lerp
        // Turn off physics and interactions while it's doing its death animation
        if (TryGetComponent(out Rigidbody2D rigidbody))
        {
            rigidbody.simulated = false;
        }
        else
        {
            Debug.LogError($"{displayName} was unable to find a rigidbody.");
        }
        if (TryGetComponent(out Collider2D collider))
        {
            collider.enabled = false;
        }
        else
        {
            Debug.LogError($"{displayName} was unable to find a collider.");
        }
    }
}