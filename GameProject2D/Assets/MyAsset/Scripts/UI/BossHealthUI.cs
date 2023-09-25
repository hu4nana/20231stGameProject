using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    public GameObject boss;
    public GameObject empty;
    public Image curHealthBar;
    
    float bossCurHealth;
    float currentAlpha = 1f;
    float fadeSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CurHealthUI();
    }
    void CurHealthUI()
    {
        if (boss.GetComponent<Monster>().GetCurHealth() <= 0)
        {
            if (currentAlpha > 0f)
            {
                currentAlpha -= fadeSpeed * Time.deltaTime;
                currentAlpha = Mathf.Clamp01(currentAlpha);

                UnityEngine.Color currentColor = empty.GetComponent<Image>().color;
                currentColor.a = currentAlpha;
                empty.GetComponent<Image>().color = currentColor;
            }
            else
            {
                // ���� ���� 0 ���ϰ� �Ǹ� ������Ʈ�� �����մϴ�.
                Destroy(gameObject);
            }

        }
        curHealthBar.fillAmount =
            boss.GetComponent<Monster>().GetCurHealth() / boss.GetComponent<Monster>().GetMaxHealth();
    }
    
}
