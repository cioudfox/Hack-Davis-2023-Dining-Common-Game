using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamgePopup : MonoBehaviour
{
    public static DamgePopup Create(Vector3 position, int damageAmount, bool isCriticalHit)
    {
        Transform damagePopupTransform = Instantiate(Resources.Load<Transform>("DamagePopup"), position, Quaternion.identity);
        DamgePopup damagePopup = damagePopupTransform.GetComponent<DamgePopup>();
        damagePopup.Setup(damageAmount, isCriticalHit);
        return damagePopup;
    }
    private TextMeshPro textMesh;
    private const float DISAPPEAR_TIMER_MAX = 1.0f;
    private float disappearTimer;
    private Color textColor;
    private float disappearSpeed = 3.0f;
    private Vector3 moveVector;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount, bool isCriticalHit)
    {
        textMesh.SetText(damageAmount.ToString());
        if (!isCriticalHit)
        {
            textMesh.fontSize = 5;
            ColorUtility.TryParseHtmlString("yellow", out textColor); 
        }
        else
        {
            textMesh.fontSize = 7;
            ColorUtility.TryParseHtmlString("red", out textColor); 
        }
        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;

        moveVector = new Vector3(1,1) * 2.0f;
    }

    void Update()
    {   

        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 0.8f * Time.deltaTime;

        if (disappearTimer > DISAPPEAR_TIMER_MAX * 0.5f)
        {
            // First half of pop up
            transform.localScale += Vector3.one * 1.0f * Time.deltaTime;
        }
        else
        {
            transform.localScale -= Vector3.one * 1.0f * Time.deltaTime;
        }




        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor; 
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }

    }
}
