using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    public static Game Instance;

    [HideInInspector] public List<Route> readyRoutes = new();

    private int totalRoutes;
    private int successfullParks;

    //events:
    public UnityAction<Route> OnCarEntersPark;
    public UnityAction OnCarCollision;

    // Thêm biến cho UI
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject defeatPanel;
    [SerializeField] private GameObject completePanel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        totalRoutes = transform.GetComponentsInChildren<Route>().Length;
        successfullParks = 0;

        OnCarEntersPark += OnCarEntersParkHandler;
        OnCarCollision += OnCarCollisionHandler;

        // Ẩn các panel UI lúc khởi đầu
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(false);
        completePanel.SetActive(false);
    }

    private void OnCarCollisionHandler()
    {
        Debug.Log("GameOver!");

        defeatPanel.SetActive(true); // Hiển thị UI thua

        DOVirtual.DelayedCall(2f, () => {
            Time.timeScale = 1f; // Dừng thời gian
        });
    }

    private void OnCarEntersParkHandler(Route route)
    {
        route.car.StopDancingAnim();
        successfullParks++;

        if (successfullParks == totalRoutes)
        {
            Debug.Log("You Win!");
            int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;

            if (nextLevel < SceneManager.sceneCountInBuildSettings)
            {
                victoryPanel.SetActive(true); // Hiển thị UI thắng
                Time.timeScale = 1f; // Dừng thời gian
            }
            else
            {
                // Nếu không còn level nào khác, hiện UI hoàn thành game
                completePanel.SetActive(true);
                Time.timeScale = 1f; // Dừng thời gian
            }
        }
    }

    public void RegisterRoute(Route route)
    {
        readyRoutes.Add(route);

        if (readyRoutes.Count == totalRoutes)
            MoveAllCars();
    }

    private void MoveAllCars()
    {
        foreach (var route in readyRoutes)
            route.car.Move(route.linePoints);
    }

    // Các hàm UI button gọi đến:
    public void ContinueGame()
    {
        Time.timeScale = 1f; // Tiếp tục thời gian
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextLevel);
    }

    public void RetryGame()
    {
        Time.timeScale = 1f; // Tiếp tục thời gian
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f; // Tiếp tục thời gian
        SceneManager.LoadScene("Main Menu"); // Tên Scene Menu của bạn
    }
}
