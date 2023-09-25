using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurHealthUI : MonoBehaviour
{
    public GameObject player;
    public GameObject health0;
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;
    public GameObject health4;

    float playerHealth;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = player.GetComponent<Player>().GetCurHealth();

        switch (playerHealth)
        {
            case 0:
                health0.SetActive(false);
                health1.SetActive(false);
                health2.SetActive(false);
                health3.SetActive(false);
                health4.SetActive(false);
                break;
            case 1:
                health0.SetActive(true);
                health1.SetActive(false);
                health2.SetActive(false);
                health3.SetActive(false);
                health4.SetActive(false);
                break;
            case 2:
                health0.SetActive(true);
                health1.SetActive(true);
                health2.SetActive(false);
                health3.SetActive(false);
                health4.SetActive(false);
                break;
            case 3:
                health0.SetActive(true);
                health1.SetActive(true);
                health2.SetActive(true);
                health3.SetActive(false);
                health4.SetActive(false);
                break;
                case 4:
                health0.SetActive(true);
                health1.SetActive(true);
                health2.SetActive(true);
                health3.SetActive(true);
                health4.SetActive(false);
                break;
            case 5:
                health0.SetActive(true);
                health1.SetActive(true);
                health2.SetActive(true);
                health3.SetActive(true);
                health4.SetActive(true);
                break;
        }
    }
}
