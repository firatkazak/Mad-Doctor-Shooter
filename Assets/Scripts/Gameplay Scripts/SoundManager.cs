using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;// instance ad�nda public ve static olacak �ekilde SoundManager t�rettik.
    public AudioClip shootSound;//Ate� etme sesi. d�k��n d�k��n diye. :)
    private void Awake()
    {
        if (instance == null)//E�er instance null ise
            instance = this;//O zaman instance budur.
        //Burada instance'a referans vermi� olduk.
    }
    public void PlayShootSound()//Ate� etti�imizde �al��acak.(PlayerShootingManager scriptinde)
    {
        AudioSource.PlayClipAtPoint(shootSound, transform.position);//Sound Manager isimli game objemizin oldu�u pozisyonda ate� sesi �alacak.
        //PlayClipAtPoint belirli bir konumda sesi �almak istiyorsak kullanaca��m�z fonksiyondur. S�rekli ekran�n ortas�nda �al���yor.
        //Ekran�n ortas�na yakla�t�k�a ses art�yor, uzakla�t�k�a azal�yor.(Basit oyun oldu�u i�in fazla u�ra��lmam��.)
    }
}//Class SoundManager
