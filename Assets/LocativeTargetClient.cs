
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Concurrent;
using System.Threading;
using NetMQ;
using NetMQ.Sockets;


public class NetMqListener
{
    
    private readonly Thread _listenerWorker;
    private readonly Thread _listenerWorker2;
    private readonly Thread _listenerWorker3;

    private bool _listenerCancelled;

    public delegate void MessageDelegate(string message);
    public delegate void MessageDelegate2(string droneLocation);
    public delegate void MessageDelegate3(string objectLocation);

    private readonly MessageDelegate _messageDelegate;
    private readonly MessageDelegate2 _messageDelegate2;
    private readonly MessageDelegate3 _messageDelegate3;

    private readonly ConcurrentQueue<string> _messageQueue = new ConcurrentQueue<string>();
    private readonly ConcurrentQueue<string> _droneLocationQueue = new ConcurrentQueue<string>();
    private readonly ConcurrentQueue<string> _objectLocationQueue = new ConcurrentQueue<string>();

    private void ListenerWork()
    {
        AsyncIO.ForceDotNet.Force();
        using (var subSocket = new SubscriberSocket())
        {
            subSocket.Options.ReceiveHighWatermark = 1000;
            subSocket.Connect("tcp://192.168.1.5:12345");
            subSocket.Subscribe("");
            while (!_listenerCancelled)
            {
                string frameString;
                if (!subSocket.TryReceiveFrameString(out frameString)) continue;
                Debug.Log(frameString);
                _messageQueue.Enqueue(frameString);
            }
            subSocket.Close();
        }
        NetMQConfig.Cleanup();
    }
    private void ListenerWork2()
    {
        AsyncIO.ForceDotNet.Force();
        using (var subSocket = new SubscriberSocket())
        {
            subSocket.Options.ReceiveHighWatermark = 1000;
            subSocket.Connect("tcp://192.168.1.5:12345");
            subSocket.Subscribe("");
            while (!_listenerCancelled)
            {
                string frameString2;
                if (!subSocket.TryReceiveFrameString(out frameString2)) continue;
                Debug.Log(frameString2);
                _objectLocationQueue.Enqueue(frameString2);
                
            }
            subSocket.Close();
        }
        NetMQConfig.Cleanup();
    }
    private void ListenerWork3()
    {
        AsyncIO.ForceDotNet.Force();
        using (var subSocket = new SubscriberSocket())
        {
            subSocket.Options.ReceiveHighWatermark = 1000;
            subSocket.Connect("tcp://192.168.1.5:12345");
            subSocket.Subscribe("");
            while (!_listenerCancelled)
            {
                string frameString3;
                if (!subSocket.TryReceiveFrameString(out frameString3)) continue;
                Debug.Log(frameString3);
                _droneLocationQueue.Enqueue(frameString3);
                
            }
            subSocket.Close();
        }
        NetMQConfig.Cleanup();
    }

    public void Update()
    {
        while (!_messageQueue.IsEmpty)
        {
            string message;
            if (_messageQueue.TryDequeue(out message))
            {
                _messageDelegate(message);
            }
            else
            {
                break;
            }
        }
        while (!_droneLocationQueue.IsEmpty)
        {
            string droneLocation;
            if (_droneLocationQueue.TryDequeue(out droneLocation))
            {
                _messageDelegate2(droneLocation);
            }
            else
            {
                break;
            }
        }
        while (!_objectLocationQueue.IsEmpty)
        {
            string objectLocation;
            if (_objectLocationQueue.TryDequeue(out objectLocation))
            {
                _messageDelegate3(objectLocation);
            }
            else
            {
                break;
            }
        }
    }

    public NetMqListener(MessageDelegate messageDelegate, MessageDelegate2 messageDelegate2, MessageDelegate3 messageDelegate3)
    {
        _messageDelegate = messageDelegate;
        _messageDelegate2 = messageDelegate2;
        _messageDelegate3 = messageDelegate3;
        _listenerWorker = new Thread(ListenerWork);
        _listenerWorker2 = new Thread(ListenerWork2);
        _listenerWorker3 = new Thread(ListenerWork3);
        
    }
    

    public void Start()
    {
        _listenerCancelled = false;
        _listenerWorker.Start();
        _listenerWorker2.Start();
        _listenerWorker3.Start();
    }

    public void Stop()
    {
        _listenerCancelled = true;
        _listenerWorker.Join();
        _listenerWorker2.Join();
        _listenerWorker3.Join();
    }
}

public class LocativeTargetClient : MonoBehaviour
{

    [Header("Google Maps Coordinates")]
    public float latitude;
    public float latitude1;
    public float latitude2;
    public float longitude;
    public float longitude1;
    public float longitude2;
    public float altitude;
    public float altitude1;
    public float altitude2;
    public GameObject LarTarget;
    public GameObject objectLocationTarget;
    public GameObject droneTarget;

    //[Header("Enable")]
    //public float MaximumDistance=1000;

    private double raioTerra = 6372797.560856f;
    private readonly Thread _listenerWorker;
    private readonly Thread _listenerWorker2;
    private readonly Thread _listenerWorker3;
    private NetMqListener _netMqListener;
    private NetMqListener _netMqListener2;
    private NetMqListener _netMqListener3;

    private bool _listenerCancelled;
    public delegate void MessageDelegate(string message);
    public delegate void MessageDelegate2(string droneLocation);
    public delegate void MessageDelegate3(string objectLocation);

    private readonly MessageDelegate _messageDelegate;
    private readonly MessageDelegate2 _messageDelegate2;
    private readonly MessageDelegate3 _messageDelegate3;

    private readonly ConcurrentQueue<string> _messageQueue = new ConcurrentQueue<string>();
    private readonly ConcurrentQueue<string> _droneLocationQueue = new ConcurrentQueue<string>();
    private readonly ConcurrentQueue<string> _objectLocationQueue = new ConcurrentQueue<string>();

    private void ListenerWork()
    {
        AsyncIO.ForceDotNet.Force();
        using (var subSocket = new SubscriberSocket())
        {
            subSocket.Options.ReceiveHighWatermark = 1000;
            subSocket.Connect("tcp://192.168.200.245:12345");
            subSocket.Subscribe("");
            while (!_listenerCancelled)
            {
                string frameString;
                if (!subSocket.TryReceiveFrameString(out frameString)) continue;
                Debug.Log(frameString);
                _messageQueue.Enqueue(frameString);
            }
            subSocket.Close();
        }
        NetMQConfig.Cleanup();
    }
    private void ListenerWork2()
    {
        AsyncIO.ForceDotNet.Force();
        using (var subSocket = new SubscriberSocket())
        {
            subSocket.Options.ReceiveHighWatermark = 1000;
            subSocket.Connect("tcp://192.168.200.245:12345");
            subSocket.Subscribe("");
            while (!_listenerCancelled)
            {
                string frameString2;
                if (!subSocket.TryReceiveFrameString(out frameString2)) continue;
                Debug.Log(frameString2);
                _droneLocationQueue.Enqueue(frameString2);
            }
            subSocket.Close();
        }
        NetMQConfig.Cleanup();
    }
    private void ListenerWork3()
    {
        AsyncIO.ForceDotNet.Force();
        using (var subSocket = new SubscriberSocket())
        {
            subSocket.Options.ReceiveHighWatermark = 1000;
            subSocket.Connect("tcp://192.168.200.245:12345");
            subSocket.Subscribe("");
            while (!_listenerCancelled)
            {
                string frameString3;
                if (!subSocket.TryReceiveFrameString(out frameString3)) continue;
                Debug.Log(frameString3);
                _objectLocationQueue.Enqueue(frameString3);
            }
            subSocket.Close();
        }
        NetMQConfig.Cleanup();
    }

    private void Start()
    {
        //print(raioTerra + " " + longitude);
        _netMqListener = new NetMqListener(HandleMessage);
        _netMqListener2 = new NetMqListener(HandleDroneLocation);
        _netMqListener3 = new NetMqListener(HandleObjectLocation);
        _netMqListener.Start();
        _netMqListener2.Start();
        _netMqListener3.Start();
    }
    private void HandleMessage(string message)
    {
        if (LarTarget.name == GameObject.Find("LarTarget").ToString())
            {
                var splittedStrings = message.Split(' ');
                if (splittedStrings.Length != 3) return;
                latitude = float.Parse(splittedStrings[0]);
                longitude  = float.Parse(splittedStrings[1]);
                altitude = float.Parse(splittedStrings[2]);
                LarTarget.transform.position = new Vector3(latitude, longitude, altitude);
            }
        else if(objectLocationTarget.name == GameObject.Find("objectLocationTarget").ToString())
            {   
                var splittedStrings = message.Split(' ');
                if (splittedStrings.Length != 3) return;
                latitude1 = float.Parse(splittedStrings[0]);
                longitude1  = float.Parse(splittedStrings[1]);
                altitude1 = float.Parse(splittedStrings[2]);
                objectLocationTarget.transform.position = new Vector3(latitude1, longitude1, altitude1);
        }
        else {
                var splittedStrings = message.Split(' ');
                if (splittedStrings.Length != 3) return;
                latitude2 = float.Parse(splittedStrings[0]);
                longitude2  = float.Parse(splittedStrings[1]);
                altitude2 = float.Parse(splittedStrings[2]);
                droneTarget.transform.position = new Vector3(latitude2, longitude2, altitude2);
        }
    }
    private void HandleObjectLocation(string objectLocation)
    {
        if (objectLocationTarget.name == GameObject.Find("objectLocationTarget").ToString()){
            var splittedStrings = objectLocation.Split(' ');
            if (splittedStrings.Length != 3) return;
            latitude1 = float.Parse(splittedStrings[0]);
            longitude1  = float.Parse(splittedStrings[1]);
            altitude1 = float.Parse(splittedStrings[2]);
            objectLocationTarget.transform.position = new Vector3(latitude1, longitude1, altitude1);
        }
        else if(LarTarget.name == GameObject.Find("LarTarget").ToString())
            {
                var splittedStrings = objectLocation.Split(' ');
                if (splittedStrings.Length != 3) return;
                latitude = float.Parse(splittedStrings[0]);
                longitude  = float.Parse(splittedStrings[1]);
                altitude = float.Parse(splittedStrings[2]);
                LarTarget.transform.position = new Vector3(latitude, longitude, altitude);
            }
        else {
                var splittedStrings = objectLocation.Split(' ');
                if (splittedStrings.Length != 3) return;
                latitude2 = float.Parse(splittedStrings[0]);
                longitude2  = float.Parse(splittedStrings[1]);
                altitude2 = float.Parse(splittedStrings[2]);
                droneTarget.transform.position = new Vector3(latitude2, longitude2, altitude2);
        }
    }
    private void HandleDroneLocation(string droneLocation)
    {
        if (droneTarget.name == GameObject.Find("droneTarget").ToString()){
            var splittedStrings = droneLocation.Split(' ');
            if (splittedStrings.Length != 3) return;
            latitude2 = float.Parse(splittedStrings[0]);
            longitude2  = float.Parse(splittedStrings[1]);
            altitude2 = float.Parse(splittedStrings[2]);
            droneTarget.transform.position = new Vector3(latitude2, longitude2, altitude2);
        }
        else if(LarTarget.name == GameObject.Find("LarTarget").ToString())
        {
            var splittedStrings = droneLocation.Split(' ');
            if (splittedStrings.Length != 3) return;
            latitude = float.Parse(splittedStrings[0]);
            longitude  = float.Parse(splittedStrings[1]);
            altitude = float.Parse(splittedStrings[2]);
            LarTarget.transform.position = new Vector3(latitude, longitude, altitude);
        }
        else 
        {
            var splittedStrings = droneLocation.Split(' ');
            if (splittedStrings.Length != 3) return;
            latitude1 = float.Parse(splittedStrings[0]);
            longitude1  = float.Parse(splittedStrings[1]);
            altitude1 = float.Parse(splittedStrings[2]);
            objectLocationTarget.transform.position = new Vector3(latitude1, longitude1, altitude1);
        }
    }

    private void Update()
    {
        Vector3 coord = calculaPosCoordCam();
        Vector3 coord2 = calculaPosCoordCam1();
        Vector3 coord3 = calculaPosCoordCam2();
        LarTarget.transform.position = coord;
        objectLocationTarget.transform.position = coord2;
        droneTarget.transform.position = coord3;
        //print(coord.x +" " + coord.y + " " + coord.z);
        _netMqListener.Update();
        _netMqListener2.Update();
        _netMqListener3.Update();
    }


    private void OnDestroy()
    {
        _netMqListener.Stop();
        _netMqListener2.Stop();
        _netMqListener3.Stop();
    }


    private Vector3 calculaPosCoordCam()
    {
        

        double dlat = latitude - LocativeGPS.Instance.latitude;
        dlat = getCoordEmMetrosDeRaio(raioTerra, dlat);

        double dlon = longitude - LocativeGPS.Instance.longitude;

        double raioLat = Mathf.Cos((float)latitude) * raioTerra;
        dlon = getCoordEmMetrosDeRaio(raioLat, dlon);


        double dalt = altitude - 0.0f;// LocativeGPS.Instance.altitude; 
        return new Vector3((float)dlat, (float)dalt, (float)dlon);
    }

    private Vector3 calculaPosCoordCam1()
    {
        

        double dlat1 = latitude1 - LocativeGPS.Instance.latitude;
        dlat1 = getCoordEmMetrosDeRaio(raioTerra, dlat);

        double dlon1 = longitude1 - LocativeGPS.Instance.longitude;

        double raioLat1 = Mathf.Cos((float)latitude1) * raioTerra;
        dlon1 = getCoordEmMetrosDeRaio(raioLat1, dlon1);


        double dalt1 = altitude1 - 0.0f;// LocativeGPS.Instance.altitude; 
        return new Vector3((float)dlat1, (float)dalt1, (float)dlon1);
    }
    private Vector3 calculaPosCoordCam2()
    {
        

        double dlat2 = latitude2 - LocativeGPS.Instance.latitude;
        dlat2 = getCoordEmMetrosDeRaio(raioTerra, dlat2);

        double dlon2 = longitude2 - LocativeGPS.Instance.longitude;

        double raioLat2 = Mathf.Cos((float)latitude2) * raioTerra;
        dlon2 = getCoordEmMetrosDeRaio(raioLat2, dlon2);


        double dalt2 = altitude2 - 0.0f;// LocativeGPS.Instance.altitude; 
        return new Vector3((float)dlat2, (float)dalt2, (float)dlon2);
    }



    private double getCoordEmMetrosDeRaio(double raio, double angulo)
    {
        double metros = (raio / 180) * Mathf.PI;//
        metros *= angulo;
        return metros;
    }


}
