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

        // Переместим персонажа к центру поля
        MoveCharacterToCenter();
    }

    void GenerateBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(x * cellSize, y * cellSize, 0); // Учитываем размер клетки
                string element = boardLayout[y, x];

                // Создаем элементы, основываясь на макете
                if (element == "G")
                {
                    Instantiate(grassPrefab, position, Quaternion.identity);
                }
                else if (element == "T")
                {
                    Instantiate(treePrefab, position, Quaternion.identity);
                }
                else if (element == "P")
                {
                    Instantiate(platePrefab, position, Quaternion.identity);
                }
                else if (element == "R")
                {
                    Instantiate(rockPrefab, position, Quaternion.identity);
                }
                else if (element == "C")
                {
                    Instantiate(characterPrefab, position, Quaternion.identity);
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
            Vector3 centerPosition = new Vector3((width - 1) / 2.0f * cellSize, (height - 1) / 2.0f * cellSize, 0);
            character.transform.position = centerPosition;

            // Ограничиваем движение персонажа в пределах 100 px от границ поля
            character.transform.position = new Vector3(
                Mathf.Clamp(character.transform.position.x, -100 + cellSize / 2, width * cellSize + 100 - cellSize / 2),
                Mathf.Clamp(character.transform.position.y, -100 + cellSize / 2, height * cellSize + 100 - cellSize / 2),
                character.transform.position.z);
        }
    }
}
// using UnityEngine;

// public class GameBoard : MonoBehaviour
// {
//     public GameObject grassPrefab;
//     public GameObject rockPrefab;
//     public GameObject treePrefab;
//     public GameObject platePrefab;
//     public GameObject characterPrefab;

//     private const int width = 10;
//     private const int height = 10;

//     // Определение поля в виде двумерного массива
//     private string[,] boardLayout = new string[,]
//     {
//         {"G", "G", "G", "G", "G", "G", "G", "G", "G", "G"},
//         {"G", "P", "G", "G", "G", "R", "G", "G", "G", "G"},
//         {"G", "G", "G", "G", "T", "G", "G", "G", "G", "G"},
//         {"G", "G", "P", "G", "G", "R", "G", "G", "G", "G"},
//         {"G", "G", "G", "G", "G", "G", "G", "G", "G", "G"},
//         {"G", "G", "G", "G", "G", "G", "G", "G", "T", "G"},
//         {"G", "G", "R", "G", "G", "G", "G", "G", "G", "G"},
//         {"G", "G", "G", "G", "G", "G", "G", "P", "G", "G"},
//         {"G", "G", "G", "G", "G", "G", "G", "G", "G", "G"},
//         {"G", "G", "G", "G", "G", "C", "G", "G", "G", "G"},
//     };

//     void Start()
//     {
//         GenerateBoard();
//     }

//     void GenerateBoard()
//     {
//         for (int x = 0; x < width; x++)
//         {
//             for (int y = 0; y < height; y++)
//             {
//                 Vector3 position = new Vector3(x, y, 0);
//                 string element = boardLayout[y, x];

//                 switch (element)
//                 {
//                     case "G":
//                         Instantiate(grassPrefab, position, Quaternion.identity);
//                         break;
//                     case "R":
//                         Instantiate(rockPrefab, position, Quaternion.identity);
//                         break;
//                     case "T":
//                         Instantiate(treePrefab, position, Quaternion.identity);
//                         break;
//                     case "P":
//                         Instantiate(platePrefab, position, Quaternion.identity);
//                         break;
//                     case "C":
//                         Instantiate(characterPrefab, position, Quaternion.identity);
//                         break;
//                 }
//             }
//         }
//     }
// }
// using UnityEngine;

// public class GameBoard : MonoBehaviour
// {
//     public GameObject grassPrefab;
//     public GameObject rockPrefab;
//     public GameObject treePrefab;
//     public GameObject platePrefab;
//     public GameObject characterPrefab;

//     private const int width = 10;
//     private const int height = 10;

//     void Start()
//     {
//         GenerateBoard();
//     }

//     void GenerateBoard()
//     {
//         for (int x = 0; x < width; x++)
//         {
//             for (int y = 0; y < height; y++)
//             {
//                 Vector3 position = new Vector3(x, y, 0);
                
//                 int randomElement = Random.Range(0, 10);
//                 if (randomElement < 6)
//                 {
//                     Instantiate(grassPrefab, position, Quaternion.identity);
//                 }
//                 else if (randomElement < 8)
//                 {
//                     Instantiate(rockPrefab, position, Quaternion.identity);
//                 }
//                 else if (randomElement < 9)
//                 {
//                     Instantiate(treePrefab, position, Quaternion.identity);
//                 }
//                 else
//                 {
//                     Instantiate(platePrefab, position, Quaternion.identity);
//                 }
//             }
//         }

//         Vector3 characterPosition = new Vector3(Random.Range(0, width), Random.Range(0, height), 0);
//         Instantiate(characterPrefab, characterPosition, Quaternion.identity);
//     }
// }

