using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    //private Text dialogText;
    //private GameObject dialogPanel;

    //bool dialogGoing;
    //int i;
    //float startTimer;
    //float timer;
    //int textIndex;
    //string[] textArray;
    //string finishText;
    //string[] wordsColor;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    dialogPanel = transform.GetChild(0).gameObject;
    //    dialogText = dialogPanel.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>();

    //    string[] words = new string[4];
    //    words[0] = "Takс";
    //    words[1] = "тест диалога значит";
    //    words[2] = "вроде работает норм";
    //    words[3] = "и даже цвет нром!";

    //    string[] colorWords = new string[4];
    //    colorWords[0] = "wwww";
    //    colorWords[1] = "wwwwwrrrrrrrwwwwwww";
    //    colorWords[2] = "wwwwwwwwwwwwwwwwwww";
    //    colorWords[3] = "wwrgbrwrgygwwryrb";

    //    startDialog(words, colorwords, 0.15f);
    //}

    //public void startDialog(string[] text, string[] textColor, float speed)
    //{
    //    dialogPanel.SetActive(true);
    //    dialogGoing = true;
    //    i = 0;
    //    textIndex = 0;
    //    textArray = text;
    //    startTimer = speed;
    //    wordsColor = textColor;
    //    finishText = textArray[textIndex];
    //}


    //public void endDialog()
    //{
    //    textIndex = 0;
    //    i = 0;
    //    dialogText.text = "";
    //    dialogGoing = false;
    //    dialogPanel.SetActive(false);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) && dialogGoing)
    //    {
    //        if (i >= finishText.Length)
    //        {
    //            textIndex++;
    //            dialogText.text = "";
    //            if (textIndex >= textArray.Length)
    //            {
    //                endDialog();
    //            }
    //            else
    //            {
    //                finishText = textArray[textIndex];
    //                i = 0;
    //            }
    //        }
    //        else
    //        {
    //            i = finishText.Length; 
    //            dialogText.text = "";
    //            for (int q = 1; q < i + 1; q++){
    //                dialogText.text += "<color=" + getColor(wordsColor[textIndex].Substring(q - 1, 1)) + ">" + textArray[textIndex].Substring;
    //            }
    //        }
    //    }

    //}
    //public string getColor(string idColor)
    //{
    //    if (idColor == "w")
    //    {
    //        return "white";
    //    }
    //    if (idColor == "r")
    //    {
    //        return "red";
    //    }
    //    if (idColor = "g")
    //    {
    //        return "green";
    //    }
    //    if (idColor == "b")
    //    {
    //        return "blue";
    //    }
    //    if (idColor = "y")
    //    {
    //        return "yellow";
    //    }
    //    return "white";
    //}
}
