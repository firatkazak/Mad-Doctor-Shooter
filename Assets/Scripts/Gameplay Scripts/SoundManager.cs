using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;// instance adýnda public ve static olacak þekilde SoundManager türettik.
    public AudioClip shootSound;//Ateþ etme sesi. dýkþýn dýkþýn diye. :)
    private void Awake()
    {
        if (instance == null)//Eðer instance null ise
            instance = this;//O zaman instance budur.
        //Burada instance'a referans vermiþ olduk.
    }
    public void PlayShootSound()//Ateþ ettiðimizde çalýþacak.(PlayerShootingManager scriptinde)
    {
        AudioSource.PlayClipAtPoint(shootSound, transform.position);//Sound Manager isimli game objemizin olduðu pozisyonda ateþ sesi çalacak.
        //PlayClipAtPoint belirli bir konumda sesi çalmak istiyorsak kullanacaðýmýz fonksiyondur. Sürekli ekranýn ortasýnda çalýþýyor.
        //Ekranýn ortasýna yaklaþtýkça ses artýyor, uzaklaþtýkça azalýyor.(Basit oyun olduðu için fazla uðraþýlmamýþ.)
    }
}//Class SoundManager
