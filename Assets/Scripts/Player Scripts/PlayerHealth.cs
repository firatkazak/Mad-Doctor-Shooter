using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    private float health = 100;//Doktorumuzun can�.
    private PlayerMovement playerMovement;//PlayerMovement scriptinde i�lem yapaca��m�z i�in tan�mlad�k.
    public Slider healthSlider;//Doktorun can cubu�u.
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();//komponent atama i�lemi.
    }
    public void TakeDamage(float damageAmount)
    {
        if (health <= 0)//Can� 0 ya da daha az ise,
            return;//D�nd�r.Mant�k: Can�m�z -5 vb. oldu�unda oyun mant�k hatas� vermeden fonksiyon �al��maya devam edecek.
        health -= damageAmount;//Al�nan hasar kadar can�m�zdan d��ecek. 100 can�m�z var 1 kere vurdu 20 d��t�. 80 kald� gibi.
        if (health <= 0)//E�er can�m�z 0 ya da daha d���kse;
        {
            playerMovement.PlayerDied();//playerMovement'daki PlayerDied fonksiyonunu oynat.
            GamePlayController.instance.RestartGame();//Can�m�z 0 olunca oyunu yeniden ba�latacak.
        }
        healthSlider.value = health;//Can�, can bar�na ata. 80 can�m�z var. Bar�n 5'te 1'i dolu vb.
    }
}//Class PlayerHealth
