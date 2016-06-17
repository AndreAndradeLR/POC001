<%@ Page Language="C#" AutoEventWireup="true" Inherits="DetectScreen" Codebehind="DetectScreen.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var b = navigator.appName
        var ver = navigator.appVersion
        var thestart = parseFloat(ver.indexOf("MSIE")) + 1
        var bv = parseFloat(ver.substring(thestart + 4, thestart + 7))
        var h = screen.height;
        var w = screen.width;
        var c = screen.colorDepth;

        res = "&w=" + w + "&h=" + h + "&c=" + c + "&b=" + b + "&bv=" + bv; 
        top.location.href="DetectScreen.aspx?action=set"+res
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
