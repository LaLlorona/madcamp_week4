using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;

public class PlanetScript : NetworkBehaviour
{

    public float gravity = -12;
    public GameObject card;

    public bool first;
    public static Object[] Textures;

    

    private void Start()
    {
        Debug.Log("START PLANET");
        
        

        Textures = Resources.LoadAll("Textures", typeof(Texture2D));

        NetworkServer.ClearSpawners();

        foreach (var t in Textures)
        {
            Debug.Log(t.name);
        }
        CmdSpawnCards();



    }

    [Command]
    public void CmdSpawnCards()
    {
        for (int i = 0; i < 52; ++i)
        {
            float radii = 15;
            float phi = Random.Range(0, 360) * 2 * Mathf.PI / 180; // To RAD
            float theta = Random.Range(0, 360) * 2 * Mathf.PI / 180;
            var newCardG = Instantiate(card, new Vector3(radii * Mathf.Cos(theta), radii * Mathf.Sin(theta) * Mathf.Cos(phi), radii * Mathf.Sin(theta) * Mathf.Sin(phi)), Quaternion.identity);
            var newCard = newCardG.GetComponent<CardInit>();


            
            
            Renderer renderer = newCardG.GetComponent<Renderer>();
            Texture2D cardbg = (Texture2D) Textures[2*i];
            renderer.material.mainTexture = cardbg;
                
            newCard.num = i;
            Attract(newCardG.transform);
            NetworkServer.Spawn(newCardG);
            

        }

    }

    public void Attract(Transform playerTransform)
    {
        Vector3 gravityUp = (playerTransform.position - transform.position).normalized;
        Vector3 localUp = playerTransform.up;

        playerTransform.GetComponent<Rigidbody>().AddForce(gravityUp * gravity);

        Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * playerTransform.rotation;
        playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, 50f * Time.deltaTime);
    }




}
