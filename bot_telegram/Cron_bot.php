<?php
include "CorsPolicy.php";
include "ConnectDB.php";
	if(!$con){
		echo "База данных не работает -> Бот R.I.P.";
	}
	else{
		$result = mysqli_query($con,"UPDATE `u747826294_tuto`.`Teacher` SET temporary_code='00000000'");
		if (!$result) {
			die('Invalid query: ' . mysqli_error($con));
		}
		echo ( mysqli_affected_rows($con) > 0 ) ? "Success update" : "No rows update";
	}


?>