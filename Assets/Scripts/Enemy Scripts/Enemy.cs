using UnityEngine;
public class Enemy : MonoBehaviour
{
    private float moveSpeed = 2.0f;//Hareket hızı.
    private float stoppingDistance = 1.5f;//Durma mesafesi
    private float attackWaitTime = 2.5f;//Düşmanın bize tekrar vurması için geçen süre.
    private float attackTimer;//Saldırı zamanlayıcısı.
    private float attackFinishedWaitTime = 0.5f;//Saldırı sonrası yarım saniye bekle. Saniyede 2 kere ateş edebileceğiz.
    private float attackFinishedTimer;// Saldırı bitirme zamanlayıcısı.
    private bool enemyDied;//Düşmanın ölmesiyle ilgili bool değişkeni.
    private Transform playerTarget;//Oyuncuyu takip edecek.
    private Vector3 tempScale;//Geçici skala.
    private PlayerAnimation enemyAnimation;//Enemy'nin animasyonu.
    public EnemyDamageArea enemyDamageArea;//EnemyDamageArea scriptinde işlem yapacağız.
    public RectTransform healthBarTransform;//Can barının konumu.
    private Vector3 healthBarTempScale;//Can barının geçici skalası.
    private void Awake()
    {
        playerTarget = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;
        //Player tagına sahip objeyi playerTarget'a tanıttık.
        enemyAnimation = GetComponent<PlayerAnimation>();
        //PlayerAnimation scriptindeki fonksiyonu kullanacağımız için enemyAnimation adıyla tanımladık.
    }
    private void Update()
    {
        if (enemyDied)//Player öldüğünde de döndürecek. Çünkü Player ölünce Player'i arayamaz,
            return;//Haliyle null reference hatası verir. Bunun olmaması için if return kullandık.
        SearchForPlayer();//Sürekli oyuncuyu arayacak.
    }
    private void SearchForPlayer()
    {
        if (!playerTarget)//Eğer player öldüyse; return ile kodu döndür.
            return;//Eğer Player ölürse takip edecek Player bulamayacak ve null reference hatası verecek. bunun için yaptık.
        if (Vector3.Distance(transform.position, playerTarget.position) > stoppingDistance)
        //Eğer Enemy'nin pozisyonu ile Player'in pozisyonu arası stoppingDistance(1.5) büyükse
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, moveSpeed * Time.deltaTime);
            //Enemy'nin pozisyonunu Player'in pozisyonuna moveSpeed hızında taşı.
            enemyAnimation.PlayAnimation(TagManager.WALK_ANIMATION_NAME);
            //PlayerAnimation scriptindeki PlayAnimation fonksiyonunu oynat(walk tagına sahip olanı) 
            HandleFacingDirection();//Düşmanın yüzünün sağa sola bakacağı fonksiyonu çalıştır.
        }
        else
        {
            CheckIfAttackFinished();
            //Idle animasyonunu oynattığımız fonksiyon.Burada 1.5 birim yakınlık oluştuğunda Idle animasyonu oynayacak.Yürüme animasyonu mantıksız çünkü.
            Attack();//Saldırı yapan fonksiyon.
        }
    }
    private void HandleFacingDirection()
    {
        tempScale = transform.localScale;//Mevcut skalayı geçici skalaya ata.
        if (transform.position.x > playerTarget.position.x)
            //Eğer Enemy'nin X düzlemindeki konumu Player'in X düzlemindeki konumundan büyük ise;
            tempScale.x = Mathf.Abs(tempScale.x);//tempScale'in mutlak değerini alıp tekrar ata.
        else//Yoksa
            tempScale.x = -Mathf.Abs(tempScale.x);//tempScale'in mutlak değerini - olarak alıp tekrar ata.
        transform.localScale = tempScale;//tempScale'i localScale'e ata.
        //Yukarıdaki kod ile Enemy'nin yüzü Player'in olduğu tarafa bakacak. Solunda ise soluna, sağında ise sağına.
        //Mantık: Enemy'nin ölçeği x:1,y:1,z:1 şeklinde. Matf ile mutlak değerini alıp solda iken - mutlak değer aldırdık.
        //Sola giderken Scale X -1 oldu. Böylece sola bakar hale geldi. Sağa giderken sağa bakar şekilde 1 olarak kalmaya devam etti.
        healthBarTempScale = healthBarTransform.localScale;
        if (transform.localScale.x > 0)
            healthBarTempScale.x = Mathf.Abs(healthBarTempScale.x);
        else
            healthBarTempScale.x = -Mathf.Abs(healthBarTempScale.x);
        healthBarTransform.localScale = healthBarTempScale;
        //Aynı mantığı health barına yaptık. sola giderken sola doğru, sağa giderken sağa doğru bakacak. gözü yormayacak.
    }
    private void CheckIfAttackFinished()
    {
        if (Time.time > attackFinishedTimer)
            enemyAnimation.PlayAnimation(TagManager.IDLE_ANIMATION_NAME);
        //Düşman bize 2.5 sn'de 1 kez vuruyor. bize vurduktan sonra geçen 2.5 sn'de IDLE animasyonuna geçecek bu fonksiyon ile.
    }
    private void Attack()
    {
        if (Time.time > attackTimer)
        {
            attackFinishedTimer = Time.time + attackFinishedWaitTime;//Saldırı bitirme zamanlayıcısına attackFinishedWaitTime(yarım saniye) ekle.
            attackTimer = Time.time + attackWaitTime;//Mevcut zamana attackWaitTime(2.5) saniye ekle. attackTimer'a ata.
            enemyAnimation.PlayAnimation(TagManager.ATTACK_ANIMATION_NAME);//Atak animasyonunu oynat.
        }
    }
    private void EnemyAtacked()//Bu fonksiyon Animation event'ten erişilip, copun doktora vurduğu an çalışıyor.
    {
        enemyDamageArea.gameObject.SetActive(true);//enemyDamageArea'ı true yap.
        enemyDamageArea.ResetDeactivateTimer();//enemyDamageArea'daki ResetDeactivateTimer fonksiyonunu çalıştır.
    }
    public void EnemyDied()
    {
        enemyDied = true;//Düşman öldü mü? True yap.
        enemyAnimation.PlayAnimation(TagManager.DEATH_ANIMATION_NAME);//Ölme animasyonunu oynat.
        Invoke("DestroyEnemyAfterDelay", 1.5f);//Düşman ölünce 1.5 saniye sonra ekrandan yok edecek. 
    }
    private void DestroyEnemyAfterDelay()
    {
        Destroy(gameObject);//Ölen düşmanı yok edecek fonksiyon.
    }
}//Class Enemy
