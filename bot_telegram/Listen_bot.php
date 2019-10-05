<?php


$botToken= "825286084:AAGMyqm01-ojoxU59Gq2CDx2tthQCQpdcPs";
$website = "https://api.telegram.org/bot".$botToken;
$con = mysqli_connect('mysql.hostinger.com.ua','u747826294_tuto','tutortutor','u747826294_tuto');



$update = file_get_contents("php://input");
$updateArray = json_decode($update, true);


$chatId = $updateArray["message"]["chat"]["id"];
$username = $updateArray["message"]["chat"]["username"];
$text = $updateArray["message"]["text"];

if(!$con){
	sendMessage($chatId, "База данных не работает -> Бот R.I.P.");
}
else{
	$query = "SELECT email_teacher as em FROM `u747826294_tuto`.`Teacher` WHERE id_telegram = '$chatId'";
	$result = mysqli_query($con,$query);
	$num_rows = mysqli_num_rows($result);
	if ($num_rows == 0){
		if(strlen($text)==15){
			$key = str_replace('/start ','',$text);
			if($key!='00000000'){
				$query = "SELECT email_teacher as em FROM `u747826294_tuto`.`Teacher` WHERE `temporary_code`='".$key."'";
				$result = mysqli_query($con,$query);
				$num_rows = mysqli_num_rows($result);
				$value = mysqli_fetch_array($result);
				if($num_rows==1){
					$query = "UPDATE `u747826294_tuto`.`Teacher` SET `id_telegram`='$chatId',`temporary_code`='00000000' WHERE `temporary_code`='".$key."'";
					$result = mysqli_query($con,$query);
					sendMessage($chatId,$value['em'].", теперь вы получаете уведомления😜");
					
				}
				else{
					sendMessage($chatId,"Что-то пошло не так😵, перейдите по ссылке на сайте еще раз.");
				}
			}
			else{
				sendMessage($chatId,"Что-то пошло не так😵, перейдите по ссылке на сайте еще раз.");
			}
		}
		else{
			sendMessage($chatId,"Что-то пошло не так😵, перейдите по ссылке на сайте еще раз.");
		}

	}
	else {
		$value = mysqli_fetch_array($result);
		sendMessage($chatId,$value['em'].", вы получаете уведомления😜");
	}

}



function sendMessage($chatId, $message){
	$url = $GLOBALS['website']."/sendMessage?chat_id=".$chatId."&disable_web_page_preview=true&text=".urlencode($message);
	file_get_contents($url);
	echo "Успешно отправлено";
}
?>