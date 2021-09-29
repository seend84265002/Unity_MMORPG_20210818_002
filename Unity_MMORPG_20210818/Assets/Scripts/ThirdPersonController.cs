using UnityEngine;          //�ޥ� Unity API  (�ܮw�A��ƻP�\��)
using UnityEngine.Video;    //�ޥ� �v�� API    

//�׹��� ���O ���O�W�� : �~�����O 
// MonoBehaviour Unity �����O :�n���b����W�@�w�n�~��
// �~�ӫ�|�ɦ������O������
// �b���O�H�Φ����W��K�[�T���׽u�|�K�[�K�n
// �`�Φ���:��� Frid �B�ݩ� Property(�ܼ�)�B ��k Method �B �ƥ� Event 
/// <summary>
/// Wen 2021.09.06
/// �ĤT�H�ٱ��
/// ���ʡA���D 
/// </summary>
public class ThirdPersonController : MonoBehaviour
{
    #region ��� Freld
    
    //�x�s�C����ơA�Ҧp:���ʳt�סA���D����..
    //�`�Υ|�j����:��ơA�B�I�ơA�r��A���L��
    //���y�k:�׹����A��������A���W��(���w �w�]��) ����
    //�׹���:
    //1.���} public  ���\��L���O�s���A��ܦb�ݩʭ��O�A�ݭn�վ㪺��Ƴ]�w�����}
    //2.�p�H private  �T���L���O�s���A���æb�ݩʭ��O�A �w�]��
    //   Unity �H�ݩʭ��O���D
    //   ��_�{���w�]�ȽЫ�....>Reset
    //  ����ݩ� Attribute :���U�����
    //  ����ݩʻy�k : [�ݩʦW��(�ݩʭ�)]
    //  Header ���D
    //  Tooltip ����:�ƹ����d�b���W�٤W�|��ܪ�����
    //  Range �d��:�i�H�ϥΦb�ƭ�������ƤW�A�Ҧp: int , float
    [Header("���ʳt��"), Tooltip("�Ψӽվ㨤�Ⲿ�ʳt��"), Range(0, 500)]
    public float speed = 10.5f;
    [Header("���D����"), Tooltip("�Ψӽվ㨤����D����"), Range(0, 1000)]
    public int jump = 100;

    [Header("�ˬd�a�O���")]
    [Tooltip("�ˬd����O�_�b�a�O�W��")]
    public bool v3floor;
    public Vector3 v3checkGroundoffect;
    [Range(0, 3)]
    public float v3radius = 0.2f;

    [Header("�����ɮ�")]
    public AudioClip Audiojump;
    public AudioClip Audiofloor;

    [Header("�ʵe�Ѽ�")]
    public string aniWalk = "�����}��";
    public string aniRun = "�]�B�}��";
    public string aniHurt = "����Ĳ�o";
    public string aniDie = "���`Ĳ�o";

    private AudioSource aud;
    private Rigidbody rig;
    private Animator ani;
    



    #region Unity �������
    /** �m�� Unity  �������       UML  ���ε{�� �}�o�M�� �ϥ�
    //�C�� color
    public Color color;
    public Color white = Color.white;                       //�����C��
    public Color yellow = Color.yellow;
    public Color color1 = new Color(0.5f, 0.5f, 0);         //�ۭq�C�� RGB
    public Color color2 = new Color(0, 0.5f, 0.5f, 0.5f);   //�ۭq�C�� RGBA   
    //�y�� Vector2-4
    public Vector2 v2;                                      // Vector2 2���Ŷ����   (x,y)
    public Vector2 v2Right = Vector2.right;                 //���k��
    public Vector2 v2UP = Vector2.up;                       //���e��
    public Vector2 v2One = Vector2.one;                         
    public Vector2 v2Custom = new Vector2(7.5f, 100.9f);
    public Vector3 v3 = new Vector3(1, 2, 3);               // Vector3  3���Ŷ����  (x,y,z)
    public Vector3 v3Forward = Vector3.forward;
    public Vector4 v4 = new Vector4(2, 1, 3, 5);            // Vector4  4���Ŷ����  (x,y,z,����)
    //���� �C�|��� enum
    public KeyCode key;
    public KeyCode move = KeyCode.W;
    public KeyCode jump = KeyCode.Space;

    //�C���������: ������w�w�]��
    // �s�� Project �M�פ������
    public AudioClip sound;              // ���� mp3 ,ogg ,wav
    public VideoClip videoClip;          // �v�� mp4
    public Sprite sprite;                // �Ϥ� png ,jpge ���䴩 gif  
    public Texture2D texture2d;          // 2D �Ϥ� png ,jpge 
    public Material material;            // ����y   
    [Header("����")]
    //���� Component :�ݩʭ��W�i�H���|��
    public Transform tra;
    public Animation aniold;
    public Animator aniNew;
    public Light lig;
    public Camera cam;

    //���L�C
    //1.��ĳ���n�ϥΪ��W��
    //2.�ϥιL�ɪ�API
    /**/

    #endregion
    #endregion

    #region �ݩ� Property 

    #region �ݩʽm��
    
     
    //�x�s��ơA�P���ۦP
    //�t���b��:�i�H�]�w�s���v��
    //�ݩʻy�k:�׹��� ������� �ݩʦW��{ ��; �s; }
    /*
    public int readAndWrite { get; set; }
    public int read { get; }
    // ��Ū�ݩ�:�z�Lget �]�w�w�]�ȡA����r return ���Ǧ^��
    public int readValue {
        get
        {
            return 77;
        }
    }
    //�߼g�ݩʬO�T�
    //public int Write {  set; }
    // value �����O���w����
    private int _hp;
    public int hp {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
        }
    }
    

    public KeyCode keyJump;
    */


    #endregion
    #endregion

    #region ��k Method
    //�w�q�P��@ �������{�����϶��A�\��
    //��k�y�k : �׹��� �Ǧ^������� ��k�W�� (�Ѽ�1,...�Ѽ�N){ �{���϶� }
    //�`�ζǦ^���� : �L�Ǧ^ void - ����k�S���Ǧ^���
    //�榡�� �ƪ� ctrl + K + D
    //�P�| ctrl + M O
    //�i�} ctrl + M L
    //�W���C�⬰�H���� - �S���Q�I�s
    //�W���C�⬰���� - ���Q�I�s

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="speedmove">���ʳt��</param>    
    private void Move(float speedmove)
    {
        //�Ш��� Animator �ݩ� Apply Root Motion :�Ŀ�ɨϥΰʵe�첾��T
        //����.�[�t�� = �T���V�q�A�[�t�ץΨӱ������ �T�Ӷb�V���B�ʳt��
        //�e��*��J�� =���ʳt��
        //�ϥΫe�ᥪ�k�b�V�B�ʨëO���쥻���a�ߤޤO
        rig.velocity = Vector3.back * moveInput("Vertical") * -speedmove+
                       Vector3.left * moveInput("Horizontal") *-speedmove+
                       Vector3.up * rig.velocity.y;

    }
    /// <summary>
    /// ���ʫ����J
    /// </summary>
    /// <param name="axisName">�n�����b�V�W��</param>
    /// <returns>�^�ǭȯB�I��0</returns>
    private float moveInput( string axisName)
    {
        return Input.GetAxis(axisName);
    }
    /// <summary>
    /// �ˬd�a�O
    /// </summary>
    /// <returns>�^�ǥ��L��false</returns>
    private bool CheckGround()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position +
            transform.right * v3checkGroundoffect.x +
            transform.up * v3checkGroundoffect.y +
            transform.forward * v3checkGroundoffect.z
            , v3radius, 1<<3); //�]���O3�ҥH�g 1<<3   �H������  10�N�O 1<<10
        //print("�y��I��Ĥ@�Ӫ��� : " + hits[0].name);
        // �Ǧ^ �I���}�C�ƶq > 0 �A�u�n�I����w�ϼh�A����N�N��b�a���W 
        return hits.Length>0;
    }
    /// <summary>
    /// ���D
    /// </summary>
    private void Jump()
    {
        print("�y��I��Ĥ@�Ӫ��� : " + CheckGround());
    }
    /// <summary>
    /// ��s�ʵe
    /// </summary>
    private void updata()
    {

    }

    /*
    private void Text()
    {
        print("�ڬO�ۭq����k~~");
    }
    private int RetureJump()
    {
        return 999;
    }
    */
    #region �Ѽƻy�k
    /*
    //�Ѽƻy�k : ������� �ѼƦW��
    private void Skill(int damage,string effect = "�ǹЯS��",string sound ="�سس�")
    {
        print("�Ѽƪ���-�ˮ`��:"  + damage);
        print("�Ѽƪ���-�ޯ�S��:" + effect);
        print("�Ѽƪ���-����:" + sound);
    }
    
    /// <summary>
    /// �p��BMI����k BMI=weight/(height*height)
    /// </summary>
    /// <param name="weight">�魫�A��줽��</param>
    /// <param name="height">�����A��줽��</param>
    /// <param name="name">�m�W</param>
    /// <returns></returns>
    private float BMI(float weight,float height,string name)
    {
        print(name + "��BMI��:");
        return weight / (height * height);
    }
    
    //���ϥΰѼơA���C���@�P�X�R��
    private void skill100()
    {
        print("�Ѽƪ���-�ˮ`��" + 100);
        print("�ޯ�S��");
    }
    private void skill200()
    {
        print("�Ѽƪ���-�ˮ`��" + 200);
        print("�ޯ�S��");
    }
    private void skill1000()
    {
        print("�Ѽƪ���-�ˮ`��" + 1000);
        print("�ޯ�S��");
    }
    */
    #endregion
    #endregion

    public GameObject playerObject;
    #region �ƥ� Event 
    // �S�w�ɶ��I�|���檺��k�A�{�����J�f Start ���� Console Main
    // �}�l�ƥ�:�C���}�l����@���A�B�z��l�ơA�������..����
    private void Start()
    {
        #region  ��X��k
        /*print("Hello Wolrd");

        Debug.Log("�@��T��");
        Debug.LogError("���~�T��");
        Debug.LogWarning("ĵ�i�T��");*/
        #endregion
        #region   �ݩʽm��
        /*
        print("����� - ���ʳt��:" + speed);
        print("�ݩʸ�� - Ū�g���:" + readAndWrite);
        speed = 10f + speed;
        readAndWrite = 90;
        print("�ק�᪺���");
        print("����� - ���ʳt��:" + speed);
        print("�ݩʸ�� - Ū�g���:" + readAndWrite);

        //read = -7;  //�߿W�ݩʤ���]�w set
        print("��Ū�ݩ�:" + read);
        print("��Ū�ݩ�:" + readAndWrite);

        //�ݩʦs���m��
        print("�eHP:" + hp);
        hp = 100;
        print("��HP:" + hp);

        print(BMI(73f, 1.7f, "�A�O��??"));
        
        // �I�s�ۭq����k:��k�W��();
        Text();
        //�I�s���Ǧ^�Ȫ���k
        //1.�ϰ��ܼƫ����O���w�Ǧ^��-�ϰ��ܼƶȯ�b�����c(�j�A��)���s��
        int j = RetureJump();
        print("���D��:" + j);
        //2.�N�Ǧ^��k���Ȩϥ�
        print("A���D��:" + (RetureJump() + 1));

        Skill(300);
        Skill(500,sound:"������");         //���h�ӰѼƥi�ϥΫ��W�Ѽƪ��y�k  �Ѽƻy�k:��  
        */
        #endregion

        //move(100f);
        //move();
        //Floor();
        //Jump();
        //Update();
        //1.�������W�١A���o����(����(��������)) ��@ ��������;
        aud = playerObject.GetComponent(typeof(AudioSource)) as AudioSource;
        //2.���}���C������A���o���<�x��>();
        rig = gameObject.GetComponent<Rigidbody>();
        //3.���o����<�x��>();
        //���O�i�H�ϥ��~�����O(���˧O)�������A���}�ΫO�@
        ani = GetComponent<Animator>();
    }


    //��s�ƥ�:�@������� 60 �� �A60FPS -Frame Per Secound �A�ΨӳB�z�D�ʹB�ʡA���ʪ���A��ť���a��J����C
    private void Update()
    {
        //print("YO YO YO~");
        //Move(speed);
        CheckGround();
        Jump();
    }

    
     
    // �T�w��s�ƥ� : �T�w 0.02 �����@��
    //�ΨӳB�z���z�欰: �Ҧp Rigidbody API
    private void FixedUpdate()
    {
        //Move(0);
    }
    private void OnDrawGizmos()
    {
        //1.���w �C��
        //2.ø�s�ϧ�

        Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
        // transform �P���}���b�P���h�� transform ����
        Gizmos.DrawSphere(transform.position+
            transform.right*v3checkGroundoffect.x+
            transform.up*v3checkGroundoffect.y+
            transform.forward*v3checkGroundoffect.z
            , v3radius);   //Gizmos.DrawSphere(�y�� x y z �A�b�|)
    }
    #endregion



}
