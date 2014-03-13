<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Account_Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
      <asp:Repeater ID="FriendsList" runat="server">
        <ItemTemplate>

            <p>
                
               <asp:Label runat="server" Text='<%# Eval("UserName") %>'></asp:Label>:
               <asp:Button ID="Button1" runat="server" Text="UnFollow" CommandArgument='<%# Eval("UserID") %>' CommandName="CmdUnFollow" OnClick="MyBtnHandler" />
 
            </p>
            


        </ItemTemplate>
    </asp:Repeater>


     <asp:Repeater ID="NonFriendsList" runat="server">
        <ItemTemplate>

            <p>
                
               <asp:Label ID="Label1" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>:
               <asp:Button ID="Button1" runat="server" Text="Follow" CommandArgument='<%# Eval("UserID") %>' CommandName="CmdFollow" OnClick="MyBtnHandler" />
 
            </p>
            


        </ItemTemplate>
    </asp:Repeater>

</asp:Content>

