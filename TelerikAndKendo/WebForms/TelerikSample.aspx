<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TelerikSample.aspx.cs" Inherits="TelerikAndKendo.WebForms.TelerikSample" %>

<%@ Register Assembly="Telerik.Web.UI"
    Namespace="Telerik.Web.UI"
    TagPrefix="telerik" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Telerik Web Forms Sample</title>
</head>
<body>
    <form runat="server">
        <telerik:RadScriptManager runat="server" />
        <telerik:RadGrid ID="RadGrid1" runat="server"
            AutoGenerateColumns="false">
            <MasterTableView>
                <Columns>
                    <telerik:GridBoundColumn DataField="Id" HeaderText="Id" />
                    <telerik:GridBoundColumn DataField="Name" HeaderText="Nom" />
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </form>
</body>
</html>
