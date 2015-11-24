using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{

    public int pointsToSpend;
    private int upgradeTier;
    private bool done = false;
    private bool pressed = false;

    public Button[] humanButtons = new Button[3];
    public Button[] annaButtons = new Button[3];

    public Button finalHumanButton;
    public Button finalAnnaButton;
    private bool finalChoice = false;
    private bool decided;

    //Bool array for tiers: true = annaChosen, false = playerChosen
    public bool[] upgradeChoices;

    // Use this for initialization
    void Start()
    {
        upgradeChoices = new bool[3];
        humanButtons[1].interactable = false;
        humanButtons[2].interactable = false;
        annaButtons[1].interactable = false;
        annaButtons[2].interactable = false;


        StartCoroutine(Upgrade());

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Upgrade()
    {

        AudioSource audioSource = this.GetComponent<AudioSource>();
        int tierNumber = 0;

        while (tierNumber < upgradeChoices.Length)
        {

            if (SixenseInput.Controllers[0].Trigger != 0)
            {
                if (!pressed)
                {
                    pressed = true;
                    audioSource.clip = AudioController.Disproval;
                    audioSource.Play();

                    upgradeChoices[tierNumber] = false; //anna not chosen
                    annaButtons[tierNumber].interactable = false; //disable

                    //Gatling lasers unlocked!
                    if (tierNumber == 1)
                    {
                        InputScript.iScript.continuousLasers = true;
                    }

                    else if (tierNumber == 2)
                    {
                        InputScript.iScript.leftShield.SetActive(true);
                        InputScript.iScript.rightShield.SetActive(true);

                    }

                    else if (tierNumber == 3)
                    {

                    }

                    tierNumber++;

                    if (tierNumber < upgradeChoices.Length)
                    {
                        annaButtons[tierNumber].interactable = true;
                        humanButtons[tierNumber].interactable = true;
                    }
                }
            }

            else if (SixenseInput.Controllers[1].Trigger != 0)
            {
                if (!pressed)
                {
                    pressed = true;
                    audioSource.clip = AudioController.ExcellentChoice;
                    audioSource.Play();

                    upgradeChoices[tierNumber] = true; //anna chosen
                    humanButtons[tierNumber].interactable = false;

                    //More RAM unlocked!
                    if (tierNumber == 1)
                    {
                        InputScript.iScript.maxActiveTargets = 5;
                    }

                    else if (tierNumber == 2)
                    {
                        InputScript.iScript.concurrentTargets = 3;
                    }

                    else if (tierNumber == 3)
                    {

                    }

                    tierNumber++;

                    if (tierNumber < upgradeChoices.Length)
                    {
                        annaButtons[tierNumber].interactable = true;
                        humanButtons[tierNumber].interactable = true;
                    }
                }
            }

            else
            {
               pressed = false;
            }

            yield return null;

        }

        done = true;
    }

    public void displayFinalOption()
    {

        finalHumanButton.gameObject.SetActive(true);
        finalAnnaButton.gameObject.SetActive(true);

        StartCoroutine(getFinalChoice());

    }

    IEnumerator getFinalChoice()
    {

        decided = false;

        while (!decided)
        {

            if (SixenseInput.Controllers[0].Trigger != 0)
            {

                finalChoice = false;
                decided = true;

            }

            else if (SixenseInput.Controllers[1].Trigger != 0)
            {

                finalChoice = true;
                decided = true;

            }


            yield return null;
        }
    }

    public bool finalChoiceMade()
    {

        return decided;

    }

    public bool getEndChoice()
    {

        return finalChoice;

    }

    public bool[] getUpgradeChoices()
    {

        return upgradeChoices;

    }

    public bool isDone()
    {

        return done;

    }
}
