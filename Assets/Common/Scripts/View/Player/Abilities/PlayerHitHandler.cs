using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class PlayerHitHandler : PlayerBehaviour
{
    public CapsuleCollider HitCapsule;

    private void Awake()
    {
        HitCapsule = GetComponent<CapsuleCollider>();
    }

    void OnEnable()
    {
        HitCapsule.enabled = false;
        HitCapsule.isTrigger = true;
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<CapsuleCollider>(out var capsule))
        {
            GetHit("Main Player Get Hit");
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.TryGetComponent<PlayerHitHandler>(out var playerHitHandler))
        {
            playerHitHandler.GetHit("enemy get hit");
        }
    }

    public void GetHit(string msg)
    {
        Debug.Log(msg);
    }
}
