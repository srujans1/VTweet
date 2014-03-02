<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Account_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
                <h2>Welcome to VTweet!</h2>
            </hgroup>
            <p>
          
        <asp:Button ID="UploadBtn" Text="Upload Video" OnClick="UploadBtn_Click" runat="server"   />
        <br />
         <asp:Button ID="SentBtn" Text="Sent Videos" OnClick="SentBtn_Click" runat="server"   />

            </p>
        </div>
    </section>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
</asp:Content>

