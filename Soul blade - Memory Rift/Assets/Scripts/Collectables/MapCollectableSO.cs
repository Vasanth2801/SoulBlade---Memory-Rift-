using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Collectabes/Map")]
public class MapCollectableSO : CollectableSO
{
    public List<string> roomsToReveal;

    public override void Collect(Player player)
    {
        MiniMapSystem miniMap = ServiceLocator.Get<MiniMapSystem>();
        miniMap.Maprooms(roomsToReveal);
        miniMap.VisitRoom(SceneManager.GetActiveScene().name);
    }
}