using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using System;
using TMPro;

public class DataController : MonoBehaviour
{
    public TextAsset textAssetData;
    public string[] orbis22 = new string[12];

    public int[] monthlyUsage = new int[12];

    int currentYear = 2023;
    int currentMonth = 1;
    int currentDay = 1;
    
    [SerializeField] private TextMeshProUGUI TodayDate;
    [SerializeField] private TextMeshProUGUI LYU;
    [SerializeField] private TextMeshProUGUI TMU;

    [SerializeField] private TextMeshProUGUI[] MONTHS = new TextMeshProUGUI[12];


    // 하루에 1500 1533 1566
    // Start is called before the first frame update 
    void Start()
    {
        LastYearUsage();
        StartCoroutine(nextDay());
    }

    void LastYearUsage(){
        string[] data = textAssetData.text.Split(new string[] {",", "\n"}, StringSplitOptions.None);
        for(int i = 0; i < 12; i++){
            orbis22[i] = data[9 * (i + 1) + 3];
        }
    }

    // Update is called once per frame
    void Update()
    {
        LYU.text = orbis22[currentMonth-1];

        for(int i = 0; i < 12; i++){
            Color m_Color = new Color(1,1,1);
            if(monthlyUsage[i] == 0) m_Color = new Color(1,1,1);
            else if(monthlyUsage[i] < int.Parse(orbis22[i])) m_Color = new Color(0,0.6f,0);
            else if(monthlyUsage[i] >= int.Parse(orbis22[i])) m_Color = new Color(1,0,0);
            MONTHS[i].color = m_Color;
        }
    }

    IEnumerator nextDay(){
        int[] days = new int[12]{31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
        while(currentMonth != 12 || currentDay != 31){
            string cm = "";
            string cd = "";
            if(currentMonth/10 == 0) cm = "0";
            if(currentDay/10 == 0) cd = "0";
            monthlyUsage[currentMonth-1] += Random.Range(1500, 1566);
            TodayDate.text = "TODAY : " + currentYear + "-" + cm + currentMonth + "-" + cd +currentDay;
            if(monthlyUsage[currentMonth-1] < int.Parse(orbis22[currentMonth-1])) TMU.color = new Color(0,0.6f,0);
            else TMU.color = new Color(1,0,0);
            TMU.text = "" + monthlyUsage[currentMonth-1];
            currentDay += 1;
            for(int i = 1; i <= 12; i++){
                if(currentMonth == i){
                    if(currentDay > days[i-1]){
                        currentMonth += 1;
                        currentDay = 1;
                    }
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
