using UnityEngine;

public class ValTest : MonoBehaviour
{
    public SpriteRenderer boat1;

    public Sprite boatLvl1;

    public void spawnBoat1()
    {
        boat1.sprite = boatLvl1;
    }
}