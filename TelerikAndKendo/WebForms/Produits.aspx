<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Produits.aspx.cs" Inherits="TelerikAndKendo.WebForms.Produits" %>

<%@ Register Assembly="Telerik.Web.UI"
    Namespace="Telerik.Web.UI"
    TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <%--<telerik:RadScriptManager runat="server" />--%>

        <telerik:RadGrid ID="RadGridProducts"
                         runat="server"
                         AutoGenerateColumns="false"
                         AllowPaging="true"
                         AllowSorting="true"
                         PageSize="10"
                         CssClass="grid-container"
                         OnNeedDataSource="RadGridProducts_NeedDataSource"
                         OnInsertCommand="RadGridProducts_InsertCommand"
                         OnUpdateCommand="RadGridProducts_UpdateCommand"
                         OnDeleteCommand="RadGridProducts_DeleteCommand"
                         Width ="60%">

            <MasterTableView DataKeyNames="Id"
                             CommandItemDisplay="Top"
                             InsertItemPageIndexAction="ShowItemOnFirstPage"
                             EditMode="PopUp">

                <CommandItemSettings AddNewRecordText="Ajouter un produit" />

                <Columns>
                    <telerik:GridBoundColumn DataField="Id"
                                             HeaderText="ID"
                                             ReadOnly="true" />

                    <telerik:GridBoundColumn DataField="Name"
                                             HeaderText="Nom"
                                             SortExpression="Name" />

                    <telerik:GridNumericColumn DataField="Price"
                                               HeaderText="Prix"
                                               DataFormatString="{0:C}" />

                    <telerik:GridEditCommandColumn ButtonType="ImageButton" />
                    <telerik:GridButtonColumn CommandName="Delete"
                                              Text="Supprimer"
                                              ConfirmText="Confirmer la suppression" />
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
   </asp:Content>