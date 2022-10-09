using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    private float smoothSpeed = 2.0f;// Yukar�/A�a�� giderken kameran�n takip h�z�. 20 yaparsan aniden gidiyor mesela.
    private float Y_Gap = 2.0f;// Y eksenindeki Gap.
    private float playerBoundMin_Y = -1f, playerBoundMin_X = -65f, playerBoundMax_X = 65f;//Kameran�n takip s�n�rlar�.
    private Transform playerTarget;//Doktoru takip etmek i�in Transform tan�mlad�k.
    private Vector3 tempPos;//Ge�ici pozisyon
    private void Start()
    {
        playerTarget = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;
        //playerTarget'a Tag'� Player olan game objemizin Transform'unu atad�k.
    }
    private void Update()
    {
        if (!playerTarget)//E�er doktor yoksa
            return;//D�nd�r.Mant�k: Doktor �l�nce ekranda takip edilecek doktor olmayacak ve oyun hata verecek. Bu olmas�n diye yapt�k.
        tempPos = transform.position;//Mevcut pozisyonu tempPos'a ata.
        if (playerTarget.position.y <= playerBoundMin_Y)//E�er oyuncunun �st s�n�ra gelmi�se(y'de -1)
            tempPos = Vector3.Lerp(transform.position, new Vector3(playerTarget.position.x, playerTarget.position.y, -10f), Time.deltaTime * smoothSpeed);
        //Lerp'in i�indeki 1. pozisyondan, 2. pozisyona, Time.deltaTime*smoothSpeed h�z�nda git.
        else
            tempPos = Vector3.Lerp(transform.position, new Vector3(playerTarget.position.x, playerTarget.position.y + Y_Gap, -10f), Time.deltaTime * smoothSpeed);
        //Lerp'in i�indeki 1. pozisyondan, 2. pozisyona, Time.deltaTime*smoothSpeed h�z�nda git.(Y_Gap ile -2 birim pay veriyoruz.)
        if (tempPos.x > playerBoundMax_X)
            tempPos.x = playerBoundMax_X;
        if (tempPos.x < playerBoundMin_X)
            tempPos.x = playerBoundMin_X;
        //�stteki 2 if: Kamera oyunun s�n�rlar�n�n alt�n� ya da �st�n� g�steremesin diye. S�n�r koyma mant���n�n ayn�s�.
        transform.position = tempPos;//tempPos'u transform.position'a ata.
    }
}//CameraFollow
