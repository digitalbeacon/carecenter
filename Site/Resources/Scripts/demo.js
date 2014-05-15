function demoSignIn(username) {
	$('#Username').val(username);
	$('#Password').val('demo');
	$('#save').click();
}
$(document).ready(function() {
	if ($('#signInPanel').length) {
		$('#demoAdminLink').click(function() { demoSignIn('demo.admin'); });
		$('#demoUserLink').click(function() { demoSignIn('demo.user'); });
	}
});
