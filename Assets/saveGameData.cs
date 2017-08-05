using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class saveGameData {

    public bool define = false;

    public int HP = 0;
    public int SP = 0;

    public int abilityHPMax = 0;
    public int HPBuff = 0;

    public int abilitySPMax = 0;
    public int SPBuff = 0;

    public int ATKLevel = 0;
    public int ATKlevelpercent = 0;
    public int ATKBuff = 0;

    public int DEFLevel = 0;
    public int DEFlevelpercent = 0;
    public int DEFBuff = 0;

    public int COIN = 0;
    public int POINT = 0;

    public int reviveTimes = 0;
    public bool revive_value = false;
    public int ResetTimes = 0;
    public int currentFloor = 0;
    public int currentLifeMaxFloor = 0;
    public int currentAlyreadyWatchAdsLevel;
    public int maxFloor = 0;

    public double runTimeDouble = 0;

    public item[] playerItem = new item[2];

    public vector2 playerCenter;

    public string lookDir;
    public int cameraEuler;

    public List<floor> allFloorVector2 = new List<floor>();
    public List<vector2> allChestVector2InMap = new List<vector2>();
    public List<vector2> allExitVector2InExit = new List<vector2>();
    public List<item> allItemData = new List<item>();
    public List<enemy> allEnemyData = new List<enemy>();

    //    public itemScript[] itemArrayClone = new itemScript[2];

    [System.Serializable]
    public class floor : vector2
    {
        public bool isSpike;
        public int curRoundCountDown;
    }

    [System.Serializable]
    public class enemy : vector2
    {
        public int  levelType;
        public int HP;
    }

    [System.Serializable]
    public class item : vector2
    {
        public itemType type;
        public int level;
    }

    [System.Serializable]
    public class vector2
    {
        public int X;
        public int Y;
    }

}
