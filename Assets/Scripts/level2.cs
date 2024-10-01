
using UnityEngine;

public class level2 : MonoBehaviour
{
    public GameObject grassPrefab;
    public GameObject rockPrefab;
    public GameObject treePrefab;
    public GameObject platePrefab;
    public GameObject characterPrefab;

    private const int width = 10;
    private const int height = 10;
    private const float cellSize = 1.0f;

    private string[,] boardLayout = new string[,]
    {
        {"G", "G", "G", "G", "G", "G", "G", "G", "G", "G"},
        {"G", "P", "G", "G", "G", "R", "G", "G", "G", "G"},
        {"G", "G", "G", "G", "T", "G", "G", "G", "G", "G"},
        {"G", "G", "P", "G", "G", "R", "G", "G", "G", "G"},
        {"G", "G", "G", "G", "G", "G", "G", "G", "G", "G"},
        {"G", "G", "G", "G", "G", "G", "G", "G", "T", "G"},
        {"G", "G", "R", "G", "C", "G", "G", "G", "G", "G"},
        {"G", "G", "G", "G", "G", "G", "G", "P", "G", "G"},
        {"G", "G", "G", "G", "G", "G", "G", "G", "G", "G"},
        {"G", "G", "G", "G", "G", "G", "G", "G", "G", "G"},
    };

    void Start()
    {
        GenerateBoard();
        MoveCharacterToCenter();
    }
    
    void GenerateBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(x * cellSize, y * cellSize, 0);
                Instantiate(grassPrefab, position, Quaternion.identity);
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                string element = boardLayout[y, x];

                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    Instantiate(treePrefab, new Vector3(x * cellSize, y * cellSize, 0.1f), Quaternion.identity);
                }
                else
                {
                    if (element == "T")
                    {
                        Instantiate(treePrefab, new Vector3(x * cellSize, y * cellSize, 0.1f), Quaternion.identity);
                    }
                    else if (element == "P")
                    {
                        // Создаем плиту и устанавливаем её индекс
                        Plate newPlate = Instantiate(platePrefab, new Vector3(x * cellSize, y * cellSize, 0.1f), Quaternion.identity).GetComponent<Plate>();
                        newPlate.index = y * width + x; // Устанавливаем индекс для плиты
                    }
                    else if (element == "R")
                    {
                        Instantiate(rockPrefab, new Vector3(x * cellSize, y * cellSize, 0.1f), Quaternion.identity);
                    }
                    else if (element == "C")
                    {
                        Instantiate(characterPrefab, new Vector3(x * cellSize, y * cellSize, 0.1f), Quaternion.identity);
                    }
                }
            }
        }
    }
    
    void MoveCharacterToCenter()
    {
        GameObject character = GameObject.FindGameObjectWithTag("Character"); 
        if (character != null)
        {
            Vector3 centerPosition = new Vector3((width - 1) / 2.0f * cellSize, (height - 1) / 2.0f * cellSize, 1); 
            character.transform.position = centerPosition;

            character.transform.position = new Vector3(
                Mathf.Clamp(character.transform.position.x, -100 + cellSize / 2, width * cellSize + 100 - cellSize / 2),
                Mathf.Clamp(character.transform.position.y, -100 + cellSize / 2, height * cellSize + 100 - cellSize / 2),
                character.transform.position.z);
        }
    }
}