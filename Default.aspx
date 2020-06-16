<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Zadanie6.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

     <style>
#container
  {
    width: 1000px;

    margin-left:auto;
    margin-right:auto;
    background-color: cornflowerblue;
    text-align:center;
  }


    </style>

















</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
            <h3>Przekształcanie tabelki z bazy danych SQLServer2000 w plik XML</h3>
    <div>
        <table>
            <tr>
                <td>Wybór pliku: </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <div>
                    <asp:FileUpload ID="FileUpload1" runat="server" Text="Import Data"/>
                    <asp:Button ID="btnImport" runat="server" Text="Importuj dane" OnClick="btnImport_Click" />
            <asp:Button ID="btnExport" runat="server" Text="Exportuj dane" OnClick="btnExport_Click" />

           
            <br />
            <asp:Label ID="lblMessage" runat="server"  Font-Bold="true" />
            <br />
            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false">
                <EmptyDataTemplate>
                    <div style="padding:10px">
                        Nie znaleziono zadnych danych!
                    </div>
               
                    </EmptyDataTemplate>
               
                <Columns>
                    <asp:BoundField HeaderText="ID Pracownika" DataField="EmployeeID" />
                    <asp:BoundField HeaderText="Nazwa firmy" DataField="CompanyName" />
                    <asp:BoundField HeaderText="Nazwa kontaktu" DataField="ContactName" />
                    <asp:BoundField HeaderText="Tytuł kontaktu" DataField="ContactTitle" />
                    <asp:BoundField HeaderText="Adres" DataField="EmployeeAddress" />
                    <asp:BoundField HeaderText="Kod pocztowy" DataField="PostalCode" />
                </Columns>
              
            </asp:GridView>
            <br />

           
        </div>
    </div>
        
        </div>
    </form>
</body>
</html>
