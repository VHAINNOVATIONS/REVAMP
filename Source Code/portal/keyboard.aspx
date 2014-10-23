<%@ Page Language="C#" AutoEventWireup="true" CodeFile="keyboard.aspx.cs" Inherits="keyboard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
<title>Intellica On Screen Keyboard</title>
<script language="javascript">
var strValue = '<%Response.Write(Convert.ToString(Request.QueryString[2]));%>';
var strCapsLock = 'OFF';
var strShift = 'OFF';
function OnKeyPress(key, value)
{	
	if(value == 'BACK_SPACE')//backspace
	{	
		strValue = strValue.slice(0, (strValue.length-1));
	}
	else if(value == 'CLEAR')//backspace
	{	
		strValue = '';
	}
	else if(value == 'SPACE')//space
	{	
		strValue += ' ';
	}
	else if(value == 'CAPS_LOCK')//enter
	{	
		if(strCapsLock == 'OFF')
		{
			strCapsLock = 'ON';
		}
		else
		{
			strCapsLock = 'OFF';
		}
	}
	else if(value == 'SHIFT')//shift
	{	
		if(strCapsLock == 'OFF')
		{
			strCapsLock = 'ON';
		}
		else
		{
			strCapsLock = 'OFF';
		}
		
		strShift = 'ON';
	}
	else
	{
		//handle caps lock
		if(strCapsLock == 'ON')
		{
			key = key.toUpperCase();
		}
		else
		{
			key = key.toLowerCase();
		}
		
		//add to the value
		strValue += key;
		
		//reset shift
		if(strShift == 'ON')
		{	
			if(strCapsLock == 'OFF')
			{
				strCapsLock = 'ON';
			}
			else
			{
				strCapsLock = 'OFF';
			}
			strShift = 'OFF';
		}
	}
	
	document.frmKeyBoard.typed_text.value = strValue;
}
//function
function stringReplace(originalString, findText, replaceText)
{
	var pos = 0;
	var len = originalString.length;
	pos = originalString.indexOf(findText);
	while(pos != -1)
	{
		pre = originalString.substring(0, pos);
		post = originalString.substring(pos+1, originalString.length);
		originalString = pre + replaceText + post;
		pos = originalString.indexOf(findText);
	}
	return originalString;
}

function CloseForm()
{	
	//alert(strValue);
	//replace(strValue, '_', ' ');
	
	strValue = stringReplace(strValue, '_', ' ');
	
		
	window.opener.ModalDialog.value = strValue; 
	window.close(); 
}        

//not strValue = stringReplace(strValue, ' ', '_');
</script>
</head>
<body bgcolor="#ffffff" text="#000000" link="#0000ff" vlink="#800080" alink="#ff0000">
	
<form name="frmKeyBoard" id="frmKeyBoard">
	
	<table width=700 border=1 bordercolor=darkgray cellpadding=0 cellspacing=0>
	<tr><td align=center>
	
	<!--typed text output-->
	<table border="0" cellspacing="0" cellpadding="1">
	<tr>
	<td align="left">
		<font face="verdana" size="+1"><%Response.Write(Convert.ToString(Request.QueryString[1])); %>:</font>
	</td>
	</tr>
	<tr>
	<td align="center">
		<input type="text" readonly id="typed_text" name="typed_text" size="110" maxlength="1000" value="" />
	</td>
	</tr>
	<tr>
	<td align="center">
		&nbsp;
	</td>
	</tr>
	</table>
	
	
	</td></tr>
	
	<tr>
	<td align=center>
	
	<!--NUMBERS-->
	<table border="0" cellspacing="0" cellpadding="3">
		<tr>
			<td>
				<%DrawKey2("~", "~");%>

			</td>
			<td>
				<%DrawKey2("`", "`");%>
			</td>
			<td>
				<%DrawNumberKey("1", "1"); %>
			</td>
			<td >
				<%DrawNumberKey("2", "2");%>
			</td>
			<td >
				<%DrawNumberKey("3", "3");%>
			</td>
			<td >
				<%DrawNumberKey("4", "4");%>
			</td>
			<td >
				<%DrawNumberKey("5", "5");%>
			</td>
			<td >
				<%DrawNumberKey("6", "6");%>
			</td>
			<td >
				<%DrawNumberKey("7", "7");%>
			</td>
			<td >
				<%DrawNumberKey("8", "8");%>
			</td>
			<td >
				<%DrawNumberKey("9", "9");%>
			</td>
			<td >
				<%DrawNumberKey("0", "0");%>
			</td>
			<td>
				<%DrawKey2("Back Space", "BACK_SPACE");%>
			</td>
		</tr>
	</table>
	
	</td>
	</tr>
	
	<tr>
	<td align=center>
	<!--QWERTY-->
	<table border="0" cellspacing="0" cellpadding="3">
		<tr>
			<td>
				<%DrawKey2("Caps Lock", "CAPS_LOCK"); %>
			</td>
			<td>
				<%DrawKey("Q", "Q"); %>
			</td>
			<td >
				<%DrawKey("W", "W");%>
			</td>
			<td >
				<%DrawKey("E", "E");%>
			</td>
			<td >
				<%DrawKey("R", "R");%>
			</td>
			<td >
				<%DrawKey("T", "T");%>
			</td>
			<td >
				<%DrawKey("Y", "Y");%>
			</td>
			<td >
				<%DrawKey("U", "U");%>
			</td>
			<td >
				<%DrawKey("I", "I");%>
			</td>
			<td >
				<%DrawKey("O", "O");%>
			</td>
			<td >
				<%DrawKey("P", "P");%>
			</td>
			<td >
				<%DrawKey2("\\", "\\");%>
			</td>
			<td>
				<%DrawKey2("Shift", "SHIFT");%>
			</td>
						
		</tr>
	</table>
	</td>
	</tr>
	
	
	<tr>
	<td align=center>
	<!--QWERTY-->
	<table border="0" cellspacing="0" cellpadding="3">
		<tr>
			<td>
				<%DrawKey("A", "A"); %>
			</td>
			<td >
				<%DrawKey("S", "S");%>
			</td>
			<td >
				<%DrawKey("D", "D");%>
			</td>
			<td >
				<%DrawKey("F", "F");%>
			</td>
			<td >
				<%DrawKey("G", "G");%>
			</td>
			<td >
				<%DrawKey("H", "H");%>
			</td>
			<td >
				<%DrawKey("J", "J");%>
			</td>
			<td >
				<%DrawKey("K", "K");%>
			</td>
			<td >
				<%DrawKey("L", "L");%>
			</td>
			<td >
				<%DrawKey2(";", ";");%>
			</td>
			<td >
				<%DrawKey2(":", ":");%>
			</td>
			<td >
				<%DrawKey2("<", "<");%>
			</td>
			
		</tr>
	</table>
	</td>
	</tr>
	
	<tr>
	<td align=center>
	<!--QWERTY-->
	<table border="0" cellspacing="0" cellpadding="3">
		<tr>
			<td>
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			</td>
			<td>
				<%DrawKey("Z", "Z"); %>
			</td>
			<td >
				<%DrawKey("X", "X");%>
			</td>
			<td >
				<%DrawKey("C", "C");%>
			</td>
			<td >
				<%DrawKey("V", "V");%>
			</td>
			<td >
				<%DrawKey("B", "B");%>
			</td>
			<td >
				<%DrawKey("N", "N");%>
			</td>
			<td >
				<%DrawKey("M", "M");%>
			</td>
			
			<td >
				<%DrawKey2(".", ".");%>
			</td>
			<td >
				<%DrawKey2("/", "/");%>
			</td>
			<td >
				<%DrawKey2("?", "?");%>
			</td>
			<td >
				<%DrawKey2("!", "!");%>
			</td>
			<td >
				<%DrawKey2(">", ">");%>
			</td>
			
		</tr>
	</table>
	</td>
	</tr>
	
	<tr>
	<td align=center>
	<!--QWERTY-->
	<table width="100%" border="0" cellspacing="0" cellpadding="3">
		<tr>
			<td width="25%">
			<%DrawClearBar("Clear", "CLEAR");%>
			</td>
			<td width="50%">
			<%DrawSpaceBar("Space", "SPACE");%>
			</td>
			<td width="25%">
			<%DrawEnterBar("Enter", "ENTER");%>
			</td>
		</tr>
	</table>
	</td>
	</tr>
	
	</table>
</form>
<script language="javascript" type="text/javascript">document.frmKeyBoard.typed_text.value = strValue;</script>
</body>
	
</html>
