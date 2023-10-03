using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;

    public List<Room> list_room = new();
    public ObservableValue<Room> currentRoom = new(new(),4);
    private int width = 5;
    private int column = 5;


    int[,] dir_or_index_door = new int[3, 5]
    {
        {0,0,0,-1,1 },
        {0,1,-1,0,0 },
        {0,2,1,4,3 },//�·����ŵ����
    };

    public GameObject grid;
    public GameObject room_default;


    private void Awake()
    {
        instance = this;
       
    }
    public void AddRoomToList()
    {
        for (int i = 0; i < width; i++)
            for (int j = 0; j < column; j++)
            {
                list_room.Add(Instantiate(room_default, new Vector3(i * 35, j * 24, 0), Quaternion.identity, grid.transform).GetComponent<Room>());
                list_room[^1].pos_x = i + 1;
                list_room[^1].pos_y = j + 1;
                //list_room[^1].state_door[1].Value = 1;
                //list_room[^1].state_door[2].Value = 1;
                //list_room[^1].state_door[4].Value = 1;
            }
        currentRoom.Value = list_room[0];
        CameraController.instance.CallRefreshPosition();
        RefreshDoorState();
    }
    public void RefreshDoorState()
    {
        for (int i = 0;i<list_room.Count;i++)
        {
            for (int direction = 1;direction<=4;direction++)
            {
                Room hasFoundRoom = FindRoom(list_room[i].pos_x + dir_or_index_door[0, direction], list_room[i].pos_y + dir_or_index_door[1, direction]);
                //Debug.Log("RoomIndex:" + i + " direction : " + direction +" found? :" + hasFoundRoom);
                if (hasFoundRoom != null)
                {
                    list_room[i].doors[direction].SetActive(true);
                }
                else
                    list_room[i].doors[direction].SetActive(false);
            }
        }
    }

    public Vector3 MoveRoom(int direction)
    {
        
        float[,] delta_makeup = new float[2, 5]
        {
            {0,0,0,-1.2f,1.2f },
            {0,1.6f,-0.9f,0,0 },
        };
        Debug.Log("current x = " + currentRoom.Value.pos_x);
        Debug.Log("current y = " + currentRoom.Value.pos_y);
        Debug.Log("direction = " + direction);
        Debug.Log("dir x = " + dir_or_index_door[0,direction]);
        Debug.Log("dir y = " + dir_or_index_door[1,direction]);
        Room targetRoom = FindRoom(currentRoom.Value.pos_x + dir_or_index_door[0, direction], currentRoom.Value.pos_y + dir_or_index_door[1, direction]);
        currentRoom.Value = targetRoom;
        Vector3 targetPos = new
            (targetRoom.doors[dir_or_index_door[2, direction]].transform.position.x + delta_makeup[0, direction],
            targetRoom.doors[dir_or_index_door[2, direction]].transform.position.y + delta_makeup[1, direction],
            0);
        return targetPos;
    }
    public Room FindRoom(int x,int y)
    {
        for(int i = 0;i<list_room.Count;i++)
        {
            if (list_room[i].pos_x == x && list_room[i].pos_y == y)
                return list_room[i];
        }
        return null;
    }
    public void CheckRoomState()
    {
        ///TODO
        GameManager.instance.player.currentState.Value =  Character.STATE.Idling;
    }
}