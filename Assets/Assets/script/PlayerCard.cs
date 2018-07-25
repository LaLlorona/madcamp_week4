using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;

public class PlayerCard : NetworkBehaviour
{

    [SyncVar(hook = "onHandChanged")] int HandScore = -10;
    public SyncListInt iList = new SyncListInt();
    public GameObject CardView_;
    public GameObject previewCard;
    public GameObject card, user_;
  
    public GameObject sphere;
    private TextMeshProUGUI CurrentStatus;
    private TextMeshProUGUI WinSign;
    private TextMeshProUGUI LooseSign;
    


    //public TextMeshProUGUI timerText = GameObject.Find("WinOrLoose");

    //public List<int> iList = new List<int>();

    public void SetHandScore(int k)
    {
        HandScore = k;
    }

    public void Start()
    {
        ClientScene.RegisterPrefab(card);
        ClientScene.RegisterPrefab(user_);
        previewCard = Instantiate(CardView_, this.transform);
        previewCard.transform.parent = this.transform;
        previewCard.transform.localPosition = new Vector3(0, 1.8f, 0);

        CurrentStatus = GameObject.Find("WinOrLoose").GetComponent<TextMeshProUGUI>();
        WinSign = GameObject.Find("WinSign").GetComponent<TextMeshProUGUI>();
        LooseSign = GameObject.Find("LooseSign").GetComponent<TextMeshProUGUI>();


    }

    public string[] NumToSuitsAndNumber(int k)
    {
        string[] suits = { "clubs", "diamonds", "hearts", "spades" };
        string suit = suits[k / 13];
        string[] ranks = { "1", "10", "11", "12", "13", "2", "3", "4", "5", "6", "7", "8", "9" };
        string rank = ranks[k % 13];

        string[] result = { suit, rank };

        return result;




    }

    public string[,] HandInString(SyncListInt thislist)
    {
        List<int> CurrentHand = new List<int>();
        string[,] CurrentHandInString = new string[5, 2]; //change to poker game format 



        //collision.gameObject.GetComponent<PokerGame>().PlayScore();
        foreach (int element in iList)
        {
            CurrentHand.Add(element);

        }
        int i = 0;
        foreach (int element in iList)
        {
            CurrentHandInString[i, 0] = NumToSuitsAndNumber(element)[0];
            CurrentHandInString[i, 1] = NumToSuitsAndNumber(element)[1];

            i++;

        }
        return CurrentHandInString;
    }



    public void OnCollisionEnter(Collision collision)
    {
        int result1;

        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovementScript>().isAlive && this.GetComponent<PlayerMovementScript>().isAlive )
        {
            // FIGHT!


            List<int> CurrentHand = new List<int>();
            string[,] CurrentHandInString = new string[5, 2]; //change to poker game format 



            //collision.gameObject.GetComponent<PokerGame>().PlayScore();
            foreach (int element in iList)
            {
                CurrentHand.Add(element);

            }
            int i = 0;
            foreach (int element in CurrentHand)
            {
                CurrentHandInString[i, 0] = NumToSuitsAndNumber(element)[0];
                CurrentHandInString[i, 1] = NumToSuitsAndNumber(element)[1];

                i++;

            }



            Debug.Log("both player's card is" + collision.gameObject.GetComponent<PlayerCard>().BuildList());
            Debug.Log("both handscore is" + HandScore);

            result1 = GameObject.Find("TimerAndSound").GetComponent<PokerGame>().CompareHands(CurrentHandInString, collision.gameObject.GetComponent<PlayerCard>().HandInString(collision.gameObject.GetComponent<PlayerCard>().iList));

            if(result1 == 0)
            {
                if (isLocalPlayer)
                {
                    Debug.Log("You Win!");
                    WinSign.text = "WayToGo!";
                    //System.Threading.Thread.Sleep(3);
                    //WinSign.text = "";
                 
                }
            }
            else if(result1 ==1)
            {
                //removing its renderer in server
                RpcRemoveRender();
                /*
                Renderer[] renderes = this.GetComponentsInChildren<Renderer>();

                foreach (Renderer ren in renderes)
                {
                    ren.enabled = false;
                    //this part is should be on islocalplayer
                }
                */
                //NetworkServer.Destroy(this.gameObject);






                if (isLocalPlayer) //if you loose, then set it 
                {
                    LooseSign.text = "You Lost All Money";
                    CmdKill();
                    foreach (int element in iList)
                    {
                        CmdDropACards(element);
                    }
                    iList.Clear();
                }
            }


            /*




            if (isLocalPlayer)


            {//승패시 결과 처리 부분 
                
                if (result1 == 0)
                {
                    //ThisCanvas.
                    Debug.Log("You Win!");
                    WinSign.text = "WayToGo!";


                }
                else
                {
                    Debug.Log("You lost!");
                    LooseSign.text = "You Lost All Money";
                    this.GetComponent<PlayerMovementScript>().isAlive = false;
                    //this.GetComponent<MeshRenderer>().enabled = false;
                    //this.GetComponent<Collider>().enabled = false;
                    foreach (int element in iList)
                    {
                        CmdDropACards(element);
                    }


                    //iList.Clear();

                    //Rpcannounce(); //이렇게 하면 렌더링 초기화가 안됌 
                    Renderer[] renderes = GetComponentsInChildren<Renderer>();
                    foreach (Renderer ren in renderes)
                    {
                        ren.enabled = false;

                 
                    }






                }
                

            }*/
        }
    }
    [ClientRpc]
    public void RpcRemoveRender()
    {
        Renderer[] renderes = this.GetComponentsInChildren<Renderer>();

        foreach (Renderer ren in renderes)
        {
            ren.enabled = false;
        //this part is should be on islocalplayer
        }

    }
    [Command]
    public void CmdKill()
    {
        this.GetComponent<PlayerMovementScript>().isAlive = false;
    }
    public int len()
    {
        Debug.Log("The LENGTH OF LIST IS " + iList.Count());
        return iList.Count();
    }

    public void addCard(int cardcode)
    {
        if (this.len() >= 5)
        {
            Debug.Log("Card Number Exceeds 5 : discarding first hand & re-spawn");


          
            CmdSpawn(iList[0]);
            iList.RemoveAt(0);
        }

        
        iList.Add(cardcode);
        
        Rpcannounce();
        Debug.Log("add" + cardcode);
        Debug.Log("your current hand is" + BuildList());
    }

    [Command]
    public void CmdDropACards(int number)
    {
        float radii = 15;
        float phi = UnityEngine.Random.Range(0, 360) * 2 * Mathf.PI / 180; // To RAD
        float theta = UnityEngine.Random.Range(0, 360) * 2 * Mathf.PI / 180;
        GameObject newCardG = (GameObject)Instantiate(card, transform.position, Quaternion.identity);
        newCardG.GetComponent<Rigidbody>().AddForce(transform.forward * 10);
        var newCard = newCardG.GetComponent<CardInit>();
        newCard.num = number;
        NetworkServer.SpawnWithClientAuthority(newCardG, connectionToClient);

        //GameObject newSphere = (GameObject)Instantiate(sphere, new Vector3(radii * Mathf.Cos(theta), radii * Mathf.Sin(theta) * Mathf.Cos(phi), radii * Mathf.Sin(theta) * Mathf.Sin(phi)), Quaternion.identity);
        //NetworkServer.SpawnWithClientAuthority(newSphere, connectionToClient);
    }

    [Command]
    public void CmdSpawn(int num)
    {
        float radii = 15;
        float phi = UnityEngine.Random.Range(0, 360) * 2 * Mathf.PI / 180; // To RAD
        float theta = UnityEngine.Random.Range(0, 360) * 2 * Mathf.PI / 180;
        GameObject newCardG = (GameObject) Instantiate(card, new Vector3(radii * Mathf.Cos(theta), radii * Mathf.Sin(theta) * Mathf.Cos(phi), radii * Mathf.Sin(theta) * Mathf.Sin(phi)), Quaternion.identity);
        var newCard = newCardG.GetComponent<CardInit>();
        newCard.num = iList[0];
        NetworkServer.SpawnWithClientAuthority(newCardG, connectionToClient);

        //GameObject newSphere = (GameObject)Instantiate(sphere, new Vector3(radii * Mathf.Cos(theta), radii * Mathf.Sin(theta) * Mathf.Cos(phi), radii * Mathf.Sin(theta) * Mathf.Sin(phi)), Quaternion.identity);
        //NetworkServer.SpawnWithClientAuthority(newSphere, connectionToClient);
    }



    public void onHandChanged(int k)
    {

        HandScore = k;
        HandUpdate();


    }
    public void HandUpdate()
    {
        if (isLocalPlayer)
        {

            CurrentStatus.text = GameObject.Find("TimerAndSound").GetComponent<PokerGame>().PlayText(HandInString(iList));

        }
    }
    public string BuildList()
    {
        string res = "[";
        foreach (int item in iList)
        {
            res += item;
            res += ",";
        }
        res += "]";
        return res;
    }

    [ClientRpc]
    public void Rpcannounce()
    {
        // TO SHOW
        CardView_.gameObject.transform.GetChild(0);
        if (iList.Count() == 0)
        {
            for (int idx = 0; idx < this.iList.Count(); idx++)
            {
                Renderer renderer = previewCard.transform.GetChild(idx).GetComponent<Renderer>();
                Texture2D cardbg = (Texture2D)PlanetScript.Textures[iList[idx] * 2];
                renderer.material.mainTexture = cardbg;
            }

        }

        for (int idx = 0; idx < this.iList.Count(); idx++)
        {
            Renderer renderer = previewCard.transform.GetChild(idx).GetComponent<Renderer>();
            Texture2D cardbg = (Texture2D)PlanetScript.Textures[iList[idx] * 2];
            renderer.material.mainTexture = cardbg;
        }

    }

}