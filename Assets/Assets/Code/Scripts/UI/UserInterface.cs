using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class UserInterface : MonoBehaviour
{
    private Canvas canvas;
    private Button spawnButton;
    private Slider healthSlider;
    private Image gameOverImage;
    private EnemySpawner enemySpawner;
   

    void Start()
    {
        canvas = GetComponent<Canvas>();
        spawnButton = canvas.GetComponentInChildren<Button>();
        healthSlider = canvas.GetComponentInChildren<Slider>();

        Transform gameOverTransform = canvas.transform.Find("GameOver");
        if (gameOverTransform != null)
        {
            gameOverImage = gameOverTransform.GetComponent<Image>();
            gameOverImage.enabled = false;
        }

        GameObject gameManager = GameObject.Find("GameManager");
        enemySpawner = gameManager.GetComponent<EnemySpawner>();

        SetButtonListener();
    }

    private void SetButtonListener()
    {
        spawnButton.onClick.AddListener(enemySpawner.SpawnEnemy);
    }

    public void SetHealthBarPercent(float percent)
    {
        if (healthSlider)
        {
            healthSlider.value = percent;
        }
    }
   
    public void GameOverScreen()
    {
        gameOverImage.enabled = true;
    }
}
