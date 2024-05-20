using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectHeroManager : MonoBehaviour
{
    public static SelectHeroManager Instance;
    public PopupSelectHero popupSelectHero;
    public SkinPopup popupSkin;
    public Canvas canvasFindMatch;
    public Canvas canvasSelectHero;
    
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        canvasFindMatch.gameObject.SetActive(false);
    }
    public void SelectHero()
    {
        popupSelectHero.gameObject.SetActive(true);
    }
    public void SelectHero(HeroId id)
    {
        CardManager.selectedHero = id;
        //popupSelectHero.Close();
        //canvasFindMatch.gameObject.SetActive(true);
        //FindMatchManager.Instance.OnFindMatchClick();
        //canvasSelectHero.gameObject.SetActive(false);
        //SceneManager.LoadScene("gameplay");
    }

    public void ToGameplay()
    {
        DOVirtual.DelayedCall(0.05f, () =>
        {
            SceneManager.LoadScene("gameplay");
        });
        
    }
}