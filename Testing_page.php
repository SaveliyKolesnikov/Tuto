
<div>
	<b>Доступные имейлы:</b><br>
	vlad@gmail.com<br>
	vadym@gmail.com<br>
	anna@gmail.com<br>
	nastya@gmail.com<br>
	dima@gmail.com<br>
	sava@gmail.com<br>
</div>
<input id="email" onchange="setNotifyURL();" type="text" placeholder="Ваш имейл">
<h4>Генерировать код</h4>
<button onclick="generic()">Генерировать код</button>
<h4>Кнопка чтобы зарегестрироваться в боте</h4>
<a id="register" href=""><button>Перейти в бота</button></a>
<h4>Тестовое сообщение</h4>
<input id="text" onchange="setNotifyURL();" type="text" placeholder="Ваш текст">
<input id="link" onchange="setNotifyURL();" type="text" placeholder="Ссылка(необязательно)">

<button onclick="sendMessage();">Отправить</button>

<hr>
<h3>Логи:</h3>
<span id="result"></span>


<script
src="https://code.jquery.com/jquery-3.4.1.min.js"
integrity="sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo="
crossorigin="anonymous"></script>
<script type="text/javascript">
	var ref="";
	function sendMessage(){
		$.ajax({
			url: ref,
			success: function(data) {
				$('#result').html(data);
			}
		});
	}
	function generic(){
		var em = document.getElementById('email').value;
		let r = generateId(8);
		document.getElementById('register').href = "https://telegram.me/tut0_bot?start="+r;
		var url_generate="https://vladbogun.store/AVPZ/Memory_bot.php?email="+em+"&key="+r
		
		$.ajax({
			url: url_generate,
			success: function(data) {
				$('#result').html(data);
			}
		});
	}
	function dec2hex (dec) {
		return ('0' + dec.toString(16)).substr(-2)
	}

	function generateId (len) {
		var arr = new Uint8Array((len || 40) / 2)
		window.crypto.getRandomValues(arr)
		return Array.from(arr, dec2hex).join('')
	}
	function setNotifyURL(){
		var em = document.getElementById('email').value;
		var text = document.getElementById('text').value.replace(' ','%20');
		var link = document.getElementById('link').value;
		if(link!=''){
			ref = "https://vladbogun.store/AVPZ/Notify_bot.php?email="+em+"&message="+text+"&url="+link;
		}
		else{
			ref = "https://vladbogun.store/AVPZ/Notify_bot.php?email="+em+"&message="+text;

		}

	}


</script>