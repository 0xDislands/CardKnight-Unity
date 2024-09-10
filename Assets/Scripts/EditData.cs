using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;
using System.IO;
using System.Threading.Tasks;

public class EditData : MonoBehaviour
{
    [SerializeField] private UnityEngine.TextAsset textAsset;
    [SerializeField] private TMP_InputField[] enemyField;
    [SerializeField] private TMP_InputField[] growthField;
    [SerializeField] private TMP_InputField[] imuneField;
    [SerializeField] private TMP_InputField[] revengeField;
    [SerializeField] private TMP_InputField[] noHopeField;
    [SerializeField] private TMP_InputField[] noSkillField;
    public static string dataStr;
    public static bool useCustomData;

    private void OnEnable()
    {
        Display();
    }

    private void Display()
    {
        var text = string.Empty;
        if (!useCustomData || !string.IsNullOrEmpty(dataStr)) text = textAsset.text;
        else text = dataStr;
        var contents = text.Split("\n");

        var spawnData = contents[0].Replace("\r", "").Split("\t"); 
        var growthData = contents[1].Replace("\r", "").Split("\t");
        var noMagic = contents[2].Replace("\r", "").Split("\t");
        var revenge = contents[3].Replace("\r", "").Split("\t");
        var noHeal = contents[4].Replace("\r", "").Split("\t");
        var noSkill = contents[5].Replace("\r", "").Split("\t");

        for (int i = 0; i < enemyField.Length; i++)
        {
            enemyField[i].text = spawnData[i];
        }

        for (int i = 0; i < growthField.Length; i++)
        {
            growthField[i].text = growthData[i];
        }

        for (int i = 0; i < imuneField.Length; i++)
        {
            imuneField[i].text = noMagic[i];
        }

        for (int i = 0; i < revengeField.Length; i++)
        {
            revengeField[i].text = revenge[i];
        }

        for (int i = 0; i < noHopeField.Length; i++)
        {
            noHopeField[i].text = noHeal[i];
        }

        for (int i = 0; i < noSkillField.Length; i++)
        {
            noSkillField[i].text = noSkill[i];
        }
    }

    public void SetData()
    {
        useCustomData = true;
        dataStr = string.Empty;
        for (int i = 0; i < enemyField.Length; i++)
        {
            dataStr += enemyField[i].text + "\t";
        }
        dataStr += "\r" + "\n";
        for (int i = 0; i < growthField.Length; i++)
        {
            dataStr += growthField[i].text + "\t";
        }
        dataStr += "\r" + "\n";
        for (int i = 0; i < imuneField.Length; i++)
        {
            dataStr += imuneField[i].text + "\t";
        }
        dataStr += "\r" + "\n";
        for (int i = 0; i < revengeField.Length; i++)
        {
            dataStr += revengeField[i].text + "\t";
        }
        dataStr += "\r" + "\n";
        for (int i = 0; i < noHopeField.Length; i++)
        {
            dataStr += noHopeField[i].text + "\t";
        }
        dataStr += "\r" + "\n";
        for (int i = 0; i < noSkillField.Length; i++)
        {
            dataStr += noSkillField[i].text + "\t";
        }
        _ = ReWriteData(dataStr);
    }

    private async Task ReWriteData(string buildStr)
    {
        var task = File.WriteAllTextAsync(AssetDatabase.GetAssetPath(textAsset), buildStr);
        while (!task.IsCompleted) await Task.Yield();
        EditorUtility.SetDirty(textAsset);
        gameObject.SetActive(false);
    }
}
