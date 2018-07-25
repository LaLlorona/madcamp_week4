using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
public class CardPreview : NetworkBehaviour
{
   

    public void Start()
    {
        
    }

    public void ShowCards(SyncListInt cardlist)
    {
        for(int cnt=0; cnt<cardlist.Count(); cnt++)
        {
            Debug.Log("SHOWCARDS");
        }
    }
}

