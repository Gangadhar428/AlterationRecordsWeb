function validate_login() {
	document.getElementById("txtPassword").value = $.trim(document.getElementById("txtPassword").value);
	document.getElementById("txtUserName").value = $.trim(document.getElementById("txtUserName").value);
	if (isEmpty(document.getElementById("txtUserName"))) {
		document.getElementById("fail").innerHTML = '<span class="ico_cancel">Please enter Username</span>';
		document.getElementById("fail").style.visibility = "visible";
		var tmr = setTimeout("HideMessageDiv('fail');", 3000);
		document.getElementById("txtUserName").focus();
		return false;
	}
	if (isEmpty(document.getElementById("txtPassword"))) {
		document.getElementById("fail").innerHTML = '<span class="ico_cancel">Please enter Password</span>';
		document.getElementById("fail").style.visibility = "visible";
		var tmr = setTimeout("HideMessageDiv('fail');", 3000);
		document.getElementById("txtPassword").focus();
		return false;
	}
	document.getElementById("fail").style.visibility = "hidden";
	document.getElementById("fail").innerHTML = '';
	return true;
}

function HideMessageDiv(msgDivID) {
	document.getElementById(msgDivID).style.visibility = "hidden";
	document.getElementById(msgDivID).innerHTML = '';
}

function HideFailMessageDiv() {
	document.getElementById("fail").style.visibility = "hidden";
	document.getElementById("fail").innerHTML = '';
}

function ShowMessageDiv(msgDivID, msg) {
	if (msgDivID == "fail") {
		msg = '<span class="ico_cancel">' + msg + '</span>';
	}
	document.getElementById(msgDivID).style.visibility = "visible";
	document.getElementById(msgDivID).innerHTML = msg;
	var tmr = setTimeout("HideFailMessageDiv();", 3000);
}


function isEmpty(eltObj) {
	//return $.trim(eltObj.value) == "";
	var strValue = eltObj.value;

	if (strValue.length > 0) {
		return false;
	}
	return true;
}
function isEmail(obj) {
	str = obj.value;
	var filter = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i
	if (!filter.test(str)) {
		return false;
	}
	return true;
}