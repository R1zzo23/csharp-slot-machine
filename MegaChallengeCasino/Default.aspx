<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MegaChallengeCasino.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Image ID="leftImage" runat="server" Width="200px" />
            <asp:Image ID="middleImage" runat="server" Width="200px" />
            <asp:Image ID="rightImage" runat="server" Width="200px" />
            <br />
            <br />
            Your Bet:
            <asp:TextBox ID="yourBetTextBox" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="pullLeverButton" runat="server" OnClick="pullLeverButton_Click" Text="Pull The Lever" />
            <br />
            <br />
            <asp:Label ID="resultLabel" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="moneyLabel" runat="server"></asp:Label>
            <br />
            <br />
            1 Cherry&nbsp;&nbsp;&nbsp;&nbsp; ==&gt; x2 Your Bet<br />
            2 Cherries&nbsp; ==&gt; x3 Your Bet
            <br />
            3 Cherries&nbsp; ==&gt; x4 Your Bet<br />
            3 7&#39;s - Jackpot ==&gt; x100 Your Bet<br />
            <br />
            HOWEVER ... if there&#39;s even one BAR you win nothing!<br />
            <br />
            Withdrawal from the ATM:
            <asp:TextBox ID="withdrawalTextBox" runat="server"></asp:TextBox>
&nbsp;<asp:Button ID="withdrawalButton" runat="server" OnClick="withdrawalButton_Click" Text="Withdraw" />
            <br />
            <br />
            <asp:Label ID="totalPlayMoneyLabel" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
