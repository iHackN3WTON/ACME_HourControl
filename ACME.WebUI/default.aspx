<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ACME.WebUI._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" integrity="sha384-HSMxcRTRxnN+Bdg0JdbxYKrThecOKuH5zCYotlSAcp1+c8xmyTe9GYg1l9a69psu" crossorigin="anonymous">
</head>
<body style="padding-top: 50px;">

    <form id="form1" runat="server">
        <div class="container">
            <div style="padding: 40px 15px; text-align: left;">
                <div class="input-group">
                    <label class="input-group-btn">
                        <span class="btn btn-primary">
                            Examinar...<asp:FileUpload ID="LoadFileUpload" CssClass="form-control LoadFile" accept=".txt" style="display: none;" runat="server"/>
                        </span>
                    </label>
                    <input type="text" id="FileNameText" class="form-control" readonly/>
                </div>
                <div></div>
                <asp:LinkButton ID="LoadOKLinkButton" CssClass="btn btn-primary" OnClick="LoadOKLinkButton_OnClick" style="margin-top: 10px;" runat="server">OK</asp:LinkButton>
                <div ID="OutputDiv" style="margin-top: 10px;" runat="server"></div>
            </div>
        </div>
        
    </form>
    <script src="https://code.jquery.com/jquery-1.12.4.min.js" integrity="sha384-nvAa0+6Qg9clwYCGGPpDQLVpLNn0fRaROjHqs13t4Ggj3Ez50XnGQqc/r8MhnRDZ" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js" integrity="sha384-aJ21OjlMXNL5UyIl/XNwTMqvzeRMZH2w8c5cRVpzpU8Y5bApTppSuUkhZXN0VxHd" crossorigin="anonymous"></script>
    <script>
        $('body', 'html').on('change', '.LoadFile', function () { $('#FileNameText').val($(this).val().replace(/\\/g, '/').replace(/.*\//, '')); });
        $('body', 'html').on('click', '#FileNameText', function() { $('.LoadFile').trigger('click'); });
    </script>
</body>
</html>
