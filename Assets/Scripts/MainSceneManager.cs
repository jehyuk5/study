using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameManager;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField] List<Button> ListBtns;

    [SerializeField] GameObject objMainView;
    [SerializeField] GameObject objRankView;

    [SerializeField] GameObject fabRankData;
    [SerializeField] Transform trsContents;
    string keyRankData = "rankData";
    List<cRank> listRank = new List<cRank>();//0~9

    void Awake()
    {
        initBtns();
        initRank();
        onRank(false);

        Tool.IsEneterFirstScence = true;//첫 씬이 내가 원하는 씬이 아닐시 다시 원하는 씬으로 가게하는 코드
    }

    /// <summary>
    /// 랭크 데이터를 입력합니다.
    /// </summary>
    private void initRank()
    {
        string rankValue = PlayerPrefs.GetString(keyRankData, string.Empty);//"";
        int count = 0;
        if (rankValue == string.Empty)
        {
            count = 10;
            for (int iNum = 0; iNum < count; iNum++)
            {
                listRank.Add(new cRank());//
            }

            rankValue = JsonConvert.SerializeObject(listRank);
            PlayerPrefs.SetString(keyRankData, rankValue);
        }
        else//""가 아니었다면
        {
            listRank = JsonConvert.DeserializeObject<List<cRank>>(rankValue);
        }

        count = listRank.Count;
        for (int iNum = 0; iNum < count; ++iNum)
        {
            cRank rank = listRank[iNum];

            GameObject go = Instantiate(fabRankData, trsContents);
            RankData goSc  = go.GetComponent<RankData>();
            goSc.SetData(iNum + 1, rank.name, rank.score);
        }
    }

    /// <summary>
    /// 버튼들을 초기화합니다.
    /// </summary>
    private void initBtns()
    {
        ListBtns[0].onClick.AddListener(onStart);//시작버튼
        ListBtns[1].onClick.AddListener(() => onRank(true));//랭킹버튼
        ListBtns[2].onClick.AddListener(onExit);//종료버튼
        ListBtns[3].onClick.AddListener(() => onRank(false));//랭킹 닫기버튼
    }

    private void onStart()
    {
        SceneManager.LoadSceneAsync((int)SceneNums.PlayScene);//플레이씬 이동
    }

    private void onRank(bool _value)//true로 들어오면 랭크뷰를 켜줌
    {
        objMainView.SetActive(!_value);//false가 들어오면 켜짐(메인으로 이동)
        objRankView.SetActive(_value);//true가 들어오면 켜짐(랭크로 이동)
    }

    private void onExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
