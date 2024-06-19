using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonReset : MonoBehaviour
{
    public void OnClick()
    {
        var datas = DataManager.Instance.heroDatas;
        for (int i = 0; i < datas.Count; i++)
        {
            datas[i].selectedSkin = 0;
        }
        SceneManager.LoadScene(0);
    }
}