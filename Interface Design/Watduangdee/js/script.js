$(document).ready(function(){
	/* Focus First Textbox */
	$('input[type=text]:first').focus();
	$(document).keydown(function(event){
		var sys=event.keyCode;
		var char=event.which;
		if(sys=='36'){$('input[type=text]:first').val('');$('input[type=text]:first').focus();return false;}//home btn
		else if(sys=='35'){$('input[type=submit]:last').focus();return false;}// end btn
	});
	$(".studentbox").colorbox({width:"730", height:"600", iframe:true});
	$(".textbox_news").colorbox({width:"905", height:"600", iframe:true});
	$(".login").colorbox({width:"695", height:"470", iframe:true});
	$(".msg_box").colorbox({width:"695", height:"600", iframe:true});
	$(".forgot").colorbox({width:"695", height:"390", iframe:true});
	$("a[rel='gallery']").colorbox({slideshow:true});
});