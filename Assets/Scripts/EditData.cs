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

    [SerializeField] private Transform enemyTransform;
    [SerializeField] private Transform growthTransform;
    [SerializeField] private Transform imuneTransform;
    [SerializeField] private Transform revengeTransform;
    [SerializeField] private Transform noHopeTransform;
    [SerializeField] private Transform noSkillTransform;
    [SerializeField] private Transform indexTransform;

    private TMP_InputField[] enemyField;
    private TMP_InputField[] growthField;
    private TMP_InputField[] imuneField;
    private TMP_InputField[] revengeField;
    private TMP_InputField[] noHopeField;
    private TMP_InputField[] noSkillField;
    private TMP_InputField[] indexField;
    public static string dataStr;
    public static bool useCustomData;

    private void OnEnable()
    {
        Display();
    }

    private void Display()
    {
        enemyField = enemyTransform.GetComponentsInChildren<TMP_InputField>();
        growthField = growthTransform.GetComponentsInChildren<TMP_InputField>();
        revengeField = revengeTransform.GetComponentsInChildren<TMP_InputField>();
        noHopeField = noHopeTransform.GetComponentsInChildren<TMP_InputField>();
        noSkillField = noSkillTransform.GetComponentsInChildren<TMP_InputField>();
        imuneField = imuneTransform.GetComponentsInChildren<TMP_InputField>();
        indexField = indexTransform.GetComponentsInChildren<TMP_InputField>();

        var text = string.Empty;
        if (!useCustomData || !string.IsNullOrEmpty(dataStr)) text = textAsset.text;
        else text = dataStr;
        var contents = text.Split("\n");

        var spawnData = contents[0].Replace("\r", "").Split("\t"); 
        var growthData = contents[1].Replace("\r", "").Split("\t");
        var imune = contents[2].Replace("\r", "").Split("\t");
        var revenge = contents[3].Replace("\r", "").Split("\t");
        var noHeal = contents[4].Replace("\r", "").Split("\t");
        var noSkill = contents[5].Replace("\r", "").Split("\t");

        for (int i = 0; i < enemyField.Length; i++)
        {
            if (i >= spawnData.Length) { enemyField[i].gameObject.SetActive(false); break; }
            enemyField[i].text = spawnData[i];
        }

        for (int i = 0; i < growthField.Length; i++)
        {
            if (i >= growthData.Length) { growthField[i].gameObject.SetActive(false); break; }
            growthField[i].text = growthData[i] ?? "0";
        }

        for (int i = 0; i < imuneField.Length; i++)
        {
            if (i >= imune.Length) { imuneField[i].gameObject.SetActive(false); break; }
            imuneField[i].text = imune[i] ?? "0";
        }

        for (int i = 0; i < revengeField.Length; i++)
        {
            if (i >= revenge.Length) { revengeField[i].gameObject.SetActive(false); break; }
            revengeField[i].text = revenge[i] ?? "0";
        }

        for (int i = 0; i < noHopeField.Length; i++)
        {
            if (i >= noHeal.Length) { noHopeField[i].gameObject.SetActive(false); break; }
            noHopeField[i].text = noHeal[i] ?? "0";
        }

        for (int i = 0; i < noSkillField.Length; i++)
        {
            if (i >= noSkill.Length) { noSkillField[i].gameObject.SetActive(false); break; }
            noSkillField[i].text = noSkill[i] ?? "0";
        }

        for (int i = 0; i < indexField.Length; i++)
        {
            indexField[i].text = (i + 1).ToString();
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
