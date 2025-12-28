using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    [Header("Paneller")]
    public GameObject shopPanel;
    public GameObject mainMenuPanel;

    [Header("UI Elemanlarý")]
    public TMP_Text coinText;

    [Header("Butonlar")]
    public Button btnSpeed;
    public Button btnDoubleJump;
    // btnMagnet SÝLÝNDÝ

    [Header("Fiyatlar")]
    public int speedPrice = 50;
    public int doubleJumpPrice = 100;
    // magnetPrice SÝLÝNDÝ

    void Start()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        if (shopPanel != null) shopPanel.SetActive(false);

        CheckPurchases();
        UpdateUI();
    }

    public void ToggleShop()
    {
        if (shopPanel == null || mainMenuPanel == null) return;

        bool isShopOpen = shopPanel.activeSelf;
        if (isShopOpen)
        {
            shopPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
        else
        {
            shopPanel.SetActive(true);
            mainMenuPanel.SetActive(false);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("OyunSahnesi");
    }

    public void ResetAllProgress()
    {
        PlayerPrefs.DeleteAll();
        InfoCoin.totalCoins = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Oyun Sýfýrlandý!");
    }

    // --- SATIN ALMA ÝÞLEMLERÝ ---

    public void BuySpeed()
    {
        if (PlayerPrefs.GetInt("HasSpeed", 0) == 1) return;

        if (InfoCoin.totalCoins >= speedPrice)
        {
            InfoCoin.totalCoins -= speedPrice;
            PlayerPrefs.SetInt("HasSpeed", 1);
            CheckPurchases();
            UpdateUI();
        }
    }

    public void BuyDoubleJump()
    {
        if (PlayerPrefs.GetInt("HasDoubleJump", 0) == 1) return;

        if (InfoCoin.totalCoins >= doubleJumpPrice)
        {
            InfoCoin.totalCoins -= doubleJumpPrice;
            PlayerPrefs.SetInt("HasDoubleJump", 1);
            CheckPurchases();
            UpdateUI();
        }
    }

    // BuyMagnet FONKSÝYONU TAMAMEN SÝLÝNDÝ

    void CheckPurchases()
    {
        if (PlayerPrefs.GetInt("HasSpeed", 0) == 1 && btnSpeed != null)
            btnSpeed.interactable = false;

        if (PlayerPrefs.GetInt("HasDoubleJump", 0) == 1 && btnDoubleJump != null)
            btnDoubleJump.interactable = false;

        // Magnet kontrolü silindi
    }

    void Update()
    {
        if (btnSpeed != null && PlayerPrefs.GetInt("HasSpeed", 0) == 0)
            btnSpeed.interactable = (InfoCoin.totalCoins >= speedPrice);

        if (btnDoubleJump != null && PlayerPrefs.GetInt("HasDoubleJump", 0) == 0)
            btnDoubleJump.interactable = (InfoCoin.totalCoins >= doubleJumpPrice);

        // Magnet kontrolü silindi
    }

    void UpdateUI()
    {
        if (coinText != null) coinText.text = "Para: " + InfoCoin.totalCoins;
    }
}