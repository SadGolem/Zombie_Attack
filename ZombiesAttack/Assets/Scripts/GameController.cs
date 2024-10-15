
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private SpawnZombie spawnZombie;
    [SerializeField] private TextMeshProUGUI textPoints;
    [SerializeField] private GameObject gameOverWindow;
    [SerializeField] private TextMeshProUGUI textPointsEnd;
    [SerializeField] private TextMeshProUGUI textMaxPointsEnd;
    private int maxPoints = 0;
    private int currentPoints = 0;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        gameOverWindow.SetActive(false);
        Parameters.points = 0;
        textPoints.text = "Points: 0";
        maxPoints = PlayerPrefs.GetInt("maxPoints");
        textMaxPointsEnd.text = maxPoints.ToString();
        Time.timeScale = 1;
        SpawnZombie();
    }

    private void Update()
    {
        if (Parameters.points > currentPoints)
        {
            currentPoints = Parameters.points;
            UpdatePoints();
        }
    }
    public void UpdatePoints()
    {
        textPoints.text = "Points:" + Parameters.points.ToString();
    }

    public void StopGame()
    {
        SavePoints();
        textMaxPointsEnd.text = "Max points: " + maxPoints.ToString();
        textPointsEnd.text = "Points: " + Parameters.points.ToString();
        gameOverWindow.SetActive(true);
        Time.timeScale = 0;
    }

    void SavePoints()
    {
        if (Parameters.points > maxPoints)
        {
            maxPoints = Parameters.points;
            PlayerPrefs.SetInt("maxPoints", maxPoints);           
        }
    }

    void SpawnZombie()
    {
        spawnZombie.enabled = true;
    }
}
