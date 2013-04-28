<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KataGasolineras.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Kata Gasolineras</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/Site.css" rel="stylesheet" type="text/css" />
    <script src="http://cdn.jquerytools.org/1.2.7/full/jquery.tools.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="Wrapper">
            <div id="ToolBox" class="Container">
                <div id="ToolPanel" style="background: lightgray; padding: 10px">
                    <asp:Label ID="LabelLattitude" CssClass="label" runat="server">Lattitude</asp:Label>
                    <asp:TextBox ID="TxLattitude" CssClass="textbox" Text="41" type="number" min="0" max="90" required="required" runat="server"></asp:TextBox>
                    <asp:Label ID="LbLongitude" CssClass="label" runat="server">Longitude</asp:Label>
                    <asp:TextBox ID="TxLongitude" CssClass="textbox" Text="-3,2" required="true" type="number" min="-180" max="180" runat="server"></asp:TextBox>
                    <asp:Label ID="LbConsumption" CssClass="label" runat="server">Consumption (Liters/100 km)</asp:Label>
                    <asp:TextBox ID="TxConsumption" CssClass="textbox" Text="10" required="true" type="number" min="1" runat="server"></asp:TextBox>
                    <asp:Label ID="LbLiters" CssClass="label" runat="server">Remaining Liters</asp:Label>
                    <asp:TextBox ID="TxLiters" CssClass="textbox" Text="12" required="true" type="number" min="1" runat="server"></asp:TextBox>
                    <asp:Button ID="BFill" runat="server" Text="Fill map" OnClick="BFillClick" />
                </div>
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="True"></asp:ScriptManager>
            <asp:UpdatePanel runat="server" ID="GMapPanel" class="Container" UpdateMode="Conditional" EnableViewState="True">
                <ContentTemplate>
                    <gmaps:GMap ID="GMapControl" Width="1000" Height="1000" enableServerEvents="True" OnMarkerClick="MarkerClick" runat="server"></gmaps:GMap>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="BFill" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
