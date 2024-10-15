using System;

public class Bag
{
    public int QuantityCoins { get; private set; }

    public void AddCoin(int numberCoin)
    {   
        if (numberCoin < 0)
            throw new ArgumentOutOfRangeException(nameof(numberCoin));

        QuantityCoins += numberCoin;
    }
}