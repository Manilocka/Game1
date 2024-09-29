using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public GameObject grassPrefab;
    public GameObject rockPrefab;
    public GameObject treePrefab;
    public GameObject platePrefab;
    public GameObject characterPrefab;

    private const int width = 10;
    private const int height = 10;
    private const float cellSize = 1.0f; // Размер одной ячейки поля

    // Определение макета поля
    private string[,] boardLayout = new string[,]
    {
        {"G", "G", "G", "G", "G", "G", "G", "G", "G", "G"},
        {"G", "P", "G", "G", "G", "R", "G", "G", "G", "G"},
        {"G", "G", "G", "G", "T", "G", "G", "G", "G", "G"},
        {"G", "G", "P", "G", "G", "R", "G", "G", "G", "G"},
        {"G", "G", "G", "G", "G", "G", "G", "G", "G", "G"},
        {"G", "G", "G", "G", "G", "G", "G", "G", "T", "G"},
        {"G", "G", "R", "G", "G", "G", "G", "G", "G", "G"},
        {"G", "G", "G", "G", "G", "G", "G", "P", "G", "G"},
        {"G", "G", "G", "G", "G", "G", "G", "G", "G", "G"},
        {"G", "G", "G", "G", "G", "C", "G", "G", "G", "G"},
    };

    void Start()
    {
        GenerateBoard();
        MoveCharacterToCenter();
    }

   void GenerateBoard()
    {
        // Сначала создаем поле из травы
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(x * cellSize, y * cellSize, 0); // трава на Z=0
                Instantiate(grassPrefab, position, Quaternion.identity);
            }
        }

        // Теперь создаем предметы в соответствии с макетом
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                string element = boardLayout[y, x];

                // Создаем элементы, основываясь на макете
                if (element == "T")
                {
                    Instantiate(treePrefab, new Vector3(x * cellSize, y * cellSize, 0.1f), Quaternion.identity); // деревья на Z=0.1
                }
                else if (element == "P")
                {
                    Instantiate(platePrefab, new Vector3(x * cellSize, y * cellSize, 0.1f), Quaternion.identity); // плиты на Z=0.1
                }
                else if (element == "R")
                {
                    Instantiate(rockPrefab, new Vector3(x * cellSize, y * cellSize, 0.1f), Quaternion.identity); // камни на Z=0.1
                }
                else if (element == "C")
                {
                    Instantiate(characterPrefab, new Vector3(x * cellSize, y * cellSize, 0.1f), Quaternion.identity); // персонаж на Z=0.1
                }
            }
        }
    }   
    void MoveCharacterToCenter()
    {
        // Получаем персонажа по тегу (или используем другой способ)
        GameObject character = GameObject.FindGameObjectWithTag("Player"); // Предполагаем, что персонаж имеет тег "Player"
        if (character != null)
        {
            // Перемещаем персонажа в центр поля
            Vector3 centerPosition = new Vector3((width - 1) / 2.0f * cellSize, (height - 1) / 2.0f * cellSize, 1); // Устанавливаем Z на 1
            character.transform.position = centerPosition;

            // Ограничиваем движение персонажа в пределах 100 px от границ поля
            character.transform.position = new Vector3(
                Mathf.Clamp(character.transform.position.x, -100 + cellSize / 2, width * cellSize + 100 - cellSize / 2),
                Mathf.Clamp(character.transform.position.y, -100 + cellSize / 2, height * cellSize + 100 - cellSize / 2),
                character.transform.position.z);
        }
    }
}