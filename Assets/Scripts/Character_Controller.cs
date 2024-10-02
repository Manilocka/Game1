using UnityEngine;

public class Character_Controller : MonoBehaviour
{

    public float moveSpeed = 5f;
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
        Move();
        UpdatePlateActivation(); 
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        Vector2 targetPosition = (Vector2)transform.position + moveSpeed * Time.fixedDeltaTime * moveDirection;


        bool canMoveToTarget = !Physics2D.OverlapCircle(targetPosition, 0.1f, rockMask);
        canMoveToTarget &= !Physics2D.OverlapCircle(targetPosition, 0.1f, treeMask); 

        if (canMoveToTarget)
        {
            rb.MovePosition(targetPosition);
            HandlePlateActivation(targetPosition);
        }
        else
        {
            HandleRockPush(transform.position, moveDirection);
        }
    }

    void HandlePlateActivation(Vector2 position)
    {
        Collider2D plateCollider = Physics2D.OverlapCircle(position, 0.1f, plateMask);
        if (plateCollider != null)
        {
            Plate plate = plateCollider.GetComponent<Plate>();
            if (plate != null && (IsPlayerOnPlate() || IsRockOnPlate(plate)))
            {
                plate.Activate();
                currentPlate = plate; 
            }
        }
    }

    void UpdatePlateActivation()
    {
        if (currentPlate != null && !IsPlayerOnPlate()) 
        {
            currentPlate.Deactivate(); 
            currentPlate = null; 
        }
    }

    bool IsPlayerOnPlate()
    {
        Vector2 position = (Vector2)transform.position;
        Collider2D plateCollider = Physics2D.OverlapCircle(position, 0.1f, plateMask);
        return plateCollider != null;
    }

    bool IsRockOnPlate(Plate plate)
    {
        Vector2 position = plate.transform.position;
        return Physics2D.OverlapCircle(position, 0.1f, rockMask) != null;
    }

    void HandleRockPush(Vector2 currentPosition, Vector2 moveDirection)
    {
        float playerHitRadius = 1f; 
        float targetPositionCheckRadius = 0.1f; 

        Collider2D hit = Physics2D.OverlapCircle(currentPosition, playerHitRadius, rockMask);

        if (hit != null)
        {
            if (hit.TryGetComponent<Rock>(out var rock))
            {
                Vector2 rockTargetPosition = (Vector2)rock.transform.position + moveDirection.normalized;

                bool isTargetPositionFree = 
                    !Physics2D.OverlapCircle(rockTargetPosition, targetPositionCheckRadius, rockMask) && 
                    !Physics2D.OverlapCircle(rockTargetPosition, targetPositionCheckRadius, treeMask) && 
                    (!Physics2D.OverlapCircle(rockTargetPosition, targetPositionCheckRadius, plateMask) || IsPlaceOnPlate(rockTargetPosition));

                if (isTargetPositionFree)
                {
                    rock.GetComponent<Rigidbody2D>().MovePosition(rockTargetPosition);
                }
            }
        }
    }

    bool IsPlaceOnPlate(Vector2 position)
    {

        Collider2D plateCollider = Physics2D.OverlapCircle(position, 0.1f, plateMask);
        return plateCollider != null;
    }
}

