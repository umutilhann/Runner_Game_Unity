using UnityEngine;
using UnityEngine.SceneManagement; // Sahne geçişleri için şart
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Paneller")]
    public GameObject mainPanel;
    public GameObject settingsPanel;
    public GameObject creditsPanel;

    [Header("Ayarlar UI Elemanları")]
    public Toggle fullscreenToggle;

    // Başlangıçta ayarları kontrol et
    void Start()
    {
        // Mevcut ekran durumuna göre toggle'ı ayarla
        if(fullscreenToggle != null)
            fullscreenToggle.isOn = Screen.fullScreen;
    }

    // --- BUTON FONKSİYONLARI ---

    public void PlayGame()
    {
        // "GameScene" senin oyun sahnenin tam adı olmalı
        SceneManager.LoadScene("SampleScene"); 
    }

    public void OpenSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void OpenCredits()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Oyundan Çıkıldı!"); // Editörde çalışmaz, build alınca çalışır
        Application.Quit();
    }

    // --- AYAR FONKSİYONLARI (Şimdilik Basit) ---

    // Tam Ekran / Pencere Modu Ayarı
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}