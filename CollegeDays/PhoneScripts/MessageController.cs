using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageController : MonoBehaviour {
	/*
	 * This scripts controlls all messages that are created through the contacts
	 * */
    public Queue<Message> messages1 = new Queue<Message>(); //Messages with this contact
	private string lastM; //Last message receieved by this contact
    GameObject g; 
    GameObject ob;
	MessageDisplay MD; //Message Display Component
	string name; //Name of contact
	Contact c1; //Contact used
	public bool unreadMessages; //Whether or not there are unreadmessages with this contact


    public ControllerOfTextMessages CTM;
    public MessageController(Contact c)
    {
		//Create messagedisplay at same time, so that we have an object that can use OnGUI based on the screen and contact selected of the phone. 
        CTM = GameObject.Find("FPSController").GetComponentInChildren<ControllerOfTextMessages>();
        g = c.getGameObject();
        ob = (GameObject)Instantiate(g);
        ob.name = c.getName() + "MessageController";
        MD = ob.GetComponent<MessageDisplay>();
        MD.sentSkin = c.getSendSkin();
		MD.gotSkin = c.getReceiveSkin();
		MD.name = c.getName();
        name = c.getName();
		c1 = c;
		unreadMessages = false;
    }
	public bool getUnread() {
		return unreadMessages;
		}

	//If send message, add it to the queues as a sent message
    public void sendNewMessage(string message)
    {
        messages1.Enqueue(new Message(1, message));
        MD.messages2.Enqueue(new Message(1, message));
    }
	//Add message to queues as a received message, tell the controller we have unread messages, and set last message to message received
    public void receiveNewMessage(string message)
    {
        messages1.Enqueue(new Message(0, message));
        MD.messages2.Enqueue(new Message(0, message));
		unreadMessages = true;
		lastM = message;
		//moveToFront ();
    }

	//Retun name of contact
    public string getName()
    {
        return name;
    }
	//Return contact of controller
	public Contact getContact() {
		return c1;
	}
	//Display messages
    public void displayMessages()
    {
        MD.show = true;
		unreadMessages = false;
    }
	//Close messages
    public void closeMessages()
    {
        MD.show = false;
        CTM.showTexts = false;
    }
	//Pull up available responses
	public void pullUp()
	{
		CTM.showTexts = true;
	}
	//Get last message
	public string getLast() {
		return lastM;
	}

}

