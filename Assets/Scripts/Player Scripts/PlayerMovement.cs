using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 3.5f;//Hareket hızı.
    private float xAxis, yAxis;//X ve Y için çıkış.
    private float minBoundX = -71f, maxBoundX = 71f, minBoundY = -3.3f, maxBoundY = 0f;//X ve Y'de sınırlar.
    private float shootWaitTime = 0.5f;//Ateş edince yarım saniye bekleyecek. Sürekli ateş edemememiz için.
    private float waitBeforeShooting;//Ateşten önce bekle. shootWaitTime ile birlikte kullanacağız.
    private float moveWaitTime = 0.3f;//Ateş ettikten sonra 0.3 saniye bekle.
    private float waitBeforeMoving;//Hareket etmeden önce bekle. moveWaitTime ile birlikte kullanacağız.
    private bool canMove = true;//Hareket edebilir mi? EVET.
    private bool playerDied;//Oyuncu öldü. 0: Ölmedi. 1:Öldü. Mantık burada bu.
    private Vector3 tempPos;//Geçici pozisyon.
    private PlayerAnimation playerAnimation;//Player'in animasyonu için PlayerAnimation scriptini tanımladık.
    private PlayerShootingManager playerShootingManager;//PlayerShootingManager scriptindeki fonksiyonu çağıracağız. 
    private void Awake()
    {
     playerAnimation = GetComponent<PlayerAnimation>();//PlayerAnimation'ı Atadık.
     playerShootingManager = GetComponent<PlayerShootingManager>();//PlayerShootingManager'ı Atadık.
    }
    private void Update()
    {
     if(playerDied)//playerDied; 0:Ölmedi 1:Öldü. Burada eğer Player yaşıyorsa anlamında.
     return;//Döndür.
     //Yukarıdaki çalışmıyorken(ölü değilken) alttaki fonksiyonları döndür.
     HandleMovement();
     HandleAnimation();
     HandleFacingDirection();
     HandleShooting();
     CheckIfCanMove();
    }
    private void HandleMovement()
    {
     xAxis = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);//TagManager'daki Horizontal çıkış.
     yAxis = Input.GetAxisRaw(TagManager.VERTICAL_AXIS);//Vertical çıkış.
     if(!canMove)//Eğer hareket edemezse;
     return;//Alttakileri döndür.
     tempPos = transform.position;//Mevcut pozisyonu tempPos'a ata.
     tempPos.x += xAxis * moveSpeed * Time.deltaTime;
     //Geçici pozisyonun X'ini X ekseninde sabit 3.5 hızında hareket ettir.
     tempPos.y += yAxis * moveSpeed * Time.deltaTime;
     //Geçici pozisonun Y'sini Y ekseninde sabit 3.5 hızında hareket ettir.
     if(tempPos.x < minBoundX)//X'in geçici pozisyonu x'in alt sınırından küçükse
        tempPos.x = minBoundX;//x'in alt sınırını x'in geçici pozisyonuna eşitle.
     //Böylece sürekli alt sınırda kalıyor gibi olacak, yani alt sınırı aşamayacak.   
     if(tempPos.x > maxBoundX)
        tempPos.x = maxBoundX;
     if(tempPos.y < minBoundY)
        tempPos.y = minBoundY;
     if(tempPos.y > maxBoundY)
        tempPos.y = maxBoundY;
     //Yukarıdakinin aynısı X üst sınır, Y alt ve üst sınır için yapıldı.   
     transform.position = tempPos;//Geçici pozisyonu mevcut pozisyona atadık.
    }
    private void HandleAnimation()
    {
     if(!canMove)//Eğer hareket etmiyorsa;
     return;//döndür.
     if(Mathf.Abs(xAxis) > 0 || Mathf.Abs(yAxis) > 0)
     playerAnimation.PlayAnimation(TagManager.WALK_ANIMATION_NAME);
     else
     playerAnimation.PlayAnimation(TagManager.IDLE_ANIMATION_NAME);
     //Burada mutlak değer işlemi yapılıyor. Mutlak değer - olamaz. Haliyle sola da gitse
     //sağa da gitse 0'dan büyük + değer alır. eğer 0'dan büyükse walk animasyonunu oynat,
     //değilse idle animasyonunu oynat diyor. Mantık bu bu algoritmada.
    }
    private void HandleFacingDirection()
    {
     if(xAxis > 0)//Eğer X düzleminde +daysak. Yani sağa doğru hareket ediyorsak;
     playerAnimation.SetFacingDirection(true);//X'in scale'si 1 olacak. Yani sağa bakacak.
     //playerAnimation scriptindeki SetFacingDirection fonksiyonunu true yap.
     else if(xAxis < 0)//Eğer X düzleminde -deysek. Yani sola doğru hareket ediyorsak;
     playerAnimation.SetFacingDirection(false);//X'in scale'si -1 olacak. Yani sola bakacak.
     //playerAnimation scriptindeki SetFacingDirection fonksiyonunu false yap.
    }
    private void Shoot()
    {
     waitBeforeShooting = Time.time + shootWaitTime;//Tekrar ateş etmeden önce yarım saniye bekle.
     StopMovement();//Hareket etmeyi durdur.
     playerAnimation.PlayAnimation(TagManager.SHOOT_ANIMATION_NAME);//Shoot animasyonunu oynat.
     playerShootingManager.Shoot(transform.localScale.x);
     //PlayerShootingManager'daki Shoot fonksiyonunu, Player'in x'deki mevcut konumuna göre oynat.
     //Yönü nereye bakıyorsa oraya doğru ateş edecek. Burada bunu yaptık.
    }
    private void StopMovement()
    {
     canMove = false;//Ateş ederken hareket etmeyecek.
     waitBeforeMoving = Time.time + moveWaitTime;//Mevcut zaman'dan yarım saniye bekleyecek.
    }
    private void CheckIfCanMove()
    {
     if(Time.time > waitBeforeMoving)//Mevcut zamandan yarım saniye geçince
     canMove = true;//canMove'u true yap.
    }
    private void HandleShooting()
    {
     if(Input.GetKeyDown(KeyCode.Space))//Space'e basılmışsa
     {
      if(Time.time > waitBeforeShooting)//yarım saniye beklenmişse
      Shoot();//Ateş et.
     }
    }
    public void PlayerDied()
    {
     playerDied = true;//Player öldü mü? evet.
     playerAnimation.PlayAnimation(TagManager.DEATH_ANIMATION_NAME);//Ölme animasyonunu oynat.
     Invoke("DestroyPlayerAfterDelay", 2.0f);//Öldükten 2 saniye sonra Ekrandan sil.
    }
    private void DestroyPlayerAfterDelay()
    {
     Destroy(gameObject);//Doktor öldüğünde ekrandan silmek için fonksiyon.
    }
}//Class PlayerMovement
