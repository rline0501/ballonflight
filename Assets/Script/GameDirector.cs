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
        //�Q�[���J�n��ԂɃZ�b�g
        isGameUp = false;
        isSetUp = false;

        //FloorGenerator�̏���
        SetUpFloorGenerators();

        //TODO �e�W�F�l���[�^���~
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
            //floorGenerators[i].SetUpGenerator(this);
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
            Debug.Log("�����X�^�[�g");
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
    }

    /// <summary>
    /// �Q�[���I��
    /// </summary>
    public void GameUp()
    {
        //�Q�[���I��
        isGameUp = true;

        //TODO �e�W�F�l���[�^���~
        Debug.Log("������~");
    }



}
