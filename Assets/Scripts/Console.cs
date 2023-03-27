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
    [SerializeField] float regenAmount = 10f; // ���������� ������ ��������, ����������������� �� ���� ����
    [SerializeField] float regenDelay = 0.1f; // �������� ����� ��������������� �������� � ��������

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
                text.text += "������� ������:\n";
            }
            if (command == "I love pizza")
            {
                text.text += "������� �������:\n";
            }
            if (command == "/reboot all")
            {
                blackPlain.SetActive(true);
                textComp.gameObject.SetActive(true);
                text.gameObject.SetActive(false);
                textComp.text = "�� ������� ������������� ���������. ��� ���������� �� ������ �� ��, � ���� �� ��������, ���� ��� ���������.";
            }
            else if (command == "/destroy all")
            {
                blackPlain.SetActive(true);
                text.gameObject.SetActive(false);
                textComp.gameObject.SetActive(true);
                textComp.text = "�� ���������� ��������� ��� ���������. ��� ������� �� ������, ��� � ��������. ���� �����, �����-������ ��� ��������� �����.";
            }
            else if (command == "/stop all")
            {
                blackPlain.SetActive(true);
                text.gameObject.SetActive(false);
                textComp.gameObject.SetActive(true);
                textComp.text = "�� ���������� ���������. ��� ����� �� ����� � ����� �� ���-�� �� ����, ����� ��� ���������?..";
            }
        }
        else
        {
            if (command == "/hide")
            {
                Debug.Log("����");
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
                Debug.Log("���");
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
            text.text = "������� �����:\n";
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
        float maxHealth = 100f; // ������������ �������� ��������
        float currentHealth = hp.value; // ������� �������� ��������

        while (currentHealth < maxHealth)
        {
            currentHealth += regenAmount;
            hp.value = currentHealth;
            yield return new WaitForSeconds(regenDelay);
        }
    }
}
