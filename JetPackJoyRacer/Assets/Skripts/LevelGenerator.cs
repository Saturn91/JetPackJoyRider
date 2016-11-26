using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {

    public GameObject[] availableRooms; //possible prefabs for rooms i.E. room1

    public List<GameObject> currentRooms;   //rooms currently on the screen

    private float screenWidthInPoints;  //distance to check if a new room needs to be added

    // Use this for initialization
    void Start () {
        //calculate Screensize
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;
    }

    //update
    void FixedUpdate()
    {
        GenerateRoomIfRequired();
    }

    /**
     * Check if a new room is needed and add it!
     **/
    void GenerateRoomIfRequired()
    {
        //1
        List<GameObject> roomsToRemove = new List<GameObject>();

        //2
        bool addRooms = true;

        //3
        float playerX = transform.position.x;   //check where the player is at the moment

        //4
        float removeRoomX = playerX - screenWidthInPoints;  //which rooms have to be replaced?

        //5
        float addRoomX = playerX + screenWidthInPoints;     //where has to be the next room?

        //6
        float farthestRoomEndX = 0;

        foreach (var room in currentRooms)                  //loop trough all currentRooms
        {
            //7 calculate the room end and startpoints
            float roomWidth = room.transform.FindChild("floor").localScale.x;
            float roomStartX = room.transform.position.x - (roomWidth * 0.5f);
            float roomEndX = roomStartX + roomWidth;

            //8 if one of the rooms has a StartX out of the screen, there is no need to generate a new room!
            if (roomStartX > addRoomX)
                addRooms = false;

            //9 if one of the rooms has a EndX out of the screen, remove it!
            if (roomEndX < removeRoomX)
                roomsToRemove.Add(room);

            //10 check which roomEnd is the biggest
            farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
        }

        //11 remove all old rooms
        foreach (var room in roomsToRemove)
        {
            currentRooms.Remove(room);
            Destroy(room);
        }

        //12 add a new Room if nescesairy
        if (addRooms)
            AddRoom(farthestRoomEndX);
    }

    /**
     * farhtestRoomEndX is the fahrtest Point of the at the moment generated Lavel!
     * is needed to calculate where the next room shall be added 
     **/
    void AddRoom(float farhtestRoomEndX)
    {
        //1
        int randomRoomIndex = Random.Range(0, availableRooms.Length);   //choose the next room

        //2
        GameObject room = (GameObject)Instantiate(availableRooms[randomRoomIndex]);

        //3
        float roomWidth = room.transform.FindChild("floor").localScale.x;   //search the floor object in the prefab and take its width

        //4
        float roomCenter = farhtestRoomEndX + roomWidth * 0.5f;

        //5 place the room
        room.transform.position = new Vector3(roomCenter, 0, 0);

        //6
        currentRooms.Add(room);
    }
}
