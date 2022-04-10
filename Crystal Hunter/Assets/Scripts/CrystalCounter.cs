using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CrystalCounter : MonoBehaviour
{
    private int totalCrystals = 0;
    public int crystalsToWin = 3;
    public Text countText;

    void Start()
    {
        countText.text = $"0/{crystalsToWin.ToString()}";
    }

    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crystal"))
        {
            totalCrystals += 1;
            Debug.Log("Подобран кристалл");
            countText.text = $"{totalCrystals.ToString()}/{crystalsToWin.ToString()}";
            // Скрываем кристалл
            //this.GetComponent<Renderer>().enabled = false;
            Destroy(other.gameObject);

            if (totalCrystals >= crystalsToWin)
            {
                Debug.Log("Победа!");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
