using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int pos_x;//С��ͼ����(1,1)
    public int pos_y;
    public Vector3 pos_world;//���ĵ����������
    public List<GameObject> doors = new();
    public List<ObservableValue<int>> state_door = new() { new(0,5), new(0, 5), new(0, 5), new(0, 5), new(0, 5) };
    private void Awake()
    {
        pos_world = new Vector3(transform.position.x-0.5f,transform.position.y-0.5f,transform.position.z);
        for(int i=0;i<=4;i++)
        {
            doors.Add(transform.GetChild(i).gameObject);
        }
    }
}
