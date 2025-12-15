using UnityEngine;
using UnityEngine.SceneManagement; // Sahne değişimi için şart

public class GamePauseManager : MonoBehaviour
{
    [Header("UI Elemanları")]
    public GameObject pausePanel; // Açıp kapatacağımız panel
    public GameObject pauseButton; // Oyun içindeki durdur butonu (Paneli açınca bunu gizlemek isteyebilirsin)

    // Oyunun durup durmadığını kontrol eden değişken
    private bool isPaused = false;

    void Start()
    {
        // Başlangıçta panelin kapalı olduğundan emin olalım
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Oyunun hızı normal başlasın
    }

    // Klavyedeki ESC tuşunu dinlemek için (Opsiyonel ama standarttır)
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    // --- FONKSİYONLAR ---

    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true); // Paneli aç
        
        // ZAMANI DURDUR
        Time.timeScale = 0f; 
        
        // İstersen durdur butonunu gizle ki panelin altında kalmasın
        if(pauseButton != null) pauseButton.SetActive(false);
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false); // Paneli kapat
        
        // ZAMANI AKIT
        Time.timeScale = 1f;
        
        if(pauseButton != null) pauseButton.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        // KRİTİK NOKTA: Sahne değiştirmeden önce zamanı normale döndürmeliyiz.
        // Yoksa Ana Menü'ye döndüğünde oyun donuk kalır, animasyonlar oynamaz.
        Time.timeScale = 1f; 

        // Burada altınları kaybetme mantığı işleyecek (Kayıt almıyoruz)
        Debug.Log("Altınlar silindi, menüye dönülüyor...");

        // Ana Menü sahnenin adı neyse birebir aynısını yaz ("MainMenu" veya "NewMainMenu")
        SceneManager.LoadScene("menuScene4");
    }
}