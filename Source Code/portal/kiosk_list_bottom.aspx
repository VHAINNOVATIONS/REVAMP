<%@ Page Language="C#" AutoEventWireup="true" CodeFile="kiosk_list_bottom.aspx.cs" Inherits="kiosk_list_bottom" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
<title>Intellica On Screen List Selection - Bottom</title>
<script language="javascript">
function Scroll()
{
	parent.frame_kiosk_list_middle.scrollBy(0,100);//x,y
}
</script>
</head>
<body bgcolor="LightGrey" marginwidth="0" bottommargin="0" rightmargin="0" topmargin="5" leftmargin="0">
<form id="form1" name="form1">

<table width="100%" cellpadding=15 cellspacing=0 border=0>

<tr>
<td onclick="javascript:Scroll()" bgcolor=LightGrey valign=center align=center>
<input onclick="javascript:Scroll()" type="button" value="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SCROLL DOWN&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" id="button1" name="button1" />
</td>
</tr>

</table>
</form>

</body>
</html>
