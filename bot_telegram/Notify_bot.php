<?php


$botToken= "825286084:AAGMyqm01-ojoxU59Gq2CDx2tthQCQpdcPs";
$website = "https://api.telegram.org/bot".$botToken;
$con = mysqli_connect('mysql.hostinger.com.ua','u747826294_tuto','tutortutor','u747826294_tuto');

if (!empty($_GET["email"])&&!empty($_GET["message"])) 
{ 
	echo " Получены новые вводные: почта - ".$_GET["email"].", письмо - ".$_GET["message"];
	$email = $_GET["email"];
	$message = $_GET["message"];


	if(!$con){
		echo "База данных не работает -> Бот R.I.P.";
	}
	else{
		$result = mysqli_query($con,"SELECT id_telegram as 'id' FROM `u747826294_tuto`.`Teacher` WHERE email_teacher = '$email'");
		$num_rows = mysqli_num_rows($result);
		if ($num_rows == 0){
			echo "Пользователь не найден в бд";
		}
		else {
			$value = mysqli_fetch_array($result);


			if(!empty($_GET["url"])){
				$inline_button = array("text" => "Перейти к уведомлению","url" => $_GET["url"]);
				$inline_keyboard = [[$inline_button]];
				$keyboard=array("inline_keyboard"=>$inline_keyboard);
				sendMessageWithURL($value['id'],$message,$keyboard);
			}
			else 
				sendMessage($value['id'],$message);
		}





	} 
}
else { 
	echo "Переменные не дошли. Проверьте все еще раз."; 
}






function sendMessage($chatId, $message){
	$url = $GLOBALS['website']."/sendMessage?chat_id=".$chatId."&disable_web_page_preview=true&text=".urlencode($message);
	file_get_contents($url);
	echo "Успешно отправлено";
}
function sendMessageWithURL($chatId, $message, $reply){
	$url = $GLOBALS['website']."/sendMessage?chat_id=".$chatId."&disable_web_page_preview=true&text=".urlencode($message)."&reply_markup=".json_encode($reply);
	file_get_contents($url);
	echo "Успешно отправлено  c url";
}

?>