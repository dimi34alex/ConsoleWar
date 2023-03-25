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
        //hp.value = 100;
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    public void Damage()
    {
        hp.value -= 1;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "damageObject")
        {
            hp.value -= 1;
        }
    }
}
