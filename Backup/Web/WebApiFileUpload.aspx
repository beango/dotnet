<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebApiFileUpload.aspx.cs"
    Inherits="Web.WebApiFileUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="theme/css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />
    <link href="theme/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="form-group">
        <label for="exampleInputFile">
            选择文件：</label>
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="btnUpload" class="btn btn-default" runat="server" Text="上传" />
    </div>
    <div class="form-group">
        <div class="table-responsive">
            <table class="table table-bordered table-striped">
                <tbody>
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <a href="http://localhost:8346/FileUpload/DownFile?filename=测试目录\\<%#Container.DataItem.ToString().Substring(Container.DataItem.ToString().LastIndexOf("\\")+1)%>"
                                        target="_blank">
                                        <%#Container.DataItem.ToString()%></a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
    </form>
    <script src="theme/js/bootstrap.min.js" type="text/javascript"></script>
</body>
</html>
