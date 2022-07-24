using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public static StartGame inc;
    [SerializeField] Text Score;
    int TotalScore;
    [SerializeField] Vector3 posStart;
    [SerializeField] Material MyMaterial;
    List<Color> Colors = new() { Color.red, Color.blue, Color.green, Color.cyan, Color.magenta };
    [SerializeField] PlayerMove playerMove;

    [SerializeField] List<SettingsRoad> AllRoads;

    readonly List<float> typeRotations = new() { 0, -90, 90 };

    [SerializeField] GameObject PanelControll;

    [SerializeField] GameObject PanelEndGame;
    [SerializeField] Text TypeEndGame;
    [SerializeField] Text ScoreEndGame;

    [SerializeField] InputField SpeedPlayer;
    [SerializeField] InputField LeghtWay;

    private void Start()
    {
        posStart = playerMove.transform.position;
        inc = this;
        AllRoads[0].GeneralObject.SetActive(true);
        SpeedPlayer.text = "100";
        LeghtWay.text = "30";
    }

    public void UpdateScore(int value)
    {
        if (value > 0)
        {
            TotalScore += value;
            if (TotalScore > 10)
                TotalScore = 10;
            else
                playerMove.NewSace(0.1f);
        }
        else
        {
            TotalScore += value;
            if (TotalScore <= 0)
                EndGame(false);
            else playerMove.NewSace(-0.1f);
        }
        Score.text = "Score: " + TotalScore.ToString();
    }

    public void CreateGame()
    {
        MyMaterial.color = Colors[Random.Range(0, Colors.Count)];
        CreateRoad();
        playerMove.Flip(0);
        PanelControll.SetActive(true);

        playerMove.normalSpeed = System.Convert.ToInt32(SpeedPlayer.text) / 1000f;
    }

    void CreateRoad()
    {
        int LeghtRoads = System.Convert.ToInt32(LeghtWay.text);
        Vector3 pos = AllRoads[0].myObject.transform.position;
        float newPos = typeRotations[Random.Range(0, typeRotations.Count)];
        float oldRot = 0;
        AllRoads[0].NextRoad = AllRoads[1];
        for (int i = 1; i < LeghtRoads; i++)
        {
            AllRoads[i].myObject.SetActive(true);
            AllRoads[i - 1].NextRoad = AllRoads[i];
            AllRoads[i].OldRoad = AllRoads[i - 1];
            AllRoads[i].myObject.transform.position = pos;
            AllRoads[i].myObject.transform.rotation = Quaternion.Euler(0, newPos, 0);

            if (oldRot == 0)
            {
                if (newPos == 0)
                {
                    AllRoads[i].myObject.transform.position = new(pos.x, pos.y, pos.z + 10f);
                    AllRoads[i - 1].Switch = 0;
                    AllRoads[i - 1].SwitchWays[0].SetActive(true);
                }
                else
                {
                    AllRoads[i].myObject.transform.position = new(pos.x + (newPos == -90 ? -6.5f : 6.5f), pos.y, pos.z + 3.5f);
                    AllRoads[i - 1].Switch = newPos;
                }
                AllRoads[i - 1].SwitchWays[(newPos == -90 ? 1 : 0)].SetActive(true);
            }
            else
            {
                AllRoads[i - 1].SwitchWays[(newPos == -90 ? 0 : 1)].SetActive(true);
                if (newPos == 0)
                {
                    AllRoads[i].myObject.transform.position = new(pos.x + (oldRot == -90 ? -3.5f : 3.5f), pos.y, pos.z + 3.5f);
                    AllRoads[i - 1].Switch = oldRot * -1;
                    if (oldRot == -90)
                    {
                        AllRoads[i - 1].SwitchWays[(newPos == -90 ? 1 : 0)].SetActive(true);
                        AllRoads[i - 1].SwitchWays[(newPos == -90 ? 0 : 1)].SetActive(false);
                    }
                    else
                    {
                        AllRoads[i - 1].SwitchWays[(newPos == -90 ? 0 : 1)].SetActive(true);
                        AllRoads[i - 1].SwitchWays[(newPos == -90 ? 1 : 0)].SetActive(false);
                    }
                }
                else if (oldRot == newPos * -1)
                {
                    newPos *= -1;
                    AllRoads[i].myObject.transform.position = new(pos.x + (oldRot == -90 ? -10f : 10f), pos.y, pos.z);
                    AllRoads[i - 1].Switch = 0;
                    AllRoads[i].myObject.transform.rotation = Quaternion.Euler(0, newPos, 0);
                }
                else if (oldRot == newPos)
                {
                    AllRoads[i].myObject.transform.position = new(pos.x + (oldRot == -90 ? -10f : 10f), pos.y, pos.z);
                    AllRoads[i - 1].Switch = 0;
                }
            } 
            pos = AllRoads[i].myObject.transform.position;
            oldRot = newPos;
            newPos = typeRotations[Random.Range(0, typeRotations.Count)];
            AllRoads[i].LoadSettings(playerMove.MyMaterial.color, Colors[Random.Range(0, Colors.Count)]);
            if (i == LeghtRoads - 1) AllRoads[i].Finish.SetActive(true);
        }
        AllRoads[0].NextRoad.GeneralObject.SetActive(true);
        AllRoads[1].NextRoad.GeneralObject.SetActive(true);
        CameraMove.StartLook = true;
    }

    public void OnClickTryAgain()
    {
        PanelEndGame.SetActive(false);
        CreateGame();
    }

    public void EndGame(bool type)
    {
        CameraMove.StartLook = false;
        for (int i = 1; i < AllRoads.Count; i++)
        {
            AllRoads[i].myObject.SetActive(false);
            AllRoads[i].GeneralObject.SetActive(false);
            AllRoads[i].SwitchWays[0].SetActive(false);
            AllRoads[i].SwitchWays[0].SetActive(false);
        }
        AllRoads[0].GeneralObject.SetActive(true);
        playerMove.moves[0] = 0;
        playerMove.moves[1] = 0;
        PanelEndGame.SetActive(true);
        TypeEndGame.text = (type == true ? "Win" : "Lose");
        ScoreEndGame.text = "Score: " + TotalScore;
        playerMove.transform.SetPositionAndRotation(posStart, Quaternion.Euler(0, 0, 0));
        playerMove.myRotate = 0;
        Camera.main.transform.SetPositionAndRotation(new(0, 3.5f, -10f), Quaternion.Euler(13, 0, 0));
        TotalScore = 0;
        playerMove.EndGame();
        Score.text = string.Empty;
    }
}
