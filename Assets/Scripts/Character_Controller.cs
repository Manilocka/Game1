
// using UnityEngine;

// public class Character_Controller : MonoBehaviour
// {
//     public float moveSpeed = 5f;
//     public LayerMask plateMask; 
//     public LayerMask rockMask; 

//     private Rigidbody2D rb;

//     private void Start()
//     {
//         rb = GetComponent<Rigidbody2D>(); 
//     }

//     private void FixedUpdate() 
//     {
//         Move();
//     }

//     void Move()
//     {
//         float moveX = Input.GetAxis("Horizontal");
//         float moveY = Input.GetAxis("Vertical");

//         Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
//         Vector2 targetPosition = (Vector2)transform.position + moveSpeed * Time.fixedDeltaTime * moveDirection;

//         if (!Physics2D.OverlapCircle(targetPosition, 0.1f, rockMask))
//         {
//             rb.MovePosition(targetPosition);
//             HandlePlateActivation(targetPosition);
//         }
//         else
//         {
//             HandleRockPush(transform.position, moveDirection);
//         }
//     }

//     void HandlePlateActivation(Vector2 position)
//     {
//         Collider2D plateCollider = Physics2D.OverlapCircle(position, 0.1f, plateMask);
//         if (plateCollider != null)
//         {
//             Plate plate = plateCollider.GetComponent<Plate>();
//             if (plate != null && (IsPlayerOnPlate() || IsRockOnPlate(plate)))
//             {
//                 plate.Activate(); // Активируем плиту только если на ней стоит персонаж или камень
//             }
//         }
//     }

    // bool IsPlayerOnPlate()
    // {
    //     // Проверяем, стоит ли персонаж на плите
    //     Vector2 position = (Vector2)transform.position;
    //     Collider2D plateCollider = Physics2D.OverlapCircle(position, 0.1f, plateMask);
    //     return plateCollider != null;
    // }

//     bool IsRockOnPlate(Plate plate)
//     {
//         // Проверяем, стоит ли камень на данной плите
//         Vector2 position = plate.transform.position;
//         return Physics2D.OverlapCircle(position, 0.1f, rockMask) != null;
//     }

//     void HandleRockPush(Vector2 currentPosition, Vector2 moveDirection)
//     {
//         float playerHitRadius = 1f; 
//         float targetPositionCheckRadius = 0.1f; 

//         Collider2D hit = Physics2D.OverlapCircle(currentPosition, playerHitRadius, rockMask);

//         if (hit != null)
//         {
//             if (hit.TryGetComponent<Rock>(out var rock))
//             {
//                 Vector2 rockTargetPosition = (Vector2)rock.transform.position + moveDirection.normalized;

//                 bool isTargetPositionFree = 
//                     !Physics2D.OverlapCircle(rockTargetPosition, targetPositionCheckRadius, rockMask) && 
//                     !Physics2D.OverlapCircle(rockTargetPosition, targetPositionCheckRadius, LayerMask.GetMask("tree")) && 
//                     (!Physics2D.OverlapCircle(rockTargetPosition, targetPositionCheckRadius, plateMask) || IsPlaceOnPlate(rockTargetPosition));

//                 if (isTargetPositionFree)
//                 {
//                     rock.GetComponent<Rigidbody2D>().MovePosition(rockTargetPosition); 
//                 }
//             }
//         }
//     }

//     bool IsPlaceOnPlate(Vector2 position)
//     {
//         // Проверяем, находится ли данная позиция на плите
//         Collider2D plateCollider = Physics2D.OverlapCircle(position, 0.1f, plateMask);
//         return plateCollider != null;
//     }
// }
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    
    public float moveSpeed = 5f;
    public LayerMask plateMask; 
    public LayerMask rockMask; 

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    private void FixedUpdate() 
    {
        Move();
        UpdatePlateStates(); // Обновите состояния плит в каждом FixedUpdate
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        Vector2 targetPosition = (Vector2)transform.position + moveSpeed * Time.fixedDeltaTime * moveDirection;

        if (!Physics2D.OverlapCircle(targetPosition, 0.1f, rockMask))
        {
            rb.MovePosition(targetPosition);
        }
        else
        {
            HandleRockPush(transform.position, moveDirection);
        }
    }

    void UpdatePlateStates()
    {
        // Обновите состояние всех плит на поле
        Plate[] plates = FindObjectsOfType<Plate>();
        foreach (Plate plate in plates)
        {
            if (plate.IsPlayerOrRockOnPlate(LayerMask.GetMask("Player"), rockMask))
            {
                plate.Activate();
            }
            else
            {
                plate.Deactivate();
            }
        }
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
                    !Physics2D.OverlapCircle(rockTargetPosition, targetPositionCheckRadius, LayerMask.GetMask("tree")) && 
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
        // Проверяем, находится ли данная позиция на плите
        Collider2D plateCollider = Physics2D.OverlapCircle(position, 0.1f, plateMask);
        return plateCollider != null;
    }

    public bool IsPlayerOrRockOnPlate(LayerMask playerMask, LayerMask rockMask)
    {
        // Проверяем, есть ли игрок или камень на плите
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, 0.1f, playerMask);
        Collider2D rockCollider = Physics2D.OverlapCircle(transform.position, 0.1f, rockMask);

        // Отладочный вывод для проверки
        if (playerCollider != null)
        {
            Debug.Log("Player is on the plate");
        }
        if (rockCollider != null)
        {
            Debug.Log("Rock is on the plate");
        }

        return playerCollider != null || rockCollider != null;
    }

}