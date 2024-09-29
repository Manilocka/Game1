// using UnityEngine;

// public class Character_Controller : MonoBehaviour
// {
//     public float moveDistance = 0.1f;  
//     private Vector2 direction;

//     void Update()
//     {
//         // Получение ввода с клавиатуры
//         direction.x = Input.GetAxisRaw("Horizontal");
//         direction.y = Input.GetAxisRaw("Vertical");

//         // Если введены клавиши "W", "A", "S", "D" или стрелки, то перемещаем персонажа
//         if (direction != Vector2.zero)
//         {
//             Move(direction);
//         }
//     }

//     void Move(Vector2 direction)
//     {
//         // Рассчитаем новую позицию
//         Vector3 targetPosition = transform.position + (Vector3)direction * moveDistance;

//         // Проверка на столкновения
//         if (CanMoveTo(targetPosition))
//         {
//             transform.position = targetPosition;  // Перемещаем персонажа
//             // Проверка на возможность толкания камня
//             TryPushStone(targetPosition, direction);
//         }
//     }

//     bool CanMoveTo(Vector3 position)
//     {
//         RaycastHit2D hit = Physics2D.Raycast(transform.position, position - transform.position, moveDistance);
//         if (hit.collider != null && hit.collider.CompareTag("Rock"))
//         {
//             return false; // Не можем проходить через камни
//         }
//         return true; // Можно перемещать
//     }

//     void TryPushStone(Vector3 targetPosition, Vector2 direction)
//     {
//         RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, moveDistance);
//         if (hit.collider != null && hit.collider.CompareTag("Rock"))
//         {
//             Vector3 stoneTargetPosition = hit.collider.transform.position + (Vector3)direction * moveDistance;

//             if (CanMoveTo(stoneTargetPosition))
//             {
//                 hit.collider.transform.position = stoneTargetPosition; // Толкаем камень
//             }
//         }
//     }
// }

// using UnityEngine;

// public class Character_Controller : MonoBehaviour
// {
//     public float moveSpeed = 5f;
//     public LayerMask obstacleMask; 
//     public LayerMask plateMask; 
//     public GameObject rockPrefab;

//     void Update()
//     {
//         Move();
//     }

//     void Move()
//     {
//         Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
//         Vector2 moveDirection = moveInput.normalized;

//         Vector2 targetPosition = (Vector2)transform.position + moveDirection * moveSpeed * Time.deltaTime;

//         if (!Physics2D.OverlapCircle(targetPosition, 0.1f, obstacleMask))
//         {
//             transform.position = targetPosition;
//         }

//         HandlePlateActivation(targetPosition);
//         HandleRockPush(targetPosition);
//     }

//     void HandlePlateActivation(Vector2 position)
//     {
//         Collider2D plateCollider = Physics2D.OverlapCircle(position, 0.1f, plateMask);
//         if (plateCollider != null)
//         {
//             Plate plate = plateCollider.GetComponent<Plate>();
//             if (plate != null)
//             {
//                 plate.Activate();
//             }
//         }
//     }

//     void HandleRockPush(Vector2 position)
//     {
//         Collider2D rockCollider = Physics2D.OverlapCircle(position, 0.1f, obstacleMask);
//         if (rockCollider != null)
//         {
//             Rock rock = rockCollider.GetComponent<Rock>();
//             if (rock != null)
//             {
//                 Vector2 pushDirection = (position - (Vector2)transform.position).normalized;
//                 Vector2 rockTargetPosition = (Vector2)rock.transform.position + pushDirection;

//                 if (!Physics2D.OverlapCircle(rockTargetPosition, 0.1f, obstacleMask))
//                 {
//                     rock.transform.position = rockTargetPosition;
//                 }
//             }
//         }
//     }
// }
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public float moveSpeed = 10f;
    // public LayerMask obstacleMask; 
    public LayerMask plateMask; 
    public LayerMask rockMask; 

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        rockMask = LayerMask.GetMask("rockMask");
        plateMask = LayerMask.GetMask("plate");
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

      
        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        Vector2 targetPosition = (Vector2)transform.position + moveSpeed * Time.deltaTime * moveDirection;

      
        if (!Physics2D.OverlapCircle(targetPosition, 0.1f, rockMask))
        {
          
            rb.MovePosition(targetPosition);
            HandlePlateActivation(targetPosition);

        }else{
            HandleRockPush(transform.position,moveDirection);
        }
    }

    void HandlePlateActivation(Vector2 position)
    {
        Collider2D plateCollider = Physics2D.OverlapCircle(position, 0.1f, plateMask);
        if (plateCollider != null)
        {
            Plate plate = plateCollider.GetComponent<Plate>();
            if (plate != null)
            {
                plate.Activate(); // Активируем плиту
            }
        }
    }
void HandleRockPush(Vector2 currentPosition, Vector2 moveDirection)
{
    float playerHitRadius = 1f; 
    float targetPositionCheckRadius = 0.1f; 

    // Use the character's position to check for rock collision
    Collider2D hit = Physics2D.OverlapCircle(currentPosition, playerHitRadius, rockMask);

    Debug.Log("Hit rock: " + (hit != null)); 

    if (hit != null)
    {
        Debug.Log("Rock name: " + hit.name); 

        if (hit.TryGetComponent<Rock>(out var rock))
        {
            Vector3 moveDirection3d = new(moveDirection.x,moveDirection.y,0);
            // Calculate the target tile position (move exactly 1 unit in the moveDirection)
            Vector3 rockTargetPosition = rock.transform.position + rock.transform.TransformDirection(moveDirection3d);
            Debug.Log("Player pos:"+currentPosition+" rock target pos:"+ rockTargetPosition+" rock current pos: "+rock.transform.position);

            bool isTargetPositionFree =!Physics2D.OverlapCircle(rockTargetPosition, targetPositionCheckRadius, rockMask) && 
                                        !Physics2D.OverlapCircle(rockTargetPosition, targetPositionCheckRadius, LayerMask.GetMask("tree")) && 
                                        !Physics2D.OverlapCircle(rockTargetPosition, targetPositionCheckRadius, plateMask);

            Debug.Log("Is target position free: " + isTargetPositionFree); 

            if (isTargetPositionFree)
            {
                Debug.Log("Moving rock to position: " + rockTargetPosition);
                rock.GetComponent<Rigidbody2D>().MovePosition(rockTargetPosition); 
            }
        }
        else
        {
            Debug.Log("Rock component not found on the hit object."); 
        }
    }
}
    // void HandleRockPush(Vector2 moveDirection)
    // {         
    //     // Collider2D rockCollider = Physics2D.OverlapCircle(position, 0.1f, rockMask);
    //     // if (rockCollider != null)
    //     // {
    //     //     Rock rock = rockCollider.GetComponent<Rock>();
    //     //     if (rock != null)
    //     //     {
    //     //         rock.Activate(); // Активируем плиту
    //     //     }
    //     // }
    //     //Collider2D hit = Physics2D.OverlapCircle(moveDirection, 0.1f, rockMask);
    //     // RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, 2f, rockMask);
    //     // Radius for checking if the player hits the rock
    //     float playerHitRadius = 0.1f; 
    //     // Radius for checking if the target position is free
    //     float targetPositionCheckRadius = 0.5f; 

    //     Collider2D hit = Physics2D.OverlapCircle(moveDirection, playerHitRadius, rockMask);

    //     if (TryGetComponent<Rock>(out var rock))
    //     {
    //         if (rock != null) 
    //         {
    //             Vector2 rockTargetPosition = (Vector2)rock.transform.position + moveDirection;

    //             // Check if the target position is free from rocks, trees, and plates
    //             if (!Physics2D.OverlapCircle(rockTargetPosition, targetPositionCheckRadius, rockMask) && 
    //                 !Physics2D.OverlapCircle(rockTargetPosition, targetPositionCheckRadius, LayerMask.GetMask("Tree")) && 
    //                 !Physics2D.OverlapCircle(rockTargetPosition, targetPositionCheckRadius, LayerMask.GetMask("Plate")))
    //             {
    //                 // Move the rock to the target position
    //                 Debug.Log("Moving rock to position: " + rockTargetPosition);
    //                 rock.GetComponent<Rigidbody2D>().MovePosition(rockTargetPosition); 
    //             }
    //         }
    //     }
    // }

}