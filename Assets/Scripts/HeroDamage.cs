using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroDamage : MonoBehaviour
{
    [SerializeField] Slider hp;
    // Start is called before the first frame update
    void Start()
    {
        hp.value = 100;
    }

    // Update is called once per frame
    void Update()
    {
                
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "damageObject")
        {
            hp.value -= 1;
        }
    }
}
