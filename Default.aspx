<%@ Page Title="VTweet" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
                <h2>The ultimate social networking experience.</h2>
            </hgroup>
            
        </div>
    </section>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>We suggest the following:</h3>
    <ol class="round">
        <li class="one">
            <h5>Getting Started</h5>
            VTweet allows you to share videos over your network. A great site for social networking. Please register yourself.
             
        </li>
        <li class="two">
            <h5>Upload Video</h5>
            Upload video and send it across your social network
           
        </li>
        <li class="three">
            <h5>Reply to videos</h5>
            You can easily reply to a video posted, with another video.
        </li>
    </ol>
</asp:Content>