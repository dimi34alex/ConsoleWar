using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class Console : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] GameObject text;
    [SerializeField] GameObject tpPointPrefad;
    [SerializeField] GameObject player;
    [SerializeField] Slider hp;

    [SerializeField] float regenAmount = 10f; // количество единиц здоровья, восстанавливаемых за один кадр
    [SerializeField] float regenDelay = 0.1f; // задержка между восстановлением здоровья в секундах

    private bool active;
    private GameObject tpPoint;
    // Start is called before the first frame update
    void Start()
    {
        Activity(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (active == true)
                Activity(false);
            else
            {
                Activity(true);
                inputField.ActivateInputField();
            }
        }
    }

    public void InCommand(string command)
    {
        if (command == "/hide")
        {
            Debug.Log("Хайд");
            GameObject[] scene = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject gameObject in scene)
            {
                if (gameObject.tag == "CanHide")
                {
                    gameObject.SetActive(false);
                }
            }
        }
        else if (command == "/show")
        {
            Debug.Log("Шов");
            GameObject[] scene = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (GameObject gameObject in scene)
            {
                gameObject.SetActive(true);
            }
        }
        else if (command == "/revive")
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        else if (command == "/create tp point")
        {
            tpPoint = Instantiate(tpPointPrefad, player.transform.position, Quaternion.identity, null);
        }
        else if (command == "/tp to point")
        {
            player.transform.position = tpPoint.transform.position;
        }
        else if (command == "/hp regen")
        {
            StartCoroutine(RegenHealth());
        }
        Activity(false);
    }
    private void Activity(bool active1)
    {
        if (active1)
        {
            active = true;
            text.SetActive(true);
            panel.gameObject.SetActive(true);
            inputField.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            active = false;
            text.SetActive(false);
            panel.gameObject.SetActive(false);
            inputField.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    private IEnumerator RegenHealth()
    {
        float maxHealth = 100f; // максимальное значение здоровья
        float currentHealth = hp.value; // текущее значение здоровья

        while (currentHealth < maxHealth)
        {
            currentHealth += regenAmount;
            hp.value = currentHealth;
            yield return new WaitForSeconds(regenDelay);
        }
    }
}
