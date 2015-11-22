using UnityEngine;
using System.Collections;

public class EnemyFiring : MonoBehaviour {
    public float shotDelay;
    public Object laserObject;
    public float laserSpeed;
    public float accuracyOffset;
    public float laserDamage;
    public GameObject playerObject;
    public GameObject[] turrets;

    // Use this for initialization
    void Start () {

        StartCoroutine(fireLasers());
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator fireLasers()
    {

        while (true)
        {

            yield return new WaitForSeconds(shotDelay);

            for (int i = 0; i < turrets.Length; i++)
            {
                GameObject lasers = (GameObject)GameObject.Instantiate(laserObject, turrets[i].transform.position, turrets[i].transform.rotation);
                lasers.GetComponent<Laser>().Initialize(true, laserSpeed, accuracyOffset, laserDamage, playerObject.transform.position);
            }
            yield return null;

        }

    }
}
