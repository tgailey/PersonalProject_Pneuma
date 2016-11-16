using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Contact : MonoBehaviour {
	/*
	 * This class holds all messages for each contact created, allows interaction of messages through one controller
	 * */
    public List<string> contacts = new List<string>();

	public int count;
    string name;
    GameObject controller;
    GUISkin sendSkin, receiveSkin;
    MessageController messages;
    PhoneController PC;
    public Contact(string n, GameObject g, GUISkin SS, GUISkin RS)
    {
        controller = g;
        sendSkin = SS;
        receiveSkin = RS;
        PC = PC = GameObject.FindGameObjectWithTag("Phone").GetComponent<PhoneController>();
        name = n;
        contacts.Add(n);
        messages = new MessageController(this);
        //messages.name = name + "Messages";
        PC.messageControllers.Add(messages);

		//DontDestroyOnLoad (this);
    }
	//return contact name
    public string getName()
    {
        return name;
    }
	//return controller
    public GameObject getGameObject()
    {
        return controller;
    }
	//return skin used for sending messgaes
    public GUISkin getSendSkin()
    {
        return sendSkin;
    }
	//return skin used for receiving messages
    public GUISkin getReceiveSkin()
    {
        return receiveSkin;
    }
    public void send(string s)
    {
        messages.sendNewMessage(s);
        moveToFront();
        
    }
    public void receive(string s)
    {
        messages.receiveNewMessage(s);
        moveToFront();
		if (!PC.countedNames.Contains (name)) {
			PC.countedNames.Add(name);
		}
    }
    public MessageController myMessages()
    {
        return messages;
    }
	//Displays messages
	public void display() {
		messages.displayMessages ();
        if (PC.countedNames.Contains (name)) {
			//count--;
			PC.countedNames.Remove(name);
		}
	}
	//closesMessages
	public void close() {
		messages.closeMessages ();
	}
	//getsLastMessage
	public string lastMessage() {
		return messages.getLast ();
	}
	//Get the amount of undread messages
	public int getUnreadCount() {
		return PC.countedNames.Count;
	}
	//Pull up options
	public void pullUpOptions()
	{
		messages.pullUp();
	}
	//When receieving/sending a new message, move it to first in the list so the messaegs will be displayed by newest first.
    void moveToFront()
    {
		int index = -1;
		int count = 0;
		MessageController savedM = messages;
		foreach (MessageController mc in PC.messageControllers) {

			if (mc.getName().Equals(name)) {
				index = count;
				savedM = PC.messageControllers[index];
			}
			count++;
		}
        //int index = PC.messageControllers.IndexOf(messages);
        Debug.Log(index);
       
      	PC.messageControllers.RemoveAt(index);
		PC.messageControllers.Insert(0, savedM);

    }
}
