<%@ Page Language="C#" AutoEventWireup="true" CodeFile="kiosk_list_frame.aspx.cs" Inherits="kiosk_list_frame" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script language="javascript" type="text/jscript">
function Rand()
{var randomnumber=Math.floor(Math.random()*50);
return randomnumber;}
function CloseForm()
{	
	//alert(strValue);
	//replace(strValue, '_', ' ');
	strValue = stringReplace(strValue, '_', ' ');

	window.opener.ModalDialog.value = strValue; 
	window.close(); 
}        
</script>
<html><head><title>Intellica On Screen List Selection</title></head>
<frameset rows="10%, 80%, 10%" framespacing="0" frameborder="0" border="0">
	<frame frameborder="0" name="frame_kiosk_list_top"    src="kiosk_list_top.aspx?<%=Request.QueryString%>"    scrolling="no"  noresize ></frame>
	<frame frameborder="0" name="frame_kiosk_list_middle" src="kiosk_list_middle.aspx?<%=Request.QueryString%>" scrolling="yes" noresize></frame>
	<frame frameborder="0" name="frame_kiosk_list_bottom" src="kiosk_list_bottom.aspx?<%=Request.QueryString%>" scrolling="no"  noresize></frame>
</frameset></html>
