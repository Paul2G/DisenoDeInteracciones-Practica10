using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;

using System;

public class MotionSensor : MonoBehaviour
{
    public string brokerIpAddress = "192.168.0.113";
	public int brokerPort = 5001;
	public string motionTopic = "casa/sala/movimiento";
    private MqttClient client;

	string lastMessage;
    void Start()
    {
        client = new MqttClient(IPAddress.Parse(brokerIpAddress), brokerPort, false, null);
        string clientId = Guid.NewGuid().ToString(); 
		client.Connect(clientId); 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entro");
            client.Publish(
                motionTopic,
                System.Text.Encoding.UTF8.GetBytes("Entrando"),
                MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,
                true
            );
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Salio");
            client.Publish(
                motionTopic,
                System.Text.Encoding.UTF8.GetBytes("Saliendo"),
                MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,
                true
            );
        }
    }

    void Update()
    {
        
    }
    
    void OnApplicationQuit()
	{
		client.Disconnect();
	}
}
