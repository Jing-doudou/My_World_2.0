using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public enum Block_Type
{
    Rock, Earth, Grass, Bronze, Iron, Masonry, Water
}
public struct FaceInformation
{
    public Vector3 v3;
    public string Other;
    public FaceInformation(Vector3 v, string s)
    {
        v3 = v;
        Other = s;
    }
}
public class MainGame : MonoBehaviour
{
    // Start is called before the first frame update
    public static MainGame mainGame;
    public static UnitSpace[,,] unit;
    public List<UnitSpace> prefab;
    public static Dictionary<string, FaceInformation> Direction = new Dictionary<string, FaceInformation>();
    public static int World_x = 20;
    public static int World_y = 5;
    public static int World_z = 20;

    public Block target;


    public void OnButton()
    {
        CreateBlock((int)target.Posion.x, (int)target.Posion.y + 1, (int)target.Posion.z, 2);
    }
    void Start()
    {
        mainGame = GameObject.Find("MainGame").GetComponent<MainGame>();
        InitDirection();
        InitWorld();
    }

    private void InitDirection()
    {
        Direction.Add("Up", new FaceInformation(new Vector3(0, 1, 0), "Down"));
        Direction.Add("Down", new FaceInformation(new Vector3(0, -1, 0), "Up"));
        Direction.Add("Front", new FaceInformation(new Vector3(0, 0, 1), "Behind"));
        Direction.Add("Behind", new FaceInformation(new Vector3(0, 0, -1), "Front"));
        Direction.Add("Left", new FaceInformation(new Vector3(-1, 0, 0), "Right"));
        Direction.Add("Right", new FaceInformation(new Vector3(1, 0, 0), "Left"));
    }
    public void InitWorld()
    {
        unit = new UnitSpace[World_x, 2 * World_y, World_z];
        //��ʼ�����絥λ�ռ�
        for (int i = 0; i < World_x; i++)
        {
            for (int j = 0; j < 2 * World_y; j++)
            {
                for (int k = 0; k < World_z; k++)
                {
                    unit[i, j, k] = Instantiate(prefab[0], transform);
                    unit[i, j, k].Posion = new Vector3(i, j, k);
                }
            }
        }
        //�������ڵķ�Χ�ڵ�Сһ�ŷ�Χ������block��ֹ�����±�Խ��
        for (int i = 1; i < World_x - 1; i++)
        {
            for (int j = 1; j < World_y - 1; j++)
            {
                for (int k = 1; k < World_z - 1; k++)
                {
                    CreateBlock(i, j, k, 1);
                }
            }
        }
        //�����������
        CreatingTerrain();
        CreateEarth();
    }

    //�����ϲ��ʯͷBlock�ϴ���������
    public void CreateEarth()
    {
        for (int i = 1; i < World_x - 1; i++)
        {
            for (int k = 1; k < World_z - 1; k++)
            {
                int topBlock = 0;
                for (int j = 1; j < 2 * World_y - 1; j++)
                {
                    if (unit[i, j + 1, k].unitType == UnitType.Null && unit[i, j, k].unitType == UnitType.Block)
                    {
                        topBlock = j + 1;
                    }
                }
                if (topBlock == 0)
                {
                    topBlock = 1;
                }
                for (int l = 0; l < 4; l++)
                {
                    CreateBlock(i, topBlock + l, k, 2);
                }
            }
        }
    }

    private void CreatingTerrain()
    {
        //��������ص㣬�����С��
        int changeNum = UnityEngine.Random.Range(2, Math.Max(2, World_x / 4));
        bool succ;
        {
            //����ɽ����¼�ص�
            for (int i = 0; i < changeNum; i++)
            {
                succ = CreateMountainOnTopBlock();
                //���ʧ�ܣ���˴β���
                if (!succ)
                {
                    changeNum--;
                }

            }
        }
        changeNum = UnityEngine.Random.Range(4, Math.Max(4, World_x / 2));
        succ = false;
        {
            //�����ݵأ���¼�ص�
            for (int i = 0; i < changeNum; i++)
            {
                succ = CreatePicOnTopBlock();
                //���ʧ�ܣ���˴β���
                if (!succ)
                {
                    changeNum--;
                }

            }
        }
    }

    private bool CreateMountainOnTopBlock()
    {

        //���������
        int _x = UnityEngine.Random.Range(1, World_x - 1);
        int _z = UnityEngine.Random.Range(1, World_z - 1);
        //�������һ����
        for (int j = 1; j < 2 * World_y - 1; j++)
        {
            //����һ��Ϊ��ʱ
            if (unit[_x, j, _z].unitType == UnitType.Null)
            {
                //����һ��Բ��
                int h = 0;//ɽ�ĸ߶�
                while (h < 4)
                {
                    int r = UnityEngine.Random.Range(5, 5) - h;//ɽ�İ뾶
                    //ͨ��x���y��ֵ
                    int y1, y2;
                    for (int i = -r; i <= r; i++)
                    {
                        ReturnY(i, r, out y1, out y2);
                        //����y��ÿһ�н���ʵ����
                        for (int k = y2; k <= y1; k++)
                        {
                            //����ǵ�ͼ��ʱ,��һ�������࿪ʼ�������������ϴ�����������Ҫ�ж��ĸ������Ƿ�Խ��
                            if (_x + i >= World_x - 1 || _z + k >= World_z - 1 || _x + i < 1 || _z + k < 1 || j + h >= 2 * World_y - 1)
                            {
                                continue;
                            }
                            CreateBlock(_x + i, j + h, _z + k, 1);
                        }
                    }
                    h++;
                }
                return true;
            }
        }
        return true;
    }
    private void ReturnY(int x, int r, out int y1, out int y2)
    {
        double x_2 = x * x;
        y1 = (int)Math.Round(Math.Pow(r * r - x_2, 0.5f));
        y2 = -y1;
    }
    /// <summary>
    /// ����һ�������������һ�㿪ʼ����������ʧ�ܻ��ط��ؽ�
    /// </summary>
    public bool CreatePicOnTopBlock()
    {
        //���������
        int _x = UnityEngine.Random.Range(1, World_x - 1);
        int _z = UnityEngine.Random.Range(1, World_z - 1);
        //�������һ����
        for (int j = 1; j < 2 * World_y - 1; j++)
        {
            //����һ��Ϊ��ʱ
            if (unit[_x, j + 1, _z].unitType == UnitType.Null)
            {
                //ɾ��һ�����������ķ�Χ
                int h = 0;//�������ĸ߶�
                int l = UnityEngine.Random.Range(5, 7);//��������һ��ı߳�
                while (h < 3)
                {
                    //ÿɾ��һ�㣬��һ��ı߳��ͻ���С
                    for (int rx = h; rx < l - h; rx++)
                    {
                        for (int ry = h; ry < l - h; ry++)
                        {
                            //����ǵ�ͼ��ʱ,��Ϊ�����б���һ���㿪ʼ���Һ���ɾ�������Բ���Ҫ�ж�С�ڵ����
                            if (_x + rx > World_x - 1 || _z + ry > World_z - 1)
                            {
                                continue;
                            }
                            if (j - h <= 0)
                            {
                                break;
                            }
                            if (unit[_x + rx, j - h, _z + ry].unitType != UnitType.Null)
                            {
                                CreateBlock(_x + rx, j - h, _z + ry, -1);
                            }
                        }
                    }
                    h++;
                }
                return true;
            }
        }
        return true;
    }
    /// <summary>
    /// �ı�block
    /// </summary>
    public void CreateBlock(int _x, int _y, int _z, int index)
    {
        if (_x <= 0 || _x >= World_x || _y <= 0 || _y >= 2 * World_y - 1 || _z <= 0 || _z >= World_z)
        {
            return;
        }
        if (index == -1)
        {
            Block block = (Block)unit[_x, _y, _z];
            block.DesEvent();
            return;
        }
        Destroy(unit[_x, _y, _z].gameObject);
        unit[_x, _y, _z] = Instantiate(prefab[index], this.transform);
        //�����¼����ڵ���ש�����ر��ڵ�����

        unit[_x, _y, _z].Posion = new Vector3(_x, _y, _z);
    }
}
