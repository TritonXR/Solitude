using UnityEngine;
using System.Collections;

public class PlotScript : MonoBehaviour
{
    public static PlotScript pScript;

    public GameObject playerShip;
    public Object enemyShip;
    public float shipSpawnDistance;
    public int firstWaveShips;
    public int secondWaveShips;
    public int bigWaveShips;
    public float finalWaveShips;
    public Object earthObject;
    public float earthToShipDistance;

    public Material shipMaterial;

    private AudioSource audioSource;
    private Vector3[] shipSpawns;
    private int shipCount = 0;

    public GameObject shipSphere;

    // Use this for initialization
    void Start()
    {
        pScript = this;
        audioSource = this.GetComponent<AudioSource>();

        StartCoroutine(Plot());


    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Plot()
    {

        //Anna intro tutorial
        SetPilotMode(false);
        audioSource.clip = AudioController.Intro;
        audioSource.Play();

        /*while (audioSource.isPlaying) {

          yield return null;

        }*/

        yield return new WaitForSeconds(2);

        SetPilotMode(true);

        for (int i = 0; i < firstWaveShips; i++)
        {
            spawnShip();
            yield return null;
        }

        audioSource.clip = AudioController.CombatTraining;
        audioSource.Play();

        while (shipCount > 0)
        {

            yield return null;

        }


        audioSource.clip = AudioController.Plasma;
        audioSource.Play();

        for (int i = 0; i < secondWaveShips; i++)
        {
            spawnShip();
            yield return null;

        }

        while (shipCount > 0)
        {

            yield return null;

        }

        SetPilotMode(false);

        audioSource.clip = AudioController.UpgradeTutorial;
        audioSource.Play();

        /* Do a bunch of upgrade stuff. This is important! */

        while (audioSource.isPlaying)
        {

            yield return null;

        }

        audioSource.clip = AudioController.AnotherWave;
        audioSource.Play();

        while (audioSource.isPlaying)
        {

            yield return null;

        }

        SetPilotMode(true);

        for (int i = 0; i < bigWaveShips; i++)
        {

            spawnShip();
            yield return null;
        }

        while (shipCount > 0)
        {

            yield return null;

        }

        audioSource.clip = AudioController.GoingHome;
        audioSource.Play();

        while (audioSource.isPlaying)
        {

            yield return null;

        }

        /* Cool hyperdrive sound and ship moving? */
        GameObject earth = (GameObject)GameObject.Instantiate(
          earthObject, this.transform.forward * 1000, this.transform.rotation);

        while (Mathf.Abs(Vector3.Distance(playerShip.transform.position, earth.transform.position))
                                            > earthToShipDistance)
        {

            playerShip.GetComponent<Rigidbody>().AddForce(this.transform.forward * 2, ForceMode.Impulse);
            yield return null;

        }

        playerShip.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(2);

        audioSource.clip = AudioController.Dilemma;
        audioSource.Play();

        while (audioSource.isPlaying)
        {

            yield return null;

        }

        bool choseAnna = false;

        /* Important choice goes here. VERY IMPORTANT! */


        if (choseAnna)
        {

            audioSource.clip = AudioController.AnnaTrust;
            audioSource.Play();

            for (int i = 0; i < finalWaveShips; i++)
            {
                spawnShip();
                yield return null;
            }


            yield return new WaitForSeconds(30);

            audioSource.clip = AudioController.SidedWithAnna;
            audioSource.Play();

        }

        else
        {

            audioSource.clip = AudioController.Disproval;
            audioSource.Play();

            for (int i = 0; i < finalWaveShips; i++)
            {
                spawnShip();
                yield return null;
            }

            while (shipCount > 0)
            {

                yield return null;

            }

        }

    }

    void SetPilotMode(bool mode)
    {

        float transparency = 1;

        //Show the world.
        if (mode)
        {

            audioSource.clip = AudioController.PilotMode;
            audioSource.Play();
            transparency = 0;

            shipSphere.SetActive(false);

        }

        //Hide the world
        else
        {
            shipSphere.SetActive(true);
        }

        /*Color changeColor = shipMaterial.color;
        changeColor.a = transparency;
        shipMaterial.SetColor("_Color", changeColor);*/

    }

    //call this when a ship is destroyed
    public void ReduceShipNumber()
    {

        shipCount--;

    }

    GameObject spawnShip()
    {
        shipCount++;

        Vector3 spawnLocation = new Vector3(playerShip.transform.position.x + Random.Range(-shipSpawnDistance, shipSpawnDistance) * 2,
                                              playerShip.transform.position.y + Random.Range(-shipSpawnDistance, shipSpawnDistance) * 2,
                                              playerShip.transform.position.z + Random.Range(-shipSpawnDistance, shipSpawnDistance) * 2);


        GameObject newShip = (GameObject)GameObject.Instantiate(enemyShip, spawnLocation, this.transform.rotation);
        newShip.GetComponent<EnemyFiring>().playerObject = playerShip;
        return newShip;


    }

}
