<%@ Page Language="C#" AutoEventWireup="true" CodeFile="kiosk_list_middle.aspx.cs" Inherits="kiosk_list_middle" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
<title>Intellica On Screen List Selection - Middle</title>
<script language="javascript" type="text/javascript">
var strListID = '<%Response.Write(Request.QueryString[0]);%>'
var strQ = '<%Response.Write(Request.QueryString[1]);%>'
var strSelection = '<%Response.Write(Request.QueryString[2]);%>'
var strValue = '';

function DrawListTable()
{
	var cbo = parent.window.opener.document.getElementById(strListID);

	document.open();
	document.writeln("<table border=0 width=100% cellpadding=20 cellspacing=5>");

	for (var i=0; i<cbo.length; i++)
	{
		//alert(cbo.options[i].text);
		//cbo.selectedIndex = i;
		document.writeln("<tr><td ");
		document.writeln("id='");
		document.writeln(i);
		document.writeln("' style='cursor:pointer;cursor:hand;' onclick='javascript:CloseForm(");
		document.writeln(i);
		document.writeln(");' bgcolor='CEDAE2'>")
		document.writeln("<font face=verdana size=+1 color=black>")
		document.writeln(cbo.options[i].text);
		document.writeln("</font>")
		document.writeln("</td></tr>");
	}	

	document.writeln("</table>");
	document.close();
	
}

function CloseForm(i)
{	
	//alert('close');
	strValue = i;
	parent.window.opener.ModalDialog.value = strValue; 
	parent.window.close(); 
}        

</script>
</head>
<body marginwidth="0" bottommargin="0" rightmargin="0" topmargin="0" leftmargin="0">
<script language="javascript">DrawListTable();</script>
</body>
</html>

