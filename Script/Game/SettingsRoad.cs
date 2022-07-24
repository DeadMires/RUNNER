using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsRoad : MonoBehaviour
{
    public GameObject myObject;
    public GameObject GeneralObject;
    public MeshRenderer[] Materials;
    public GameObject[] SwitchWays;
    public float Switch;
    [SerializeField] GameObject Walls;
    public SettingsRoad NextRoad;
    public SettingsRoad OldRoad;
    public GameObject Finish;

    public void LoadSettings(Color c1, Color c2)
    {
        int ran = Random.Range(0, 2);
        Walls.SetActive(true);
        if (ran == 0)
        {
            Materials[0].material.color = c1;
            Materials[1].material.color = c2;
        }
        else
        {
            Materials[0].material.color = c2;
            Materials[1].material.color = c1;
        }
    }

    public void DisableWall()
    {
        Walls.SetActive(false);
    }

    public void SwitchWay()
    {
        PlayerMove.inc.Flip(Switch);
        if (NextRoad.NextRoad != null)
            NextRoad.NextRoad.GeneralObject.SetActive(true);
        if (OldRoad != null) OldRoad.GeneralObject.SetActive(false);
    }
}
