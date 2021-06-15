using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    //�S�[���n�_�̃v���t�@�u���A�T�C��
    private GoalChecker goalHousePrefab;

    [SerializeField]
    //�q�G�����L�[�ɂ���Yuko_Player�Q�[���I�u�W�F�N�g���A�T�C��
    private PlayerController playerController;

    [SerializeField]
    //floorGenerator�X�N���v�g�̃A�^�b�`����Ă���Q�[���I�u�W�F�N�g���A�T�C��
    private FloorGenerator[] floorGenerators;

    [SerializeField]
    //RandomObjectGenerator�X�N���v�g�̃A�^�b�`����Ă���Q�[���I�u�W�F�N�g���A�T�C��
    private RandomObjectGenerator[] randomObjectGenerators;

    [SerializeField]
    //�q�G�����L�[�ɂ���AudioManager�X�N���v�g�̃A�^�b�`����Ă���Q�[���I�u�W�F�N�g���A�T�C��
    private AudioManager audioManager;

    //�Q�[���̏�������p�Btrue�ɂȂ�ƃQ�[���J�n
    private bool isSetUp;

    //�Q�[���̏I������p�Btrue�ɂȂ�ƃQ�[���I��
    private bool isGameUp;

    //�󒆏��̐�����
    private int generateCount;

    //generateCount�ϐ��̃v���p�e�B
    public int GenerateCount
    {
        set
        {
            generateCount = value;

            Debug.Log("������ / �N���A�ڕW���F" + generateCount + " / " + clearCount);

            if (generateCount >= clearCount)
            {
                //�S�[���n�_�𐶐�
                GenerateGoal();

                //�Q�[���I��
                GameUp();
            }
        }
        get{ 
    return generateCount;
    }
}

//�S�[���n�_�𐶐�����܂łɕK�v�ȋ󒆏��̐�����
public int clearCount;

    void Start()
    {
        //�^�C�g���ȍĐ�
        StartCoroutine(audioManager.PlayBGM(0));

        //�Q�[���J�n��ԂɃZ�b�g
        isGameUp = false;
        isSetUp = false;

        //FloorGenerator�̏���
        SetUpFloorGenerators();

        //TODO �e�W�F�l���[�^���~
        StopGenerators();

        Debug.Log("������~");
    }

    /// <summary>
    /// FloorGenerator�̏���
    /// </summary>
    private void SetUpFloorGenerators()
    {
        for (int i = 0; i < floorGenerators.Length; i++)
        {
            //FloorGenerator�̏����E�����ݒ���s��
            floorGenerators[i].SetUpGenerator(this);
        }
    }

    void Update()
    {
        //�v���C���[�����߂ăo���[���𐶐�������
        if(playerController.isFirstGenerateBallon && isSetUp == false)
        {
            //��������
            isSetUp = true;

            //TODO �e�W�F�l���[�^�𓮂����n�߂�
            ActivateGenerators();

            Debug.Log("�����X�^�[�g");

            //�^�C�g���Ȃ��I�����A���C���Ȃ��Đ�
            StartCoroutine(audioManager.PlayBGM(1));
        }
        
    }

    /// <summary>
    /// �S�[���n�_�̐���
    /// </summary>
    private void GenerateGoal()
    {
        //�S�[���n�_�𐶐�
        GoalChecker goalHouse = Instantiate(goalHousePrefab);

        //ToDO �S�[���n�_�̏����ݒ�
        Debug.Log("�S�[���n�_�A����");

        //�S�[���n�_�̏����ݒ�
        goalHouse.SetUpGoalHouse(this);

    }

    /// <summary>
    /// �Q�[���I��
    /// </summary>
    public void GameUp()
    {
        //�Q�[���I��
        isGameUp = true;

    //TODO �e�W�F�l���[�^���~
    StopGenerators();

        Debug.Log("������~");
    }

    /// <summary>
    /// �e�W�F�l���[�^���~����
    /// </summary>
    private void StopGenerators()
    {
        for(int i = 0; i < floorGenerators.Length; i++)
        {
            randomObjectGenerators[i].SwitchActivation(false);
        }

        for(int i = 0; i < floorGenerators.Length; i++)
        {
            floorGenerators[i].SwitchActivation(false);
        }
    }

    /// <summary>
    /// �e�W�F�l���[�^�𓮂����n�߂�
    /// </summary>
    private void ActivateGenerators()
    {
        for (int i = 0; i < randomObjectGenerators.Length; i++)
        {
            randomObjectGenerators[i].SwitchActivation(true);
        }

        for (int i = 0; i < floorGenerators.Length; i++)
        {
            floorGenerators[i].SwitchActivation(true);
        }
    }

    /// <summary>
    /// �S�[������
    /// </summary>
    public void GoalClear()
    {
        //�N���A�̋ȍĐ�
        StartCoroutine(audioManager.PlayBGM(2));
    }

}
