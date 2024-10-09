using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] Toggle musicToggle;
    [SerializeField] Toggle sfxToggle;
    [SerializeField] AudioManager audioManager;

    private void Start()
    {
        // Đặt giá trị mặc định cho các toggle
        musicToggle.isOn = true;
        sfxToggle.isOn = true;

        // Đăng ký sự kiện khi thay đổi giá trị toggle
        musicToggle.onValueChanged.AddListener(delegate { ToggleMusic(); });
        sfxToggle.onValueChanged.AddListener(delegate { ToggleSFX(); });
    }

    public void ToggleMusic()
    {
        audioManager.ToggleMusic();
    }

    public void ToggleSFX()
    {
        audioManager.ToggleSFX();
    }
}
