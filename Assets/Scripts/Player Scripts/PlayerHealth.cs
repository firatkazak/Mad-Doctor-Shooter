using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    private float health = 100;//Doktorumuzun caný.
    private PlayerMovement playerMovement;//PlayerMovement scriptinde iþlem yapacaðýmýz için tanýmladýk.
    public Slider healthSlider;//Doktorun can cubuðu.
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();//komponent atama iþlemi.
    }
    public void TakeDamage(float damageAmount)
    {
        if (health <= 0)//Caný 0 ya da daha az ise,
            return;//Döndür.Mantýk: Canýmýz -5 vb. olduðunda oyun mantýk hatasý vermeden fonksiyon çalýþmaya devam edecek.
        health -= damageAmount;//Alýnan hasar kadar canýmýzdan düþecek. 100 canýmýz var 1 kere vurdu 20 düþtü. 80 kaldý gibi.
        if (health <= 0)//Eðer canýmýz 0 ya da daha düþükse;
        {
            playerMovement.PlayerDied();//playerMovement'daki PlayerDied fonksiyonunu oynat.
            GamePlayController.instance.RestartGame();//Canýmýz 0 olunca oyunu yeniden baþlatacak.
        }
        healthSlider.value = health;//Caný, can barýna ata. 80 canýmýz var. Barýn 5'te 1'i dolu vb.
    }
}//Class PlayerHealth
