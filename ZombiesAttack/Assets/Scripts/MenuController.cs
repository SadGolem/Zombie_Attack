using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI points;

    void Start()
    {
        points.text = "Max points:" + PlayerPrefs.GetInt("maxPoints").ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
