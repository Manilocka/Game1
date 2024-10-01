using UnityEngine;

public class Rock : MonoBehaviour
{
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
        UpdatePlateActivation(); // Проверка состояния плиты
    }

    void HandlePlateActivation(Vector2 position)
    {
        Collider2D plateCollider = Physics2D.OverlapCircle(position, 0.1f, plateMask);
        if (plateCollider != null)
        {
            Plate plate = plateCollider.GetComponent<Plate>();
            if (plate != null && IsRockOnPlate())
            {
                plate.Activate(); // Активируем плиту
                currentPlate = plate; // Сохраняем текущую плиту
            }
        }
    }

    void UpdatePlateActivation()
    {
        if (currentPlate != null && !IsRockOnPlate()) // Если плита активирована, но камень на ней нет
        {
            currentPlate.Deactivate(); // Деактивируем плиту
            currentPlate = null; // Сбрасываем ссылку на активированную плиту
        }
    }

    bool IsRockOnPlate()
    {
        // Проверяем, стоит ли камень на плите
        Vector2 position = (Vector2)transform.position;
        Collider2D plateCollider = Physics2D.OverlapCircle(position, 0.1f, plateMask);
        return plateCollider != null;
    }

    public void MoveRock(Vector2 moveDirection)
    {
        Vector2 targetPosition = (Vector2)transform.position + moveDirection.normalized;

        // Проверка на столкновение с деревьями и другими камнями
        if (!Physics2D.OverlapCircle(targetPosition, 0.1f, rockMask) && 
            !Physics2D.OverlapCircle(targetPosition, 0.1f, treeMask))
        {
            rb.MovePosition(targetPosition); // Двигаем камень
        }
    }
}