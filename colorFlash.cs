using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff : MonoBehaviour
{
    [SerializeField]
    private string playerTag;

    [SerializeField]
    private float debuffTime;

    private basicControl playerControl;

    private float currentTime;

    private bool DebuffBool;

    private GameObject bubbleEffect;

    private SpriteRenderer sr;

    private Coroutine flashCoroutine;

    void Start()
    {
        DebuffBool = false;
        currentTime = 0;
        playerControl = GameObject.FindGameObjectWithTag(playerTag).GetComponent<basicControl>();
        bubbleEffect = GameObject.FindGameObjectWithTag(playerTag).transform.GetChild(3).gameObject;
        sr = GameObject.FindGameObjectWithTag(playerTag).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (DebuffBool)
        { 
            //bubbleEffect.SetActive(true);

            currentTime += Time.deltaTime;

            if (currentTime >= debuffTime)
            {
                StopAllCoroutines();
                sr.color = Color.white;
                currentTime = 0;
                DebuffBool = false;
                playerControl.moveable = true;
                bubbleEffect.SetActive(false);
            }
        }
    }

    public void StopMoving()
    {
        soundManager.Instance.playHitSound(.5f);
        Flash(Color.red);
        DebuffBool = true;
        playerControl.moveable = false;
        bubbleEffect.SetActive(true);
    }

    private IEnumerator flashRoutine(Color changeColor, float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);

            sr.color = changeColor;

            yield return new WaitForSeconds(time);

            sr.color = Color.white;
        }

    }

    private void Flash(Color color)
    { 
       flashCoroutine = StartCoroutine(flashRoutine(color, .1f));
    }

}
