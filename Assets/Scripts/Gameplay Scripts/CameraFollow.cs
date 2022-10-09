using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    private float smoothSpeed = 2.0f;// Yukarý/Aþaðý giderken kameranýn takip hýzý. 20 yaparsan aniden gidiyor mesela.
    private float Y_Gap = 2.0f;// Y eksenindeki Gap.
    private float playerBoundMin_Y = -1f, playerBoundMin_X = -65f, playerBoundMax_X = 65f;//Kameranýn takip sýnýrlarý.
    private Transform playerTarget;//Doktoru takip etmek için Transform tanýmladýk.
    private Vector3 tempPos;//Geçici pozisyon
    private void Start()
    {
        playerTarget = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;
        //playerTarget'a Tag'ý Player olan game objemizin Transform'unu atadýk.
    }
    private void Update()
    {
        if (!playerTarget)//Eðer doktor yoksa
            return;//Döndür.Mantýk: Doktor ölünce ekranda takip edilecek doktor olmayacak ve oyun hata verecek. Bu olmasýn diye yaptýk.
        tempPos = transform.position;//Mevcut pozisyonu tempPos'a ata.
        if (playerTarget.position.y <= playerBoundMin_Y)//Eðer oyuncunun üst sýnýra gelmiþse(y'de -1)
            tempPos = Vector3.Lerp(transform.position, new Vector3(playerTarget.position.x, playerTarget.position.y, -10f), Time.deltaTime * smoothSpeed);
        //Lerp'in içindeki 1. pozisyondan, 2. pozisyona, Time.deltaTime*smoothSpeed hýzýnda git.
        else
            tempPos = Vector3.Lerp(transform.position, new Vector3(playerTarget.position.x, playerTarget.position.y + Y_Gap, -10f), Time.deltaTime * smoothSpeed);
        //Lerp'in içindeki 1. pozisyondan, 2. pozisyona, Time.deltaTime*smoothSpeed hýzýnda git.(Y_Gap ile -2 birim pay veriyoruz.)
        if (tempPos.x > playerBoundMax_X)
            tempPos.x = playerBoundMax_X;
        if (tempPos.x < playerBoundMin_X)
            tempPos.x = playerBoundMin_X;
        //Üstteki 2 if: Kamera oyunun sýnýrlarýnýn altýný ya da üstünü gösteremesin diye. Sýnýr koyma mantýðýnýn aynýsý.
        transform.position = tempPos;//tempPos'u transform.position'a ata.
    }
}//CameraFollow
