﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour {
    public string Name;
    public Sprite look;
    public string[] Dialogue;
	public GameObject textbox;
	public GameObject can;
	public GameObject play;

    public bool hasQuest;
    public string whatQuest;

    private int dialogueSize;
    private bool isCreated = false;
	private GameObject tb;
    private string[] garbage;

	//text stuff
	private int curFrame = 0;
	private bool isUp = true;
	private bool isDown = false;
    private bool interacted = false;

	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sprite = look;
        dialogueSize = Dialogue.Length;
        
    }
	
	// Update is called once per frame
	void Update () {
        WithinRange();
	}

	//if the player is within a distance of NPC show dialogue
	void WithinRange(){
        float distance = Mathf.Pow(play.transform.position.x - transform.position.x, 2) + Mathf.Pow(play.transform.position.y - transform.position.y, 2);
        distance = Mathf.Sqrt(distance);

		if (distance < 3 &&(!hasQuest || !GlobalStuff.HaveQuestItem)) {
			if (isCreated == false) {
				tb = Instantiate (textbox, new Vector2 (Screen.width / 2, Screen.height / 6f), new Quaternion (0, 0, 0, 0), can.transform);
				isCreated = true;
				if (interacted == false) {
					GlobalStuff.AnxTalk++;
					interacted = true;
				}
				play.GetComponent<Player> ().InInteraction = true;
			}
			tb.transform.GetChild (1).GetComponent<Image> ().sprite = look;
			if (GlobalStuff.Aniexty == true) {
				//gibberish
				tb.transform.GetChild (0).GetComponent<Text> ().text = "sdasheabfdcb";
				tb.transform.GetChild (2).GetComponent<Text> ().text = "adnlsak";
			} else {
				tb.transform.GetChild (0).GetComponent<Text> ().text = Dialogue [0];
				tb.transform.GetChild (2).GetComponent<Text> ().text = Name;
			}
			displayText ();
		}
		else if (distance < 3 && hasQuest && GlobalStuff.HaveQuestItem) {
			if (isCreated == false) {
				tb = Instantiate (textbox, new Vector2 (Screen.width / 2, Screen.height / 6f), new Quaternion (0, 0, 0, 0), can.transform);
				isCreated = true;
				if (interacted == false) {
					GlobalStuff.AnxTalk++;
					interacted = true;
				}
				play.GetComponent<Player> ().InInteraction = true;
			}
			tb.transform.GetChild (1).GetComponent<Image> ().sprite = look;
			tb.transform.GetChild (2).GetComponent<Text> ().text = Name;
			Debug.Log ("for the love of god");
			if (hasQuest) {
				switch (whatQuest) {
				case "Overthinking":
					if (GlobalStuff.HaveQuestItem) {
						tb.transform.GetChild (0).GetComponent<Text> ().text = "Thank you!!"; //temp
					}
					break;
				}
			}
		}
        else {
            if (isCreated) {
                Destroy(tb, 0);
                isCreated = false;
                play.GetComponent<Player>().InInteraction = false;

				switch (whatQuest) {
				case "Overthinking":
					if (GlobalStuff.HaveQuestItem) {
						SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
					}
					break;
				}
            }
            
        }
    }

    void displayText() {
        if (!GlobalStuff.Aniexty) {
            if (Input.GetKeyUp(KeyCode.E))
            {
                isUp = true;
                isDown = false;
            }
            else if (Input.GetKeyDown(KeyCode.E) && isUp)
            {
                isDown = true;
                isUp = false;
                curFrame++;
                play.GetComponent<Player>().CanMove = false;
            }
        }

		if (curFrame > dialogueSize - 1) {
			curFrame = 0;
			play.GetComponent<Player>().CanMove = true;
			if (hasQuest) {
				switch (whatQuest) {
				case "Overthinking":
					GlobalStuff.OverthinkingStart = true;
					break;
				}
			}
		}
        if (GlobalStuff.Aniexty == true)
        {
            //gibberish
            tb.transform.GetChild(0).GetComponent<Text>().text = "sdasheabfdcb";
        }
        else {
            tb.transform.GetChild(0).GetComponent<Text>().text = Dialogue[curFrame];
        }
    }
}
