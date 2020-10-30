using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum World
{
    Desert,
    Forest,
    Plains,
    Rocky
}

[CreateAssetMenu(menuName = "GameValues")]
public class GameValues : ScriptableObject
{
    public World currentWorld = World.Desert;



    public string[] popupMessagesPlants;

    public string[] popupMessagesGroundDesert;
    public string[] popupMessagesGroundForest;
    public string[] popupMessagesGroundRocky;
    public string[] popupMessagesGroundPlains;



    public string GetGroundStatus()
    {
        switch (currentWorld)
        {
            case World.Desert:
                if (popupMessagesGroundDesert.Length > 0)
                    return popupMessagesGroundDesert.GetRandomValue();
                else
                    return "Ground is too dry to plant here";
            case World.Forest:

                if (popupMessagesGroundForest.Length > 0)
                    return popupMessagesGroundForest.GetRandomValue();
                else
                    return "Ground is too dirty to plant here";

            case World.Plains:

                if (popupMessagesGroundPlains.Length > 0)
                    return popupMessagesGroundPlains.GetRandomValue();
                else
                    return "Ground is too plainy to plant here";

            case World.Rocky:

                if (popupMessagesGroundRocky.Length > 0)
                    return popupMessagesGroundRocky.GetRandomValue();
                else
                    return "Ground is too hard to plant here";

            default:
                return "";
        }
    }

    public string GetSeedMessage()
    {
        if (popupMessagesPlants.Length > 0) return popupMessagesPlants.GetRandomValue();
        else return "This seeds seems good";
    }


    public void SetWorld(World world)
    {
        currentWorld = world;
    }
}
