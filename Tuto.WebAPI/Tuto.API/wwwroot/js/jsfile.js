var URL_PATH = "https://localhost:44367/";
var URL_SERVER = "https://localhost:44367/";
var LOGIN_PAGE = "log%20in.html";
var PROFILE_PAGE_STUDENT = "profile-student.html";
var PROFILE_PAGE_TEACHER = "profile-teacher.html";

// login page js

function login_page(){
	var url = URL_SERVER + "oauth/authorize?returnUrl=" + URL_SERVER+"ChooseRole.html";
	document.querySelector("#login_url").href = url;
}
function Render(){
	var play = document.createElement("div");
	var round = document.createElement("div");
	play.setAttribute("id", "hellopreloader");
	round.setAttribute("id", "hellopreloader_preload");
	play.appendChild(round);
	document.body.appendChild(play);
	var hellopreloader = document.getElementById("hellopreloader_preload");
	function fadeOutnojquery(el){
		el.style.opacity = 1;
		var interhellopreloader = setInterval(function(){
			el.style.opacity = el.style.opacity - 0.05;
			if (el.style.opacity <=0.05){
				clearInterval(interhellopreloader);
				hellopreloader.style.display = "none";
			}
		},16);
	}
	window.onload = function(){
		setTimeout(function(){fadeOutnojquery(hellopreloader);},1000);
	};
}
//Check Login status
function isNew(){
	if(getUser(Login())["IsProfileFilled"]){
		if(getUser(Login())['TeacherInfo'])
			location.href = URL_PATH+PROFILE_PAGE_TEACHER;
		else
			location.href = URL_PATH+PROFILE_PAGE_STUDENT;
	}
	else
		return true;
	return false;
}
function isTeacher(user){
	if(user['TeacherInfo'])
		return true;
	return null;
}
function Login(){
	var AUTHENTICATE = null;
	$.ajax({
		type: "GET", 
		async: false,
		dataType: 'json',
		url: URL_SERVER+"oauth/GetCurrentUserId",
		success: function (data) {
			AUTHENTICATE = data;
		}
	});
	if(AUTHENTICATE!=null&&AUTHENTICATE!="")
		return AUTHENTICATE;
	location.href = URL_PATH+LOGIN_PAGE;
}
function Logout(){
	$.ajax({
		type: "POST",
		dataType: 'json',
		async: false,
		data: null,
		url: URL_SERVER+"oauth/LogOut",
		success: function (data, textStatus, xhr) {
			var status = xhr.status;
			switch(status){
				case 200:
				location.href = URL_PATH+LOGIN_PAGE;
				break;
				default:
				alert("ServerError");
				break;
			}
		}
	});
}
function DeleteTeacher(){
	var id = getUser(Login())['TeacherInfo']["Id"];
	$.ajax({
		type: "DELETE", 
		async: false,
		dataType: 'json',
		url: URL_SERVER+"Odata/TeacherInfos("+id+")",
		success: function (data, textStatus, xhr) {
			var status = xhr.status;
			switch(status){
				case 204:
				DeleteUser();
				break;
				default:
				alert("ServerError");
				break;
			}
		}
	});
}
function DeleteUser(){
	var id = Login();
	$.ajax({
		type: "POST",
		dataType: 'json',
		async: false,
		data: null,
		url: URL_SERVER+"oauth/LogOut",
		success: function (data, textStatus, xhr) {
			var status = xhr.status;
			switch(status){
				case 200:
				break;
				default:
				return;
				break;
			}
		}
	});
	$.ajax({
		type: "DELETE", 
		async: false,
		dataType: 'json',
		url: URL_SERVER+"Odata/Users("+id+")",
		success: function (data, textStatus, xhr) {
			var status = xhr.status;
			switch(status){
				case 204:
				location.href = URL_PATH+LOGIN_PAGE;
				break;
				default:
				alert("ServerError");
				break;
			}
		}
	});
}
function getCityId(name){
	var city = null;
	var url = encodeURI(URL_SERVER+"Odata/Cities?$filter=Name eq '"+name+"'");
	$.ajax({
		type: "GET", 
		async: false,
		dataType: 'json',
		url: url,
		success: function (data) {
			console.log(data);
			city = data['value'][0]['Id'];
			
		}
	});
	return city;
}
function getCityName(id){
	var city = null;
	var url = encodeURI(URL_SERVER+"Odata/Cities("+id+")");
	$.ajax({
		type: "GET", 
		async: false,
		dataType: 'json',
		url: url,
		success: function (data) {
			console.log(data);
			city = data['Name'];
			
		}
	});
	return city;
}
function getUser(id){
	var USR = null;
	//https://localhost:44367/Odata/Users?$expand=TeacherInfo&$filter=Id%20eq%20a4460ac1-4414-ea11-b815-00155d0ace00
	var url = URL_SERVER+"Odata/Users?$expand=TeacherInfo&$filter=Id%20eq%20"+id;
	console.log(url);
	$.ajax({
		type: "GET", 
		async: false,
		dataType: 'json',
		url: url,
		success: function (data) {
			console.log(data['value'][0]);
			USR = data['value'][0];
		}
	});
	if(USR)
		USR["Address"] = JSON.parse(USR["Address"]);
	return USR;
}
function setCities(id){
	$.ajax({
		type: "GET", 
		async: false,
		dataType: 'json',
		url: URL_SERVER+"odata/Cities",
		success: function (data) {
			console.log(data);
			var obj = data;
			var selectcity = s(id);
			obj['value'].forEach(function (element) {
				var name = element['Name'];
				var id = element['Id']
				var p = document.createElement("option");
				p.value = name;
				p.setAttribute("id",id);
				selectcity.appendChild(p);
			});
		}
	});
	let cityStudy = $(id).selectize({
		plugins: ['remove_button'],
		create: true,
		maxItems: 2,
		sortField: 'Name',
		valueField: 'Name',
		labelField: 'Name',
		searchField: ['Name']

	});
}
function uploadPhoto(upload,canvas){
	$("#photo").change(function(){
		if(this.files&& this.files[0]){
			var reader = new FileReader();
			reader.onload = function() {
				var img = new Image();
				img.onload = function(){
					var canvas = $("#canvas")[0];
					var ctx = canvas.getContext('2d');
					ctx.clearRect(0,0,canvas.width,canvas.height);
					ctx.drawImage(img, 0, 0,img.width,img.height,0,0,200,200);
				}
				img.src = event.target.result;
			}
			reader.readAsDataURL(this.files[0]);
		}
	});
}
// for select objects
function s(obj){
	return document.querySelector(obj);
}
function sa(obj){
	return document.querySelectorAll(obj);
}
//for ChooseRole js
function ChooseRole(AUTHENTICATE){
	var closeButton = document.querySelector("#close-button");
	closeButton.addEventListener("click", function () {
		let form = document.forms.choose_role;
		let elem = form.elements.choose_role_radio;
		var radioActive = false;
		for (prop in elem) {
			if (elem[prop].checked) {
				radioActive = true;
			}
		}

		if (radioActive) {
			if (elem[0].checked) {
				document.location.href=URL_PATH+"UserReg.html";
			} else if (elem[1].checked) {
				document.location.href=URL_PATH+"TeacherReg.html";
			}
		} else {
			alert("You didn`t choose the role")
		}
	});
}
// for UserReg & TeacherReg js
function change_step_next(i){
	i--;
	if(i>0&&i<$('.steps').length){
		$('.steps')[i-1].className = "steps closed";
		$('.steps')[i].classList.remove('closed');
	}
}
function change_step_prev(i){
	if(i>0&&i<$('.steps').length){
		$('.steps')[i].className = "steps closed";
		$('.steps')[i-1].classList.remove('closed');
	}   
}
function toggle(el) {
	el.style.display = (el.style.display == 'none') ? 'block' : 'none'
}
// for TeacherReg js
function TeacherReg(AUTHENTICATE){
	var idUser = s('#IdUser');
	idUser.value = AUTHENTICATE;
	setCities('#selectCity'); 
	let daywork = $("#selectDay").selectize({
		plugins: ['remove_button'],
		create: true,
		sortField: 'text'
	});
	uploadPhoto("#photo","#canvas");
}
function sendForm_Teacher(id){
	var userId = id;
	var MinimumWage = s("#minPrice").value;
	var daysArr = sa("#selectDay>option");
	var PreferredDaysOfWeek = [];
	daysArr.forEach(function (element) {
		PreferredDaysOfWeek.push(element.value);
	});
	PreferredDaysOfWeek = PreferredDaysOfWeek.join(',');
	console.log(PreferredDaysOfWeek);
	var array_post = {
		"UserId": userId,
		"MinimumWage": MinimumWage,
		"PreferredDaysOfWeek": PreferredDaysOfWeek
	}
	console.log(array_post);
	$.ajax({
		type: "POST",
		headers: {'Content-type':'application/json'},
		dataType: 'json',
		async: false,
		data: JSON.stringify(array_post),
		url: URL_SERVER+"/odata/TeacherInfos",
		success: function (data, textStatus, xhr) {
			var status = xhr.status;
			switch(status){
				case 201:
				location.href = URL_PATH+PROFILE_PAGE_TEACHER;
				break;
				default:
				alert("ServerError");
				break;
			}
            //document.location.href="profile-student.html";
        }
    });

}
// for UserReg js
function UserReg(AUTHENTICATE){

	var idUser = document.getElementById('IdUser');
	idUser.value = AUTHENTICATE;
	setCities('#selectCity');   
	uploadPhoto("#photo","#canvas");
}
function sendForm_User(a){
	var id = s('#IdUser').value;
	var name = s('#Name').value;
	var surname = s('#Surname').value;
	var checkers = {
		'Student`s home':s('#student-home').checked,
		'Tutors`s home':s('#tutor-home').checked,
		'Another location':s('#another-location').checked
	};
	var selectCity = s('#selectCity>option');
	var bio = s('#additional').value;
	var input_file = s('#photo');
	var base64 = s('#canvas').toDataURL('image/jpeg',0.7);
	if(!id||!name||!surname||!selectCity||!bio||!input_file||!base64){
		alert("Something Wrong");
		return;
	}
	var cityId = getCityId(selectCity.value);
	var array_post = getUser(id);
	array_post["IsProfileFilled"] = true;
	array_post["CityId"] = cityId;
	array_post["Picture"] = base64;
	array_post["Name"] = name;
	array_post["Surname"] = surname;
	array_post["Address"] = JSON.stringify(checkers);
	array_post["Description"] = bio;
	console.log(array_post);
	$.ajax({
		type: "PUT",
		headers: {'Content-type':'application/json'},
		dataType: 'json',
		async: false,
		data: JSON.stringify(array_post),
		url: URL_SERVER+"Odata/Users("+id+")",
		success: function (data, textStatus, xhr) {
			var status = xhr.status;
			switch(status){
				case 204:
				if(!a){
					location.href = URL_PATH+PROFILE_PAGE_STUDENT;
				}
				break;
				default:
				alert("ServerError");
				break;
			}
            //document.location.href="profile-student.html";
        }
    });
	if(a){
		sendForm_Teacher(id);
	}
}
// for Profile teacher js
function ProfileTeacher(AUTHENTICATE){
	setCities('#selectCity');
	uploadPhoto("#photo","#canvas");
	var modal = s("#modal2"),
	modalOverlay = s("#modal-overlay2"),
	closeButton = s("#close-button2");
	let openButton = s(".edit");


	closeButton.addEventListener("click", function () {
		toggle(modal);
		toggle(modalOverlay);
	});

	openButton.addEventListener("click", function () {
		toggle(modal);
		toggle(modalOverlay);
	});

	let comment = s("#profile-comment");

	comment.addEventListener("click", function () {
		let first = s(".first-slide");
		let second = s(".second-slide");
		toggle(first);
		toggle(second);
	});
	let daywork = $("#selectday").selectize({
		plugins: ['remove_button'],
		create: true,
		sortField: 'text'
	});
	var block = s(".chat");
	block.scrollTop = block.scrollHeight;

	var USR_DATA = getUser(AUTHENTICATE);
	s("#place").innerText = getCityName(USR_DATA["CityId"]);
	s("#additional_text").innerText = USR_DATA["Description"] 

	var location = s("#location");
	location.innerHTML = '';
	for(var prop in USR_DATA["Address"]){
		if(USR_DATA["Address"][prop]){
			var span = document.createElement('span');
			span.className = 'location text-nano';
			span.innerText = prop;
			location.appendChild(span);
		}
	}
	var photos = sa(".User_Photo");
	[].forEach.call(photos, function(photo){
		photo.src = USR_DATA["Picture"];
	});
	var names = sa(".User_Name");
	[].forEach.call(names, function(name){
		name.innerText = USR_DATA["Name"]+" "+USR_DATA["Surname"];
	}); 
	var canvas = s("#canvas");
	var ctx = canvas.getContext("2d");

	var image = new Image();
	image.onload = function() {
		ctx.drawImage(image, 0, 0);
	};
	image.src =USR_DATA["Picture"];
	// s("#change_name").value = USR_DATA["Name"];
	// s("#change_surname").value = USR_DATA["Surname"];
	// s("#change_additional").value = USR_DATA["Description"];
	// s("#student-home").checked = USR_DATA["Address"]['Student`s home'];
	// s("#tutor-home").checked = USR_DATA["Address"]['Tutors`s home'];
	// s("#another-location").checked = USR_DATA["Address"]['Another location'];
}
// for Profile Student js
function ProfileStudent(AUTHENTICATE){
	setCities('#selectCity');
	uploadPhoto("#photo","#canvas");

	var modal = s("#modal2"),
	modalOverlay = s("#modal-overlay2"),
	closeButton = s("#close-button2");
	let openButton = s(".edit");
	closeButton.addEventListener("click", function () {
		toggle(modal);
		toggle(modalOverlay);
	});
	openButton.addEventListener("click", function () {
		toggle(modal);
		toggle(modalOverlay);
	});
	let daywork = $("#selectday").selectize({
		plugins: ['remove_button'],
		create: true,
		sortField: 'text'
	});

	var USR_DATA = getUser(AUTHENTICATE);
	s("#place").innerText = getCityName(USR_DATA["CityId"]);
	s("#additional_text").innerText = USR_DATA["Description"] 

	var location = s("#location");
	location.innerHTML = '';
	for(var prop in USR_DATA["Address"]){
		if(USR_DATA["Address"][prop]){
			var span = document.createElement('span');
			span.className = 'location text-nano';
			span.innerText = prop;
			location.appendChild(span);
		}
	}
	var photos = sa(".User_Photo");
	[].forEach.call(photos, function(photo){
		photo.src = USR_DATA["Picture"];
	});
	var names = sa(".User_Name");
	[].forEach.call(names, function(name){
		name.innerText = USR_DATA["Name"]+" "+USR_DATA["Surname"];
	}); 
	var canvas = s("#canvas");
	var ctx = canvas.getContext("2d");

	var image = new Image();
	image.onload = function() {
		ctx.drawImage(image, 0, 0);
	};
	image.src =USR_DATA["Picture"];
	s("#change_name").value = USR_DATA["Name"];
	s("#change_surname").value = USR_DATA["Surname"];
	s("#change_additional").value = USR_DATA["Description"];
	s("#student-home").checked = USR_DATA["Address"]['Student`s home'];
	s("#tutor-home").checked = USR_DATA["Address"]['Tutors`s home'];
	s("#another-location").checked = USR_DATA["Address"]['Another location'];
	
    // var select = s("#selectCity");
    // var cityName = getCityName(USR_DATA["CityId"]);
    // var city = document.createElement("option");
    // city.setAttribute("value", cityName);
    // city.setAttribute("selected","selected");
    // select.appendChild(city);
    // var selectDiv = s(".Search>.selectize-control>.selectize-input");
    // var cityDiv =  document.createElement("div");
    // cityDiv.setAttribute("data-value", cityName);
    // cityDiv.setAttribute("class", "item");
    // cityDiv.innerHTML = cityName + '<a href="javascript:void(0)" class="remove" tabindex="-1" title="Remove">×</a>';
    // selectDiv.appendChild(cityDiv);


//<option value="Aleksandrovsk" selected="selected">Aleksandrovsk</option>
// <div class="selectize-input items has-options has-items not-full">
//     <div data-value="Alupka" class="item">
//         Alupka
//         <a href="javascript:void(0)" class="remove" tabindex="-1" title="Remove">×</a>
//     </div>
//     <input type="text" autocomplete="off" tabindex="" style="width: 4px; opacity: 1; position: relative; left: 0px;">
// </div>
}
function ChangeProfileStudent(AUTHENTICATE){
	var USR_DATA = getUser(AUTHENTICATE);
	var id = AUTHENTICATE;
	var name = s('#change_name').value;
	var surname = s('#change_surname').value;
	var checkers = {
		'Student`s home':s('#student-home').checked,
		'Tutors`s home':s('#tutor-home').checked,
		'Another location':s('#another-location').checked
	};
	var selectCity = s('#selectCity>option');
	var bio = s('#change_additional').value;
	var input_file = s('#photo');
	var base64 = s('#canvas').toDataURL('image/jpeg',0.7);
	if(!id||!name||!surname||!selectCity||!bio||!input_file||!base64){
		alert("Something Wrong");
		return;
	}
	var cityId = getCityId(selectCity.value);
	USR_DATA["CityId"] = cityId;
	USR_DATA["Picture"] = base64;
	USR_DATA["Name"] = name;
	USR_DATA["Surname"] = surname;
	USR_DATA["Address"] = JSON.stringify(checkers);
	USR_DATA["Description"] = bio;
	console.log(USR_DATA);
	$.ajax({
		type: "PUT",
		headers: {'Content-type':'application/json'},
		dataType: 'json',
		async: false,
		data: JSON.stringify(USR_DATA),
		url: URL_SERVER+"Odata/Users("+id+")",
		success: function (data, textStatus, xhr) {
			var status = xhr.status;
			switch(status){
				case 204:
				location.href = URL_PATH+"profile-student.html";
				break;
				default:
				alert("ServerError");
				break;
			}
            //document.location.href="profile-student.html";
        }
    });
}