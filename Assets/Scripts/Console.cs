using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.SceneManagement;

public class Console : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject inputField;
    [SerializeField] GameObject help;
    [SerializeField] GameObject tpPointPrefad;
    [SerializeField] GameObject player;

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
                Activity(true);
        }
    }

    public void InCommand(string command)
    {
        if (command == "/hide")
        {
            Debug.Log("����");
            Activity(false);
            GameObject[] scene = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject gameObject in scene)
            {
                if (gameObject.tag == "CanHide")
                {
                    gameObject.SetActive(false);
                }
            }
        }
        if (command == "/show")
        {
            Debug.Log("���");
            GameObject[] scene = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (GameObject gameObject in scene)
            {
                gameObject.SetActive(true);
            }
            Activity(false);
        }
        if (command == "/revive")
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (command == "/create tp point")
        {
            tpPoint = Instantiate(tpPointPrefad, player.transform.position, Quaternion.identity, null);
        }
        if (command == "/tp to point")
        {
            player.transform.position = tpPoint.transform.position;
        }
    }

    private void Activity(bool active1)
    {
        if (active1)
        {
            active = true;
            help.SetActive(true);
            panel.SetActive(true);
            inputField.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            active = false;
            help.SetActive(false);
            panel.SetActive(false);
            inputField.SetActive(false); 
            Time.timeScale = 1f;
        }
    }
}
