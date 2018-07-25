using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PokerGame : MonoBehaviour {

    private int[] hand = { 1, 2, 3, 4, 5 };

    private static string[] suits = { "hearts", "spades", "diamonds", "clubs" };
    private static string[] ranks = { "1", "2", "3", "4", "5", "6", "7",
                   "8", "9", "10", "11", "12", "13" };

    //모든 플레이어의 핸드는 {{"heart","3"},{"spade","Ace"}} 와 같이 되어 있다고 생각한다. 



    // Use this for initialization
    void Start () {
        
        string[,] hand1 = { { "heart", "3" }, { "spade", "3" }, { "clubs", "3" }, { "diamonds", "3" }, { "clubs", "6" } }; //four of kind
        string[,] hand2 = { { "heart", "3" }, { "spade", "3" }, { "clubs", "3" }, { "diamonds", "6" }, { "clubs", "6" } }; //full house
        string[,] hand3 = { { "heart", "3" }, { "heart", "4" }, { "heart", "5" }, { "heart", "6" }, { "heart", "10" } }; //flush

        string[,] hand4 = { { "heart", "3" }, { "spade", "4" }, { "clubs", "5" }, { "diamonds", "6" }, { "clubs", "7" } }; //straight
        string[,] hand5 = { { "heart", "3" }, { "spade", "3" }, { "clubs", "3" }, { "diamonds", "10" }, { "clubs", "6" } };//three of kind 
        

        string[,] hand6 = { { "heart", "3" }, { "spade", "3" }, { "clubs", "5" }, { "diamonds", "5" }, { "clubs", "6" } }; //two pair
        string[,] hand7 = { { "heart", "3" }, { "spade", "4" }, { "clubs", "3" }, { "diamonds", "10" }, { "clubs", "6" } }; //one pair

        
        Debug.Log(PlayText(hand1));
        Debug.Log(PlayText(hand2));
        Debug.Log(PlayText(hand3));
        Debug.Log(PlayText(hand4));
        Debug.Log(PlayText(hand5));
        Debug.Log(PlayText(hand6));
        Debug.Log(PlayText(hand7));

        Debug.Log(CompareHands(hand7, hand4));





    }
	
	// Update is called once per frame
	void Update () {
       
		
	}

    public int Count_Number(string[,] PlayerHand,int index) // 포커 핸드에서 index 번째 카드와 같은 수를 가진 카드가 총 몇장인지 세는 함수 
    {
        int counter=0;
        
        for (int i=0;i < PlayerHand.GetLength(0); i++)
        {
            if(PlayerHand[i,1] == PlayerHand[index,1])
            {
                counter++;
            }
        }
        
        

        return counter;
    }
    public bool NotEnoughtCard(string[,] PlayerHand)
    {
        for (int i=0; i< PlayerHand.GetLength(0); i++)
        {
            if (PlayerHand[i,0] == null){
                return true;
            }
            
        }
        return false;
    }
    public bool IsOnePair(string[,] PlayerHand) //같은 숫자 2장으로 이루어진 경우 
    {
        bool result = false;
        int counter = 0;
        for (int i = 0; i < PlayerHand.GetLength(0); i++)
        {
            counter += Count_Number(PlayerHand, i);
        }
        if (counter == 7)
        {
            result = true;
        }
        return result;
    }
    public bool IsTwoPair(string[,] PlayerHand) // 같은 숫자 2장의 조합이 두 개 이상 나오는 경우 
    {
        bool result = false;
        int counter = 0;
        for (int i = 0; i < PlayerHand.GetLength(0); i++)
        {
            counter += Count_Number(PlayerHand, i);
        }
        if (counter == 9)
        {
            result = true;
        }
        return result;
    }
    public bool IsThreeOfAKind(string[,] PlayerHand) //같은 숫자 3장을 가지는 조합 
    {
        bool result = false;
        int counter = 0;
        for (int i = 0; i < PlayerHand.GetLength(0); i++)
        {
            counter += Count_Number(PlayerHand, i);
        }
        if (counter == 11)
        {
            result = true;
        }
        return result;
    }
    public bool IsStraight(string[,] PlayerHand) //5장의 카드의 숫자가 순서대로 나오는 경우
    {
        bool result = false;
        int counter = 0;
        string[] numbers = { null, null, null, null, null };
        for (int i = 0; i < PlayerHand.GetLength(0); i++)
        {
            numbers [i] = PlayerHand[i, 1];
            counter += Count_Number(PlayerHand, i);
        }
        if (counter == 5)
        {
            
            Array.Sort(numbers, 0, numbers.Length);
            int num1 = int.Parse(numbers[0]);
            int num2 = int.Parse(numbers[1]);
            int num3 =int.Parse(numbers[2]);
            int num4 =int.Parse(numbers[3]);
            int num5 =int.Parse(numbers[4]);
            

            if ((num2 == num1+1)&& (num3 == num1 + 2) && (num4 == num1 + 3) && (num5 == num1 + 4))
            {
                result = true;

            }


            
        }
        return result;

    }
    public bool IsFlush(string[,] PlayerHand) //모든 무늬가 같은 조합 
    {
        bool result= true;
        String suit = PlayerHand[0, 0];
        for (int i = 0; i < PlayerHand.GetLength(0); i++)
        {
            if (suit != PlayerHand[i, 0])
            {
                result = false;
                break;
            }
        }
        return result;

    }
    public bool IsFullHouse(string[,] PlayerHand) //쓰리오브 어 카인드와 원페어가 같이 있는 조합 
    {
        bool result = false;
        int counter = 0;
        for (int i = 0; i < PlayerHand.GetLength(0); i++)
        {
            counter += Count_Number(PlayerHand, i);
        }
        if (counter == 13)
        {
            result = true;
        }
        return result;
    }
    public bool IsFourOfKind(string[,] PlayerHand) // 같은 숫자 4개를 가지는 조합 
    {
        bool result = false;
        int counter = 0;
        for (int i=0;i < PlayerHand.GetLength(0); i++)
        {
            counter += Count_Number(PlayerHand, i);
        }
        if (counter == 17)
        {
            result = true;
        }
        return result;
    }

    public bool IsStraightFlush() // 5장의 카드가 순서대로 나오면서, 무늬가 모두 같은 경우 
    {
        return false;
    }
    

    public String PlayText(string[,] PlayerHand)
    {
        if (NotEnoughtCard(PlayerHand)) return "Not enough cards";
       
        else if (IsFlush(PlayerHand)) return "Flush";
        else if (IsOnePair(PlayerHand)) return "OnePair";
        else if (IsTwoPair(PlayerHand)) return "TwoPair";
        else if (IsThreeOfAKind(PlayerHand)) return "ThreeOfKind";
        else if (IsStraight(PlayerHand)) return "Straight";
        else if (IsFullHouse(PlayerHand)) return "FullHouse";
        else if (IsFourOfKind(PlayerHand)) return "FourOfKind";
        else return "Your hand SUCKS";
    }

    public int PlayScore(string[,] PlayerHand)
    {
        if (NotEnoughtCard(PlayerHand)) return -5;
        else if (IsFlush(PlayerHand)) return 5;
        else if (IsOnePair(PlayerHand)) return 1;
        else if (IsTwoPair(PlayerHand)) return 2;
        else if (IsThreeOfAKind(PlayerHand)) return 3;
        else if (IsStraight(PlayerHand)) return 4;
        else if (IsFullHouse(PlayerHand)) return 6;
        else if (IsFourOfKind(PlayerHand)) return 7;
        else return 0;
    }
    public int SuitCompare(string suit)
    {
        if(suit == "spades")
        {
            return 4;
        }
        else if (suit == "diamonds")
        {
            return 3;

        }
        else if (suit == "hearts")
        {
            return 2;
        }
        else return 1;
    }
    public int CompareHands(string[,] PlayerHand1, string[,] PlayerHand2) //if player 1 hand is better, return 0. else, return 1
    {
        if (PlayScore(PlayerHand1) > PlayScore(PlayerHand2))
        {
            return 0;
        }
        else if (PlayScore(PlayerHand1) < PlayScore(PlayerHand2))
        {
            return 1;
        }
        else //숫자 비교 
        {
            int Player1CardNum = 0;
            int Player1SuitNum = 0;
            int Player2CardNum = 0;
            int Player2SuitNum = 0;

            for (int i = 0; i < PlayerHand1.GetLength(0); i++)
            {
                Player1CardNum += int.Parse(PlayerHand1[i, 1]);
                Player1SuitNum += SuitCompare(PlayerHand1[i, 0]);
                Player2CardNum += int.Parse(PlayerHand2[i, 1]);
                Player2SuitNum += SuitCompare(PlayerHand2[i, 0]);
            }
            if (Player1CardNum > Player2CardNum)
            {
                return 0;
            }
            else if (Player1CardNum < Player2CardNum)
            {
                return 1;
            }
            else
            {
                if (Player1SuitNum > Player2SuitNum)
                {
                    return 0;
                }
                else return 0;

            }
            
        }
    }
}
