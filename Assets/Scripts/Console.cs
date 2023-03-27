using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class Console : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject tpPointPrefad;
    [SerializeField] GameObject player;
    [SerializeField] Slider hp;

    public TextMeshProUGUI textComp;
    public GameObject blackPlain;
    [SerializeField] float regenAmount = 10f; // количество единиц здоровья, восстанавливаемых за один кадр
    [SerializeField] float regenDelay = 0.1f; // задержка между восстановлением здоровья в секундах

    private bool active;
    bool end;
    private GameObject tpPoint;
    public HeroMovement heroScript;
    // Start is called before the first frame update
    void Start()
    {
        Activity(false);
        blackPlain.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        end = heroScript.end;
        if (Input.GetKeyDown(KeyCode.LeftAlt) && end)
        {
            if (active == true)
                Activity(false);
            else
            {
                Activity(true, true);
                inputField.ActivateInputField();
            }
        }
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
        if (end)
        {
            if (command == "Sushi")
            {
                text.text += "Введите пароль:\n";
            }
            if (command == "I love pizza")
            {
                text.text += "Введите команду:\n";
            }
            if (command == "/reboot all")
            {
                blackPlain.SetActive(true);
                textComp.gameObject.SetActive(true);
                text.gameObject.SetActive(false);
                textComp.text = "Вы успешно перезагрузили Вселенную. Она совершенно не похожа на то, к чему вы привыкли, зато она настоящая.";
            }
            else if (command == "/destroy all")
            {
                blackPlain.SetActive(true);
                text.gameObject.SetActive(false);
                textComp.gameObject.SetActive(true);
                textComp.text = "Вы уничтожили известную вам Вселенную. Она погибла во взрыве, как и родилась. Быть может, когда-нибудь она возникнет снова.";
            }
            else if (command == "/stop all")
            {
                blackPlain.SetActive(true);
                text.gameObject.SetActive(false);
                textComp.gameObject.SetActive(true);
                textComp.text = "Вы остановили симуляцию. Мир встал на паузу — придёт ли кто-то за вами, чтобы это исправить?..";
            }
        }
        else
        {
            if (command == "/hide")
            {
                Debug.Log("Хайд");
                GameObject[] scene = GameObject.FindObjectsOfType<GameObject>();
                foreach (GameObject gameObject in scene)
                {
                    if (gameObject.tag == "Platform")
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
                    if (gameObject.tag == "Platform")
                    {
                        gameObject.SetActive(true);
                    }
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
            else if (command == "/spawn platforms")
            {
                GameObject[] scene = GameObject.FindGameObjectsWithTag("Platform");
                foreach (GameObject gameObject in scene)
                {
                    gameObject.SetActive(true);
                }
            }
            Activity(false);
        }
        

    }
    public void Activity(bool active1, bool end = false)
    {
        if (active1)
        {
            active = true;
            text.gameObject.SetActive(true);
            panel.gameObject.SetActive(true);
            inputField.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            active = false;
            text.gameObject.SetActive(false);
            panel.gameObject.SetActive(false);
            inputField.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
        if (end)
        {
            active = true;
            text.gameObject.SetActive(true);
            text.text = "Введите логин:\n";
            panel.gameObject.SetActive(true);
            inputField.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void End(string command)
    {
        
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
