<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Project created by:</h2>
    </hgroup>

    <section class="contact">
        <p>
            <span class="label">Anshul Mehra:</span>
            <span><a href="mailto:anshul.mehra@nyu.edu">anshul.mehra@nyu.edu</a></span>
        </p>
        <p>
            <span class="label">Srujan Saggam:</span>
            <span><a href="mailto:srujan.saggam@nyu.edu">srujan.saggam@nyu.edu</a></span>
        </p>
    </section>
</asp:Content>