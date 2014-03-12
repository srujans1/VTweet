<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="UploadVideo.aspx.cs" Inherits="Account_UploadVideo" %>

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
          
        <asp:FileUpload ID="VideoUploader" ValidateRequestMode="Enabled" runat="server"/>  
        <asp:RegularExpressionValidator ID="uplValidator" runat="server" ControlToValidate="VideoUploader"
 ErrorMessage=".mp4, .mov & wmb formats are allowed" 
 ValidationExpression="(.+\.([Mm][Pp][4])|.+\.([Mm][Oo][Vv])|.+\.([Ww][Mm][Bb]))">

            </asp:RegularExpressionValidator>
        <asp:Button runat="server" ID="UploadFileButton" Text="Upload" OnClick="UploadFileButton_Click"/>
        <br />

       <asp:Label ID="lblPath" runat="server"></asp:Label>
            </p>
        </div>
    </section>
    
    <div>
        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
</asp:Content>
        
 
