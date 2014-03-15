<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Reply.aspx.cs" Inherits="Account_Reply" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">
    <section class="featured">
        <div class="content-wrapper">
            <video id="vid1" runat="server" width="640" height="360" preload="metadata" controls="controls">
            </video>

        </div>
    </section>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <p style="text-align: center">
                <asp:Repeater ID="Responser" runat="server">
                    <ItemTemplate>

                        <p>
                            <video id="sampleMovie" preload="metadata" controls="controls">
                                <source src="<%# Eval("Url") %>" />
                            </video>
                            <br /> - By <asp:Label Text='<%# Eval("UserName") %>' runat="server"></asp:Label>
                            <br />

                        </p>



                    </ItemTemplate>
                </asp:Repeater>
        </ContentTemplate>
    </asp:UpdatePanel>

         <p style="text-align: center">

         <asp:FileUpload ID="VideoUploader" ValidateRequestMode="Enabled" runat="server" />
         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="VideoUploader"
             ErrorMessage=".mp4, .mov & wmb formats are allowed"
             ValidationExpression="(.+\.([Mm][Pp][4])|.+\.([Mm][Oo][Vv])|.+\.([Ww][Mm][Bb]))">

         </asp:RegularExpressionValidator>
         <br />
         <asp:Button runat="server" ID="Button1" Text="Upload" CausesValidation="false" UseSubmitBehavior="false" OnClick="UploadFileButton_Click" />
         <br />

     </p>


</asp:Content>

