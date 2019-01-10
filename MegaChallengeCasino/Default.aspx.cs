using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MegaChallengeCasino
{
    public partial class Default : System.Web.UI.Page
    {
        double betAmount = 0.0;
        double withdrawalAmount = 0.0;
        //double playersMoney = 100.0;
        //double changeInPlayersMoney = 0;
        Random random = new Random();
        String[] reelValues = new string[3];
        Image[] reelImages = new Image[3];

        protected void Page_Load(object sender, EventArgs e)
        {
            reelImages[0] = leftImage;
            reelImages[1] = middleImage;
            reelImages[2] = rightImage;
            if (!IsPostBack)
            {
                ViewState.Add("PlayerMoney", 100.0);
                ViewState.Add("TotalGamblingMoney", 100.0);
                randomizeReelValues();
                showResults();
                moneyLabel.Text = String.Format("Player's Money: {0:C}", Double.Parse(ViewState["PlayerMoney"].ToString()));
            }
        }

        protected void pullLeverButton_Click(object sender, EventArgs e)
        {
            // Is bet all numerical?
            if (checkIfBetIsValid() && playerHasSufficientFunds())
            {
                resultLabel.Text = "Bet is valid!";
                calculateRemainingPlayerMoney();

                // Get random names for all three reels.
                randomizeReelValues();

                // Display reel pictures.
                showResults();

                // Check for loss
                if (checkForBars() || checkForCherries() == 0) playerLost();
                // Check for win with cherries
                else if (checkForCherries() > 0) playerWon(playerWonWithCherries(checkForCherries()));
                // Check for win with jackpot
                else if (checkForJackPot()) playerWon(playerHitTheJackPot());
            }
            //else resultLabel.Text = "Bet is INVALID!";
        }

        private void calculateRemainingPlayerMoney()
        {
            double playerMoney = Double.Parse(ViewState["PlayerMoney"].ToString());
            double moneyRemaining = playerMoney - betAmount;
            ViewState["PlayerMoney"] = moneyRemaining;
            moneyLabel.Text = String.Format("Player's Money: {0:C}", Double.Parse(ViewState["PlayerMoney"].ToString()));
        }

        private bool playerHasSufficientFunds()
        {
            if (Double.Parse(ViewState["PlayerMoney"].ToString()) >= betAmount) return true;
            else return false;
        }

        private bool checkIfBetIsValid()
        {
            if (Double.TryParse(yourBetTextBox.Text.Trim(), out betAmount)) return true;
            else return false;
        }

        private double deductBetFromPlayersMoney(double playersMoney, double betAmount)
        {
            double moneyRemaining = playersMoney - betAmount;
            return moneyRemaining;
        }

        private void randomizeReelValues()
        {
            for (int i = 0; i < reelValues.Length; i++)
            {
                reelValues[i] = spinReel();
            }
        }

        private string spinReel()
                {
                    string[] images = new string[] { "Bar", "Bell", "Cherry", "Clover", "Diamond", "HorseShoe", "Lemon", "Orange", "Plum", "Seven", "Strawberry", "Watermelon" };
                    return images[random.Next(11)];
                }

        private void showResults()
        {
            for (int i = 0; i < reelImages.Length; i++)
            {
                reelImages[i].ImageUrl = "/Images/" + reelValues[i] + ".png";
            }
        }

        private bool checkForJackPot()
        {
            if (reelValues[0] == "Seven" && reelValues[1] == "Seven" && reelValues[2] == "Seven")
            {
                return true;
            }
            else return false;
        }

        private bool checkForBars()
        {
            if (reelValues[0] == "Bar" || reelValues[1] == "Bar" || reelValues[2] == "Bar")
            {
                return true;
            }
            else return false;
        }

        private int checkForCherries()
        {
            int cherryCount = 0;
            for (int i = 0; i < reelValues.Length; i++)
            {
                if (reelValues[i] == "Cherry") cherryCount++;
            }
            return cherryCount;
        }

        private void playerLost()
        {
            resultLabel.Text = String.Format("Sorry, you lost {0:C}. Better luck next time.", betAmount);
        }

        private void playerWon(double winnings)
        {
            resultLabel.Text = String.Format("You bet {0:C} and won {1:C}!", betAmount, winnings);
            double playerMoney = Double.Parse(ViewState["PlayerMoney"].ToString());
            double moneyRemaining = playerMoney + winnings;
            ViewState["PlayerMoney"] = moneyRemaining;
            moneyLabel.Text = String.Format("Player's Money: {0:C}", Double.Parse(ViewState["PlayerMoney"].ToString()));
        }

        private double playerWonWithCherries(int numberOfCherries)
        {
            double winnings;
            if (numberOfCherries == 1) winnings = betAmount * 2;
            else if (numberOfCherries == 2) winnings = betAmount * 3;
            else winnings = betAmount * 4;

            return winnings;
        }

        private double playerHitTheJackPot()
        {
            double winnings = betAmount * 100;
            return winnings;
        }

        protected void withdrawalButton_Click(object sender, EventArgs e)
        {
            if (checkIfWithdrawalIsValid())
            {
                double playerMoney = Double.Parse(ViewState["PlayerMoney"].ToString());
                double totalGambled = Double.Parse(ViewState["TotalGamblingMoney"].ToString());
                double moneyRemaining = playerMoney + withdrawalAmount;
                totalGambled += withdrawalAmount;
                ViewState["PlayerMoney"] = moneyRemaining;
                ViewState["TotalGamblingMoney"] = totalGambled;
                moneyLabel.Text = String.Format("Player's Money: {0:C}", Double.Parse(ViewState["PlayerMoney"].ToString()));
                totalPlayMoneyLabel.Text = String.Format("Total Money Deposited In This Slot: {0:C}", Double.Parse(ViewState["TotalGamblingMoney"].ToString()));
            }
        }

        private bool checkIfWithdrawalIsValid()
        {
            if (Double.TryParse(withdrawalTextBox.Text.Trim(), out withdrawalAmount)) return true;
            else return false;
        }
    }
}