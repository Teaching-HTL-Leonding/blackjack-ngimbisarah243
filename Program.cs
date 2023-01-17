const string STARS = "***";
const int JACK = 10;
const int ASS = 11;
const int QUEEN = 12;
const int KING = 13;

string card = string.Empty;
decimal guthaben = 100;

int jackOr10;
int rounds = 0;
int randomCard;
int playersValue;
int dealersValue;
int betvalue = 0;
int countCards = 0;

char yesNo = '0';
bool runGame = true;
bool validInput, InputIfAnotherCard;
bool bet;

PrintWelcome();
Blackjack();

void Blackjack()
{
    if (runGame == false) { PrintGoodbye(); }

    while (runGame)
    {
        InitializeGame();
        PlayersTurn();

    }

}

void PlayersTurn()
{
    playersValue = HandoutRandomCard(playersValue);
    PrintCard("You have", playersValue);
    AskForBet();

    do
    {
        AskIfAnotherCard();

        if (yesNo == 'n') { break; }
        playersValue = HandoutRandomCard(playersValue);
        PrintCard("You have", playersValue);

    } while (yesNo == 'y' && playersValue < 21);

    countCards = 0;

    if (yesNo == 'n' || playersValue > 21)
    {
        if (yesNo == 'n')
        {
            Console.WriteLine();
            DealersTurn();

        }
        else if (playersValue > 21)
        {
            Console.WriteLine("You busted.");


            if (guthaben <10)
            {
                runGame = false;
            }
            Blackjack();
        }
    }
    if (playersValue == 21)
    {
        DealersTurn();
    }
    if (playersValue == 21 && countCards == 2)
    {
        Console.WriteLine("You have Blackjack! You won with Blackjack!"); guthaben += betvalue * 1.5m; Blackjack();
    }
}
void DealersTurn()
{
    Console.WriteLine("Dealer's turn....");

    while (dealersValue <= 16)
    {
        dealersValue = HandoutRandomCard(dealersValue);
        PrintCard("Dealer has", dealersValue);
    }

    if (dealersValue > 16)
    {
        PrintWinner();
        Blackjack();
    }

    if (dealersValue == 21 && countCards == 2)
    {
        Console.WriteLine("Dealer won with Blackjack! ");
        Blackjack();
    }
}
void PrintWelcome()
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Write(STARS);
    Console.ResetColor();
    Console.Write(" WELCOME TO BLACKJACK ");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(STARS);
    Console.ResetColor();
    Console.WriteLine();
    Console.WriteLine("You have 100€ in your pocket. Try to double it!");
    Console.WriteLine("You will lose if you have no money left");
    Console.WriteLine();
}
void InitializeGame()
{
    playersValue = 0;
    dealersValue = 0;

    rounds++;

    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Write(STARS);
    Console.ResetColor();
    Console.Write($" ROUND {rounds}, you have {guthaben} Euro left ");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(STARS);
    Console.ResetColor();
    Console.WriteLine();
}
int HandoutRandomCard(int value)
{
    randomCard = Random.Shared.Next(2, 14);

    switch (randomCard)
    {
        case 2: value += 2; break;
        case 3: value += 3; break;
        case 4: value += 4; break;
        case 5: value += 5; break;
        case 6: value += 6; break;
        case 7: value += 7; break;
        case 8: value += 8; break;
        case 9: value += 9; break;
        case JACK:
            value += 10;
            jackOr10 = Random.Shared.Next(1, 3);
            if (jackOr10 == 1) { card = "Jack"; } 
            else { card = "10"; } break;
        case ASS: value += 11; card = "Ass"; break;
        case QUEEN: value += 10; card = "Queen"; break;
        case KING: value += 10; card = "King"; break;
    }

    if (randomCard == ASS && value > 21) { value -= 10; }

    countCards++;

    return value;
}
void PrintCard(string player, int value)
{
    if (randomCard >= 10) Console.WriteLine($"{player} {card}, hand value is {value}");
    else Console.WriteLine($"{player} {randomCard}, hand value is {value}");
}
void AskForBet()
{
    do
    {
        Console.Write($"How much do you want to bet? Bet must be >= 10 Euro and <= {guthaben} Euro. Press Enter for minimal bet.  ");
        bet = int.TryParse(Console.ReadLine(), out betvalue);

        validInput = betvalue >= 10 && betvalue <= guthaben && bet == true;

        Console.ForegroundColor = ConsoleColor.Red;

        if (!validInput)
        {
            if (bet == false)
            {
                Console.WriteLine("Invalid Input!!");
            }
            else if (betvalue < 10)
            {
                Console.WriteLine("Invalid input!! Bet has to be >= 10€ ");
                Console.ResetColor();
            }
            else if (betvalue > guthaben)
            {
                Console.WriteLine($"Invalid Input!! You can spend maximal {guthaben} Euro");
            }
        }

        Console.ResetColor();



    } while (!validInput);

    guthaben -= betvalue;

}
void AskIfAnotherCard()
{
    do
    {
        Console.Write("Do you want another card? (y/n) ");
        yesNo = Convert.ToChar(Console.ReadLine()!);

        InputIfAnotherCard = yesNo == 'y' || yesNo == 'n';

        if (!(yesNo == 'y' || yesNo == 'n'))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ungültige Eingabe!");
            Console.ResetColor();
        }

    } while (!InputIfAnotherCard);


}
void PrintGoodbye()
{
    Console.WriteLine();
    Console.BackgroundColor = ConsoleColor.Red;
    Console.WriteLine("GAME OVER....");
    Console.ResetColor();
    if (guthaben < 10) { Console.WriteLine("You don't have enough money to continue the game"); }
    Console.BackgroundColor = ConsoleColor.Magenta;
    Console.WriteLine("GOODBYE!");
    Console.ResetColor();
}

void PrintWinner()
{
    if (dealersValue > 21)
    {
        guthaben += betvalue * 2;
        Console.WriteLine("Dealer busted, you won!");
    }
    else if (dealersValue > playersValue)
    {
        Console.WriteLine("Dealer wins");
    }
    else if (dealersValue == playersValue)
    {
        Console.WriteLine("Standoff");
        guthaben += betvalue;
    }
    else if (playersValue > dealersValue)
    {
        guthaben += betvalue * 2;
        Console.WriteLine("You win");
    }
    if (guthaben >= 200)
    {
        Console.WriteLine($"You have {guthaben} Euro, you at least doubled your money!");
        runGame = false;
    }
    if (guthaben < 10) { runGame = false; }
}