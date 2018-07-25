using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player_Health : NetworkBehaviour {
    [SyncVar(hook = "OnHealthChanged")] private int health = 100; //network 변수 health 를 정의하고, 이를 바꾸는 함수의 이름은 on health change 를 사용함 

    private Text healthText;
    private bool shouldDie = false;
    public bool isDead = false;

    public delegate void DieDelegate();

    public event DieDelegate Eventdie;


	// Use this for initialization
	void Start () {
        healthText = GameObject.Find("Health_Text").GetComponent<Text>();
        SetHealthText();

		
	}

    // Update is called once per frame
    void Update()
    {
        CheckCondition();
    }
    void CheckCondition()
    {
        if(health <= 0 && !shouldDie && !isDead)
        {
            shouldDie = true;
        }

        if(health<=0 && shouldDie)
        {
            if(Eventdie != null)
            {
                Eventdie();
            }
            shouldDie = false;
        }
    }
    int GetHealth()
    {
        return health;
    }

    void SetHealthText()
    {
        if(isLocalPlayer)
        {
            healthText.text =  "Health " + health.ToString();
        }
    }
    public void DeductHealth(int dmg)
    {
        health -= dmg;
    }
    void OnHealthChanged(int hith)
    {
        health = hith;
        SetHealthText();
    }
}
