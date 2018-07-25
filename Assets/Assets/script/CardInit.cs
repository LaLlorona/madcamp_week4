using UnityEngine;
using UnityEngine.Networking;
public class CardInit : NetworkBehaviour
{
    
    [SyncVar]
    public int num;

    public void Start()
    {
        Renderer renderer = this.gameObject.GetComponent<Renderer>();
        Texture2D cardbg = (Texture2D)PlanetScript.Textures[num*2];
        renderer.material.mainTexture = cardbg;
    }

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerMovementScript>().isAlive==true)
            {
                //collission.gameObject.getcomponenet<player_Card>.add(4);


                /*
                GameObject Explostion = Instantiate(explosion, transform.position, transform.rotation);
                NetworkServer.SpawnWithClientAuthority(Explostion, connectionToClient);
                */
                CmdSendCard(collision.gameObject);
                CmdSendScore(collision.gameObject);

                //CmdSendCard(collision.gameObject);

                Debug.Log(this.num);
                Debug.Log("this card's suit is " + collision.gameObject.GetComponent<PlayerCard>().NumToSuitsAndNumber(num)[0] + " " + collision.gameObject.GetComponent<PlayerCard>().NumToSuitsAndNumber(num)[1]);
                Debug.Log("current hand is" + collision.gameObject.GetComponent<PokerGame>().PlayText(collision.gameObject.GetComponent<PlayerCard>().HandInString(collision.gameObject.GetComponent<PlayerCard>().iList)));


                NetworkServer.Destroy(this.gameObject);
            }

            


        }
        

    }
    [Command]
    public void CmdSendCard(GameObject player) //update card information to the player hand
    {
        player.GetComponent<PlayerCard>().addCard(num);
        
    }
    [Command]
    public void CmdSendScore(GameObject player) //based on updated card information, update current player hand score 
    {
        player.GetComponent<PlayerCard>().SetHandScore(player.GetComponent<PokerGame>().PlayScore(player.gameObject.GetComponent<PlayerCard>().HandInString(player.gameObject.GetComponent<PlayerCard>().iList)));
    }
}

    