using UnityEngine;

public class Rock : MonoBehaviour
{
    public LayerMask plateMask;
    public LayerMask rockMask;
    public LayerMask treeMask; 

    private Rigidbody2D rb;
    private Plate currentPlate; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandlePlateActivation(transform.position);
        UpdatePlateActivation(); 
    }

    void HandlePlateActivation(Vector2 position)
    {
        Collider2D plateCollider = Physics2D.OverlapCircle(position, 0.1f, plateMask);
        if (plateCollider != null)
        {
            Plate plate = plateCollider.GetComponent<Plate>();
            if (plate != null && IsRockOnPlate())
            {
                plate.Activate(); 
                currentPlate = plate; 
            }
        }
    }

    void UpdatePlateActivation()
    {
        if (currentPlate != null && !IsRockOnPlate()) 
        {
            currentPlate.Deactivate(); 
            currentPlate = null; 
        }
    }

    bool IsRockOnPlate()
    {
        Vector2 position = (Vector2)transform.position;
        Collider2D plateCollider = Physics2D.OverlapCircle(position, 0.1f, plateMask);
        return plateCollider != null;
    }

    public void MoveRock(Vector2 moveDirection)
    {
        Vector2 targetPosition = (Vector2)transform.position + moveDirection.normalized;

        if (!Physics2D.OverlapCircle(targetPosition, 0.1f, rockMask) && 
            !Physics2D.OverlapCircle(targetPosition, 0.1f, treeMask))
        {
            rb.MovePosition(targetPosition); 
        }
    }
}