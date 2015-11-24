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
    public GameObject upgrades;

    public GameObject pSphere;

    private AudioSource audioSource;
    private Vector3[] shipSpawns;
    private int shipCount = 0;



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

        while (audioSource.isPlaying)
        {

            yield return null;

        }

        yield return new WaitForSeconds(2);

        SetPilotMode(true);

        audioSource.clip = AudioController.CombatTraining;
        audioSource.Play();

        /*while (audioSource.isPlaying)
        {

            yield return null;

        }*/

        for (int i = 0; i < firstWaveShips; i++)
        {
            spawnShip();
            shipCount++;
            yield return null;
        }

        while (shipCount > 0)
        {
            yield return null;

        }


        audioSource.clip = AudioController.Plasma;
        audioSource.Play();

        /*while (audioSource.isPlaying)
        {

            yield return null;

        }*/

        SetPilotMode(true);


        for (int i = 0; i < secondWaveShips; i++)
        {
            spawnShip();
            shipCount++;
            yield return null;

        }

        while (shipCount > 0)
        {

            yield return null;

        }

        SetPilotMode(false);

        audioSource.clip = AudioController.UpgradeTutorial;
        audioSource.Play();

        Vector3 upgradeSpawn = new Vector3(Camera.main.transform.position.x,
                                           Camera.main.transform.position.y,
                                           Camera.main.transform.position.z + 1);

        GameObject upgradeSystem = (GameObject)GameObject.Instantiate(upgrades,
                                    upgradeSpawn, this.transform.rotation);

        while (!upgradeSystem.GetComponent<UpgradeSystem>().isDone())
        {

            yield return null;

        }

        upgradeSystem.SetActive(false);
        audioSource.clip = AudioController.AnotherWave;
        audioSource.Play();

        SetPilotMode(true);

        while (audioSource.isPlaying)
        {

            yield return null;

        }

        for (int i = 0; i < bigWaveShips; i++)
        {

            spawnShip();
            shipCount++;
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

        upgradeSystem.SetActive(true);

        upgradeSpawn = new Vector3(Camera.main.transform.position.x,
                                   Camera.main.transform.position.y,
                                   Camera.main.transform.position.z + 1);

        /* Important choice goes here. VERY IMPORTANT! */
        upgradeSystem.GetComponent<UpgradeSystem>().displayFinalOption();
        upgradeSystem.transform.position = upgradeSpawn;

        SetPilotMode(true);

        while (!upgradeSystem.GetComponent<UpgradeSystem>().finalChoiceMade())
        {

            yield return null;

        }

        bool choseAnna = upgradeSystem.GetComponent<UpgradeSystem>().getEndChoice();
        upgradeSystem.SetActive(false);

        if (choseAnna)
        {

            audioSource.clip = AudioController.AnnaTrust;
            audioSource.Play();
            while (audioSource.isPlaying)
            {

                yield return null;

            }

            SetPilotMode(true);

            for (int i = 0; i < finalWaveShips; i++)
            {
                spawnShip();
                shipCount++;
                yield return null;
            }

            while (shipCount > 0)
            {
                yield return null;

            }

            audioSource.clip = AudioController.SidedWithAnna;
            audioSource.Play();

        }

        else
        {

            audioSource.clip = AudioController.Disproval;
            audioSource.Play();
            while (audioSource.isPlaying)
            {

                yield return null;

            }

            SetPilotMode(true);

            for (int i = 0; i < finalWaveShips; i++)
            {
                spawnShip();
                shipCount++;
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

        if (mode)
        {

            audioSource.clip = AudioController.PilotMode;
            audioSource.Play();
            //transparency = 0;
            pSphere.SetActive(false);

        }

        else
        {
            pSphere.SetActive(true);

        }

        //Color changeColor = playerShip.GetComponent<MeshRenderer>().material.color;
        //changeColor.a = transparency;
        //playerShip.GetComponent<MeshRenderer>().material.SetColor ("_Color", changeColor);

    }

    //call this when a ship is destroyed
    public void ReduceShipNumber()
    {

        shipCount--;

    }

    GameObject spawnShip()
    {

        Vector3 spawnLocation = new Vector3(playerShip.transform.position.x + Random.Range(-shipSpawnDistance, shipSpawnDistance) * 2,
                                              playerShip.transform.position.y + Random.Range(-shipSpawnDistance, shipSpawnDistance) * 2,
                                              playerShip.transform.position.z + Random.Range(-shipSpawnDistance, shipSpawnDistance) * 2);


        GameObject newShip = (GameObject)GameObject.Instantiate(enemyShip, spawnLocation, this.transform.rotation);
        newShip.GetComponent<EnemyFiring>().playerObject = playerShip;
        return newShip;


    }

}
