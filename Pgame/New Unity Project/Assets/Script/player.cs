using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{ 
#region 欄位
[Header("移動速度"), Range(0,1000)]
public float speed = 5;
[Header("跳越高度"), Range(0,1000)]
public int jump = 350 ;
[Header("血量"), Range(0,2000)]
public float hp = 500;
    public int coin = 0 ;

public bool isGround;


 [Header("音效區域")]
    public AudioClip SoundHit;
    public AudioClip SoundSlide;
    public AudioClip SoundJump;
    public AudioClip SoundCoin;

    [Header("金幣數量")]
    public Text textCoin;

    public AudioSource aud;
 [Header("動畫區域")]
    public Animator ani;
[Header("物理性質")]
public Rigidbody2D rig;
    public CapsuleCollider2D cap;
    [Header("血條")]
    public Image imageHp;
    private float hpMax;

    #endregion

#region 方法
    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        //Time.deltaTime 一禎的時間
        //Update 內移動、旋轉、運動*Time.deltaTime
        //避免不同裝置執行速度不同
        transform.Translate(speed * Time.deltaTime, 0, 0); //變形.位移(x, y ,z)


    }
    /// <summary>
    /// 跳躍
    /// </summary>
    private void Jump()
    {
        //1布林值 = 輸入.按下按鍵(按鍵列舉.空白鍵)
        bool space = Input.GetKeyDown(KeyCode.Space);

        //2D 射線碰撞物件 = 2D 物理.射線碰撞(起點,方向,長度,圖層)
        //圖層語法: 1 << 圖層編號
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0.13f, -1.01f), -transform.up, 0.1f, 1 << 8);
      if(hit)
        {
            isGround = true;
            ani.SetBool("跳躍開關", false);
        }
      //如果碰到地板 是否在地板上 = 是
      else
        {
            isGround = false; // 否則 是否在地板上 = 否
        }
      if (isGround)
        {
            //如果 按下空白鍵
            if (space)
            {
                //動畫控制器.設定布林值("參數名稱",布林值)
                ani.SetBool("跳躍開關", true);
                //剛體.添加推力(二為向量)
                rig.AddForce(new Vector2(0, jump));
                //是否在地板上 = 否
                aud.PlayOneShot(SoundJump, 0.3f);

            }
        }
     
    }

    /// <summary>
    /// 滑行
    /// </summary>
    private void Slide()
    {
        
            bool ctrl = Input.GetKey(KeyCode.LeftControl);
            ani.SetBool("滑行開關", ctrl);

        // 如果 按下 左邊ctrl 播放一次音效
        //判斷式如果只有一行程式可以省略大括號
        if (Input.GetKeyDown(KeyCode.LeftControl)) aud.PlayOneShot(SoundSlide, 0.8f);
       
        // 如果 按下ctrl
        if (ctrl)
            {
                // 滑行 位移  尺寸 
                cap.offset = new Vector2(-0.1f, -1.5f);
                cap.size = new Vector2(1.35f, 1.35f);
            }
            // 否則
            else
            {
                // 站立 位移 0.13 -0.02 尺寸 0.65 1.97
                cap.offset = new Vector2(0.13f, -0.02f);
                cap.size = new Vector2(0.65f, 1.97f);
            }
        
    }
    /// <summary>
    /// 吃金幣
    /// </summary>
    private void EatCoin(GameObject obj)
    {
        coin++;                          //遞增1
        aud.PlayOneShot(SoundCoin, 1.2f); //播放音效
        textCoin.text = "金幣數量" + coin ;//文字介面.文字= 字串+整數
        Destroy(obj, 0);                    //刪除(金幣物件 , 延遲時間)
    }
    /// <summary>
    /// 受傷
    /// </summary>
    private void Hit(GameObject obj)
    {
        hp -= 30 ;
        aud.PlayOneShot(SoundHit);
        imageHp.fillAmount = hp / hpMax;
        Destroy(obj);
    }
    /// <summary>
    /// 死亡
    /// </summary>
    private void Dead()
    {

    }
    /// <summary>
    /// 過關
    /// </summary>
    private void Pass()
    {

    }


    #endregion

    #region 事件

    //碰撞(觸發)事件:
    //兩個物件必須要有一個勾選 Is Trigger
    //Enter 進入時執行一次
    //Stay 碰撞時執行 (一秒60次)
    //Exit 離開時執行一次
    //參數:紀錄碰撞到的碰撞知訊
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //如果 碰撞資訊.標籤 等於 金幣 吃掉金幣
        if (collision.tag == "Coin") EatCoin(collision.gameObject);
    }


    // 繪製圖示事件：繪製輔助線條，僅在 Scene 看得到
    private void OnDrawGizmos()
    {
        // 圖示.顏色 = 顏色.紅色
        Gizmos.color = Color.red;
        // 圖示.繪製射線(起點，方向)
        // transform 此物件的變形元件
        // transform.position 此物件的座標
        // transform.up 此物件上方      Y 預設為 1
        // transform.right 此物件右方   X 預設為 1
        // transform.forward 此物件前方 Z 預設為 1
        Gizmos.DrawRay(transform.position + new Vector3(0.13f, -1.01f), -transform.up * 0.1f);
    }

    private void Start()
    {
        hpMax = hp;
    }

    private void Update()
    {
        Jump();
        Slide();
        Move();
    }





    #endregion




}
