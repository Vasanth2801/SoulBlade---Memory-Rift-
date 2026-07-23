using System.Collections.Generic;
using UnityEngine;

public class MiniMapSystem : MonoBehaviour
{
    [SerializeField] private List<MiniMapRoom> rooms;
    [SerializeField] private RectTransform playerIcon;
    private Dictionary<string, MiniMapRoom> roomData;

    private void Awake()
    {
        ServiceLocator.Register<MiniMapSystem>(this);

        roomData = new Dictionary<string, MiniMapRoom>();
        
        foreach(MiniMapRoom room in rooms)
        {
            roomData.Add(room.name, room);   
        }
        playerIcon.gameObject.SetActive(false); 
    }

    public void Maprooms(List<string> roomNames)
    {
        foreach(string name in roomNames)
        {
            if(!roomData.TryGetValue(name, out var room))
            {
                continue;
            }

            room.SetState(RoomState.Mapped);
        }
    }

    public void VisitRoom(string roomName)
    {
        if (!roomData.TryGetValue(roomName, out var room))
        {
            Debug.LogWarning("No Minimap Found for " + roomName);
            return;
        }
        
        if(room.State == RoomState.Mapped)
        {
            room.SetState(RoomState.Visited);
        }

        playerIcon.gameObject.SetActive(true);

        playerIcon.localPosition = room.transform.localPosition;
    }
}