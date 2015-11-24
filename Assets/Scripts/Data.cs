using UnityEngine;
using System.Collections;

public class Data : MonoBehaviour
{

    public float health; //ship health
    public float shipSpeed; //ship speed

    public Object explosion; //explosion prefab

    private bool enemy = true; //is this an enemy?

    private bool counted = false;

    // Use this for initialization
    void Start()
    {

        if (this.tag == "Player") enemy = false;


    }

    // Update is called once per frame
    void Update()
    {


    }

    public bool isEnemy()
    {

        return enemy;

    }

    public Vector3 getLocation()
    {

        return this.transform.position;

    }

    //does damage to this ship
    public void damage(float value)
    {

        health -= value; //decrements health

        //if the ship drops below 0 health
        if (health <= 0 && !counted)
        {
            counted = true;
            PlotScript.pScript.ReduceShipNumber();
            GameObject.Instantiate(explosion, this.transform.position, this.transform.rotation);
            GameObject.Destroy(this.gameObject);

        }
    }
}
