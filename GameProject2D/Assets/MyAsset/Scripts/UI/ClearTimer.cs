using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearTimer : MonoBehaviour
{
    float sec;
    int min;

    [SerializeField]
    GameObject record;

    [SerializeField]
    Text timerText;
    [SerializeField]
    Text recordText;
    [SerializeField]
    GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }
    void Timer()
    {
        if (boss==null)
        {
            record.SetActive(true);
            recordText.text=timerText.text;
        }
        else
        {
            record.SetActive(false);
            sec += Time.deltaTime;
            timerText.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);

            if ((int)sec > 59)
            {
                sec = 0;
                min++;
            }
        }
        

    }
}
