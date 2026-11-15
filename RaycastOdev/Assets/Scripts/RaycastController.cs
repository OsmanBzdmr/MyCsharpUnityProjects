using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastController : MonoBehaviour
{
    [SerializeField] private float range = 100f;
    [SerializeField] private float impactForce = 10f;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private LayerMask hitLayers = ~0;

    private void Awake()
    {
        if (shootPoint == null)
        {
            shootPoint = transform;
        }
    }

    private void Update()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Debug.DrawRay(shootPoint.position, shootPoint.forward * range, Color.red, 0.5f);

        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out RaycastHit hit, range, hitLayers))
        {
            
            Rigidbody rb = hit.collider.attachedRigidbody;
            if (rb != null)
            {
                rb.AddForceAtPosition(shootPoint.forward * impactForce, hit.point, ForceMode.Impulse);
            }

            
            Target target = hit.collider.GetComponentInParent<Target>();
            if (target != null)
            {
                target.TakeImpact();
            }
        }
    }
}