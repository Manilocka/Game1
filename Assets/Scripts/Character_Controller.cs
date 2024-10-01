using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask plateMask;
    public LayerMask rockMask;
    public LayerMask treeMask; // Маска для деревьев

    private Rigidbody2D rb;
    private Plate currentPlate; // Хранит текущую активированную плиту

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        UpdatePlateActivation(); // Проверка состояния плиты
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        Vector2 targetPosition = (Vector2)transform.position + moveSpeed * Time.fixedDeltaTime * moveDirection;

        // Проверка на столкновение с камнями и деревьями
        bool canMoveToTarget = !Physics2D.OverlapCircle(targetPosition, 0.1f, rockMask);
        canMoveToTarget &= !Physics2D.OverlapCircle(targetPosition, 0.1f, treeMask); // Проверка на деревья

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
                plate.Activate(); // Активируем плиту
                currentPlate = plate; // Сохраняем текущую плиту
            }
        }
    }

    void UpdatePlateActivation()
    {
        if (currentPlate != null && !IsPlayerOnPlate()) // Если плита активирована, но персонажа на ней нет
        {
            currentPlate.Deactivate(); // Деактивируем плиту
            currentPlate = null; // Сбрасываем ссылку на активированную плиту
        }
    }

    bool IsPlayerOnPlate()
    {
        // Проверяем, стоит ли персонаж на плите
        Vector2 position = (Vector2)transform.position;
        Collider2D plateCollider = Physics2D.OverlapCircle(position, 0.1f, plateMask);
        return plateCollider != null;
    }

    bool IsRockOnPlate(Plate plate)
    {
        // Проверяем, стоит ли камень на данной плите
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
                    !Physics2D.OverlapCircle(rockTargetPosition, targetPositionCheckRadius, treeMask) && // Проверка на деревья
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
}



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
//                 plate.Activate(); 
//             }
//         }
//     }

//     bool IsPlayerOnPlate()
//     {
//         Vector2 position = (Vector2)transform.position;
//         Collider2D plateCollider = Physics2D.OverlapCircle(position, 0.1f, plateMask);
//         return plateCollider != null;
//     }

//     bool IsRockOnPlate(Plate plate)
//     {

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
//         Collider2D plateCollider = Physics2D.OverlapCircle(position, 0.1f, plateMask);
//         return plateCollider != null;
//     }
// }




// using UnityEngine;

// public class Character_Controller : MonoBehaviour
// {
//     public float moveSpeed = 5f;
//     public LayerMask plateMask;
//     public LayerMask rockMask;

//     private Rigidbody2D rb;
//     private Plate currentPlate; // Хранит текущую активированную плиту

//     private void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//     }

//     private void FixedUpdate()
//     {
//         Move();
//         UpdatePlateActivation(); // Проверка состояния плиты
//     }

//     void Move()
//     {
//         float moveX = Input.GetAxis("Horizontal");
//         float moveY = Input.GetAxis("Vertical");

//         Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
//         Vector2 targetPosition = (Vector2)transform.position + moveSpeed * Time.fixedDeltaTime * moveDirection;

//         // Проверка на столкновение с камнями
//         bool canMoveToTarget = !Physics2D.OverlapCircle(targetPosition, 0.1f, rockMask);
        
//         // Проверка на столкновение с деревьями
//         canMoveToTarget &= !Physics2D.OverlapCircle(targetPosition, 0.1f, LayerMask.GetMask("tree"));

//         if (canMoveToTarget)
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
//                 plate.Activate(); // Активируем плиту
//                 currentPlate = plate; // Сохраняем текущую плиту
//             }
//         }
//     }

//     void UpdatePlateActivation()
//     {
//         if (currentPlate != null && !IsPlayerOnPlate()) // Если плита активирована, но персонажа на ней нет
//         {
//             currentPlate.Deactivate(); // Деактивируем плиту
//             currentPlate = null; // Сбрасываем ссылку на активированную плиту
//         }
//     }

//     bool IsPlayerOnPlate()
//     {
//         // Проверяем, стоит ли персонаж на плите
//         Vector2 position = (Vector2)transform.position;
//         Collider2D plateCollider = Physics2D.OverlapCircle(position, 0.1f, plateMask);
//         return plateCollider != null;
//     }

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
//             if (plate != null)
//             {
//                 if (IsPlayerOnPlate() || IsRockOnPlate(plate))
//                 {
//                     plate.Activate(); // Activates the plate if player or rock is on it
//                 }
//                 else
//                 {
//                     plate.Deactivate(); // Deactivates the plate if both are not on it
//                 }
//             }
//         }
//     }

//     bool IsPlayerOnPlate()
//     {
//         Vector2 position = (Vector2)transform.position;
//         Collider2D plateCollider = Physics2D.OverlapCircle(position, 0.1f, plateMask);
//         return plateCollider != null;
//     }

//     bool IsRockOnPlate(Plate plate)
//     {
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
//                     HandlePlateActivation(rockTargetPosition); // Check for plate activation after moving the rock
//                 }
//             }
//         }
//     }

//     bool IsPlaceOnPlate(Vector2 position)
//     {
//         Collider2D plateCollider = Physics2D.OverlapCircle(position, 0.1f, plateMask);
//         return plateCollider != null;
//     }
// }

