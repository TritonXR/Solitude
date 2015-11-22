using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ControlScript : MonoBehaviour {

    public float timeToControl = 30;

    public float shotDelay = .1f;
    public Object laserObject;
    public float laserSpeed = 25;
    public float accuracyOffset;
    public float laserDamage;
    public List<GameObject> targets;
    public GameObject[] turrets;

    // Use this for initialization
    void Start () {
        this.GetComponent<EnemyFiring>().enabled = false;
        this.transform.Find("Helping").gameObject.SetActive(true);

	}
	
    public void Initialize(List<GameObject> targets)
    {
        this.targets = targets;
        StartCoroutine(fireLasers());
    }

	// Update is called once per frame
	void Update () {
        timeToControl -= Time.deltaTime;

        if (timeToControl < 0)
        {
            this.transform.Find("Helping").gameObject.SetActive(false);
            this.GetComponent<EnemyFiring>().enabled = true;
            enabled = false;
        }
	}

    IEnumerator fireLasers()
    {
        while (true)
        {

            yield return new WaitForSeconds(shotDelay);

            for (int i = 0; i < turrets.Length; i++)
            {
                if (targets.Count != 0)
                {
                    GameObject lasers = (GameObject)GameObject.Instantiate(laserObject, turrets[i].transform.position, turrets[i].transform.rotation);
                    lasers.GetComponent<Laser>().Initialize(true, laserSpeed, accuracyOffset, laserDamage, targets[0].transform.position);
                }
                yield return null;

            }
            yield return null;

        }

    }
}
