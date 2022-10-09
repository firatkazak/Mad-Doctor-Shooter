using UnityEngine;
public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;//anim adında Animator komponenti tanımladık.
    private Vector3 tempScale;//Geçici skala.
    private int currentAnimation;//Mevcut oynayan animasyonun int değeri için değişken.
    private void Awake()
    {
        anim = GetComponent<Animator>();//anim'e Animator komponentini atadık.
    }
    public void PlayAnimation(string animationName)
    {
        if (currentAnimation == Animator.StringToHash(animationName))//Eğer mevcut animasyon ile animasyon adı aynı ise
            return;//döndür.(Yine mantıksal hata olmasın diye return ettiriyoruz fonksiyona.)
        anim.Play(animationName);//Animasyon adı aynı olanı oynat.
        currentAnimation = Animator.StringToHash(animationName);//Mevcut animasyona ata.
    }
    public void SetFacingDirection(bool faceRight)
    {
        tempScale = transform.localScale;//Mevcut scale'i tempScale'e ata.
        if (faceRight)//Eğer yüzü sağa bakıyorsa;
            tempScale.x = 1f;//X'i 1 yap.
        else//değilse
            tempScale.x = -1f;//x'i -1 yap.
        transform.localScale = tempScale;//teçici scale'i mevcut scale'e ata.
    }
}//Class PlayerAnimation
