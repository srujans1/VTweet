<%@ Page Title="Feed" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Account_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
                <h2>Welcome to VTweet!</h2>
            </hgroup>
              <asp:Button ID="UploadBtn" Text="Upload Video" OnClick="UploadBtn_Click" runat="server" />
        </div>
    </section>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">


      



    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>

            <p>
                <video id="sampleMovie" width="400" height="250" preload="metadata" controls="controls">
                    <source src="<%# Eval("Url") %>" />
                </video> By <asp:Label runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
               <br />
               <asp:Button ID="Button1" runat="server" Text="View Conversation" CommandArgument='<%# Eval("VideoID") %>' CommandName="ThisBtnClick" OnClick="MyBtnHandler" />
 
            </p>
            


        </ItemTemplate>
    </asp:Repeater>

    





</asp:Content>

