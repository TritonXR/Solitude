using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

    public AudioClip intro, combatTraining, plasma, anotherWave, confirmed, control,
                     disproval, dilemma, excellentChoice, goingHome, humanPath, pilotMode,
                     repairingShip, annaTrust, sidedWithAnna, upgradeTutorial,
                     targetDestroyed;
                     
	public static AudioClip Intro, CombatTraining, Plasma, AnotherWave, Confirmed, Control,
	  Disproval, Dilemma, ExcellentChoice, GoingHome, HumanPath, PilotMode,
	  RepairingShip, AnnaTrust, SidedWithAnna, UpgradeTutorial,
	  TargetDestroyed;

	// Use this for initialization
	void Start () {
	
	  Intro = intro;
	  CombatTraining = combatTraining;
	  Plasma = plasma;
	  AnotherWave = anotherWave;
	  Confirmed = confirmed;
	  Control = control;
	  Disproval = disproval;
	  Dilemma = dilemma;
	  ExcellentChoice = excellentChoice;
	  GoingHome = goingHome;
	  HumanPath = humanPath;
	  PilotMode = pilotMode;
	  RepairingShip = repairingShip;
	  AnnaTrust = annaTrust;
	  SidedWithAnna = sidedWithAnna;
	  UpgradeTutorial = upgradeTutorial;
	  TargetDestroyed = targetDestroyed;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
