<?php
include "CorsPolicy.php";
include "ConnectDB.php";

if (!empty($_GET["email"])&&!empty($_GET["key"])) 
{ 
	echo "<h4> Получены новые вводные: почта - <i>".$_GET["email"]."</i>, ключ - <i>".$_GET["key"]."</i></h4>";
	$email = $_GET["email"];
	$key = $_GET["key"];

	$con = mysqli_connect('mysql.hostinger.com.ua','u747826294_tuto','tutortutor','u747826294_tuto');
	if(!$con){
		echo "==> База данных не работает -> Бот R.I.P.";
	}
	else{
		$result = mysqli_query($con,"UPDATE `u747826294_tuto`.`Teacher` SET id_telegram='', temporary_code='".$key."' WHERE email_teacher='".$email."'");
		if (!$result) {
			die('==> Ошибка запроса в бд: ' . mysqli_error($con));
		}
		echo ( mysqli_affected_rows($con) > 0 ) ? "==> Ключ успешно создан" : "==> Не удалось создать ключ";
	}

}
else { 
	echo "<h4>Переменные не дошли. Проверьте все еще раз.</h4>"; 
}



























?>