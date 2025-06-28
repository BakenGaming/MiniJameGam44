using System.Collections;
using UnityEngine;

public class HouseHandler : MonoBehaviour
{
    [SerializeField] private GameObject enterHouseMenu;
    private bool canEnterHouse;
    public void Initialize()
    {
        enterHouseMenu.SetActive(false);
        canEnterHouse = false;
        StartCoroutine("HouseTimer");
    }

    IEnumerator HouseTimer()
    {
        yield return new WaitForSecondsRealtime(10f);
        canEnterHouse = true;
    }
    private void OpenEnterHouseMenu()
    {
        enterHouseMenu.SetActive(true);
    }

    public void CloseEnterHouseMenu()
    {
        enterHouseMenu.SetActive(false);
    }

    public void EnterHouse()
    {
        Debug.Log("Entering The House");
        CloseEnterHouseMenu();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canEnterHouse) return;
        IHandler playerHandler = collision.GetComponent<IHandler>();
        if (playerHandler != null) OpenEnterHouseMenu();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!canEnterHouse) return;
        IHandler playerHandler = collision.GetComponent<IHandler>();
        if (playerHandler != null) CloseEnterHouseMenu();
    }
}
