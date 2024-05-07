using UnityEngine;

public class SelectHeroManager : MonoBehaviour
{
    public static SelectHeroManager Instance;
    public PopupSelectHero popupSelectHero;
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
        popupSelectHero.Close();
        canvasFindMatch.gameObject.SetActive(true);
        canvasSelectHero.gameObject.SetActive(false);
    }
}