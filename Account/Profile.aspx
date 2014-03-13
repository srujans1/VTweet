<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="Account_Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">

    <section class="featured">
        <div class="content-wrapper">
            <asp:Repeater ID="videoRepeater" runat="server">
        <ItemTemplate>

            <p>
                <video id="sampleMovie" width="640" height="360" preload="none" controls="controls">
                    <source src="<%# Eval("Url") %>" />
                </video>
            </p>



        </ItemTemplate>
    </asp:Repeater>

        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
</asp:Content>

