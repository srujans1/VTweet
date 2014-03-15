<%@ Page Title="Users" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Account_Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">
    <section class="featured">
        <div class="content-wrapper">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <asp:Repeater ID="FriendsList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <p>

                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                                </p>
                            </td>
                            <td>
                                <p>
                                    <asp:Button ID="Button1" runat="server" Text="UnFollow" CommandArgument='<%# Eval("UserID") %>' CommandName="CmdUnFollow" OnClick="MyBtnHandler" />

                                </p>
                            </td>

                        </tr>

                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <hr />
            <table>
            <asp:Repeater ID="NonFriendsList" runat="server">
                <ItemTemplate>
                    <tr>
                        <td style="width:auto;text-wrap:normal">
                            <p>

                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                            </p>
                        </td>
                        <td>
                            <p>
                                <asp:Button ID="Button1" runat="server" Text="Follow" CommandArgument='<%# Eval("UserID") %>' CommandName="CmdFollow" OnClick="MyBtnHandler" />

                            </p>
                        </td>

                    </tr>

                </ItemTemplate>
            </asp:Repeater>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
            </div></section>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">

    

</asp:Content>

